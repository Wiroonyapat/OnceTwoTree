using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace OnceTwoTree
{
    public class GameTest : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D p1Texture, p2Texture, tileTexture;
        Player p1, p2;

        List<Vector2> tilePos = new List<Vector2>();
        List<Vector2> wallPos = new List<Vector2>();

        int gravity = 10;
        int wallPredic = 5;

        public GameTest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 384 * 2;
            graphics.PreferredBackBufferHeight = 216 * 2;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            p1Texture = Content.Load<Texture2D>("Character\\player4");
            p2Texture = Content.Load<Texture2D>("Character\\player2");
            tileTexture = Content.Load<Texture2D>("MapTile\\Ground");
            p1 = new Player("Player1", 5);
            p2 = new Player("P2", 1);

            //Spawn point
            p1.Position.X = 48;
            p2.Position.X = Window.ClientBounds.Width - 96;
            //Ground
            for (int i = 0; i * 24 * 2 < Window.ClientBounds.Width; i++)
            {
                tilePos.Add(new Vector2(24 * 2 * i, Window.ClientBounds.Height - 24 * 2));
            }
            //Wall
            for (int j = 3; j * 48 < Window.ClientBounds.Height - 48; j++)
            {
                wallPos.Add(new Vector2((Window.ClientBounds.Width / 2) - 48, 48 * j));
                wallPos.Add(new Vector2((Window.ClientBounds.Width / 2), 48 * j));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Controler
            p1.InputControl(Keys.W, Keys.S, Keys.A, Keys.D);
            p2.InputControl(Keys.Up, Keys.Down, Keys.Left, Keys.Right);

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                p1.Respawn(new Vector2(0, 50));
            }
            //Player1 Rectangle
            Rectangle playerRec = new Rectangle((int)p1.Position.X, (int)p1.Position.Y + 16, 48, 48);
            Rectangle playerHitbox = new Rectangle((int)p1.Position.X, (int)p1.Position.Y, 48, 64);
            Rectangle player2Rec = new Rectangle((int)p2.Position.X, (int)p2.Position.Y, 24 * 2, 48 * 2);

            //Collide
            Collision(playerRec);


            if (!p1.onGround)
            {
                Console.WriteLine("Garvity " + p1.onGround);
                p1.Position.Y += gravity;
            }

            p2.Position.Y += gravity;


            for (int j = 0; j < tilePos.Count; j++)
            {
                Rectangle tileRec = new Rectangle((int)tilePos[j].X, (int)tilePos[j].Y, 24 * 2, 24 * 2);

                if (player2Rec.Intersects(tileRec))
                {

                    p2.onGround = true;
                    p2.Position.Y = tilePos[j].Y - (p2Texture.Height * 2) + 31;
                    break;

                }
                else p2.onGround = false;
            }

            if (p1.onWall)
            {
                //System.Console.WriteLine("Player Pos" + playerRec.Top);
                System.Console.WriteLine("Wall = " + p1.onWall);
            }
            else if (!p1.onWall)
            {
                System.Console.WriteLine("Wall = " + p1.onWall);

            }


            if (p1.Position.X + 48 > Window.ClientBounds.Width) p1.Position.X = 0;
            if (p1.Position.X + 48 < 0) p1.Position.X = Window.ClientBounds.Width - 48;
            if (p2.Position.X + 48 > Window.ClientBounds.Width) p2.Position.X = 0;
            if (p2.Position.X + 48 < 0) p2.Position.X = Window.ClientBounds.Width - 48;



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(p1Texture, p1.Position, new Rectangle(0, 0, 24, 48), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            spriteBatch.Draw(p2Texture, p2.Position, new Rectangle(0, 0, 24, 48), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            for (int i = 0; i < tilePos.Count; i++)
            {
                spriteBatch.Draw(tileTexture, tilePos[i], new Rectangle(24, 0, 24, 24), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            }
            for (int i = 0; i < wallPos.Count; i++)
            {
                if (wallPos[i].X < Window.ClientBounds.Width / 2)
                {
                    spriteBatch.Draw(tileTexture, wallPos[i], new Rectangle(24 * 0, 24 * 1, 24, 24), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                }
                if (wallPos[i].X > Window.ClientBounds.Width / 2 - 1)
                {
                    spriteBatch.Draw(tileTexture, wallPos[i], new Rectangle(24 * 2, 24 * 1, 24, 24), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                }
            }
            spriteBatch.Draw(tileTexture, new Vector2(144, Window.ClientBounds.Height - 48), new Rectangle(24 * 0, 24 * 1, 24, 24), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);

            spriteBatch.End();





            base.Draw(gameTime);
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
                        p1.Position.Y = tileRec.Top - 64 + 1;
                    }
                    else p1.onGround = false;
                }


            }

            for (int wallC = 0; wallC < wallPos.Count; wallC++)
            {
                Rectangle wallRec = new Rectangle((int)wallPos[wallC].X, (int)wallPos[wallC].Y, 48, 48);
                //wallRec = new Rectangle(144, Window.ClientBounds.Height - 48, 24 * 2, 24 * 2);

                if (player.Intersects(wallRec))
                {
                    //Console.WriteLine("wall Num " + wallC);
                    //On Right >>>
                    if (player.Right > wallRec.Left && player.Left < wallRec.Left
                        && player.Top <= wallRec.Bottom && player.Bottom > wallRec.Top &&
                        player.Top >= wallRec.Top)
                    {
                        /*Console.WriteLine("WallR pos " + wallRec.Top);
                        p1.onWall = true;*/
                        p1.Position.X = wallRec.Left - 47;
                    }
                    //On Left <<<<
                    if (player.Left < wallRec.Right && player.Right > wallRec.Right
                        && player.Top <= wallRec.Bottom && player.Bottom > wallRec.Top
                        && player.Top >= wallRec.Top)
                    {
                        /*Console.WriteLine("WallL pos " + wallRec.Top);
                        p1.onWall = true;*/
                        p1.Position.X = wallRec.Right + 1;
                    }

                    if ((player.Left - wallPredic < wallRec.Right || player.Right + wallPredic > wallRec.Left) && !p1.onClimb)
                    {
                        p1.onWall = true;
                        break;
                    }

                }
                else { p1.onWall = false; }



            }
        }
    }
}

