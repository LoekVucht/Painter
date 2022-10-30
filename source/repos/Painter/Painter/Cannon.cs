using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Painter
{
    public class Cannon : ThreeColorGameObject
    {
        private Texture2D _cannonBarrel;
        private Vector2 _barrelOrigin;
        private float _barrelRotation;
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
            rotation = 0.0f;
            _currentColor = Color.Blue;
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
            rotation = (float)Math.Atan2(opposite, adjacent);
        }

        public Vector2 BallPosition
        {
            get
            {
                float opposite = (float)Math.Sin(Angle) * _cannonBarrel.Width * 0.75f;
                float adjacent = (float)Math.Cos(Angle) * _cannonBarrel.Width * 0.75f;
                return _barrelPosition + new Vector2(adjacent, opposite);
            }
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
