interface Props {
  onReset: () => void;
}

export default function GameControls({ onReset }: Props) {
  return <button onClick={onReset}>Reset Game</button>;
}