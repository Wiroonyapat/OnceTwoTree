using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace OnceTwoTree
{
    public class TitleScreen : Scene
    {
        Texture2D menuTexture;
        Game1 game;
        public TitleScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            menuTexture = game.Content.Load<Texture2D>("UI\\StartGame");
            this.game = game;
        }
        public override void Update(GameTime theTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.X) == true)
            {
                game.mClimbScene.GameConfig();
                ScreenEvent.Invoke(game.mClimbScene, new EventArgs());
                return;
            }

            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(menuTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0); base.Draw(theBatch);
        }
    }
}
