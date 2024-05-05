

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



        public FrameItem 物品界面 = new FrameItem();
        public FrameItemTree 物品树枝界面 = new FrameItemTree();
        // public FrameItemTree 物品树枝界面 = new FrameItemTree();

        public FrameRecipe 配方界面 = new FrameRecipe();
        public FrameRecipeNode 配方节点界面 = new FrameRecipeNode();


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
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Items")) { guilayerindex = 1; }
            if (GUILayout.Button("ItemTree")) { guilayerindex = 2; }
            if (GUILayout.Button("Item统计")) { guilayerindex = 3; }
            if (GUILayout.Button("ItemDepth")) { guilayerindex = 4; }
            if (GUILayout.Button("ItemDepth")) { guilayerindex = 5; }

            if (GUILayout.Button("Recipes")) { guilayerindex = 21; }
            if (GUILayout.Button("RecipeNode")) { guilayerindex = 22; }

            if (GUILayout.Button("Sprites")) { guilayerindex = 31; }
            if (GUILayout.Button("Tests")) { guilayerindex = 32; }
            if (GUILayout.Button("Lines")) { guilayerindex = 33; }
            if (GUILayout.Button("Textures")) { guilayerindex = 34; }


            GUILayout.EndHorizontal();
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
                    物品树枝界面.updateTree();
                    物品树枝界面.showRecipeSelect();
                    物品树枝界面.showRecipeTree();
                    break;

                case 21:
                    配方界面.showRecipes();
                    break;

                case 22:
                    配方节点界面.showRecipeNodes();
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



