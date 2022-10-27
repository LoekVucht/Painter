using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    public class Ball
    {
        private readonly Texture2D _colorRed, _colorGreen, _colorBlue;
        private Vector2 _position, _origin;
        private Color _color;
        private bool _shooting;
        private Vector2 _velocity;
        private const float VELOCITY_INCREASER = 1.2f;
        private const float Y_POSITION_DECREASER = 400.0f;

        public Ball(ContentManager content)
        {
            _colorRed = content.Load<Texture2D>("spr_ball_red");
            _colorGreen = content.Load<Texture2D>("spr_ball_green");
            _colorBlue = content.Load<Texture2D>("spr_ball_blue");
            _origin = new Vector2(_colorRed.Width / 2.0f, _colorRed.Height / 2.0f);
            Reset();
        }

        public void Reset()
        {
            _velocity = Vector2.Zero;
            _shooting = false;
            _color = Color.Blue;
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.MouseLeftButtonPressed() && !_shooting)
            {
                _shooting = true;
                _velocity = (inputHelper.MousePosition - Painter.GameWorld.Cannon.Position) * VELOCITY_INCREASER;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_shooting)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _velocity.Y += Y_POSITION_DECREASER * dt;
                _position += _velocity * dt;
            }
            else
            {
                _color = Painter.GameWorld.Cannon.Color;
                _position = Painter.GameWorld.Cannon.BallPosition;
            }
            if (_shooting && GameWorld.IsOutsideWorld(_position))
            {
                Reset();
            }
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
    }
}