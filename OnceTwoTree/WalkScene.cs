using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace OnceTwoTree
{
    public class WalkScene : Scene
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D p1Texture, tileTexture,bg,block;
        Player p1;

        List<Vector2> tilePos = new List<Vector2>();
        List<Vector2> wallPos = new List<Vector2>();

        public Vector2 hitBlockPos;
        public Vector2 scaleHitBlock;
        int gravity = 10;
        int wallPredic = 5;

        Game1 game;

        public WalkScene(Game1 game ,EventHandler theScreenEvent) : base(theScreenEvent)
        {
            

            this.game = game;

            LoadContent();

        }

        protected void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            p1Texture = game.Content.Load<Texture2D>("Character\\SpriteSheet");
            tileTexture = game.Content.Load<Texture2D>("MapTile\\Ground");
            bg = game.Content.Load<Texture2D>("UI\\background_01");
            block = game.Content.Load<Texture2D>("Character\\block");

            p1 = new Player("Player1", false,5,new Vector2(24,32), new Vector2(50, 0), new Vector2(2, 2)) ;
            
            //Ground
            for (int i = 0; i * 24 * 2 < game.Window.ClientBounds.Width; i++)
            {
                tilePos.Add(new Vector2(24 * 2 * i, game.Window.ClientBounds.Height - 24 * 2));
            }
            //Wall
            for (int j = 3; j * 48 < game.Window.ClientBounds.Height - 48; j++)
            {
                wallPos.Add(new Vector2((game.Window.ClientBounds.Width / 2) - 48, 48 * j));
                wallPos.Add(new Vector2((game.Window.ClientBounds.Width / 2), 48 * j));
            }
        }

        public override void Update(GameTime theTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1) == true)
            {
                ScreenEvent.Invoke(game.mTitleScreen, new EventArgs());
                return;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad3) == true)
            {
                ScreenEvent.Invoke(game.mClimbScene, new EventArgs());
                return;
            }

            //Controler
            p1.InputControl(Keys.Space, Keys.S, Keys.A, Keys.D);
           

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                p1.Respawn(new Vector2(0, 50));
            }
            //Player1 Rectangle
            Rectangle playerRec = new Rectangle((int)p1.position.X, (int)p1.position.Y, (int)(p1.Hitblock.X),(int)(p1.Hitblock.Y) );
            Rectangle playerHitbox = new Rectangle((int)p1.position.X, (int)p1.position.Y, 48, 64);

            //hitBlock
            hitBlockPos = new Vector2(p1.position.X,p1.position.Y);
            scaleHitBlock = new Vector2(p1.Hitblock.X/24,p1.Hitblock.Y/24);
            //Collide
            Collision(playerRec);
            

            if (!p1.onGround)
            {
                p1.position.Y += gravity;
            }

            //Walk out of Scene
            if (p1.position.X + 48 > game.Window.ClientBounds.Width) p1.position.X = 0;
            if (p1.position.X + 48 < 0) p1.position.X = game.Window.ClientBounds.Width - 48;
           
            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            game.GraphicsDevice.Clear(Color.CornflowerBlue);
            theBatch.Begin();
            theBatch.Draw(bg, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            theBatch.Draw(block, hitBlockPos,null, Color.Blue, 0, Vector2.Zero, scaleHitBlock, SpriteEffects.None, 0);
            theBatch.Draw(p1Texture, p1.position, new Rectangle(0, 0, 24, 32), Color.White, 0, Vector2.Zero, p1.scale, SpriteEffects.None, 0);
            for (int i = 0; i < tilePos.Count; i++)
            {
                theBatch.Draw(tileTexture, tilePos[i], new Rectangle(25, 0, 24, 24), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            }
            for (int i = 0; i < wallPos.Count; i++)
            {
                if (wallPos[i].X < game.Window.ClientBounds.Width / 2)
                {
                    theBatch.Draw(tileTexture, wallPos[i], new Rectangle(24 * 0, 24 * 1, 24, 24), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                }
                if (wallPos[i].X > game.Window.ClientBounds.Width / 2 - 1)
                {
                    theBatch.Draw(tileTexture, wallPos[i], new Rectangle(24 * 2, 24 * 1, 24, 24), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                }
            }
            theBatch.End();

        }

        public void Collision(Rectangle player)
        {

            //Ground
            for (int i = 0; i < tilePos.Count; i++)
            {
                Rectangle tileRec = new Rectangle((int)tilePos[i].X, (int)tilePos[i].Y, 24 * 2, 24 * 2);

                if (player.Intersects(tileRec))
                {

                    //top
                    if (player.Bottom > tileRec.Top && player.Top < tileRec.Top - 24
                        && player.Left < tileRec.Right && player.Right > tileRec.Left)
                    {
                        p1.onGround = true;
                        p1.position.Y = tileRec.Top - 64 + 1;
                    }
                    else p1.onGround = false;
                }


            }

            for (int wallC = 0; wallC < wallPos.Count; wallC++)
            {
                Rectangle wallRec = new Rectangle((int)wallPos[wallC].X, (int)wallPos[wallC].Y, 48, 48);


                if (player.Intersects(wallRec))
                {
                    //Console.WriteLine("wall Num " + wallC);
                    //On Right >>>
                    if (player.Right > wallRec.Left && player.Left < wallRec.Left
                        && player.Top <= wallRec.Bottom && player.Bottom > wallRec.Top &&
                        player.Top >= wallRec.Top && !p1.onClimb)
                    {
                        Console.WriteLine("WallR pos " + wallRec.Top);
                        p1.onWall = true;
                        p1.position.X = wallRec.Left - 47;
                    }
                    //On Left <<<<
                    if (player.Left < wallRec.Right && player.Right > wallRec.Right
                        && player.Top <= wallRec.Bottom && player.Bottom > wallRec.Top
                        && player.Top >= wallRec.Top && !p1.onClimb)
                    {
                        Console.WriteLine("WallL pos " + wallRec.Top);
                        p1.onWall = true;
                        p1.position.X = wallRec.Right + 1;
                    }

                    if ((player.Left - wallPredic < wallRec.Right || player.Right + wallPredic > wallRec.Left) && !p1.onClimb)
                    {
                        p1.onWall = true;
                        break;
                    }


                }
                else { p1.onWall = false; }

                if (player.Intersects(wallRec))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W) && p1.onWall)
                    {
                        p1.onClimb = true;
                        if (player.Left < wallRec.Left && player.Right + wallPredic > wallRec.Left)
                        {
                            Console.WriteLine("On left ");
                            p1.position.X = wallRec.Left;
                        }
                        if (player.Right > wallRec.Right && player.Left - wallPredic < wallRec.Right)
                        {
                            p1.position.X = wallRec.Right;

                        }
                    }
                }


            }
        }
    }
}

