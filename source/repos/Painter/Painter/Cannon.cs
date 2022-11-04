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

        public Cannon(ContentManager content) : base(content, "spr_cannon_red", "spr_cannon_green", "spr_cannon_blue")
        {
            _cannonBarrel = content.Load<Texture2D>("spr_cannon_barrel");
            Position = new Vector2(72, 405);
            _barrelOrigin = new Vector2(_cannonBarrel.Height / 2, _cannonBarrel.Height / 2);
        }

        public override void Reset()
        {
            Rotation = 0.0f;
            Color = Color.Blue;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.R))
            {
                Color = Color.Red;
            }
            else if (inputHelper.KeyPressed(Keys.G))
            {
                Color = Color.Green;
            }
            else if (inputHelper.KeyPressed(Keys.B))
            {
                Color = Color.Blue;
            }

            double opposite = inputHelper.MousePosition.Y - Position.Y;
            double adjacent = inputHelper.MousePosition.X - Position.X;
            Rotation = (float)Math.Atan2(opposite, adjacent);
        }

        public Vector2 BallPosition
        {
            get
            {
                float opposite = (float)Math.Sin(Rotation) * _cannonBarrel.Width * 0.75f;
                float adjacent = (float)Math.Cos(Rotation) * _cannonBarrel.Width * 0.75f;
                return Position + new Vector2(adjacent, opposite);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_cannonBarrel, Position, null, Color.White, Rotation, _barrelOrigin, 1.0f, SpriteEffects.None, 0);
            base.Draw(gameTime, spriteBatch);
        }
    }
}
