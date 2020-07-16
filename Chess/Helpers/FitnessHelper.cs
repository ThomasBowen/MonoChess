using MonoChess.Enums;
using MonoChess.Models;
using System.Linq;

namespace MonoChess.Helpers
{
    public static class FitnessHelper
    {
        public static int GetBoardFitness(Board board, PieceColour pieceColour)
        {
            if (board.Pieces.OfType<King>().Single(k => k.PieceColour != pieceColour).CheckMate)
            {
                return int.MaxValue;
            }

            var myPieces = board.Pieces.Where(p => p.PieceColour == pieceColour);
            var opponentPieces = board.Pieces.Where(p => p.PieceColour != pieceColour);

            var score = myPieces.Sum(p => p.Value);
            score -= opponentPieces.Sum(p => p.Value);

            return score;
        }
    }
}