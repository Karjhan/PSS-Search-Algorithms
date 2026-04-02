import { useSudoku } from "./hooks/useSudoku";
import { useSolver } from "./hooks/useSolver";
import SudokuGrid from "./components/Sudoku/SudokuGrid";
import ControlsPanel from "./components/Controls/ControlsPanel";
import StatsPanel from "./components/Stats/StatsPanel";
import './App.css'

function App() {
  const { board, setBoard, updateCell, reset, randomize } = useSudoku();
  const solver = useSolver(board, setBoard);

  return (
    <div className="app">
      <h1>Sudoku Simulated Annealing</h1>
      <SudokuGrid board={board} onChange={updateCell} />
      <ControlsPanel
        running={solver.running}
        setRunning={solver.setRunning}
        algorithm={solver.algorithm}
        setAlgorithm={solver.setAlgorithm}
        speed={solver.speed}
        setSpeed={solver.setSpeed}
        temperature={solver.temperature}
        setTemperature={solver.setTemperature}
        coolingRate={solver.coolingRate}
        setCoolingRate={solver.setCoolingRate}
        onReset={reset}
        onRandom={randomize}
      />
      <StatsPanel iteration={solver.iteration} conflicts={solver.conflicts} temperature={solver.temperature} />
    </div>
  );
}

export default App

