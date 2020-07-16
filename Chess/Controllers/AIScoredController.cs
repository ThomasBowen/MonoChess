using MonoChess.Enums;
using MonoChess.Helpers;
using MonoChess.Models;
using System;

namespace MonoChess.Controllers
{
    public class AIScoredController : Participant
    {
        public AIScoredController(PieceColour pieceColour) : base(pieceColour) { }

        public override Move GetMove()
        {
            Piece currentBest = null;
            Tuple<int, int> currentBestMove = null;
            int currentBestFitness = int.MinValue;

            var myPieces = Engine.Instance.Board.GetPieces(PieceColour);

            foreach (var piece in myPieces)
            {
                foreach (var move in piece.PossibleMoves)
                {
                    var clonedBoard = Engine.Instance.Board.Clone();

                    var clonePiece = clonedBoard.GetPieceAtLocation(piece.Row, piece.Column);

                    clonedBoard.MakeMove(new Move() { Piece = clonePiece, Location = move });

                    ProcessPromotion(clonedBoard, clonePiece);

                    var fitness = FitnessHelper.GetBoardFitness(clonedBoard, PieceColour);

                    if (fitness > currentBestFitness)
                    {
                        currentBestFitness = fitness;
                        currentBest = piece;
                        currentBestMove = move;
                    }

                    if (currentBestFitness == int.MaxValue) break;
                }

                if (currentBestFitness == int.MaxValue) break;
            }

            return new Move
            {
                Piece = currentBest,
                Location = currentBestMove
            };
        }

        public override Piece Promote(Pawn pawn)
        {
            return new Queen(pawn.Board, pawn.Row, pawn.Column, pawn.PieceColour);
        }

        private void ProcessPromotion(Board board, Piece piece)
        {
            if (!(piece is Pawn pawn) || !pawn.ToBePromoted) return;

            var newPiece = new Queen(board, pawn.Row, pawn.Column, pawn.PieceColour);

            board.Pieces.Remove(piece);
            board.Pieces.Add(newPiece);
        }
    }
}