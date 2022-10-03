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
            if(Keyboard.GetState().IsKeyDown(Keys.NumPad2) == true)
            {
                ScreenEvent.Invoke(game.mWalkScreen, new EventArgs());
                game.mClimbScene.GameConfig();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.X) == true)
            {
                ScreenEvent.Invoke(game.mClimbScene, new EventArgs());
                return;
            }

            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Begin();
            theBatch.Draw(menuTexture,new Rectangle(0,0,768,432), Color.White) ; base.Draw(theBatch);
            theBatch.End();
        }
    }
}
