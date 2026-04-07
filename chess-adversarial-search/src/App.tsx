import ChessBoard from "./components/ChessBoard";
import GameControls from "./components/GameControls";
import Metrics from "./components/Metrics";
import { useChessGame } from "./hooks/useChessGame";

export default function App() {
  const { fen, makeMove, resetGame, metrics } = useChessGame();

  return (
    <div>
      <h1>Chess Adversarial Search</h1>
      <ChessBoard fen={fen} onMove={makeMove} />
      <GameControls onReset={resetGame} />
      <Metrics metrics={metrics} />
    </div>
  );
}