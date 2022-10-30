﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace OnceTwoTree
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public ClimbScene mClimbScene;
        public TitleScreen mTitleScreen;
        public Scene mCurrentScreen;

        public WalkScene mWalkScreen;

        Texture2D BG;

        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 972;
            graphics.PreferredBackBufferWidth = 1728;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mWalkScreen = new WalkScene(this, new EventHandler(GameplayScreenEvent));
            mTitleScreen = new TitleScreen(this, new EventHandler(GameplayScreenEvent));
            BG = Content.Load<Texture2D>("UI\\background_01");
            mClimbScene = new ClimbScene(this, new EventHandler(GameplayScreenEvent));
            mCurrentScreen = mTitleScreen;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mCurrentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mCurrentScreen.Draw(spriteBatch);


            base.Draw(gameTime);
        }

        public void GameplayScreenEvent(object obj, EventArgs e)
        {
            mCurrentScreen = (Scene)obj;
        }

    }
}
