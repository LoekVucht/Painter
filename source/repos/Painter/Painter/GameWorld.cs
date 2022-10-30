using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Diagnostics.SymbolStore;
using Microsoft.Xna.Framework.Input;

namespace Painter
{
    public class GameWorld
    {
        private Texture2D _background, _livesBalloon, _gameOver;
        private Cannon _cannon;
        private Ball _ball;
        private PaintCan[] cans;
        private int _amountOfLives;
        private const int AMOUNT_OF_CANS = 3;
        private const int MAX_AMOUNT_OF_LIVES = 5;

        public GameWorld(ContentManager content)
        {
            _background = content.Load<Texture2D>("spr_background");
            _livesBalloon = content.Load<Texture2D>("spr_lives");
            _gameOver = content.Load<Texture2D>("spr_gameover");
            _cannon = new(content);
            _ball = new(content);
            cans = new PaintCan[AMOUNT_OF_CANS] {
                new(content, 480.0f, Color.Red),
                new(content, 610.0f, Color.Green),
                new(content, 740.0f, Color.Blue)};
            _amountOfLives = MAX_AMOUNT_OF_LIVES;
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (IsGameOver && inputHelper.KeyPressed(Keys.Space)) {
                Reset();
            }
            _cannon.HandleInput(inputHelper);
            _ball.HandleInput(inputHelper);
        }

        private void Reset()
        {
            _amountOfLives = MAX_AMOUNT_OF_LIVES;
            _cannon.Reset();
            _ball.Reset();
            foreach(PaintCan can in cans) {
                can.Reset();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsGameOver)
            {
                return;
            }
            _ball.Update(gameTime);
            foreach(PaintCan can in cans)
            {
                can.Update(gameTime);
            }
        }

        public void LoseLife()
        {
            _amountOfLives--;
        }

        public Cannon Cannon { get { return _cannon; } }

        public Ball Ball { get { return _ball; } }

        public static bool IsOutsideWorld(Vector2 position)
        {
            return position .X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
        }

        public bool IsGameOver {
            get { return _amountOfLives <= 0; }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _ball.Draw(gameTime, spriteBatch);
            _cannon.Draw(gameTime, spriteBatch);
            foreach (PaintCan can in cans)
            {
                can.Draw(gameTime, spriteBatch);
            }

            for (int i = 0; i < _amountOfLives; i++)
            {
                spriteBatch.Draw(_livesBalloon, new Vector2(i * _livesBalloon.Width + 15, 20), Color.White);
            }

            if (IsGameOver)
            {
                spriteBatch.Draw(_gameOver, new Vector2(Painter.ScreenSize.X - _gameOver.Width,
                    Painter.ScreenSize.Y - _gameOver.Height) / 2, Color.White);
            }

            spriteBatch.End();
        }
    }
}
