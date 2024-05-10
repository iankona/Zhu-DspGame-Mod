

using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

namespace Qtool
{


    public class 鼠标更新
    {
        bool 鼠标位于标题栏 = false;
        public static int window_x = Screen.width / 2; // 3 * Screen.width / 4
        public static int window_y = 0;
        public static int window_width = Screen.width / 2; // Screen.width / 4
        public static int window_height = Screen.height;

        public static float window_offset_x = 0;//溢出偏移
        public static float window_offset_y = 0;//纵向溢出偏移

        public static int frame_x = 0;
        public static int frame_y = 0;
        public static int frame_width = 0;
        public static int frame_height = 0;


        public static int frame_padx = 6;
        public static int frame_pady = 6;
        public static float frame_offset_x = 0;//溢出偏移
        public static float frame_offset_y = 0;//纵向溢出偏移


        public static int columnDefault = 20;
        public static float side = 0;//格子边长
        public static float padx = 4;//格子间距
        public static float pady = 4;//边界距离

        Vector3 lastMousePosition;//上一个鼠标位置
        Vector3 nextMousePosition;//下一个鼠标位置



        public void update鼠标滚轮滑动()
        {
            columnDefault -= (int)Input.mouseScrollDelta.y;
            columnDefault = columnDefault <  4 ?  4 : columnDefault;
            columnDefault = columnDefault > 35 ? 35 : columnDefault;
            side = (window_width - 2 * frame_padx - (columnDefault - 1) * padx) / columnDefault;

        }
        public void update鼠标左键按下()
        {
            lastMousePosition = Input.mousePosition;
            float mouse_x = lastMousePosition.x;
            float mouse_y = Screen.height - lastMousePosition.y; // 鼠标坐标系 -> GUI坐标系

            float start_x = window_x + window_offset_x;
            float start_y = window_y + window_offset_y; 
            float final_x = window_x + window_offset_x + window_width;
            float final_y = window_y + window_offset_y + 20; 
            //Debug.Log("鼠标左键按下::" + start_x + "::" + final_x + "::" + start_y + "::" + final_y); // 鼠标左键按下::1920::0::2560::20
            bool inx = false;
            bool iny = false;
            if (start_x < mouse_x && mouse_x < final_x)
                inx = true;
            if (start_y < mouse_y && mouse_y < final_y)
                iny = true;
            if (inx && iny)
            {
                鼠标位于标题栏 = true;
            }
            else 
            {
                鼠标位于标题栏 = false;
            }
            //Debug.Log("鼠标左键按下::" + 鼠标位于标题栏 + "::" + mouse_x + "::" + mouse_y); 
            //Debug.Log("鼠标左键按下::" + 鼠标位于标题栏 + "::" + lastMousePosition.ToString()); // 鼠标左键按下::False::(2327.0, 1430.0, 0.0)
        }

        public void update鼠标左键按住()
        {

            nextMousePosition = Input.mousePosition;
            if (鼠标位于标题栏) 
            {
                window_offset_x += nextMousePosition.x - lastMousePosition.x;
                window_offset_y -= nextMousePosition.y - lastMousePosition.y;
            }
            else
            {
                frame_offset_x += nextMousePosition.x - lastMousePosition.x;
                frame_offset_y -= nextMousePosition.y - lastMousePosition.y;
            }
            lastMousePosition = Input.mousePosition;
        }
        public void update鼠标左键松开()
        {
            鼠标位于标题栏 = false;
        }
    }




    public class 部件方框
    {
        int frame_padx;
        int frame_pady;

        int columnDefault = 10;
        float side = 0;//格子边长
        float padx = 4;//格子间距
        float pady = 4;//边界距离

        float offset_x = 0;//溢出偏移
        float offset_y = 0;//纵向溢出偏移

        float height0 = 0;
        float height1 = 20;
        float height2 = 0;
        float height3 = 0;
        float height4 = 0;
        float height5 = 0;


        void get鼠标更新()
        {
            frame_padx = 鼠标更新.frame_padx;
            frame_pady = 鼠标更新.frame_pady;

            columnDefault = 鼠标更新.columnDefault;
            side = 鼠标更新.side;
            padx = 鼠标更新.padx;
            pady = 鼠标更新.pady;

            offset_x = 鼠标更新.frame_offset_x;
            offset_y = 鼠标更新.frame_offset_y;

            GUI.skin.label.fontSize = (int)(side / 5);
            GUI.skin.button.fontSize = (int)(side / 5);
            GUI.skin.textField.fontSize = (int)(side / 5);

        }


        public void set字体比例(float scale)
        {
            if (scale <= 0 )
                return;
            GUI.skin.label.fontSize = (int)(side * scale);
            GUI.skin.button.fontSize = (int)(side * scale);
            GUI.skin.textField.fontSize = (int)(side * scale);
        }


        public Rect newrectWindown()
        {
            int window_x = 鼠标更新.window_x + (int)鼠标更新.window_offset_x;
            int window_y = 鼠标更新.window_y + (int)鼠标更新.window_offset_y;
            int window_width = 鼠标更新.window_width;
            int window_height = 鼠标更新.window_height;
            return new Rect(window_x, window_y, window_width, window_height);

        }
        public Rect newrectFrameRivet()
        {
            get鼠标更新();

            int fondSize = 13;
            GUI.skin.button.fontSize = fondSize;
            float topb = height1;
            height2 = (fondSize + frame_pady)*3.7f;
            return new Rect(frame_padx, topb, 鼠标更新.window_width - 2 * frame_padx, height2);
        }


        public Rect newrectFrameLayer(int i)
        {
            get鼠标更新();

            float left = frame_padx;
            float topb = frame_pady + height1 + height2;

            float x = left + i % columnDefault * (side + padx);
            float y = topb + i / columnDefault * (side + pady);
            return new Rect(x, y, side, side);
        }


        public Rect newrectFrameLayer(int row, int col)
        {
            get鼠标更新();

            float left = frame_padx;
            float topb = frame_pady + height1 + height2;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady);
            return new Rect(x, y, side, side);
        }

        public Rect newrectFrameLayerName(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx;
            float topb = frame_pady + height1 + height2;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady) - side / 3;
            return new Rect(x, y, side*2, side - side / 3);
        }



        public Rect newrectFrameLayerXY(int i)
        {
            get鼠标更新();

            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + i % columnDefault * (side + padx);
            float y = topb + i / columnDefault * (side + pady);
            return new Rect(x, y, side, side);
        }


        public Rect newrectFrameLayerX(int i)
        {
            get鼠标更新();

            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2;

            float x = left + i % columnDefault * (side + padx);
            float y = topb + i / columnDefault * (side + pady);
            return new Rect(x, y, side, side);
        }

        public Rect newrectFrameLayerY(int i)
        {
            get鼠标更新();

            float left = frame_padx;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + i % columnDefault * (side + padx);
            float y = topb + i / columnDefault * (side + pady);
            return new Rect(x, y, side, side);
        }

        public Rect newrectFrameIconXY(int row, int col) 
        {
            get鼠标更新();

            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady);
            return new Rect(x, y, side, side);
        }


        public Rect newrectFrameRecipeIcon(int row, int col)
        {
            get鼠标更新();

            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx * 5);
            float y = topb + row * (side + pady);
            return new Rect(x, y, side, side);
        }

        public Rect newrectFrameRecipeName(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx * 5);
            float y = topb + row * (side + pady) - side / 3;
            return new Rect(x, y, 2 * side, side / 3);

        }
        public Rect newrectFrameRecipeText(int row, int col)
        {
            get鼠标更新();

            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            row += 1;
            float x = left + col * (side + padx * 5);
            float y = topb + row * (side + pady) - side / 8;
            return new Rect(x, y, 2 * side, side / 3);

        }



        public Rect newrectFrameRecipeText上方(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx * 5);
            float y = topb + row * (side + pady) - side;
            return new Rect(x, y, side, side / 3);

        }

        public Rect newrectFrameRecipeText中间(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx * 5);
            float y = topb + row * (side + pady) - side / 3*2;
            return new Rect(x, y, side, side / 3);

        }

        public Rect newrectFrameRecipeText下方(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx * 5);
            float y = topb + row * (side + pady) - side / 3;
            return new Rect(x, y, side, side / 3);

        }
    }

























}









