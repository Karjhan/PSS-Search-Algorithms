import { Chess } from "chess.js";
import { alphaBeta, resetNodes, nodesExplored } from "../adversarial/alphaBeta";

self.onmessage = (event: MessageEvent) => {
  const { fen, depth, moves } = event.data;
  const game = new Chess(fen);

  let bestMove = "";
  let bestValue = -Infinity;
  let totalNodes = 0;

  const startTime = performance.now();

  for (const move of moves) {
    resetNodes();

    game.move(move);

    const value = alphaBeta(game, depth - 1, -Infinity, Infinity, false);

    game.undo();

    totalNodes += nodesExplored;

    if (value > bestValue) {
      bestValue = value;
      bestMove = move;
    }
  }

  const endTime = performance.now();

  self.postMessage({
    bestMove,
    bestValue,
    nodes: totalNodes,
    timeMs: Math.round(endTime - startTime),
  });
};