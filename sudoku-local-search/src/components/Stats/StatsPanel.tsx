export default function StatsPanel({ iteration, conflicts, temperature }: any) {
  return (
    <div className="stats">
      <p>Iterations: {iteration}</p>
      <p>Conflicts: {conflicts}</p>
      <p>Temp: {temperature?.toFixed(2)}</p>
    </div>
  );
}