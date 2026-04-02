import type { Board } from "../models/SudokuTypes";
import { countConflicts } from "../utils/sudokuValidator";

function clone(board: Board): Board {
  return board.map(r => r.map(c => ({ ...c })));
}

export function initializeAnnealing(board: Board): Board {
  const newBoard = clone(board);

  for (let boxRow = 0; boxRow < 3; boxRow++) {
    for (let boxCol = 0; boxCol < 3; boxCol++) {
      const nums = new Set<number>();
      const empty: [number, number][] = [];

      for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
          const r = boxRow * 3 + i;
          const c = boxCol * 3 + j;

          if (newBoard[r][c].value) {
            nums.add(newBoard[r][c].value!);
          } else {
            empty.push([r, c]);
          }
        }
      }

      const missing = [1,2,3,4,5,6,7,8,9].filter(n => !nums.has(n));

      for (let i = missing.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [missing[i], missing[j]] = [missing[j], missing[i]];
      }

      empty.forEach(([r, c], idx) => {
        newBoard[r][c] = { value: missing[idx], fixed: false };
      });
    }
  }

  return newBoard;
}

function randomNeighbor(board: Board): Board {
  const newBoard = clone(board);

  const boxRow = Math.floor(Math.random() * 3);
  const boxCol = Math.floor(Math.random() * 3);

  const cells: [number, number][] = [];

  for (let i = 0; i < 3; i++) {
    for (let j = 0; j < 3; j++) {
      const r = boxRow * 3 + i;
      const c = boxCol * 3 + j;
      if (!newBoard[r][c].fixed) {
        cells.push([r, c]);
      }
    }
  }

  if (cells.length < 2) return board;

  const [a, b] = [
    cells[Math.floor(Math.random() * cells.length)],
    cells[Math.floor(Math.random() * cells.length)]
  ];

  const temp = newBoard[a[0]][a[1]].value;
  newBoard[a[0]][a[1]].value = newBoard[b[0]][b[1]].value;
  newBoard[b[0]][b[1]].value = temp;

  return newBoard;
}

export function simulatedAnnealingStep(board: Board, temperature: number): Board {
  const currentCost = countConflicts(board);
  const neighbor = randomNeighbor(board);
  const newCost = countConflicts(neighbor);

  if (newCost < currentCost) return neighbor;

  const prob = Math.exp((currentCost - newCost) / temperature);
  if (Math.random() < prob) return neighbor;

  return board;
}

