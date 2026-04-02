import type { Board } from "../models/SudokuTypes";

export function randomStep(board: Board): Board {
  const copy = board.map(r => r.map(c => ({ ...c })));
  const r = Math.floor(Math.random() * 9);
  const c = Math.floor(Math.random() * 9);
  if (!copy[r][c].fixed) {
    copy[r][c].value = Math.floor(Math.random() * 9) + 1;
  }
  return copy;
}