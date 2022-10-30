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
        int titleFrame;
        Texture2D menuTexture;
        Game1 game;
        public TitleScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            titleFrame = 0;
            menuTexture = game.Content.Load<Texture2D>("OTT Resources\\Title\\TitleScene");
            this.game = game;
        }
        public override void Update(GameTime theTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.NumPad2) == true)
            {
                ScreenEvent.Invoke(game.mWalkScreen, new EventArgs());
                game.mClimbScene.GameConfig();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.NumPad3) == true)
            {
                ScreenEvent.Invoke(game.mClimbScene, new EventArgs());
                return;
            }
            
            
            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Begin();
            theBatch.Draw(menuTexture,Vector2.Zero,new Rectangle(1728*titleFrame,0,1728,972), Color.White) ; base.Draw(theBatch);
            theBatch.End();
        }
    }
}
