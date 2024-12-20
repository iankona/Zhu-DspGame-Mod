

using Steamworks;
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

        public Rect newrectFrameLayerColumn(int rowindex, int i)
        {
            get鼠标更新();

            float left = frame_padx;
            float topb = frame_pady + height1 + height2;
            float row = i / columnDefault;
            float col = i % columnDefault;
            if (rowindex >= 0) 
                row += rowindex;
            float x = left +  col * (side + padx);
            float y = topb + row * (side + pady);
            return new Rect(x, y, side, side);
        }

        public Rect newrectFrameLayerColumnName(int rowindex, int i)
        {
            get鼠标更新();

            float left = frame_padx;
            float topb = frame_pady + height1 + height2;
            float row = i / columnDefault;
            float col = i % columnDefault;
            if (rowindex >= 0)
                row += rowindex;
            row += 1;
            float x = left + col * (side + padx);
            float y = topb + row * (side + pady) - side / 3;
            return new Rect(x, y, side, side - side / 3);
        }



        public Rect newrectFrameLayer(int row, int col)
        {
            get鼠标更新();

            float left = frame_padx;
            float topb = frame_pady + height1 + height2;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady) ;
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
            return new Rect(x, y, side, side - side / 3);
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




        public Rect newrectFrameRecipeIcon(int row, int col, int rowspan, int colspan)
        {
            get鼠标更新();

            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx * 5);
            float y = topb + row * (side + pady);
            return new Rect(x, y, colspan * side, rowspan * side);
        }

        public Rect newrectFrameRecipeName(int row, int col, int rowspan, int colspan)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx * 5);
            float y = topb + row * (side + pady) - side / 3;
            return new Rect(x, y, colspan * side, side / 3);

        }
        public Rect newrectFrameRecipeText(int row, int col, int rowspan, int colspan)
        {
            get鼠标更新();

            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            row += 1;
            float x = left + col * (side + padx * 5);
            float y = topb + row * (side + pady) - side / 8;
            return new Rect(x, y, colspan * side, side / 3);

        }



        public Rect newrectFrameRecipeIconX(int row, int col)
        {
            get鼠标更新();

            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady);
            return new Rect(x, y, side, side);
        }

        public Rect newrectFrameRecipeNameX(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2;

            float x = left + col * (side + padx );
            float y = topb + row * (side + pady) - side / 3;
            return new Rect(x, y, 2*side, side / 3);

        }



        public Rect newrectFrameRecipeIconY(int row, int col)
        {
            get鼠标更新();

            float left = frame_padx ;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady);
            return new Rect(x, y, side, side);
        }

        public Rect newrectFrameRecipeNameY(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady) - side / 3;
            return new Rect(x, y, 2 * side, side / 3);

        }


        public Rect newrectFrameRecipeText上方Y(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady) - side;
            return new Rect(x, y, side, side / 3);

        }

        public Rect newrectFrameRecipeText中间Y(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady) - side / 3 * 2;
            return new Rect(x, y, side, side / 3);

        }

        public Rect newrectFrameRecipeText下方Y(int row, int col)
        {
            get鼠标更新();
            row += 1;
            float left = frame_padx;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + col * (side + padx);
            float y = topb + row * (side + pady) - side / 3;
            return new Rect(x, y, side, side / 3);

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

        public Rect newRectInfoIcon(int sumx, int sumy, int width, int height)
        {
            get鼠标更新();

            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + sumx + padx * 5;
            float y = topb + sumy + pady;
            return new Rect(x, y, width, height);
        }

        public Rect newRectInfoName(int sumx, int sumy, int width, int height)
        {
            get鼠标更新();
            float left = frame_padx + offset_x;
            float topb = frame_pady + height1 + height2 + offset_y;

            float x = left + sumx + padx * 5;
            float y = topb + sumy + pady;
            return new Rect(x, y, width, side / 3);

        }






    }









    public class RectInfo
    {
        public int row;
        public int col;

        public Texture texture;

        public int x;
        public int y;
        public int width;
        public int height;

        public int sumx;
        public int sumy;
        public int maxwidth;
        public int maxheight;

        public Rect newRect()
        {
            return new Rect(x, y, width, height);
        }

    }

    public class RectInfoList
    {
        public List<RectInfo> iconRectList = new List<RectInfo>(2048);
        List<int> 列宽列表 = new List<int>(1024);
        List<int> 行高列表 = new List<int>(1024);
        List<int> 累加列宽列表 = new List<int>(1024);
        List<int> 累加行高列表 = new List<int>(1024);

        int 行数 = 0;
        int 列数 = 0;

        public void Clear() 
        {
            iconRectList.Clear();
            列宽列表.Clear();
            行高列表.Clear();
            累加列宽列表.Clear();
            累加行高列表.Clear();
        }

        public void addTexture(int row1, int col1, Texture texture)
        {
            RectInfo rectInfo1 = new RectInfo();
            rectInfo1.row = row1;
            rectInfo1.col = col1;
            rectInfo1.texture = texture;
            rectInfo1.width = texture.width;
            rectInfo1.height = texture.height;

            bool 在列表里 = inTextureList(rectInfo1);
            if (在列表里)
            {
                Debug.Log("已有对象在列表里，对象对象::" + rectInfo1.row + "::" + rectInfo1.col + "::" + rectInfo1.width + "::" + rectInfo1.height + "未加入列表");
                return;
            }
            iconRectList.Add(rectInfo1);
            // Debug.Log(row1+"::" + col1 +"::"+ texture.name + "::" + texture.width + "::" + texture.height);
        }

        public RectInfo getRectInfo(int row1, int col1)
        {

            foreach (RectInfo rectInfo2 in iconRectList)
            {
                if (row1 == rectInfo2.row && col1 == rectInfo2.col)
                {
                    return rectInfo2;
                }
            }
            return null;
        }




        bool inTextureList(RectInfo rectInfo1)
        {
            bool 在列表里 = false;
            foreach (RectInfo rectInfo2 in iconRectList) 
            { if(rectInfo1.row == rectInfo2.row && rectInfo1.col == rectInfo2.col)
                {
                    在列表里 = true; 
                    break;
                }
            }
            return 在列表里;
        }

        List<RectInfo> getRowList(int row)
        {
            List<RectInfo> result = new List<RectInfo>(256);
            foreach (RectInfo rectInfo1 in iconRectList)
            {
                if(rectInfo1.row == row)
                    result.Add(rectInfo1);
            }
            if (result.Count > 0)
                return result;
            return null;
        }

        List<RectInfo> getColList(int col)
        {
            List<RectInfo> result = new List<RectInfo>(256);
            foreach (RectInfo rectInfo1 in iconRectList)
            {
                if (rectInfo1.col == col)
                    result.Add(rectInfo1);
            }
            if (result.Count > 0)
                return result;
            return null;
        }

        void update行数()
        {
            行数 = 0;
            foreach (RectInfo rectInfo1 in iconRectList)
            {
                int count = rectInfo1.row + 1;
                if (count > 行数)
                    行数 = count;
            }
        }

        void update列数()
        {
            列数 = 0;
            foreach (RectInfo rectInfo1 in iconRectList)
            {
                int count = rectInfo1.col + 1;
                if (count > 列数)
                    列数 = count;
            }
        }

        void update行高列表()
        {
            行高列表.Clear();
            for (int i = 0; i < 行数; i++)
            {
                List<RectInfo> rowRectInfoList = getRowList(i);
                if (rowRectInfoList == null) 
                {
                    行高列表.Add(0);
                    continue;
                }
                int maxHeight = 0;
                foreach(RectInfo rectInfo2 in rowRectInfoList) 
                { 
                    if (rectInfo2.height > maxHeight)
                        maxHeight = rectInfo2.height; 
                }

                行高列表.Add(maxHeight);

                foreach (RectInfo rectInfo2 in rowRectInfoList)
                    rectInfo2.maxheight = maxHeight;

            }
        }



        void update列宽列表()
        {
            列宽列表.Clear();
            for (int i = 0; i < 列数; i++)
            {
                List<RectInfo> colRectInfoList = getColList(i);
                if (colRectInfoList == null)
                {
                    列宽列表.Add(0);
                    continue;
                } 
                int maxWidth = 0;
                foreach (RectInfo rectInfo2 in colRectInfoList)
                {
                    if (rectInfo2.width > maxWidth)
                        maxWidth = rectInfo2.width;
                }

                列宽列表.Add(maxWidth);

                foreach (RectInfo rectInfo2 in colRectInfoList)
                    rectInfo2.maxwidth = maxWidth;
            }
        }



        void update累加行高列表外部()
        {
            累加行高列表.Clear();
            累加行高列表.Add(0);
            int sumY = 0;
            for (int i = 1; i < 行数; i++)
            {
                int maxHeight = 行高列表[i - 1];
                sumY += maxHeight;
                累加行高列表.Add(sumY);
            }

            foreach (RectInfo rectInfo1 in iconRectList)
                rectInfo1.sumy = 累加行高列表[rectInfo1.row];

        }



        void update累加列宽列表外部()
        {
            累加列宽列表.Clear();
            累加列宽列表.Add(0);
            int sumX = 0;
            for (int i = 1; i < 列数; i++)
            {
                int maxWidth = 列宽列表[i - 1];
                sumX += maxWidth;
                累加列宽列表.Add(sumX);
            }
            foreach (RectInfo rectInfo1 in iconRectList)
                rectInfo1.sumx = 累加列宽列表[rectInfo1.col];
        }








        void update累加列宽列表内部()
        {
            for (int i = 0; i < 列数; i++)
            {
                List<RectInfo> colRectInfoList = getColList(i);
                if (colRectInfoList == null)
                    continue;
                int sumX = 0;
                foreach (RectInfo rectInfo2 in colRectInfoList)
                {
                    rectInfo2.sumx = sumX;
                    sumX += rectInfo2.width;
                }

            }
        }

        void update累加行高列表内部()
        {
            for (int i = 0; i < 行数; i++)
            {
                List<RectInfo> rowRectInfoList = getRowList(i);
                if (rowRectInfoList == null)
                    continue;
                int sumY = 0;
                foreach (RectInfo rectInfo2 in rowRectInfoList)
                {
                    rectInfo2.sumy = sumY;
                    sumY += rectInfo2.height;
                }

            }
        }

        void SumXYtoXY()
        {
            foreach (RectInfo rectInfo2 in iconRectList)
            {
                rectInfo2.x = rectInfo2.sumx;
                rectInfo2.y = rectInfo2.sumy;
            }

        }



        public void 网格分布()
        {
            update列数();
            update列宽列表();
            update累加列宽列表外部();

            update行数();
            update行高列表();
            update累加行高列表外部();

            SumXYtoXY();
        }

        public void 列靠分布()
        {
            update列数();
            update列宽列表();
            update累加列宽列表内部();

            update行数();
            update行高列表();
            update累加行高列表外部();

            SumXYtoXY();
        }

        public void 行靠分布()
        {
            update列数();
            update列宽列表();
            update累加列宽列表外部();

            update行数();
            update行高列表();
            update累加行高列表内部();

            SumXYtoXY();
        }
    }













}









