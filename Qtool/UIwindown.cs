

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
        public FrameSelect统计 物品多选统计界面 = new FrameSelect统计();


        public FrameItem 物品界面 = new FrameItem();
        public FrameItemTree 物品树枝界面 = new FrameItemTree();
        public FrameItem统计 物品统计界面 = new FrameItem统计();
        public FrameItemInverte 物品反向界面 = new FrameItemInverte();
        public FrameItemRecipe 物品配方界面 = new FrameItemRecipe();



        //public FrameSprite 图标界面 = new FrameSprite();
        //public FrameTest 测试界面 = new FrameTest();
        //public FrameLine 直线界面 = new FrameLine();
        //public FrameTexture 贴图界面 = new FrameTexture();

        public void showWindown()
        {

            GUI.Window(0, Plugin.实例.布局.newrectWindown(), drawWindowFunction, "啊啊啊~");

        }


        void drawWindowFunction(int windowID)
        {
            drawFrameRivet();
            drawFrameLayer();
            // GUI.DragWindow();

        }

        void drawFrameRivet()
        {
            GUILayout.BeginArea(Plugin.实例.布局.newrectFrameRivet());
            // GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Sprites")) { guilayerindex = 31; }
            if (GUILayout.Button("Tests")) { guilayerindex = 32; }
            if (GUILayout.Button("Lines")) { guilayerindex = 33; }
            if (GUILayout.Button("Textures")) { guilayerindex = 34; }
            if (GUILayout.Button("Textures")) { guilayerindex = 35; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Select物品")) { guilayerindex = 21; }
            if (GUILayout.Button("Select量化")) { guilayerindex = 22; }
            if (GUILayout.Button("Select统计")) { guilayerindex = 23; }
            if (GUILayout.Button("Select反向")) { guilayerindex = 24; }
            if (GUILayout.Button("Select配方")) { guilayerindex = 25; }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Items")) { guilayerindex = 1; }
            if (GUILayout.Button("Item量化")) { guilayerindex = 2; }
            if (GUILayout.Button("Item统计")) { guilayerindex = 3; }
            if (GUILayout.Button("Item反向")) { guilayerindex = 4; }
            if (GUILayout.Button("Item配方")) { guilayerindex = 5; }
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
                    物品配方界面.showRecipeNodes();
                    break;

                case 21:
                    物品多选界面.showItems();
                    break;

                case 22:
                    物品多选树枝界面.showTrees();
                    break;

                case 23:
                    物品多选统计界面.showRecipe统计();
                    break;


                    //case 11:
                    //    图标界面.showSprites();
                    //    break;

                    //case 12:
                    //    测试界面.showTests();
                    //    break;
                    //case 13:
                    //    直线界面.showLines();
                    //    break;

                    //case 14:
                    //    贴图界面.showTextures();
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



