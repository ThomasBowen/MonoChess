using MonoChess.Enums;
using MonoChess.Models;

namespace MonoChess.Controllers
{
    public abstract class Participant
    {
        public PieceColour PieceColour { get; }

        public Participant(PieceColour pieceColour)
        {
            PieceColour = pieceColour;
        }

        public abstract Move GetMove();

        public abstract Piece Promote(Pawn pawn);
    }
}