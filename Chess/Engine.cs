using Microsoft.Xna.Framework.Graphics;
using MonoChess.Controllers;
using MonoChess.Enums;
using MonoChess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoChess
{
    public sealed class Engine
    {
        private List<Participant> _participants { get; set; }
        private Participant _currentParticipant => _participants.Single(p => p.PieceColour == CurrentTurn);

        public GraphicsDevice GraphicsDevice { get; set; }

        public Board Board { get; set; } = new Board();

        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        public PieceColour CurrentTurn { get; set; } = PieceColour.White;
        public GameState GameState { get; set; } = GameState.Running;

        public int SquareSize => ScreenWidth / 8;
        public int PieceSize => SquareSize / 2;

        public Random RNG { get; } = new Random();

        private Engine()
        {
        }

        public static Engine Instance { get; } = new Engine();

        public void InitiliseEngine(List<Piece> pieces, GraphicsDevice graphicsDevice, int width, int height)
        {
            GraphicsDevice = graphicsDevice;
            ScreenWidth = width;
            ScreenHeight = height;
            Board.Pieces = pieces;

            Board.SetUpPieces();

            _participants = new List<Participant>
            {
                new PlayerController(PieceColour.White),
                new AIScoredController(PieceColour.Black)
            };
        }

        public void Update()
        {
            switch (GameState)
            {
                case GameState.Menu:
                    break;
                case GameState.Running:
                    TakeTurn();
                    break;
                case GameState.Finished:
                    break;
                default:
                    break;
            }
        }

        private void TakeTurn()
        {
            var move = _currentParticipant.GetMove();

            if (move != null) MakeMove(move);
        }

        private void MakeMove(Move move)
        {
            Board.MakeMove(move);

            var kings = Board.Pieces.OfType<King>().ToList();

            if (kings.Any(k => k.CheckMate))
            {
                GameState = GameState.Finished;
            }
            else
            {
                ProcessPromotion(move.Piece);

                CurrentTurn = CurrentTurn == PieceColour.Black ? PieceColour.White : PieceColour.Black;
            }
        }

        private void ProcessPromotion(Piece piece)
        {
            if (!(piece is Pawn pawn) || !pawn.ToBePromoted) return;

            var newPiece = _currentParticipant.Promote(pawn);

            Board.Pieces.Remove(piece);
            Board.Pieces.Add(newPiece);
        }
    }
}