import { Chessboard } from "react-chessboard";

interface Props {
  fen: string;
  onMove: (move: any) => boolean;
}

export default function ChessBoard({ fen, onMove }: Props) {
  return (
    <Chessboard
        options={{
            position: fen,
            onPieceDrop: ({ sourceSquare, targetSquare }) => {
            return onMove({
                from: sourceSquare,
                to: targetSquare,
                promotion: "q",
            });
            },
        }}
    />
  );
}