using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    internal class PaintCan
    {
        private readonly Texture2D _colorRed, _colorGreen, _colorBlue;
        private Vector2 _position, _origin, _velocity;
        private Color _color;
        private Color _targetColor;
        private float _verticalMovementSpeed;

        public PaintCan(ContentManager content, float positionOffset, Color targetColor)
        {
            _colorRed = content.Load<Texture2D>("spr_can_red");
            _colorGreen = content.Load<Texture2D>("spr_can_green");
            _colorBlue = content.Load<Texture2D>("spr_can_blue");
            _origin = new Vector2(_colorRed.Width, _colorRed.Height) / 2;
            _targetColor = targetColor;
            _position = new Vector2(positionOffset, -_origin.Y);
            _verticalMovementSpeed = 30;
            Reset();
        }

        public void Update(GameTime gameTime)
        {
            float elapsedFrameTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _verticalMovementSpeed += 0.01f * elapsedFrameTimeInSeconds;
            if (_velocity != Vector2.Zero)
            {
                _position += _velocity * elapsedFrameTimeInSeconds;
                if (BoundingBox.Intersects(Painter.GameWorld.Ball.BoundingBox))
                {
                    _color = Painter.GameWorld.Ball.Color;
                    Painter.GameWorld.Ball.Reset();
                }
                if (GameWorld.IsOutsideWorld(_position - _origin))
                {
                    if (_color != _targetColor)
                    {
                        Painter.GameWorld.LoseLife();
                    }
                    Reset();
                }
            }
            else if (Painter.Random.NextDouble() < 0.01)
            {
                _velocity = CalculateRandomVelocity();
                _color = CalculateRandomColor();
            }
        }

        private Vector2 CalculateRandomVelocity()
        {
            return new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + _verticalMovementSpeed);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // determine the sprite based on the current color
            Texture2D currentSprite;
            if (_color == Color.Red)
                currentSprite = _colorRed;
            else if (_color == Color.Green)
                currentSprite = _colorGreen;
            else
                currentSprite = _colorBlue;

            // draw that sprite
            spriteBatch.Draw(currentSprite, _position, null,
                Color.White, 0f, _origin, 1.0f, SpriteEffects.None, 0);
        }

        private static Color CalculateRandomColor()
        {
            int randomNumber = Painter.Random.Next(3);
            return randomNumber switch
            {
                0 => Color.Red,
                1 => Color.Green,
                _ => Color.Blue,
            };
        }

        public Rectangle BoundingBox
        {
            get
            {
                Rectangle spriteBounds = _colorRed.Bounds;
                spriteBounds.Offset(_position - _origin);
                return spriteBounds;
            }
        }
        public void Reset()
        {
            _color = Color.Blue;
            _velocity = Vector2.Zero;
            _position.Y = -_origin.Y;
        }
    }
}
