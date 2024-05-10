

using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{
    public class FrameSelect统计
    {

        RecipeStatistics 树枝统计 = null;

        void update树枝统计()
        {
            if (Plugin.实例.树枝统计 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
                return;
            树枝统计 = Plugin.实例.树枝统计;
        }




        public void showRecipe统计()
        {
            update树枝统计();
            draw树枝统计();
        }

        void draw树枝统计()
        {
            if (树枝统计 == null)
                return;

            int row = 0;
            int col = 0 + 3;
            draw单树统计(row, col, 树枝统计.统计汇总);
            col += (树枝统计.统计汇总.maxResultsCount + 树枝统计.统计汇总.maxItemsCount + 3);

            foreach (RecipeInfoList 统计 in 树枝统计.统计列表)
            {
                draw单树统计(row, col, 统计);
                col += (统计.maxResultsCount + 统计.maxItemsCount + 3);
            }

        }

        void draw单树统计(int row, int col, RecipeInfoList 统计)
        {

            foreach (RecipeInfo 记录 in 统计.显示列表)
            {
                drawBox记录(row + 记录.rowSum, col, 记录, 统计);
                draw记录(row + 记录.rowSum, col, 记录, 统计);

            }
        }
        void drawBox记录(int row, int col, RecipeInfo 记录, RecipeInfoList 统计)
        {
            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);
            row = row + 记录.rowStepMax + 1+1 -1;
            col = col + 统计.maxResultsCount + 统计.maxItemsCount + 3-1;
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);

            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");
        }

        void draw记录(int row, int col, RecipeInfo 记录, RecipeInfoList 统计)
        {
            col = col + 统计.maxResultsCount + 1;
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


