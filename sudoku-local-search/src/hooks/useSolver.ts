import { useEffect, useState } from "react";
import type { Board } from "../models/SudokuTypes";
import type { AlgorithmType } from "../models/SolverTypes";
import { randomStep } from "../algorithms/randomSolver";
import { simulatedAnnealingStep, initializeAnnealing } from "../algorithms/simulatedAnnealing";
import { countConflicts } from "../utils/sudokuValidator";

export function useSolver(board: Board, setBoard: Function) {
  const [running, setRunning] = useState(false);
  const [algorithm, setAlgorithm] = useState<AlgorithmType>("manual");
  const [speed, setSpeed] = useState(100);

  const [iteration, setIteration] = useState(0);
  const [conflicts, setConflicts] = useState(0);

  const [temperature, setTemperature] = useState(1);
  const [coolingRate, setCoolingRate] = useState(0.995);

  useEffect(() => {
    if (algorithm === "annealing") {
      const init = initializeAnnealing(board);
      setBoard(init);

      const samples: number[] = [];
      let tempBoard = init;

      for (let i = 0; i < 200; i++) {
        tempBoard = simulatedAnnealingStep(tempBoard, 1);
        samples.push(countConflicts(tempBoard));
      }

      const mean = samples.reduce((a, b) => a + b, 0) / samples.length;
      const variance = samples.reduce((a, b) => a + (b - mean) ** 2, 0) / samples.length;
      setTemperature(Math.sqrt(variance));

      setIteration(0);
    }
  }, [algorithm]);

  useEffect(() => {
    if (!running) return;

    const id = setInterval(() => {
      setBoard((prev: Board) => {
        let next = prev;

        if (algorithm === "random") {
          next = randomStep(prev);
        }

        if (algorithm === "annealing") {
          next = simulatedAnnealingStep(prev, temperature);

          setTemperature(t => t * coolingRate);
        }

        setIteration(i => i + 1);
        setConflicts(countConflicts(next));

        if (algorithm === "annealing" && conflicts === 0) {
          setRunning(false);
        }

        return next;
      });
    }, speed);

    return () => clearInterval(id);
  }, [running, algorithm, speed, temperature]);

  const result = {
    running,
    setRunning,
    algorithm,
    setAlgorithm,
    speed,
    setSpeed,
    iteration,
    conflicts,
    temperature,
    setTemperature,
    coolingRate,
    setCoolingRate
  };

  return result
}

