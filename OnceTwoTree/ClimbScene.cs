using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace OnceTwoTree
{
    public class ClimbScene : Scene
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        Texture2D character,bg,block,towerTexture;
        Game1 game;

        Vector2 playerPos,playerScale, triggerPos;
        Vector2 barPos,dropPos,skillCheckPosL,skillCheckPosR;
        Vector2 dropScale,barScale,triggerScale,scScaleL,scScaleR;
        List<Color> colorFeedBack = new List<Color>();
        int playerColor,scLColor,scRColor;
        public bool leftHand;
        public bool PanelCheck = false;
        KeyboardState ks1,oldKsP1;

        bool onPressedL, onPressedR,onPressed;
        int characterSpeed,trigerSpeed,energy;
        float maxTime, totalTime;

        public ClimbScene(Game1 game,EventHandler theScreenEvent):base(theScreenEvent)
        {
            this.game = game;
            character = game.Content.Load<Texture2D>("Character\\player3");
            bg = game.Content.Load<Texture2D>("UI\\background_01");
            block = game.Content.Load<Texture2D>("Character\\block");
            towerTexture = game.Content.Load<Texture2D>("Maptile\\Normal-wall-1");
            font = game.Content.Load<SpriteFont>("Font");

            #region Add Color //0.White 1.Red 2.Green
            colorFeedBack.Add(Color.White);
            colorFeedBack.Add(Color.Red);
            colorFeedBack.Add(Color.LightGreen);

            #endregion
            GameConfig();
        }


        public override void Update(GameTime theTime)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1) == true)
            {
                ScreenEvent.Invoke(game.mTitleScreen, new EventArgs());
                return;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2) == true)
            {
                ScreenEvent.Invoke(game.mWalkScreen, new EventArgs());
                return;
            }
            
            Energy((float)theTime.ElapsedGameTime.TotalSeconds);

            Rectangle triggerRec = new Rectangle((int)triggerPos.X,(int)triggerPos.Y,(int)(block.Width*triggerScale.X),(int)(block.Height*triggerScale.Y));
            Rectangle barRec = new Rectangle((int)barPos.X, (int)barPos.Y, (int)(block.Width * barScale.X), (int)(block.Height * barScale.Y));
            Rectangle dropRec = new Rectangle((int)dropPos.X, (int)dropPos.Y, (int)(block.Width * dropScale.X), (int)(block.Height * dropScale.Y));
            Rectangle scLRec = new Rectangle((int)skillCheckPosL.X, (int)skillCheckPosL.Y, (int)(block.Width * scScaleL.X), (int)(block.Height * scScaleL.Y));
            Rectangle scRRec = new Rectangle((int)skillCheckPosR.X, (int)skillCheckPosR.Y, (int)(block.Width * scScaleR.X), (int)(block.Height * scScaleR.Y));

            CoreGame(barRec, triggerRec);
            KeyInput(barRec, triggerRec, dropRec, scLRec, scRRec);

            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Begin();
            theBatch.Draw(bg, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);base.Draw(theBatch);
            //Tower
            for(int i = 0;i*96 < game.Window.ClientBounds.Height; i++)
            {
                theBatch.Draw(towerTexture, new Vector2(game.Window.ClientBounds.Width - 140, 96 * i), null, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0); base.Draw(theBatch);
            }
            
            theBatch.Draw(character, playerPos, new Rectangle(0, 0, 24, 48), colorFeedBack[playerColor], 0, Vector2.Zero, playerScale, SpriteEffects.None, 0); base.Draw(theBatch);
            //Whitebar
            theBatch.Draw(block, barPos, null, Color.White, 0, Vector2.Zero,barScale, SpriteEffects.None, 0); base.Draw(theBatch);
            //Drop Check
            theBatch.Draw(block, dropPos, null, Color.Yellow, 0, Vector2.Zero, dropScale, SpriteEffects.None, 0); base.Draw(theBatch);
            //SkillcheckL
            theBatch.Draw(block, skillCheckPosL, null, colorFeedBack[scLColor], 0, Vector2.Zero, scScaleL, SpriteEffects.None, 0); base.Draw(theBatch);
            //SkillcheckR
            theBatch.Draw(block, skillCheckPosR, null, colorFeedBack[scRColor], 0, Vector2.Zero, scScaleR, SpriteEffects.None, 0); base.Draw(theBatch);
            //Trigger
            theBatch.Draw(block, triggerPos, null, Color.Black, 0, Vector2.Zero, triggerScale, SpriteEffects.None, 0); base.Draw(theBatch);


            
            if (PanelCheck)
            {
                theBatch.Draw(block, Vector2.Zero, null, Color.Brown, 0,Vector2.Zero, new Vector2(10, 3),SpriteEffects.None,0);base.Draw(theBatch);
                theBatch.DrawString(font,"Character Pos " + playerPos, new Vector2(5, 5),Color.White);
                theBatch.DrawString(font, "Left Status = " + leftHand, new Vector2(5, 22), Color.White);
                theBatch.DrawString(font, "Stamina = " + energy, new Vector2(5, 39), Color.White);

            }

            theBatch.End();

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
            playerScale = new Vector2(2, 2);
            playerPos = new Vector2(this.game.Window.ClientBounds.Width - 160, this.game.Window.ClientBounds.Height - 70);
            //White Bar
            barPos = new Vector2(50,80);
            barScale = new Vector2(8, 1);
            //Drop Bar
            dropScale = new Vector2(3, 1);
            dropPos = new Vector2(barPos.X + (block.Width * barScale.X / 2 - (block.Width * dropScale.X / 2)), barPos.Y);
            //SkillCheckLeft
            scScaleL = new Vector2(2, 1);
            skillCheckPosL = new Vector2(barPos.X, barPos.Y);
            //SkillCheckRight
            scScaleR = new Vector2(2, 1);
            skillCheckPosR = new Vector2(barPos.X + (block.Width * barScale.X - (block.Width * scScaleR.X)), barPos.Y);
            //Trigger
            triggerScale = new Vector2(1, 1);
            triggerPos = new Vector2(barPos.X + (block.Width * barScale.X / 2 - (block.Width / 2)), barPos.Y);
            #endregion
            #region Color Feedback
            playerColor = 0;
            scLColor = 1;
            scRColor = 2;
            #endregion
            onPressedL = false;
            onPressedR = false;
            onPressed = false;
        }
        public void CoreGame(Rectangle barRec, Rectangle triggerRec)
        {

            triggerPos.X += trigerSpeed;
            if (triggerRec.Intersects(barRec))
            {
                if (triggerRec.Left + trigerSpeed <= barRec.Left ||
                    triggerRec.Right + trigerSpeed >= barRec.Right)
                {
                    trigerSpeed *= -1;
                }

            }

            if (leftHand)
            {
                scLColor = 2;
                scRColor = 1;
            }else if (!leftHand)
            {
                scLColor = 1;
                scRColor = 2;
            }

        }
        public void KeyInput(Rectangle barRec, Rectangle triggerRec,Rectangle dropRec ,Rectangle scLRec, Rectangle scRRec)
        {
            #region Key Input
            ks1 = Keyboard.GetState();

            if (playerPos.Y < -character.Height*playerScale.Y)
            {
                playerPos.Y = game.Window.ClientBounds.Height;
            }
            else if (playerPos.Y > game.Window.ClientBounds.Height)
            {
                playerPos.Y = game.Window.ClientBounds.Height;
            }

            

            if(ks1.IsKeyDown(Keys.O) && !onPressed)
            {
                onPressed = true;
                if (!PanelCheck) { PanelCheck = true; }
                else if (PanelCheck) { PanelCheck = false; }
            }
            else if(ks1.IsKeyUp(Keys.O)&& onPressed) { onPressed = false; }
           
            //P1 control
            if (ks1.IsKeyDown(Keys.A) && !onPressedL)
            {
                onPressedL = true;
                //Skill for check left hand
                if (triggerRec.Intersects(scLRec) && leftHand && energy > 0)
                {
                    playerColor = 2;
                    leftHand = false;
                    energy -= 2;
                    playerPos.Y -= characterSpeed;
                }
                //Check On Drop Block
                if (triggerRec.Intersects(dropRec))
                {
                    playerColor = 1;
                    energy -= 2;
                    playerPos.Y += 10;
                }
                
            }
            else if (ks1.IsKeyUp(Keys.A) && onPressedL)
            {
                playerColor = 0;
                onPressedL = false;
            }

            if (ks1.IsKeyDown(Keys.D) && !onPressedR)
            {
                onPressedR = true;
                //Skill for check Right hand
                if (triggerRec.Intersects(scRRec)&& !leftHand && energy > 0)
                {
                    playerColor = 2;
                    leftHand = true;
                    energy -= 2;
                    playerPos.Y -= characterSpeed;
                }
                //Check On Drop Block
                if (triggerRec.Intersects(dropRec))
                {
                    playerColor = 1;
                    energy -= 2;
                    playerPos.Y += characterSpeed;
                }

            }
            else if (ks1.IsKeyUp(Keys.D) && onPressedR)
            {
                playerColor = 0;
                onPressedR = false;
            }


            oldKsP1 = ks1;
            #endregion
        }

        public void Energy(float time)
        {
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
            else if (energy >= 50)
            {
                maxTime = 0.5f;
            }
            else if(energy < 50)
            {
                maxTime = 0.1f;
            }

        }

    }
}
