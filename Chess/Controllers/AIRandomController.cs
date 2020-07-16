using MonoChess.Enums;
using MonoChess.Models;
using System.Linq;

namespace MonoChess.Controllers
{
    public class AIRandomController : Participant
    {
        public AIRandomController(PieceColour pieceColour) : base(pieceColour) { }

        public override Move GetMove()
        {
            Piece piece = null;

            while (piece == null)
            {
                var myPieces = Engine.Instance.Board.GetPieces(PieceColour);

                var index = Engine.Instance.RNG.Next(0, myPieces.Count() - 1);

                piece = myPieces[index];

                if (!piece.PossibleMoves.Any()) piece = null;
            }

            var moveIndex = Engine.Instance.RNG.Next(0, piece.PossibleMoves.Count() - 1);

            return new Move
            {
                Piece = piece,
                Location = piece.PossibleMoves.ToArray()[moveIndex]
            };
        }

        public override Piece Promote(Pawn pawn)
        {
            return new Queen(pawn.Board, pawn.Row, pawn.Column, pawn.PieceColour);
        }
    }
}