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
    internal class Ball
    {
        private Texture2D _colorRed, _colorGreen, _colorBlue;
        private Vector2 _position, _origin;
        private Color _color;

        public Ball(ContentManager content)
        {
            _colorRed = content.Load<Texture2D>("spr_ball_red");
            _colorGreen = content.Load<Texture2D>("spr_ball_green");
            _colorRed = content.Load<Texture2D>("spr_ball_blue");
            _origin = new Vector2(_colorRed.Width / 2.0f, _colorRed.Height / 2.0f);
            Reset();
        }

        public void Reset()
        {
            _position = new Vector2(65, 390);
            _color = Color.Blue;
        }

        public void HandleInput(InputHelper inputHelper)
        {

        }

        public void Update(GameTime gameTime)
        {

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