import type { Board } from "../models/SudokuTypes";

export function countConflicts(board: Board): number {
  let conflicts = 0;

  for (let i = 0; i < 9; i++) {
    const row = new Set();
    const col = new Set();

    for (let j = 0; j < 9; j++) {
      const rVal = board[i][j].value;
      const cVal = board[j][i].value;

      if (rVal && row.has(rVal)) conflicts++;
      if (cVal && col.has(cVal)) conflicts++;

      row.add(rVal);
      col.add(cVal);
    }
  }
  
  for (let boxRow = 0; boxRow < 3; boxRow++) {
    for (let boxCol = 0; boxCol < 3; boxCol++) {
      const seen = new Set();

      for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
          const val = board[boxRow * 3 + i][boxCol * 3 + j].value;
          if (val && seen.has(val)) conflicts++;
          seen.add(val);
        }
      }
    }
  }

  return conflicts;
}