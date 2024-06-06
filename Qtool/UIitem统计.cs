

using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{
    public class FrameItem统计
    {

        ItemInfoList item汇总 = null;
        RecipeInfoList recipe汇总 = null;

        void update汇总()
        {
            if (Plugin.实例.item汇总 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
                return;

            if (Plugin.实例.recipe汇总 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
                return;

            item汇总 = Plugin.实例.item汇总;
            recipe汇总 = Plugin.实例.recipe汇总;
        }



        public void showRecipe统计()
        {
            update汇总();

            drawItem汇总();
            draw数量汇总();
            drawRecipe汇总();
        }





        void drawItem汇总()
        {
            if (item汇总 == null)
                return;

            if(item汇总.显示产生列表 == null || item汇总.显示消耗列表 == null)
                return;


            drawItem汇总Box();

            Rect rect1 = Plugin.实例.布局.newrectFrameLayer(0, 0);
            Plugin.实例.布局.set字体比例(0.25f);
            GUI.Box(rect1, "产生");

            Rect rect2 = Plugin.实例.布局.newrectFrameLayer(0, 1);
            Plugin.实例.布局.set字体比例(0.25f);
            GUI.Box(rect2, "消耗");


            int row = 1;
            int col = 0;

            foreach(ItemInfo 产生 in item汇总.显示产生列表) 
            {
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIconY(row + 产生.row, col), 产生.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeNameY(row + 产生.row, col), 产生.value+"/min");
            }

            col = 1;
            foreach (ItemInfo 消耗 in item汇总.显示消耗列表)
            {
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIconY(row + 消耗.row, col), 消耗.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeNameY(row + 消耗.row, col), 消耗.value + "/min");
            }



        }



        void drawItem汇总Box()
        {
            Rect startRect = Plugin.实例.布局.newrectFrameLayer(0, 0);
            Rect finalRect = Plugin.实例.布局.newrectFrameLayer(item汇总.maxRow + 1, 1);
            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");
        }




        void draw数量汇总()
        {
            if (item汇总 == null)
                return;

            if (item汇总.显示产生列表 == null || item汇总.显示消耗列表 == null)
                return;


            draw数量汇总Box();

            Rect rect1 = Plugin.实例.布局.newrectFrameLayer(0, 2);
            Plugin.实例.布局.set字体比例(0.25f);
            GUI.Box(rect1, "配方");


            int row = 1;
            int col = 2;

            int i = 0;
            foreach (RecipeInfo 记录 in recipe汇总.显示列表)
            {
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIconY(row + i, col), 记录.calcSum.recipeProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText上方Y(row + i, col), "配方：" + 记录.calcSum.recipeProto.Name);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeNameY(row + i, col), "数量：" + 记录.calcSum.配方数量);
                i++;
            }


        }




        void draw数量汇总Box()
        {
            int row = 0;
            int col = 2;
            Rect startRect = Plugin.实例.布局.newrectFrameLayer(row, col);
            Rect finalRect = Plugin.实例.布局.newrectFrameLayer(row+recipe汇总.显示列表.Count + 1, col);
            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");
        }




        void drawRecipe汇总()
        {
            if (recipe汇总 == null)
                return;
            if (recipe汇总.显示列表 == null)
                return;

            int row = 0;
            int col = 0 + 3;
            drawRecipe汇总(row, col, recipe汇总);


        }

        void drawRecipe汇总(int row, int col, RecipeInfoList recipe汇总)
        {

            foreach (RecipeInfo 记录 in recipe汇总.显示列表)
            {
                drawBox记录(row + 记录.rowSum, col, 记录, recipe汇总);
                draw记录(row + 记录.rowSum, col, 记录, recipe汇总);

            }
        }
        void drawBox记录(int row, int col, RecipeInfo 记录, RecipeInfoList recipe汇总)
        {
            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);
            row = row + 记录.rowStepMax + 1+1 -1;
            col = col + recipe汇总.maxResultsCount + recipe汇总.maxItemsCount + 3-1;
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);

            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");
        }

        void draw记录(int row, int col, RecipeInfo 记录, RecipeInfoList recipe汇总)
        {
            col = col + recipe汇总.maxResultsCount + 1;
            int i = 0;
            foreach (NodeCalculate calc in 记录.dataArray)
            {
                drawRecipeColumn(row+i, col, calc);
                i++;
            }
            
            drawRecipeColumn(row+记录.rowStepMax+1, col, 记录.calcSum);
        }


        void drawRecipeColumn(int row, int col, NodeCalculate calc)
        {

            RecipeProto recipeProto = calc.recipeProto;

            //if(recipeProto.Results.Length > 1)
            //{
            //    int column0 = col - recipeProto.Results.Length;
            //    drawRecipeItemOrResultBox(row, column0, recipeProto.Results.Length);
            //}
            
            for (int k = 0; k < recipeProto.Results.Length; k++)
            {
                int itemID1 = recipeProto.Results[k];
                int resultCount1 = recipeProto.ResultCounts[k];
                float resultValue1 = calc.ResultValues[k];

                ItemProto itemProto1 = LDB.items.Select(itemID1);
                int column1 = col - recipeProto.Results.Length + k;

                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row, column1 - 1), itemProto1.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText上方(row, column1 - 1), itemProto1.Name);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText中间(row, column1 - 1), resultCount1.ToString() + "\n"+ resultValue1 + "/min");
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText下方(row, column1 - 1), resultValue1 + "/min");
            }

            GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row, col), recipeProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText上方(row, col), "配方："+recipeProto.ID);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText中间(row, col), (recipeProto.TimeSpend / 60) + "S");
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText下方(row, col), "数量："+calc.配方数量);



            //if (recipeProto.Items.Length > 1)
            //{
            //    int column0 = col + 1;
            //    drawRecipeItemOrResultBox(row, column0, recipeProto.Results.Length);
            //}

            for (int m = 0; m < recipeProto.Items.Length; m++)
            {
                int itemID2 = recipeProto.Items[m];
                int itemCount2 = recipeProto.ItemCounts[m];
                float itemValue2 = calc.ItemValues[m];

                ItemProto itemProto2 = LDB.items.Select(itemID2);
                int column2 = col + 1 + m;

                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row, column2 + 1), itemProto2.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText上方(row, column2 + 1), itemProto2.Name);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText中间(row, column2 + 1), itemCount2.ToString());
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText下方(row, column2 + 1), itemValue2 + "/min");
            }

        }


        void drawRecipeItemOrResultBox(int row, int col, int length)
        {
            if (length <= 1)
            { return; }

            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col + length - 1);

            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");

        }

    }
}


