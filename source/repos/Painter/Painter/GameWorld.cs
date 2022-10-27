using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Painter
{
    internal class GameWorld
    {
        private Texture2D _background;
        private Cannon _cannon;

        public GameWorld(ContentManager content)
        {
            _background = content.Load<Texture2D>("spr_background");
            _cannon = new Cannon(content);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            _cannon.HandleInput(inputHelper);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _cannon.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
