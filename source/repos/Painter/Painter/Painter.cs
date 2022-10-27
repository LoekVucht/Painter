using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Painter
{
    public class Painter : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputHelper _inputHelper;
        private static GameWorld _gameWorld;
        public static Random Random { get; private set; }

        public Painter()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _inputHelper = new InputHelper();
            Random = new Random();
        }

        protected override void Initialize()
        {
            base.Initialize();
            ScreenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameWorld = new GameWorld(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _inputHelper.Update();
            _gameWorld.HandleInput(_inputHelper);
            _gameWorld.Update(gameTime);
        }

        public static GameWorld GameWorld
        {
            get { return _gameWorld; }
        }

        public static Vector2 ScreenSize { get; private set; }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _gameWorld.Draw(gameTime, _spriteBatch);
        }
    }
}