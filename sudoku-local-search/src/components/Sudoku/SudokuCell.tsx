export default function SudokuCell({ cell, onChange }: any) {
  return (
    <input
      className={`cell ${cell.fixed ? "fixed" : ""}`}
      value={cell.value ?? ""}
      onChange={e => {
        const v = Number(e.target.value);
        if (!isNaN(v)) onChange(v);
      }}
      disabled={cell.fixed}
    />
  );
}