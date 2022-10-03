using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace OnceTwoTree
{
    public class Scene
    {
        protected EventHandler ScreenEvent;

        public Scene(EventHandler theScreenEvent)
        {
            ScreenEvent = theScreenEvent;
        }

        public virtual void Update(GameTime theTime)
        {

        }

        public virtual void Draw(SpriteBatch theBatch)
        {

        }
    }
}
