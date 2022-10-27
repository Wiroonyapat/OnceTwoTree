using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OnceTwoTree.HelperClass_ott;

namespace OnceTwoTree
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MyTextureInGame _player_Tex;
        private Vector2 _player_Pos = new Vector2(200,200);
        private My_Position_Calculator _player_posss;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _player_Tex = new MyTextureInGame(Content, "player2", Color.Wheat, Origin.Left, Origin.Right, Vector2.One, 0);
            _player_posss = new My_Position_Calculator(_player_Tex, _player_Pos);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player_Tex.Update_Me(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _player_Tex.Draw_FullAnimateMe(_spriteBatch, _player_Pos, 100, false);
            _player_Tex.Draw_AnimateMe(_spriteBatch, new Vector2(500,400),1, 50, false);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
