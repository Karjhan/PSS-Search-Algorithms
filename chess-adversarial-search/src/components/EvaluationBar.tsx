interface Props {
  value: number;
}

export default function EvaluationBar({ value }: Props) {
  return <div>Evaluation: {value}</div>;
}