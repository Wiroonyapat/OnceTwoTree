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
        public Vector2 position;
        public Vector2 scale;
        public Vector2 size;
        public Vector2 Hitblock;

        public string name;
        public int speed;
        public bool leftHand;
        public bool onGround = false;
        public bool onWall = false;
        public bool onClimb = false;
        public Player(string name, bool leftState,int s,Vector2 size,Vector2 pos,Vector2 scale)
        {
            this.name = name;
            leftHand = leftState;
            speed = s;
            this.size = size;
            position = pos;
            this.scale = scale;
            

            Hitblock = new Vector2(this.size.X * this.scale.X, this.size.Y * this.scale.Y);
        }


        public void InputControl(Keys up,Keys down,Keys left ,Keys right)
        {
            KeyboardState ks = Keyboard.GetState();
            KeyboardState oldKS;
            
            
            
            if (ks.IsKeyDown(up) && onGround )
            {
                onGround = false;
                this.position.Y -= 88 * 2;
                oldKS = ks;
            }
            if (ks.IsKeyDown(left))
            {
                position.X -= speed ;
                oldKS = ks;
            }
            if (ks.IsKeyDown(right))
            {
                position.X += speed;
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
            position = vec;
        }
    }
}
