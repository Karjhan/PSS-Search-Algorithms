import { useState } from "react";
import type { Board } from "../models/SudokuTypes"

function emptyBoard(): Board {
  return Array.from({ length: 9 }, () =>
    Array.from({ length: 9 }, () => ({ value: null, fixed: false }))
  );
}

function randomBoard(): Board {
  const base = Array.from({ length: 9 }, (_, r) =>
    Array.from({ length: 9 }, (_, c) => ({
      value: ((r * 3 + Math.floor(r / 3) + c) % 9) + 1,
      fixed: true
    }))
  );

  for (let band = 0; band < 3; band++) {
    const rows = [0, 1, 2].map(i => band * 3 + i);
    rows.sort(() => Math.random() - 0.5);

    const temp = rows.map(r => base[r]);
    rows.forEach((r, i) => {
      base[r] = temp[i];
    });
  }

  const cellsToRemove = 60;
  let removed = 0;

  while (removed < cellsToRemove) {
    const r = Math.floor(Math.random() * 9);
    const c = Math.floor(Math.random() * 9);

    if (base[r][c].value !== null) {
      base[r][c] = { value: null, fixed: false };
      removed++;
    }
  }

  return base;
}

export function useSudoku() {
  const [board, setBoard] = useState<Board>(() => randomBoard());

  const updateCell = (r: number, c: number, v: number) => {
    if (v < 1 || v > 9) return;

    setBoard(prev => {
      const copy = prev.map(row => row.map(cell => ({ ...cell })));
      if (!copy[r][c].fixed) copy[r][c].value = v;
      return copy;
    });
  };

  const reset = () => setBoard(emptyBoard());
  const randomize = () => setBoard(randomBoard());

  return { board, setBoard, updateCell, reset, randomize };
}