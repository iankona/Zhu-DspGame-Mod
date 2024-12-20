

using NGPT;
using System;
using System.Collections;
using System.Security.Policy;
using UnityEngine;

namespace Qtool
{


    public class 窗口面板
    {
        // Rect Window1;
        public int guilayerindex = 1;

        public FrameSelectItem 物品多选界面 = new FrameSelectItem();
        public FrameSelectTree 物品多选树枝界面 = new FrameSelectTree();
        public FrameSelect统计物品 物品多选统计物品界面 = new FrameSelect统计物品();
        public FrameSelect统计配方 物品多选统计配方界面 = new FrameSelect统计配方();


        public FrameItem 物品界面 = new FrameItem();
        public FrameItemTree 物品树枝界面 = new FrameItemTree();
        public FrameItem统计 物品统计界面 = new FrameItem统计();
        public FrameItemInverte 物品反向界面 = new FrameItemInverte();
        public FrameItemInverteCalculate 物品反算界面 = new FrameItemInverteCalculate();
        public FrameItemRecipe 物品配方界面 = new FrameItemRecipe();



        public FrameSprite 图标界面 = new FrameSprite();
        public FrameTexture 贴图界面 = new FrameTexture();
        //public FrameLine 直线界面 = new FrameLine();
        public FrameLineTexture 直线贴图界面 = new FrameLineTexture();
        public FrameLineWhite 直线白色界面 = new FrameLineWhite();
        public FrameLineColor 直线颜色界面 = new FrameLineColor();

        //public FrameTest 测试界面 = new FrameTest();

        public void showWindown()
        {

            GUI.Window(0, Plugin.实例.布局.newrectWindown(), drawWindowFunction, "ZhuQtool3.2~");

        }


        void drawWindowFunction(int windowID)
        {
            //drawFrameRivet();
            //drawFrameLayer();
            // GUI.DragWindow();

        }

        void drawFrameRivet()
        {
            GUILayout.BeginArea(Plugin.实例.布局.newrectFrameRivet());
            // GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Sprites")) { guilayerindex = 31; }
            if (GUILayout.Button("Textures")) { guilayerindex = 32; }
            if (GUILayout.Button("Lines")) { guilayerindex = 33; }
            if (GUILayout.Button("LineTexture")) { guilayerindex = 34; }
            if (GUILayout.Button("LineWhite")) { guilayerindex = 35; }
            if (GUILayout.Button("LineColor")) { guilayerindex = 36; }
            if (GUILayout.Button("Tests")) { guilayerindex = 37; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Select物品")) { guilayerindex = 21; }
            if (GUILayout.Button("Select量化")) { guilayerindex = 22; }
            if (GUILayout.Button("Select统计")) { guilayerindex = 23; }
            if (GUILayout.Button("Select统计")) { guilayerindex = 24; }
            //if (GUILayout.Button("Select反向")) { guilayerindex = 25; }
            //if (GUILayout.Button("Select反算")) { guilayerindex = 25; }
            //if (GUILayout.Button("Select配方")) { guilayerindex = 26; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Items")) { guilayerindex = 1; }
            if (GUILayout.Button("Item量化")) { guilayerindex = 2; }
            if (GUILayout.Button("Item统计")) { guilayerindex = 3; }
            if (GUILayout.Button("Item反向")) { guilayerindex = 4; }
            if (GUILayout.Button("Item反算")) { guilayerindex = 5; }
            if (GUILayout.Button("Item配方")) { guilayerindex = 6; }
            GUILayout.EndHorizontal();

            // GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void drawFrameLayer()
        {
            switch (guilayerindex)
            {
                case 1:
                    物品界面.showItems();
                    break;

                case 2:
                    物品树枝界面.showTree();
                    break;
                case 3:
                    物品统计界面.showRecipe统计();
                    break;

                case 4:
                    物品反向界面.showTree();
                    break;

                case 5:
                    物品反算界面.showElement();
                    break;


                case 6:
                    物品配方界面.showRecipeNodes();
                    break;

                case 21:
                    物品多选界面.showItems();
                    break;

                case 22:
                    物品多选树枝界面.showTrees();
                    break;

                case 23:
                    物品多选统计物品界面.showItem统计();
                    break;

                case 24:
                    物品多选统计配方界面.showRecipe统计();
                    break;


                case 31:
                    图标界面.showSprites();
                    break;

                case 32:
                    贴图界面.showTextures();
                    break;


                case 34:
                    //直线贴图界面.showLines();
                    直线贴图界面.showLineTextures();
                    break;

                case 35:
                    直线白色界面.showLines();
                    break;
                case 36:
                    直线颜色界面.showLines();
                    break;

                    //case 12:
                    //    测试界面.showTests();
                    //    break;
                    //case 13:
                    //    直线界面.showLines();
                    //    break;




            }

        }




        //private void fillWindowFunction(int windowID)
        //{
        //    if (GUI.Button(new Rect(50 - 18, 2, 16, 16), "x"))
        //    {
        //        // Destroy(this);
        //        GUIUtility.ExitGUI();
        //    }

        //    GUILayout.BeginVertical();
        //    GUILayout.Label("VersionChecking");
        //    GUILayout.BeginHorizontal();

        //    if (GUILayout.Button("yes"))
        //    {
        //        //spaceWarpPlugin.CheckVersions();
        //        //spaceWarpPlugin.ConfigCheckVersions.Value = true;
        //        //Destroy(this);
        //    }

        //    if (GUILayout.Button("No"))
        //    {
        //        //Destroy(this);
        //    }

        //    GUILayout.EndHorizontal();

        //    GUILayout.EndVertical();
        //    GUI.DragWindow();
        //}
    }

}



