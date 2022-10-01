using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OnceTwoTree.HelperClass_ott;


namespace OnceTwoTree.HelperClass_ott
{
    public class My_Position_Calculator
    {

        private Vector2 _main_Pos;

        private Vector2 _main_Origin;

        private Rectangle _main_Rec_Hitbox;

        private MyTextureInGame _Texture_Calculate;

        /// <summary>
        /// สำหรับสร้างตำแหน่ง
        /// </summary>

        public My_Position_Calculator()
        {
            _main_Pos = new Vector2(0, 0);
            _main_Origin = new Vector2(0, 0);
            _main_Rec_Hitbox = new Rectangle(0, 0, 1, 1);
        }


        /// <summary>
        /// สำหรับสร้างตำแหน่ง พร้อม set ค่า
        /// </summary>
        /// <param name="_PositionOF"> สำหรับกำหนดตำแหน่งเริ่ม สามารถใส่ null ได้ เพื่อไม่กำหนด </param>

        public My_Position_Calculator(Vector2 _PositionOF)
        {
            _main_Pos = _PositionOF;
            _main_Origin = new Vector2(0, 0);
            _main_Rec_Hitbox = new Rectangle(0, 0, 1, 1);
        }

        /// <summary>
        /// For Create Position From Texture
        /// </summary>
        /// <param name="_Texture_Input"> สร้าง Object Class MyTextureInGame ก่อน!! </param>
        /// <param name="_PositionOF"> สำหรับกำหนดตำแหน่งเริ่ม สามารถใส่ null ได้ เพื่อไม่กำหนด </param>
        /// <return></return>

        public My_Position_Calculator(MyTextureInGame _Texture_Input, Vector2? _PositionOF) 
        {
            _Texture_Calculate = _Texture_Input;
        }
    }
}
