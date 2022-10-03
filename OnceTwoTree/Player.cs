using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OnceTwoTree
{
    public class Player
    {
        public Vector2 Position;

        public string name;
        public int speed;
        public bool leftHand;
        public bool onGround = false;
        public bool onWall = false;
        public bool onClimb = false;
        public Player(string n, bool h)
        {
            name = n;
            leftHand = h;
        }

        public Player(string n,int s)
        {
            speed = s;
            name = n;
        }

        public void InputControl(Keys up,Keys down,Keys left ,Keys right)
        {
            KeyboardState ks = Keyboard.GetState();
            KeyboardState oldKS;
            
            
            
            if (ks.IsKeyDown(up) && onGround )
            {
                onGround = false;
                this.Position.Y -= 88 * 2;
                oldKS = ks;
            }
            if (ks.IsKeyDown(left))
            {
                this.Position.X -= speed ;
                oldKS = ks;
            }
            if (ks.IsKeyDown(right))
            {
                this.Position.X += speed;
                oldKS = ks;
            }

            
        }

        public int Counting(float el)
        {
            int count = 0;
            count += (int)el;
            return count;
        }

        public void Respawn(Vector2 vec)
        {
            Position = vec;
        }
    }
}
