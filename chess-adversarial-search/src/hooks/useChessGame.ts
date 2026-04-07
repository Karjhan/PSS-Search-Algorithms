import { useState, useRef, useEffect } from "react";
import { Chess } from "chess.js";
import { DEFAULT_DEPTH } from "../utils/constants";
import { WorkerPool } from "../workers/workerPool";

type Metrics = {
  lastMove: string;
  evaluation: number;
  nodesExplored: number;
  timeMs: number;
};

export function useChessGame() {
  const [game, setGame] = useState(new Chess());
  const [fen, setFen] = useState(game.fen());
  const [metrics, setMetrics] = useState<Metrics | null>(null);

  const poolRef = useRef<WorkerPool | null>(null);

  useEffect(() => {
    poolRef.current = new WorkerPool(
      6,
      new URL("../workers/chessWorker.ts", import.meta.url).toString()
    );

    return () => poolRef.current?.terminate();
  }, []);

  function makeMove(move: any): boolean {
    const newGame = new Chess(game.fen());
    const result = newGame.move(move);

    if (!result) return false;

    setGame(newGame);
    setFen(newGame.fen());

    setTimeout(() => makeAIMove(new Chess(newGame.fen())), 200);

    return true;
  }

  async function makeAIMove(currentGame: Chess) {
    if (currentGame.isGameOver()) return;

    const moves = currentGame.moves();

    const numWorkers = Math.min(
      poolRef.current ? (poolRef.current as any).workers.length : 2,
      moves.length
    );

    const chunks: string[][] = Array.from({ length: numWorkers }, () => []);
    moves.forEach((move, i) => chunks[i % numWorkers].push(move));

    if (!poolRef.current) return;

    const promises = chunks.map((chunk) =>
      poolRef.current!.runJob({
        fen: currentGame.fen(),
        depth: DEFAULT_DEPTH,
        moves: chunk,
      })
    );

    const results = await Promise.all(promises);

    const best = results.reduce((a, b) =>
      a.bestValue > b.bestValue ? a : b
    );

    const totalNodes = results.reduce((sum, r) => sum + r.nodes, 0);
    const totalTime = Math.max(...results.map((r) => r.timeMs)); 

    const updatedGame = new Chess(currentGame.fen());
    updatedGame.move(best.bestMove);

    setGame(updatedGame);
    setFen(updatedGame.fen());

    setMetrics({
      lastMove: best.bestMove,
      evaluation: best.bestValue,
      nodesExplored: totalNodes,
      timeMs: totalTime,
    });
  }

  function resetGame() {
    const newGame = new Chess();
    setGame(newGame);
    setFen(newGame.fen());
    setMetrics(null);
  }

  return { fen, makeMove, resetGame, metrics };
}