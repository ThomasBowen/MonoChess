using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoChess.Enums;
using MonoChess.Helpers;
using MonoChess.Models;
using System;
using System.Linq;

namespace MonoChess.Controllers
{
    public class PlayerController : Participant
    {
        private MouseState _currentMouseState { get; set; }
        private MouseState _previousMouseState { get; set; }

        public PlayerController(PieceColour pieceColour) : base(pieceColour) { }

        public override Move GetMove()
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            if (_previousMouseState.LeftButton == ButtonState.Released && _currentMouseState.LeftButton == ButtonState.Pressed)
            {
                return ProcessMouseClick();
            }

            return null;
        }

        public override Piece Promote(Pawn pawn)
        {
            return new Queen(pawn.Board, pawn.Row, pawn.Column, pawn.PieceColour);
        }

        private Move ProcessMouseClick()
        {
            var previouslySelectedPiece = Engine.Instance.Board.Pieces.SingleOrDefault(p => p.Selected);

            Engine.Instance.Board.Pieces.ForEach(c => c.Selected = false);

            if (previouslySelectedPiece == null)
            {
                var mousePosition = new Point(_currentMouseState.X, _currentMouseState.Y);
                var clickedPiece = Engine.Instance.Board.Pieces.SingleOrDefault(p => p.Area.Contains(mousePosition));

                if (clickedPiece == null) return null;

                if (clickedPiece.PieceColour != PieceColour) return null;

                clickedPiece.Selected = true;
            }
            else
            {
                var row = _currentMouseState.Y / Engine.Instance.SquareSize;
                var column = _currentMouseState.X / Engine.Instance.SquareSize;

                if (previouslySelectedPiece.PossibleMoves.Any(m => m.Item1 == row && m.Item2 == column))
                {
                    return new Move
                    {
                        Piece = previouslySelectedPiece,
                        Location = new Tuple<int, int>(row, column)
                    };
                }
                else
                {
                    MessageHelper.DisplayMessage("Invalid Move");
                }
            }

            return null;
        }
    }
}