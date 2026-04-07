interface Props {
  moves: string[];
}

export default function MoveHistory({ moves }: Props) {
  return (
    <div>
      {moves.map((m, i) => (
        <div key={i}>{m}</div>
      ))}
    </div>
  );
}