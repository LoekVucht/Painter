using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Painter
{
    public class Cannon
    {
        private Texture2D _cannonBarrel, _colorRed, _colorGreen, _colorBlue;
        private Vector2 _barrelPosition, _barrelOrigin, _colorOrigin;
        private Color _currentColor;
        public float Angle { get; set; }

        public Cannon(ContentManager content)
        {
            _cannonBarrel = content.Load<Texture2D>("spr_cannon_barrel");
            _barrelOrigin = new Vector2(_cannonBarrel.Height, _cannonBarrel.Height) / 2;
            _colorRed = content.Load<Texture2D>("spr_cannon_red");
            _colorGreen = content.Load<Texture2D>("spr_cannon_green");
            _colorBlue = content.Load<Texture2D>("spr_cannon_blue");
            _colorOrigin = new Vector2(_colorRed.Width, _colorRed.Height) / 2;
            _currentColor = Color.Blue;
            _barrelPosition = new Vector2(72, 405);
        }

        public void Reset()
        {
            Angle = 0.0f;
            _currentColor = Color.Blue;
        }

        public Color Color
        {
            get { return _currentColor; }
            private set
            {
                if (value != Color.Red && value != Color.Green && value != Color.Blue)
                {
                    throw new ArgumentException();
                }
                _currentColor = value;
            }
        }

        public Vector2 Position
        {
            get { return _barrelPosition; }
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.R))
            {
                _currentColor = Color.Red;
            }
            else if (inputHelper.KeyPressed(Keys.G))
            {
                _currentColor = Color.Green;
            }
            else if (inputHelper.KeyPressed(Keys.B))
            {
                _currentColor = Color.Blue;
            }

            double opposite = inputHelper.MousePosition.Y - Position.Y;
            double adjacent = inputHelper.MousePosition.X - Position.X;
            Angle = (float)Math.Atan2(opposite, adjacent);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_cannonBarrel, _barrelPosition, null, Color.White, Angle, _barrelOrigin, 1.0f, SpriteEffects.None, 0);

            // determine the sprite based on the current color
            Texture2D currentSprite;
            if (_currentColor == Color.Red)
                currentSprite = _colorRed;
            else if (_currentColor == Color.Green)
                currentSprite = _colorGreen;
            else
                currentSprite = _colorBlue;

            // draw that sprite
            spriteBatch.Draw(currentSprite, _barrelPosition, null, Color.White, 0f, _colorOrigin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
