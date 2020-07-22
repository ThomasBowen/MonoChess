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

        private GraphicsDeviceManager _graphicsDeviceManager;
        private Frame _board;
        private Frame _sideGutter;
        private Frame _bottomGutter;
        private SpriteBatch _spriteBatch;

        private int BoardSize => 600;
        private int SideGutterSize => (int)(BoardSize * 0.35);
        private int GutterSize => (int)(BoardSize * 0.0375);

        public MainGame()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = BoardSize + GutterSize,
                PreferredBackBufferWidth = BoardSize + SideGutterSize
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            Engine.Instance.InitiliseEngine(_pieces, GraphicsDevice, BoardSize, BoardSize);

            _board =  new Frame
            {
                Texture = TextureManager.GetTexture("Content/Board.png"),
                Area = new Rectangle(0, 0, BoardSize, BoardSize)
            };

            _sideGutter = new Frame
            {
                Texture = TextureManager.GetTexture("Content/Side-Bar.png"),
                Area = new Rectangle(BoardSize, 0, GutterSize, BoardSize)
            };

            _bottomGutter = new Frame
            {
                Texture = TextureManager.GetTexture("Content/Bottom-Bar.png"),
                Area = new Rectangle(0, BoardSize, BoardSize + GutterSize, GutterSize)
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