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

        private static List<string> _TagCollide = new List<string>();

        private List<string> _TagCollide_CantCollide = new List<string>();

        private string _myTag = "default";

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
        /// สำหรับสร้าง Box จาก Texture
        /// </summary>
        /// <param name="_Texture_Input"> สร้าง Object Class MyTextureInGame ก่อน!! </param>
        /// <param name="_PositionOF"> สำหรับกำหนดตำแหน่งเริ่ม สามารถใส่ null ได้ เพื่อไม่กำหนด </param>
        /// <return></return>

        public My_Position_Calculator(MyTextureInGame _Texture_Input, Vector2? _PositionOF) 
        {
            _Texture_Calculate = _Texture_Input;

            if (_PositionOF.HasValue)
            {
                _main_Rec_Hitbox =
                    new Rectangle( (int)(_PositionOF.Value.X - _Texture_Calculate.my_origin.X),
                                   (int)(_PositionOF.Value.Y - _Texture_Calculate.my_origin.Y),
                                   _Texture_Calculate.my_Texture.Width,
                                   _Texture_Calculate.my_Texture.Height
                                 );
            }
            else
            {
                _main_Rec_Hitbox =
                    new Rectangle( 0 - (int)_Texture_Calculate.my_origin.X,
                                   0 - (int)_Texture_Calculate.my_origin.Y,
                                   _Texture_Calculate.my_Texture.Width,
                                   _Texture_Calculate.my_Texture.Height
                                 );

            }
        }

        /// <summary>
        /// สำหรับสร้าง Box จาก Texture
        /// </summary>
        /// <param name="_Texture_Input"> สร้าง Object Class MyTextureInGame ก่อน!! </param>
        /// <param name="_PositionOF"> สำหรับกำหนดตำแหน่งเริ่ม สามารถใส่ null ได้ เพื่อไม่กำหนด </param>
        /// <param name="Tag"> Tag สำหรับเช็ค Collide </param>

        public My_Position_Calculator(MyTextureInGame _Texture_Input, Vector2? _PositionOF, string Tag) 
        {
            _Texture_Calculate = _Texture_Input;

            if (_PositionOF.HasValue)
            {
                _main_Rec_Hitbox =
                    new Rectangle( (int)(_PositionOF.Value.X - _Texture_Calculate.my_origin.X),
                                   (int)(_PositionOF.Value.Y - _Texture_Calculate.my_origin.Y),
                                   _Texture_Calculate.my_Texture.Width,
                                   _Texture_Calculate.my_Texture.Height
                                 );
            }
            else
            {
                _main_Rec_Hitbox =
                    new Rectangle( 0 - (int)_Texture_Calculate.my_origin.X,
                                   0 - (int)_Texture_Calculate.my_origin.Y,
                                   _Texture_Calculate.my_Texture.Width,
                                   _Texture_Calculate.my_Texture.Height
                                 );

            }

            this._myTag = Tag;
        }

        public static void Add_Hitblock_Tag(string _Tagname)
        {
            _TagCollide.Add(_Tagname);
        }

        internal void Check_Collide()
        {
            

        }

        internal void Collide_with(My_Position_Calculator Input_posi, bool can_collide)
        {
            if (can_collide)
            {
                for(int i = 0; i < _TagCollide_CantCollide.Count; i++)
                {
                    if(Input_posi._myTag == _TagCollide_CantCollide[i])
                    {
                        _TagCollide_CantCollide.RemoveAt(i);
                        break;
                    }
                }
            }
            else if (!can_collide)
            {
                for (int i = 0; i < _TagCollide_CantCollide.Count; i++)
                {
                    if (Input_posi._myTag == _TagCollide_CantCollide[i])
                    {
                        _TagCollide_CantCollide.RemoveAt(i);
                        break;
                    }
                }

                _TagCollide_CantCollide.Add(Input_posi._myTag);
            }

        }

    }
}
