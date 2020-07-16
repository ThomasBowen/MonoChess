using MonoChess.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoChess
{
    public class MainGame : Game
    {
        private List<Piece> _pieces { get; set; } = new List<Piece>();

        private int _boardSize = 800;
        private GraphicsDeviceManager _graphicsDeviceManager;
        private Frame _board;
        private Frame _sideGutter;
        private Frame _bottomGutter;
        private SpriteBatch _spriteBatch;

        public MainGame()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = _boardSize + 30,
                PreferredBackBufferWidth = _boardSize + 280
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            Engine.Instance.InitiliseEngine(_pieces, GraphicsDevice, _boardSize, _boardSize);

            _board =  new Frame
            {
                Texture = TextureManager.GetTexture("Content/Board.png"),
                Area = new Rectangle(0, 0, _boardSize, _boardSize)
            };

            _sideGutter = new Frame
            {
                Texture = TextureManager.GetTexture("Content/Side-Bar.png"),
                Area = new Rectangle(_boardSize, 0, 30, _boardSize)
            };

            _bottomGutter = new Frame
            {
                Texture = TextureManager.GetTexture("Content/Bottom-Bar.png"),
                Area = new Rectangle(0, _boardSize, _boardSize + 30, 30)
            };

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            Engine.Instance.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_board.Texture, _board.Area, Color.White);
            _spriteBatch.Draw(_sideGutter.Texture, _sideGutter.Area, Color.White);
            _spriteBatch.Draw(_bottomGutter.Texture, _bottomGutter.Area, Color.White);

            foreach (var piece in _pieces)
            {
                var source = new Rectangle(0, 0, piece.Texture.Width, piece.Texture.Height);
                var colour = piece.Selected ? Color.Yellow : Color.White;
                colour = piece.DangerHighlight ? Color.Red : colour;

                _spriteBatch.Draw(piece.Texture, piece.Area, source, colour);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}