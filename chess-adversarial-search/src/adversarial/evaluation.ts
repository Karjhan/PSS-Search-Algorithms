import { Chess } from "chess.js";
import { PIECE_VALUES } from "../utils/constants";

const centerSquares = ["d4", "d5", "e4", "e5"];

export function evaluateBoard(game: Chess): number {
  if (game.isCheckmate()) {
    return game.turn() === "w" ? -9999 : 9999;
  }

  if (game.isDraw()) {
    return 0;
  }

  let score = 0;
  const board = game.board();

  for (let i = 0; i < 8; i++) {
    for (let j = 0; j < 8; j++) {
      const piece = board[i][j];
      if (!piece) continue;

      let value = PIECE_VALUES[piece.type];

      const square = String.fromCharCode(97 + j) + (8 - i);

      if (centerSquares.includes(square)) {
        value += 0.5;
      }

      score += piece.color === "w" ? value : -value;
    }
  }

  const mobility = game.moves().length;
  score += game.turn() === "w" ? mobility * 0.1 : -mobility * 0.1;

  if (game.inCheck()) {
    score += game.turn() === "w" ? -0.5 : 0.5;
  }

  return score;
}