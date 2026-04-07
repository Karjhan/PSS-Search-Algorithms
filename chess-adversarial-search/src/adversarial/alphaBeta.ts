import { Chess } from "chess.js";
import { evaluateBoard } from "./evaluation";

export let nodesExplored = 0;

export function resetNodes() {
  nodesExplored = 0;
}

export function alphaBeta(
  game: Chess,
  depth: number,
  alpha: number,
  beta: number,
  maximizing: boolean
): number {
  nodesExplored++;

  if (depth === 0 || game.isGameOver()) {
    return evaluateBoard(game);
  }

  const moves = game.moves({ verbose: true });

  moves.sort((a, b) => {
    let scoreA = 0;
    let scoreB = 0;

    if (a.captured) scoreA += 10;
    if (b.captured) scoreB += 10;

    if (a.promotion) scoreA += 5;
    if (b.promotion) scoreB += 5;

    return scoreB - scoreA;
  });

  if (maximizing) {
    let maxEval = -Infinity;
    for (const move of moves) {
      game.move(move.san);
      const evalScore = alphaBeta(game, depth - 1, alpha, beta, false);
      game.undo();

      maxEval = Math.max(maxEval, evalScore);
      alpha = Math.max(alpha, evalScore);
      if (beta <= alpha) break;
    }
    return maxEval;
  } else {
    let minEval = Infinity;
    for (const move of moves) {
      game.move(move.san);
      const evalScore = alphaBeta(game, depth - 1, alpha, beta, true);
      game.undo();

      minEval = Math.min(minEval, evalScore);
      beta = Math.min(beta, evalScore);
      if (beta <= alpha) break;
    }
    return minEval;
  }
}

export function getBestMove(game: Chess, depth: number) {
  resetNodes();
  const startTime = performance.now();

  let bestMove = "";
  let bestValue = -Infinity;

  const moves = game.moves({ verbose: true });

  for (const move of moves) {
    game.move(move.san);
    const boardValue = alphaBeta(game, depth - 1, -Infinity, Infinity, false);
    game.undo();

    if (boardValue > bestValue) {
      bestValue = boardValue;
      bestMove = move.san;
    }
  }

  const endTime = performance.now();

  return {
    bestMove,
    bestValue,
    nodes: nodesExplored,
    timeMs: Math.round(endTime - startTime),
  };
}