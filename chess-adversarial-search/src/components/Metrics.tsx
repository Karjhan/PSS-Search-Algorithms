interface Props {
  metrics: {
    lastMove: string;
    evaluation: number;
    nodesExplored: number;
    timeMs: number;
  } | null;
}

export default function Metrics({ metrics }: Props) {
  if (!metrics) return null;

  return (
    <div className="metrics">
      <p><strong>AI Move:</strong> {metrics.lastMove}</p>
      <p><strong>Evaluation:</strong> {metrics.evaluation.toFixed(2)}</p>
      <p><strong>Nodes Explored:</strong> {metrics.nodesExplored}</p>
      <p><strong>Time:</strong> {metrics.timeMs} ms</p>
    </div>
  );
}