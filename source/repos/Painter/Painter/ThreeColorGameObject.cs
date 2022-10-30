using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter
{
    public abstract class ThreeColorGameObject
    {
        protected Texture2D _colorRed, _colorGreen, _colorBlue;
        protected Color _color;
        protected Vector2 _position, _origin, _velocity;
        protected float rotation;

        public ThreeColorGameObject(ContentManager content, string redSprite, string greenSprite, string blueSprite)
        {
            // load the three sprites
            _colorRed = content.Load<Texture2D>(redSprite);
            _colorBlue = content.Load<Texture2D>(blueSprite);
            _colorGreen = content.Load<Texture2D>(greenSprite);

            // default origin: center of a sprite
            _origin = new Vector2(_colorRed.Width / 2.0f, _colorRed.Height / 2.0f);

            // initialize other things
            Position = Vector2.Zero;
            _velocity = Vector2.Zero;
            rotation = 0;

            Reset();
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

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public void Reset()
        {
            Color = Color.Blue;
        }

        public void HandleInput(InputHelper inputHelper)
        {
        }

        public void Update(GameTime gameTime)
        {
            Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                Color.White, rotation, _origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
