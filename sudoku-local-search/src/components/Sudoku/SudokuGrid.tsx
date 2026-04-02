import SudokuCell from "./SudokuCell";
import "./SudokuBoard.css";

export default function SudokuGrid({ board, onChange }: any) {
  return (
    <div className="grid">
      {board.map((row: any, i: number) =>
        row.map((cell: any, j: number) => (
          <SudokuCell key={`${i}-${j}`} cell={cell} onChange={(v:number)=>onChange(i,j,v)} />
        ))
      )}
    </div>
  );
}