using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace OnceTwoTree
{
    public class ClimbScene : Scene
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        Texture2D character,bg,block,towerTexture;
        Game1 game;

        Vector2 playerPos,playerScale, triggerPos;
        Vector2 barPos,dropPos,skillCheckPosL,skillCheckPosR;
        Vector2 dropScale,barScale,triggerScale,scScaleL,scScaleR;

        public bool leftHand;

        KeyboardState ks1,oldKsP1;

        bool onPressedL, onPressedR;
        int characterSpeed,trigerSpeed,energy;
        float maxTime, totalTime;

        public ClimbScene(Game1 game,EventHandler theScreenEvent):base(theScreenEvent)
        {
            this.game = game;
            character = game.Content.Load<Texture2D>("Character\\player1");
            bg = game.Content.Load<Texture2D>("UI\\background_01");
            block = game.Content.Load<Texture2D>("Character\\block");
            towerTexture = game.Content.Load<Texture2D>("Maptile\\Normal-wall-1");

            GameConfig();
        }


        public override void Update(GameTime theTime)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.Z) == true)
            {
                ScreenEvent.Invoke(game.mTitleScreen, new EventArgs());
                return;
            }
            float time = (float)theTime.ElapsedGameTime.TotalSeconds;

            totalTime += time;
            if (totalTime > maxTime)
            {
                energy = energy + 1;
                totalTime -= maxTime;
            }
             
            if (energy > 100)
            {
                energy--;
            }
            else if (energy <= 0)
            {
                energy = 0;
            }
            else if (energy <= 50)
            {
                maxTime = 0.25f;
            }
            #region Key Input
            ks1 = Keyboard.GetState();
            
            if(playerPos.Y < -32)
            {
                playerPos.Y = 448;
            }else if(playerPos.Y > 448)
            {
                playerPos.Y = 448;
            }
           
            //P1 control
            if (ks1.IsKeyDown(Keys.A)&& !onPressedL)
            {
                onPressedL = true;
                
                if(triggerPos.X+16 < 82 && triggerPos.X+16 > 50 && leftHand && energy > 0)
                {
                    leftHand = false;
                    energy -= 2;
                    playerPos.Y -= characterSpeed;
                }

                if(triggerPos.X+16 > 98 && triggerPos.X+16 < 162)
                {
                    energy -= 2;
                    playerPos.Y += characterSpeed;
                }
            }else if (ks1.IsKeyUp(Keys.A) && onPressedL)
            {
                onPressedL = false;
            }

            if (ks1.IsKeyDown(Keys.D) && !onPressedR)
            {
                onPressedR = true;
                if (triggerPos.X + 16 < 210 && triggerPos.X + 16 > 178 && !leftHand && energy > 0)
                {
                    leftHand = true;
                    energy -= 2;
                    playerPos.Y -= characterSpeed;
                }


                if (triggerPos.X + 16 > 98 && triggerPos.X + 16 < 162)
                {
                    energy -= 2;
                    playerPos.Y += characterSpeed;
                }

            }else if (ks1.IsKeyUp(Keys.D) && onPressedR)
            {
                onPressedR = false;
            }
           
            
            oldKsP1 = ks1;
            #endregion

            #region Core
            triggerPos.X += trigerSpeed;
            if (triggerPos.X <= 50 || triggerPos.X+16 >= 210)
            {
                trigerSpeed *= -1;
            }


            #endregion
            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            
            theBatch.Draw(bg, Vector2.Zero, null, Color.White, 0, Vector2.Zero, playerScale, SpriteEffects.None, 0);base.Draw(theBatch);
            //Tower
            for(int i = 0;i*96 < game.Window.ClientBounds.Height; i++)
            {
                theBatch.Draw(towerTexture, new Vector2(game.Window.ClientBounds.Width - 140, 96 * i), null, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0); base.Draw(theBatch);
            }
            
            theBatch.Draw(character, playerPos, new Rectangle(0, 0, 24, 48), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0); base.Draw(theBatch);
            //Whitebar
            theBatch.Draw(block, barPos, null, Color.White, 0, Vector2.Zero,barScale, SpriteEffects.None, 0); base.Draw(theBatch);
            //Drop Check
            theBatch.Draw(block, dropPos, null, Color.Yellow, 0, Vector2.Zero, dropScale, SpriteEffects.None, 0); base.Draw(theBatch);
            //Skillcheck
            theBatch.Draw(block, skillCheckPosL, null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0); base.Draw(theBatch);
            theBatch.Draw(block, skillCheckPosR, null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0); base.Draw(theBatch);
            //Trigger
            theBatch.Draw(block, triggerPos, null, Color.Black, 0, Vector2.Zero, triggerScale, SpriteEffects.None, 0); base.Draw(theBatch);

        }

        public void GameConfig()
        {
            characterSpeed = 10;
            trigerSpeed = 5;
            energy = 100;
            leftHand = false;

            maxTime = 0.5f;
            totalTime = 0;

            #region playerPosition / Scale&Position SkillCheckBar
            //Player Position& Player Scale
            playerPos = new Vector2(this.game.Window.ClientBounds.Width - 160, this.game.Window.ClientBounds.Height - 70);
            //White Bar
            barPos = new Vector2(50, this.game.Window.ClientBounds.Height - 80);
            barScale = new Vector2(8, 1);
            //Drop Bar
            dropScale = new Vector2(5, 1);
            dropPos = new Vector2(barPos.X + (block.Width * barScale.X / 2 - (block.Width * dropScale.X / 2)), this.game.Window.ClientBounds.Height - 80);
            //SkillCheckLeft
            scScaleL = new Vector2(1, 1);
            skillCheckPosL = new Vector2(barPos.X, barPos.Y);
            //SkillCheckRight
            scScaleR = new Vector2(1, 1);
            skillCheckPosR = new Vector2(barPos.X + (block.Width * barScale.X - (block.Width * scScaleR.X)), barPos.Y);
            //Trigger
            triggerScale = new Vector2(1, 1);
            triggerPos = new Vector2(barPos.X + (block.Width * barScale.X / 2 - (block.Width / 2)), this.game.Window.ClientBounds.Height - 80);
            #endregion

            onPressedL = false;
            onPressedR = false;
        }
        
    }
}
