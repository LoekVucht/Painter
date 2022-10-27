using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Painter
{
    public class GameWorld
    {
        private Texture2D _background;
        private Cannon _cannon;
        private Ball _ball;
        private PaintCan _leftCan;
        private PaintCan _middleCan;
        private PaintCan _rightCan;

        public GameWorld(ContentManager content)
        {
            _background = content.Load<Texture2D>("spr_background");
            _cannon = new(content);
            _ball = new(content);
            _leftCan = new(content, 480.0f, Color.Red);
            _middleCan = new(content, 610.0f, Color.Green);
            _rightCan = new(content, 740.0f, Color.Blue);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            _cannon.HandleInput(inputHelper);
            _ball.HandleInput(inputHelper);
        }

        public void Update(GameTime gameTime)
        {
            _ball.Update(gameTime);
            _leftCan.Update(gameTime);
            _middleCan.Update(gameTime);
            _rightCan.Update(gameTime);
        }

        public Cannon Cannon { get { return _cannon; } }

        public Ball Ball { get { return _ball; } }

        public static bool IsOutsideWorld(Vector2 position)
        {
            return position .X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _ball.Draw(gameTime, spriteBatch);
            _cannon.Draw(gameTime, spriteBatch);
            _leftCan.Draw(gameTime, spriteBatch);
            _middleCan.Draw(gameTime, spriteBatch);
            _rightCan.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
