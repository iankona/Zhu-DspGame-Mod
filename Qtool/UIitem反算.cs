

using Qtool;
using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{


    public class FrameItemInverteCalculate
    {


        UpTree upTree = null;
        ItemProto itemProto = null;
        int itemValue = 6000;
        ItemSelectList 物品多选 = null;
        InvertteCalculate 物品反算 = null;


        void updateSet()
        {
            if (物品多选 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
                物品多选 = new ItemSelectList();

            if (Plugin.实例.upTree == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
                return;

            if (upTree == null)
            {
                upTree = Plugin.实例.upTree;
            }

            if (itemProto == null)
            {
                itemProto = LDB.items.Select(Plugin.实例.物品ID);
                if (itemProto == null)
                {
                    return;
                }
                else // 处理第1次点击
                {
                    物品多选.setItemProtos(upTree.allItemProtoNoRepeat);
                }

            }


            if (itemProto.ID != Plugin.实例.物品ID) // 处理2~N次点击
            {
                itemProto = LDB.items.Select(Plugin.实例.物品ID);
                物品多选.setItemProtos(upTree.allItemProtoNoRepeat);
            }

            if(物品反算 == null)
                物品反算 = new InvertteCalculate();

        }



        public void showElement()
        {
            updateSet();
            draw基础矿物();
            draw选择物品();
            drawUpTreeAllItems();
            drawSelectItems();
        }


        void draw基础矿物()
        {
            int i = 0;
            int row = 0;
            foreach (ItemProto itemProto in LDB.items.dataArray)
            {
                if (itemProto.recipes.Count > 0)
                    continue;
                int col = i;
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIconX(row, col), itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeNameX(row, col), itemProto.Name);
                i++;
            }
        }
        void draw选择物品()
        {
            if (itemProto == null)
                return;
            if (物品多选 == null) 
                return;

            int row = 1;
            int col = 0;
            GUI.Box(Plugin.实例.布局.newrectFrameRecipeIconX(row, col), itemProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeNameX(row, col), itemProto.Name);

            Rect rect1 = Plugin.实例.布局.newrectFrameRecipeIconX(row, col+2);
            Plugin.实例.布局.set字体比例(0.25f);
            if(GUI.Button(rect1, "全选"))
            {
                物品多选.set全选();
                物品多选.更新显示列表();
            }
                

            Rect rect2 = Plugin.实例.布局.newrectFrameRecipeIconX(row, col+3);
            Plugin.实例.布局.set字体比例(0.25f);
            if (GUI.Button(rect2, "反选"))
            {
                物品多选.set反选();
                物品多选.更新显示列表();
            }
                

            Rect rect3 = Plugin.实例.布局.newrectFrameRecipeIconX(row, col+4);
            Plugin.实例.布局.set字体比例(0.25f);
            if (GUI.Button(rect3, "全不选"))
            {
                物品多选.set全不选();
                物品多选.更新显示列表();
            }
                

        }
        void drawUpTreeAllItems()
        {
            if (物品多选 == null)
                return;

            int row = 2;
            int col = 0;
            int i = 0;
            foreach (ItemBool itemBool in 物品多选.记录列表)
            {
                GUI.Box(Plugin.实例.布局.newrectFrameLayerColumn(row, i), itemBool.itemProto.iconSprite.texture);
                itemBool.是否选上 = GUI.Toggle(Plugin.实例.布局.newrectFrameLayerColumn(row, i), itemBool.是否选上, itemBool.itemProto.Name);
                if (itemBool.是否选上)
                    物品多选.更新显示列表(); 
                i++;
            }

        }


        void drawSelectItems()
        {

            if (物品多选 == null)
                return;
            if (itemProto == null)
                return;

            int row = 7;
            int col = 0;
            draw基础(row , col);

            row = 8; 
            int i = 0;
            foreach (ItemBool 记录 in 物品多选.显示列表)
            {
                int row2 = i / 3;
                int col2 = i % 3;
                int column = col2 * 6;
                drawBox(row + row2, col + column, 5);
                draw目标(row + row2, col + column, 记录);
                i++;
            }
        }

        void draw基础(int row, int col)
        {
            GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row, col), itemProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row, col), itemProto.Name);


            string valueText = GUI.TextField(Plugin.实例.布局.newrectFrameRecipeIcon(row, col + 1, 1, 3), itemValue.ToString());
            int.TryParse(valueText, out itemValue);

            Rect rect3 = Plugin.实例.布局.newrectFrameRecipeIcon(row, col + 4);
            Plugin.实例.布局.set字体比例(0.25f);
            if (GUI.Button(rect3, "全部\n更新"))
            {
                物品反算.setItemProto基础(itemProto);
                物品反算.setItemValue基础(itemValue);
                foreach(ItemBool itemBool in 物品多选.显示列表)
                {
                    物品反算.addItemProto目标(itemBool.itemProto);
                    itemBool.itemValue = (int)物品反算.getItemValue目标(itemBool.itemProto);
                }
            }

        }



        void draw目标(int row, int col, ItemBool 记录)
        {
            GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row, col), 记录.itemProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row, col), 记录.itemProto.Name);


            string valueText = GUI.TextField(Plugin.实例.布局.newrectFrameRecipeIcon(row, col+1, 1, 3), 记录.itemValue.ToString());
            // int.TryParse(valueText, out 记录.itemValue);

            Rect rect3 = Plugin.实例.布局.newrectFrameRecipeIcon(row, col+4);
            Plugin.实例.布局.set字体比例(0.25f);
            if(GUI.Button(rect3, "更新"))
            {
                物品反算.setItemProto基础(itemProto);
                物品反算.setItemValue基础(itemValue);
                物品反算.addItemProto目标(记录.itemProto);
                记录.itemValue = (int)物品反算.getItemValue目标(记录.itemProto);
            }

        }

        void drawBox(int row, int col, int length)
        {
            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col+length-1);

            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");
        }


    }



}



