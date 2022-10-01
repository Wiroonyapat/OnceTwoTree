using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OnceTwoTree;
using OnceTwoTree.HelperClass_ott;

namespace OnceTwoTree.HelperClass_ott
{
    public class MyTextureInGame
    {
        private Texture2D my_Texture;

        protected Vector2 my_origin;
        protected Vector2 my_origin_Default;
        private Vector2 my_scale;
        private Vector2 my_scale_Default;
        private Color my_colour;
        private Color my_colour_Default;
        private float my_depth_Default;
        private Rectangle my_Frame;
        private float my_depth;
        private bool flip;

        private double my_time;

        private int my_Animate_Amount_Row;
        private int my_Animate_Amount_Frame;
        private double my_Animate_Interval_Per_Frame = 1.0;
        private bool my_Animate_IsLooping;
        private float Elapse_Game_Time;

        public enum Origin
        {
            Left, Center, Right, Top, Down
        }

        /// <summary>
        /// สร้าง Object Texture รวมให้แล้ว
        /// </summary>
        /// <param name="content"> ใช้ "Content" จาก Game </param>
        /// <param name="asset"> ชื่อไฟล์รูปภาพ </param>
        /// <param name="Texture_Colour"> สีโทน Texture </param>
        /// <param name="origin_Horizontal"> ตำแหน่ง Anchor ของ Texture แกนแนวนอน </param>
        /// <param name="origin_Vertical"> ตำแหน่ง Anchor ของ Texture แกนแนวตั้ง </param>
        /// <param name="scale"> Scale ของ Texture </param>
        /// <param name="depth"> ความลึกรูปภาพ </param>

        public MyTextureInGame(ContentManager content, string asset, Color Texture_Colour, Origin origin_Horizontal, Origin origin_Vertical, Vector2 scale, float depth)
        {
            my_Texture = content.Load<Texture2D>(asset);
            my_scale = new Vector2(1, 1);

            my_scale = scale;
            my_depth = depth;
            my_colour = Texture_Colour;
            my_time = 0;

            // set origin of Texture
            #region origin
            if (origin_Horizontal == Origin.Left)
            {
                my_origin.X = 0;
            }

            else if (origin_Horizontal == Origin.Center)
            {
                my_origin.X = my_Texture.Width * 0.5f * my_scale.X;
            }

            else if (origin_Horizontal == Origin.Right)
            {
                my_origin.X = my_Texture.Width * my_scale.X;
            }

            else
            {
                my_origin.X = 0;
            }

            if (origin_Vertical == Origin.Top)
            {
                my_origin.Y = 0;
            }

            if (origin_Vertical == Origin.Center)
            {
                my_origin.Y = my_Texture.Height * 0.5f * my_scale.Y;
            }

            if (origin_Vertical == Origin.Down)
            {
                my_origin.Y = my_Texture.Height * my_scale.Y;
            }

            else
            {
                my_origin.Y = 0;
            }
            #endregion
            // set origin of Texture

            my_origin_Default = my_origin;
            my_scale_Default = my_scale;
            my_depth_Default = my_depth;
            my_colour_Default = my_colour;
            my_Animate_Amount_Row = 1;
            my_Animate_Amount_Frame = 1;
            my_Frame = new Rectangle(0, 0, my_Texture.Width, my_Texture.Height);
        }

        /// <summary>
        /// สร้าง Object Texture รวมให้แล้ว
        /// </summary>
        /// <param name="content"> ใช้ "Content" จาก Game </param>
        /// <param name="asset"> ชื่อไฟล์รูปภาพ </param>
        /// <param name="Texture_Colour"> สีโทน Texture </param>
        /// <param name="origin"> ตำแหน่ง Anchor ของ Texture </param>
        /// <param name="scale"> Scale ของ Texture </param>
        /// <param name="depth"> ความลึกรูปภาพ </param>

        public MyTextureInGame(ContentManager content, string asset, Color Texture_Colour, Vector2 origin, Vector2 scale, float depth)
        {
            my_Texture = content.Load<Texture2D>(asset);
            my_scale = new Vector2(1, 1);

            my_scale = scale;
            my_origin = origin;
            my_depth = depth;
            my_colour = Texture_Colour;
            my_time = 0;

            my_origin_Default = my_origin;
            my_scale_Default = my_scale;
            my_depth_Default = my_depth;
            my_colour_Default = my_colour;
            my_Animate_Amount_Row = 1;
            my_Animate_Amount_Frame = 1;
            my_Frame = new Rectangle(0, 0, my_Texture.Width, my_Texture.Height);
        }

        #region SetData

        /// <summary>
        /// ใช้ set ข้อมูลเป็นค่าแรกสุด
        /// </summary>
        public void Set_Default() // ใช้เซ็ตกลับเป็นค่าเริ่มต้น 
        {
            my_time = 0;
            my_origin = my_origin_Default;
            my_scale = my_scale_Default;
            my_depth = my_depth_Default;
            my_colour = my_colour_Default;
            my_Animate_Amount_Row = 1;
            my_Animate_Amount_Frame = 1;
            my_Animate_Interval_Per_Frame = 1 / 60;
            my_Frame = new Rectangle(0, 0, my_Texture.Width, my_Texture.Height);
        }

        /// <summary>
        /// ใช้ set ข้อมูล ตามต้องการ
        /// </summary>
        /// <param name="Texture_Colour"> สีโทน Texture ใช้ null ได้ </param>
        /// <param name="origin_Horizontal"> ตำแหน่ง Anchor ของ Texture แกนแนวนอน </param>
        /// <param name="origin_Vertical"> ตำแหน่ง Anchor ของ Texture แกนแนวตั้ง </param>
        /// <param name="scale"> Scale ของ Texture ใช้ null ได้ </param>
        /// <param name="depth"> ความลึกรูปภาพ ใช้ null ได้ </param>

        public void Set_Data(Color? Texture_Colour, Origin origin_Horizontal, Origin origin_Vertical, Vector2? scale, float? depth) // ใช้เซ็ตข้อมูลภายใน 
        {


            if (scale.HasValue) my_scale = scale.Value;
            if (depth.HasValue) my_depth = depth.Value;
            if (Texture_Colour.HasValue) my_colour = Texture_Colour.Value;

            // set origin of Texture
            #region origin
            if (origin_Horizontal == Origin.Left)
            {
                my_origin.X = 0;
            }

            else if (origin_Horizontal == Origin.Center)
            {
                my_origin.X = my_Texture.Width * 0.5f * my_scale.X;
            }

            else if (origin_Horizontal == Origin.Right)
            {
                my_origin.X = my_Texture.Width * my_scale.X;
            }

            else
            {
                my_origin.X = 0;
            }

            if (origin_Vertical == Origin.Top)
            {
                my_origin.Y = 0;
            }

            if (origin_Vertical == Origin.Center)
            {
                my_origin.Y = my_Texture.Height * 0.5f * my_scale.Y;
            }

            if (origin_Vertical == Origin.Down)
            {
                my_origin.Y = my_Texture.Height * my_scale.Y;
            }

            else
            {
                my_origin.Y = 0;
            }
            #endregion             
            // set origin of Texture

            my_time = 0;
        }

        /// <summary>
        /// ใช้ set ข้อมูล ตามต้องการ
        /// </summary>
        /// <param name="Texture_Colour"> สีโทน Texture </param>
        /// <param name="origin"> ตำแหน่ง Anchor ของ Texture </param>
        /// <param name="scale"> Scale ของ Texture </param>
        /// <param name="depth"> ความลึกรูปภาพ </param>

        public void Set_Data(Color? Texture_Colour, Vector2? origin, Vector2? scale, float? depth) // เหมือนอันบน แค่รับ Origin เป็น Vector 
        {
            if (origin.HasValue) my_origin = origin.Value;
            if (scale.HasValue) my_scale = scale.Value;
            if (depth.HasValue) my_depth = depth.Value;
            if (Texture_Colour.HasValue) my_colour = Texture_Colour.Value;
            my_time = 0;
        }

        /// <summary>
        /// ใช้ตั้งการ Flip Texture
        /// </summary>
        /// <param name="_boolflip"> boolean Flip หรือไม่ </param>

        public void Set_flip(bool _boolflip)
        {
            this.flip = _boolflip;
        }
        #endregion

        // int Framerate limit ให้ 60 >= Framerate > 0
        // int Interval limit ให้ Interval >= 0.016667 หรือ (1/60) 

        #region Set_Animate

        /// <summary>
        /// Set จำนวนในการ Animate
        /// </summary>
        /// <param name="frame"> จำนวน frame ใน 1 แถว </param>
        /// <param name="row"> จำนวนแถว </param>
        public void Set_Animate_Amount(int frame, int row) // ใช้เซ็ต จำนวน Frame & Row 
        {
            my_Animate_Amount_Frame = frame;
            my_Animate_Amount_Row = row;
        }

        /// <summary>
        /// Set ความเร็ว Frame
        /// </summary>
        /// <param name="intevalbyframe"> ระยะห่างระหว่าง Frame เป็นวินาที </param>
        public void Set_Animate_Time(double intevalbyframe) // ใช้เซ็ต Inverval 
        {
            my_Animate_Interval_Per_Frame = intevalbyframe;
        }

        #endregion

        #region Update

        void Update_My_Time(float elapse_Game_time_Second)
        {
            my_time += elapse_Game_time_Second;
        }


        /// <summary>
        /// ถ้ามีการ Animation ให้ใส่ไว้ด้วย
        /// </summary>
        /// <param name="gametime"> gametime จาก "Game" </param>
        public void Update_Me(GameTime gametime) // ใส่ใน Update Game ไว้สำหรับ Texture ที่มีการ Animate
        {
            Elapse_Game_Time = (float)gametime.ElapsedGameTime.TotalSeconds;
        }

        int change_frame = 0;
        int change_row = 0;
        int counting_row = 0;


        void Update_My_Animate(int? frame, int? row, int? framerate, double? interval, int? start_frame, int? start_row, int? end_frame, int? end_row, bool Isloop)
        {
            int Horizon_Distance = my_Texture.Width / my_Animate_Amount_Frame;
            if (frame.HasValue) Horizon_Distance = my_Texture.Width / frame.Value;

            int Vertical_Distance = my_Texture.Height / my_Animate_Amount_Row;
            if (row.HasValue) Vertical_Distance = my_Texture.Height / row.Value;

            int Starting_Frame = 1;
            if (start_frame.HasValue) Starting_Frame = start_frame.Value;

            int Starting_Row = 1;
            if (start_row.HasValue) Starting_Row = start_row.Value;

            int Ending_Frame = (my_Texture.Width / Horizon_Distance);
            if (end_frame.HasValue) Ending_Frame = end_frame.Value;

            int Ending_Row = (my_Texture.Height / Vertical_Distance);
            if (end_row.HasValue) Ending_Row = end_row.Value;


            change_row = counting_row + (Starting_Row - 1);

            double gaspofframe = my_Animate_Interval_Per_Frame;

            if (framerate.HasValue) { gaspofframe = 1f / (float)framerate.Value; }

            if (interval.HasValue) { gaspofframe = interval.Value; }


            change_frame = (int)((my_time - (gaspofframe * (Ending_Frame - Starting_Frame + 1) * counting_row)) / gaspofframe) + (Starting_Frame - 1);

            my_Animate_IsLooping = Isloop;

            if (change_frame > Ending_Frame - 1)
            {
                counting_row++;
                change_frame = 0 + (Starting_Frame - 1);

                if (change_row > Ending_Row - 1 || counting_row > Ending_Row - Starting_Row)
                {
                    if (my_Animate_IsLooping)
                    {
                        change_row = 0 + (Starting_Row - 1); my_time = 0; counting_row = 0;
                        my_Animate_End = true;
                    }
                    else
                    {
                        counting_row--;
                        change_frame = Ending_Frame - 1;
                        my_Animate_End = true;
                    }

                }
                else my_Animate_End = false;

            }
            else my_Animate_End = false;

            my_Frame = new Rectangle(Horizon_Distance * change_frame, Vertical_Distance * change_row, Horizon_Distance, Vertical_Distance);

        }

        private bool my_Animate_End = false;

        public bool Is_Animation_End() // ใช้เช็ตว่า Animattion ได้จบไปหรือยัง ? 
        {
            return my_Animate_End;
        }

        #endregion

        #region Draw Image

        /// <summary>
        /// ใช้วาด Texture เต็มๆ
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        public void Draw_FullMe(SpriteBatch batch, Vector2 position, float? Opacity_Percent) // ใช้วาดTextureแบบเต็มรูป
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;

            if (this.flip) batch.Draw(my_Texture, position, null, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, null, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

        }

        /// <summary>
        /// ใช้วาด Texture เต็มๆ
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        public void Draw_Me(SpriteBatch batch, Vector2 position, float? Opacity_Percent) // ใช้วาดตัวTextureออกมา (เหมือนอันบนแหละ)
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;

            if (this.flip) batch.Draw(my_Texture, position, null, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, null, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);
        }

        /// <summary>
        /// ใช้วาด Texture เฉพาะบางส่วน
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="part"> ส่วนที่จะใช้วาด เป็น Rectangle </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        public void Draw_PartMe(SpriteBatch batch, Vector2 position, Rectangle part, float? Opacity_Percent) // ใช้วาดแค่ส่วนๆหนึ่งของ Texture
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;

            if (this.flip) batch.Draw(my_Texture, position, part, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, part, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);
        }
        #endregion

        // -------------------------------------------------------------------------------------------------------------------

        #region Draw Animate

        /// <summary>
        /// ใช้วาดภาพเต็มๆ ไม่ต้องอัพเดตก็ได้
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_FullAnimateMe(SpriteBatch batch, Vector2 position, float? Opacity_Percent, bool IsLoop) // ใช้เพื่อวาด Animation ทั้งภาพ 
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;
            Update_My_Animate(null, null, 2, null, null, null, null, null, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }

        /// <summary>
        /// ใช้วาดภาพเต็มๆ ไม่ต้องอัพเดตก็ได้
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="frameRate"> จำนวน Frame ต่อวินาที </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_FullAnimateMe(SpriteBatch batch, Vector2 position, int frameRate, float? Opacity_Percent, bool IsLoop) // เหมือนอันบนแต่ปรับ Framerate 
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;
            Update_My_Animate(null, null, frameRate, null, null, null, null, null, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }

        /// <summary>
        /// ใช้วาดภาพเต็มๆ ไม่ต้องอัพเดตก็ได้
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="intervalPerFrame"> ระยะเวลาห่าง ระหว่าง Texture เป็นวินาที </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_FullAnimateMe(SpriteBatch batch, Vector2 position, double intervalPerFrame, float? Opacity_Percent, bool IsLoop) // เหมือนอันบนแต่ปรับ Interval 
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;
            Update_My_Animate(null, null, null, intervalPerFrame, null, null, null, null, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }

        /// <summary>
        /// วาดเฉพาะรูปๆเดียวใน Frame นั้นๆ ไม่ต้องอัพเดตก็ได้
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="frame"> ตำแหน่ง Frame </param>
        /// <param name="row"> ตำแหน่ง แถว </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_AFrameMe(SpriteBatch batch, Vector2 position, int frame, int row, float? Opacity_Percent, bool IsLoop) // ใช้เพื่อเอาภาพๆเดียวใน Frame นั้น 
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;
            Update_My_Animate(null, null, null, null, frame, row, frame, row, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }


        /// <summary>
        /// วาดเฉพาะรูปๆเดียวใน Frame นั้นๆ ไม่ต้องอัพเดตก็ได้
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="Frame_n_Row"> ตำแหน่ง Frame และ แถว </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_AFrameMe(SpriteBatch batch, Vector2 position, Vector2 Frame_n_Row, float? Opacity_Percent, bool IsLoop) // เหมือนอันบนแต่รับ Frame กับ Row เป็น Vector 
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;
            Update_My_Animate(null, null, null, null, (int)Frame_n_Row.X, (int)Frame_n_Row.Y, (int)Frame_n_Row.X, (int)Frame_n_Row.Y, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }


        /// <summary>
        /// วาด Animate Texture
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="row"> ตำแหน่งแถว ที่เริ่ม Animate </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_AnimateMe(SpriteBatch batch, Vector2 position, int row, float? Opacity_Percent, bool IsLoop) // ใช้เพื่อวาด Animation ในแถวๆนั้น 
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;
            Update_My_Animate(null, null, null, null, null, row, null, row, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }


        /// <summary>
        /// วาด Animate Texture
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="row"> ตำแหน่งแถว ที่เริ่ม Animate </param>
        /// <param name="framerate"> จำนวน Frame ต่อวินาที </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_AnimateMe(SpriteBatch batch, Vector2 position, int row, int framerate, float? Opacity_Percent, bool IsLoop) // เหมือนอันบนแต่ปรับ Framerate 
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;
            Update_My_Animate(null, null, framerate, null, null, row, null, row, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }


        /// <summary>
        /// วาด Animate Texture
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="row"> ตำแหน่งแถว ที่เริ่ม Animate </param>
        /// <param name="intervalPerFrame"> ระยะเวลาห่าง ระหว่าง Texture เป็นวินาที </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_AnimateMe(SpriteBatch batch, Vector2 position, int row, double intervalPerFrame, float? Opacity_Percent, bool IsLoop) // เหมือนอันบนแต่ปรับ Interval 
        {
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;
            Update_My_Animate(null, null, null, intervalPerFrame, null, row, null, row, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }

        /// <summary>
        /// วาด Animate Texture แบบ เจาะจงทุกส่วน
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="Amount_Frame"> จำนวน Frame ใน 1 แถว </param>
        /// <param name="Amount_Row"> จำนวนแถว </param>
        /// <param name="IntervalPerFrame"> ระยะเวลาห่าง ระหว่าง Texture เป็นวินาที </param>
        /// <param name="Start_Frame"> ต่ำแหน่ง Frame ที่เริ่ม </param>
        /// <param name="Start_Row"> ตำแหน่ง แถว ที่เริ่ม </param>
        /// <param name="End_Frame"> ต่ำแหน่ง Frame ที่จบ </param>
        /// <param name="End_Row"> ตำแหน่ง แถว ที่จบ </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_AnimateMe_Custom(SpriteBatch batch, Vector2 position, int? Amount_Frame, int? Amount_Row, double? IntervalPerFrame, int? Start_Frame, int? Start_Row, int? End_Frame, int? End_Row, float? Opacity_Percent, bool IsLoop) // ใช้เพื่อวาด Animation ตามใจฉัน 
        {
            #region MyAtti
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;

            int amoframe = my_Animate_Amount_Frame;
            if (Amount_Frame.HasValue) amoframe = Amount_Frame.Value;

            int amorow = my_Animate_Amount_Row;
            if (Amount_Row.HasValue) amorow = Amount_Row.Value;

            double inter = my_Animate_Interval_Per_Frame;
            if (IntervalPerFrame.HasValue) inter = IntervalPerFrame.Value;

            int strframe = 1;
            if (Start_Frame.HasValue) strframe = Start_Frame.Value;

            int strrow = 1;
            if (Start_Row.HasValue) strrow = Start_Row.Value;

            int endframe = (int)amoframe;
            if (End_Frame.HasValue) endframe = End_Frame.Value;

            int endrow = (int)(amorow);
            if (End_Row.HasValue) endrow = End_Row.Value;

            #endregion


            Update_My_Animate(amoframe, amorow, null, inter, strframe, strrow, endframe, endrow, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }

        /// <summary>
        /// วาด Animate Texture แบบ เจาะจงทุกส่วน
        /// </summary>
        /// <param name="batch"> batch จาก "Game" </param>
        /// <param name="position"> ตำแหน่งที่ใช้วาด </param>
        /// <param name="Amount_Frame"> จำนวน Frame ใน 1 แถว </param>
        /// <param name="Amount_Row"> จำนวนแถว </param>
        /// <param name="FrameRate"> จำนวน Frame ต่อวินาที </param>
        /// <param name="Start_Frame"> ต่ำแหน่ง Frame ที่เริ่ม </param>
        /// <param name="Start_Row"> ตำแหน่ง แถว ที่เริ่ม </param>
        /// <param name="End_Frame"> ต่ำแหน่ง Frame ที่จบ </param>
        /// <param name="End_Row"> ตำแหน่ง แถว ที่จบ </param>
        /// <param name="Opacity_Percent"> ค่าความโปร่งใส่ของ Texture ค่าระหว่าง 0-100 </param>
        /// <param name="IsLoop"> boolean วน loop หรือไม่ </param>
        public void Draw_AnimateMe_Custom(SpriteBatch batch, Vector2 position, int? Amount_Frame, int? Amount_Row, int? FrameRate, int? Start_Frame, int? Start_Row, int? End_Frame, int? End_Row, float? Opacity_Percent, bool IsLoop) // เหมือนอันบนแหล่ะ 
        {
            #region MyAtti
            float opa = 100;
            if (Opacity_Percent.HasValue) opa = Opacity_Percent.Value;

            int amoframe = my_Animate_Amount_Frame;
            if (Amount_Frame.HasValue) amoframe = Amount_Frame.Value;

            int amorow = my_Animate_Amount_Row;
            if (Amount_Row.HasValue) amorow = Amount_Row.Value;

            int fps = (int)(1 / my_Animate_Interval_Per_Frame);
            if (FrameRate.HasValue) fps = FrameRate.Value;

            int strframe = 1;
            if (Start_Frame.HasValue) strframe = Start_Frame.Value;

            int strrow = 1;
            if (Start_Row.HasValue) strrow = Start_Row.Value;

            int endframe = amoframe;
            if (End_Frame.HasValue) endframe = End_Frame.Value;

            int endrow = amorow;
            if (End_Row.HasValue) endrow = End_Row.Value;

            #endregion

            Update_My_Animate(amoframe, amorow, fps, null, strframe, strrow, endframe, endrow, IsLoop);

            if (this.flip) batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, SpriteEffects.FlipHorizontally, my_depth);
            else batch.Draw(my_Texture, position, my_Frame, my_colour * 0.01f * opa, 0, my_origin, my_scale, 0, my_depth);

            Update_My_Time(Elapse_Game_Time);
        }
        #endregion
                
    }
}
