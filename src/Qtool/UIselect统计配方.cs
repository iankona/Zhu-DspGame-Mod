

using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{
    public class FrameSelect统计配方
    {

        配方统计处理 配方统计 = null;

        void update配方统计()
        {
            if (Plugin.实例.配方统计 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
                return;
            配方统计 = Plugin.实例.配方统计;
        }




        public void showRecipe统计()
        {
            update配方统计();
            drawRecipeTitle();
            draw配方统计();
        }


        void drawRecipeTitle()
        {
            if (配方统计 == null)
                return;
            if (配方统计.汇总 == null)
                return;

            int row = 0;
            int col = 0;

            drawRecipeTitleBox(row, col, 配方统计.汇总.maxResultsCount + 配方统计.汇总.maxItemsCount + 3);

            Rect rect1 = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);
            Plugin.实例.布局.set字体比例(0.25f);
            GUI.Box(rect1, "汇总");

            col += (配方统计.汇总.maxResultsCount + 配方统计.汇总.maxItemsCount + 3);
            foreach (RecipeInfoList recipe汇总 in 配方统计.recipe汇总列表)
            {
                int length = recipe汇总.maxResultsCount + recipe汇总.maxItemsCount + 3;
                drawRecipeTitleBox(row, col, length);
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row, col), recipe汇总.itemTree.root.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row, col), recipe汇总.itemTree.root.itemProto.Name);
                col += length;
            }

        }

        void drawRecipeTitleBox(int row, int col, int length)
        {
            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col+length-1);

            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");
        }


        void draw配方统计()
        {
            if (配方统计 == null)
                return;
            if (配方统计.汇总 == null)
                return;

            int row = 1;
            int col = 0;
            drawRecipe统计(row, col, 配方统计.汇总);
            col += (配方统计.汇总.maxResultsCount + 配方统计.汇总.maxItemsCount + 3);

            foreach (RecipeInfoList recipe汇总 in 配方统计.recipe汇总列表)
            {
                drawRecipe统计(row, col, recipe汇总);
                col += (recipe汇总.maxResultsCount + recipe汇总.maxItemsCount + 3);
            }

        }

        void drawRecipe统计(int row, int col, RecipeInfoList recipe汇总)
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
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText上方(row, col), "配方：" + recipeProto.ID);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText中间(row, col), (recipeProto.TimeSpend / 60) + "S");
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText下方(row, col), calc.配方数量.ToString());


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




    }
}


