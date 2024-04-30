

using NGPT;
using System;
using System.Collections;
using System.Security.Policy;
using UnityEngine;

namespace Qtool
{


    public class 绘制逻辑
    {
        Rect windowRect = new Rect(0, 0, 0, 0);//窗口
        Material lineMaterial;

        double time = 0;
        bool 按键按下 = false;


        int max = 40;//每行最大格子数
        float line = 2;//格子间距
        float leftAndRight = 10;//边界距离
        float top = 25;//顶边距
        float bottom = 10;//底边距
        float side = 0;//格子边长

        bool mouseLock = false;//鼠标锁定flag
        Vector3 lastMousePosition;//上一个鼠标位置


        int maxColumn = 0;//现有最大列号
        float offset = 0;//溢出偏移
        float offset_y = 0;//纵向溢出偏移

        public static int layerindex = 1;
        public static ItemFrame 物品界面 = new ItemFrame();
        public static RecipeFrame 配方界面 = new RecipeFrame();


        public void _onGUI()
        {
            if (UIRoot.instance == null || GameMain.instance == null)
                return;

            if (按键按下)
                time += Time.deltaTime;

            if (time > 1000)
                time = 0;

            if (Input.GetKeyDown(KeyCode.BackQuote))
                按键按下 = true;
   
            if (按键按下)
            {



                // 缩放处理
                max -= (int)Input.mouseScrollDelta.y;
                max = max < 15 ? 15 : max;
                max = max > 52 ? 52 : max;
                side = (windowRect.width - 2 * leftAndRight - (max - 1) * line) / max;
                // 拖动处理

                UIRoot.instance.OpenLoadingUI();
                windowRect.width = Screen.width;
                windowRect.height = Screen.height;
                
                GUI.skin.label.fontSize = (int)(side / 5);
                GUI.skin.button.fontSize = (int)(side / 5);
                GUI.skin.textField.fontSize = (int)(side / 5);

                windowRect = GUI.Window(0, windowRect, drawWindow, "啊啊啊~");
            }


            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
                if (time < 0.5) // 处理按键up状态存活时间毫秒，程序刚好被执行2次，导致状态复位，UI没有显示的问题
                    return;
                time = 0;
                layerindex = 1;
                按键按下 = false;
                UIRoot.instance.CloseLoadingUI(); // 关闭背景
            }




            if (Input.GetMouseButtonDown(0) && !mouseLock)
            {
                mouseLock = true;
                lastMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0) && mouseLock)
            {
                mouseLock = false;
            }
            if (Input.GetMouseButton(0) && mouseLock)
            {
                if (offset >= 0 && offset <= getMaxOffset())
                {
                    offset += lastMousePosition.x - Input.mousePosition.x;
                }
                else
                {
                    offset = offset < 0 ? 0 : getMaxOffset();
                }
                lastMousePosition.x = Input.mousePosition.x;
                //if (offset_y >= 0 && offset_y <= getMaxOffset_y(rawLayerList))
                //{
                //    offset_y -= lastMousePosition.y - Input.mousePosition.y;
                //}
                //else
                //{
                //    offset_y = offset_y < 0 ? 0 : getMaxOffset_y(rawLayerList);
                //}
                offset_y -= lastMousePosition.y - Input.mousePosition.y;
                lastMousePosition.y = Input.mousePosition.y;
            }











        }
        void drawWindow(int WindowID)
        {
            switch (layerindex)
            {
                case 1:
                    showItems();
                    break;
                case 13:
                    物品界面.showItems();
                    break;

                case 11:
                    配方界面.showRecipes();
                    break;

                case 12:
                    // DrawLine(50, 70, 120, 900);
                    // DrawLine(50, 70, 340, 900);
                    break;

            }
        }

        void showItems()
        {
            float left = leftAndRight - offset;
            float topOfMap = top - offset_y;

            int i = 0;
            foreach (ItemProto tempItemProto in LDB.items.dataArray)
            {
                if (tempItemProto.recipes.Count > 0)
                {
                    if (GUI.Button(new Rect(left + i % max * (side + line), topOfMap + i / max * (side + line), side, side), tempItemProto.iconSprite.texture))
                    {
                        // selectedItemId = tempItemProto.ID;
                        // GUIstate = 1;
                    }
                    i++;
                }
            }
        }



        float getMaxOffset()
        {
            if (maxColumn + 5 < max)
            {
                return 0;
            }
            else
            {
                return (maxColumn - max + 5) * (side + line);
            }
        }
        //float getMaxOffset_y(ArrayList rawLayerList)
        //{
        //    if (rawLayerList.Count * 2 + 5 < (windowRect.height - top - bottom) / (side + line))
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return (rawLayerList.Count * 2 - (windowRect.height - top - bottom) / (side + line) + 5) * (side + line);
        //    }
        //}












        private void FillWindow(int windowID)
        {
            if (GUI.Button(new Rect(50 - 18, 2, 16, 16), "x"))
            {
                // Destroy(this);
                GUIUtility.ExitGUI();
            }

            GUILayout.BeginVertical();
            GUILayout.Label("VersionChecking");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("yes"))
            {
                //spaceWarpPlugin.CheckVersions();
                //spaceWarpPlugin.ConfigCheckVersions.Value = true;
                //Destroy(this);
            }

            if (GUILayout.Button("No"))
            {
                //Destroy(this);
            }

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUI.DragWindow();
        }








    }
}



