

using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{
    public class FrameSelect统计物品
    {

        物品统计处理 物品统计 = null;

        void update物品统计()
        {
            if (Plugin.实例.物品统计 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
                return;
            物品统计 = Plugin.实例.物品统计;
        }




        public void showItem统计()
        {
            update物品统计();
            drawItem统计();
        }

        void drawItem统计()
        {
            if (物品统计 == null)
                return;
            if (物品统计.汇总 == null)
                return;

            int row = 0;
            int col = 0;
            drawBoxItem汇总(row, col, 物品统计.汇总);
            drawItem汇总(row, col, 物品统计.汇总);

            int i = 1;
            foreach (ItemInfoList item汇总 in 物品统计.item汇总列表)
            {
                int column = col + i * 2;
                drawBoxItem汇总(row, column, item汇总);
                drawItem汇总(row, column, item汇总);
                i++;
            }
        }



        void drawBoxItem汇总(int row, int col, ItemInfoList item汇总)
        {
            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col+0);
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row + item汇总.maxRow + 1, col+1);
            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");
        }



        void drawItem汇总(int row, int col, ItemInfoList item汇总)
        {
            if (item汇总 == null)
                return;
            if (item汇总.显示产生列表 == null || item汇总.显示消耗列表 == null)
                return;

            Rect rect1 = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);
            Plugin.实例.布局.set字体比例(0.25f);
            if(item汇总.itemTree == null)
            {
                GUI.Box(rect1, "产生");
            }
            else
            {
                GUI.Box(rect1, item汇总.itemTree.root.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row, col), item汇总.itemTree.root.itemProto.Name);
            }
                


            Rect rect2 = Plugin.实例.布局.newrectFrameRecipeIcon(row, col + 1);
            Plugin.实例.布局.set字体比例(0.25f);
            GUI.Box(rect2, "消耗");

            //Rect rect3 = Plugin.实例.布局.newrectFrameRecipeIcon(row, col + 2);
            //Plugin.实例.布局.set字体比例(0.25f);
            //GUI.Box(rect3, "配方");




            foreach (ItemInfo 产生 in item汇总.显示产生列表)
            {
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 产生.row+1, col), 产生.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 产生.row+1, col), 产生.value + "/min");
            }

            foreach (ItemInfo 消耗 in item汇总.显示消耗列表)
            {
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 消耗.row+1, col+1), 消耗.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 消耗.row+1, col+1), 消耗.value + "/min");
            }



        }






    }
}


