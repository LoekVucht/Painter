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
        protected Texture2D ColorRed, ColorGreen, ColorBlue;
        public Color Color { get; set; }
        public Vector2 Position { get; set; }
        protected Vector2 Origin, Velocity;
        protected float Rotation;

        public ThreeColorGameObject(ContentManager content, string redSprite, string greenSprite, string blueSprite)
        {
            // load the three sprites
            ColorRed = content.Load<Texture2D>(redSprite);
            ColorBlue = content.Load<Texture2D>(blueSprite);
            ColorGreen = content.Load<Texture2D>(greenSprite);

            // default origin: center of a sprite
            Origin = new Vector2(ColorRed.Width / 2.0f, ColorRed.Height / 2.0f);

            // initialize other things
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Rotation = 0;

            Reset();
        }

        public Rectangle BoundingBox
        {
            get
            {
                Rectangle spriteBounds = ColorRed.Bounds;
                spriteBounds.Offset(Position - Origin);
                return spriteBounds;
            }
        }

        public virtual void Reset()
        {
            Color = Color.Blue;
        }

        public abstract void HandleInput(InputHelper inputHelper);

        public virtual void Update(GameTime gameTime)
        {
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // determine the sprite based on the current color
            Texture2D currentSprite;
            if (Color == Color.Red)
                currentSprite = ColorRed;
            else if (Color == Color.Green)
                currentSprite = ColorGreen;
            else
                currentSprite = ColorBlue;

            // draw that sprite
            spriteBatch.Draw(currentSprite, Position, null,
                Color.White, Rotation, Origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
