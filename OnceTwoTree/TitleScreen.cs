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
            //menuTexture = game.Content.Load<Texture2D>("title");
            this.game = game;
        }
        public override void Update(GameTime theTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                
                return;
            }

            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(menuTexture, Vector2.Zero, Color.White); base.Draw(theBatch);
        }
    }
}
