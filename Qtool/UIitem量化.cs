

using Qtool;
using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{


    public class FrameItemTree
    {
        物品配方索引处理 物品配方索引 = null;
        int 配方Index = -1;

        ItemProto itemProto = null;
        int itemValue = 60;

        NodeTree itemTree = null;
        RecipeInfoList item汇总 = null;




        void update物品配方索引()
        {
            if (Plugin.实例.物品配方索引 == null)
            {
                物品配方索引处理 物品配方索引 = new 物品配方索引处理();
                物品配方索引.初始化();
                Plugin.实例.物品配方索引 = 物品配方索引;

            }
            else
            {
                物品配方索引 = Plugin.实例.物品配方索引;
            }
        }

        
        void updateItem汇总()
        {
            if (Plugin.实例.item汇总 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
            {
                item汇总 = new RecipeInfoList();
                Plugin.实例.item汇总 = item汇总;
            }
            else
            {
                item汇总 = Plugin.实例.item汇总;
            }

        }


        void updateItemTree()
        {
            if (物品配方索引 == null)
                return;

            if (item汇总 == null)
                return;

            if (Plugin.实例.itemTree == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
            {
                itemTree = new NodeTree();
                Plugin.实例.itemTree = itemTree;
            }
            else
            {
                itemTree = Plugin.实例.itemTree;
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
                    itemTree.Clear(); // List<ItemBool> 显示
                    itemTree.setRootItem(itemProto, ref 物品配方索引);
                    itemTree.setRootResultValue(itemProto, itemValue);
                    item汇总.set树枝(itemTree);
                    item汇总.setRowStep();
                    item汇总.new显示列表();
                    item汇总.update列宽度();
                }
                    
            }


            if (itemProto.ID != Plugin.实例.物品ID) // 处理2~N次点击
            {
                itemProto = LDB.items.Select(Plugin.实例.物品ID);
                itemTree.Clear(); // List<ItemBool> 显示
                itemTree.setRootItem(itemProto, ref 物品配方索引);
                itemTree.setRootResultValue(itemProto, itemValue);
                item汇总.set树枝(itemTree);
                item汇总.setRowStep();
                item汇总.new显示列表();
                item汇总.update列宽度();
            }


        }



        public void showTree()
        {
            update物品配方索引();
            updateItem汇总();
            updateItemTree();

            drawRecipeSelect();
            drawItemSelect();
            drawRecipeTree();
        }


        void drawRecipeSelect()
        {
            if (物品配方索引 == null)
                return;

            int row = 0;
            int col = 2;
            drawRecipeSelectTitle(row, col - 1);
            drawRecipeSelectItem(row, col);
            drawRecipeSelectRecipe(row + 1, col);
            if (配方Index < 0)
                return;

            ItemRecipeIndex 显示 = 物品配方索引.显示列表[配方Index];

            int i = 0;
            foreach (RecipeProto recipeProto in 显示.recipes)
            {
                drawRecipeSelectRecipemColumn(row + 2 + i, col + 配方Index, recipeProto, 显示, i);
                i++;
            }

            if (显示.itemProto.ID == 1120) // 氢
            {
                if (GUI.Button(Plugin.实例.布局.newrectFrameLayer(row + 2 + i, col + 配方Index), 显示.itemProto.iconSprite.texture))
                {
                    配方Index = -1;
                    物品配方索引.setRecipeIndex(显示.itemProto.ID, -1);
                }
            }


        }

        void drawRecipeSelectTitle(int row, int col)
        {

            Rect rect1 = Plugin.实例.布局.newrectFrameLayer(row + 0, col);
            Plugin.实例.布局.set字体比例(2);
            GUI.Box(rect1, "物品");

            Rect rect2 = Plugin.实例.布局.newrectFrameLayer(row + 1, col);
            Plugin.实例.布局.set字体比例(2);
            GUI.Box(rect2, "配方");
        }

        void drawRecipeSelectItem(int row, int col)
        {
            int i = 0;
            foreach (ItemRecipeIndex 显示 in 物品配方索引.显示列表)
            {
                GUI.Box(Plugin.实例.布局.newrectFrameLayer(row, col + i), 显示.itemProto.iconSprite.texture);
                i++;
            }
        }

        void drawRecipeSelectRecipe(int row, int col)
        {
            int i = 0;
            foreach (ItemRecipeIndex 显示 in 物品配方索引.显示列表)
            {
                Texture 图标 = null;
                if (显示.recipeIndex < 0) // 处理氢配方索引为负数的情况
                {
                    图标 = 显示.itemProto.iconSprite.texture;
                }
                else
                {
                    图标 = 显示.recipes[显示.recipeIndex].iconSprite.texture;
                }
                if (GUI.Button(Plugin.实例.布局.newrectFrameLayer(row, col + i), 图标))
                    配方Index = i;
                GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row, col + i), "Index：" + 显示.recipeIndex);

                i++;
            }
        }

        void drawRecipeSelectRecipemColumn(int row, int col, RecipeProto recipeProto, ItemRecipeIndex 显示, int index)
        {
            for (int k = 0; k < recipeProto.Results.Length; k++)
            {
                int itemID1 = recipeProto.Results[k];
                int resultCount1 = recipeProto.ResultCounts[k];

                ItemProto itemProto1 = LDB.items.Select(itemID1);
                int column1 = col - recipeProto.Results.Length + k;

                GUI.Box(Plugin.实例.布局.newrectFrameLayer(row, column1), itemProto1.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row, column1), itemProto1.Name + "：" + resultCount1);
            }

            if (GUI.Button(Plugin.实例.布局.newrectFrameLayer(row, col), recipeProto.iconSprite.texture))
            {
                配方Index = -1;
                物品配方索引.setRecipeIndex(显示.itemProto.ID, index);
                if (recipeProto.ID == 121) // 选择配方::重整精炼::121
                    物品配方索引.setRecipeIndex(显示.itemProto.ID, 0); // 防止物品树无限递归
            }
            GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row, col), recipeProto.Name + "：" + (recipeProto.TimeSpend / 60) + "S");


            for (int m = 0; m < recipeProto.Items.Length; m++)
            {

                int itemID2 = recipeProto.Items[m];
                int itemCount2 = recipeProto.ItemCounts[m];

                ItemProto itemProto2 = LDB.items.Select(itemID2);
                int column2 = col + 1 + m;

                GUI.Box(Plugin.实例.布局.newrectFrameLayer(row, column2), itemProto2.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row, column2), itemProto2.Name + "：" + itemCount2);
            }


        }



        void drawItemSelect()
        {
            if (物品配方索引 == null || itemProto == null) // 不知道为什么，或有一次null出现，导致调用null出错
                return;
            int row = 0;
            int col = 2 + (物品配方索引.显示列表.Count - 1) + 2 + 1;
            drawItemSelectTitle(row, col - 1);
            drawItemSelectItemList(row + 0, col);
            drawItemSelectValueList(row + 1, col);
            drawItemSelectButtonList(row + 2, col);
        }



        void drawItemSelectTitle(int row, int col)
        {

            Rect rect1 = Plugin.实例.布局.newrectFrameLayer(row + 0, col);
            Plugin.实例.布局.set字体比例(2);
            GUI.Box(rect1, "物品");

            Rect rect2 = Plugin.实例.布局.newrectFrameLayer(row + 1, col);
            Plugin.实例.布局.set字体比例(2);
            GUI.Box(rect2, "产量");

            Rect rect3 = Plugin.实例.布局.newrectFrameLayer(row + 2, col);
            Plugin.实例.布局.set字体比例(2);
            GUI.Box(rect3, "按钮");

        }

        void drawItemSelectItemList(int row, int col)
        {

            GUI.Button(Plugin.实例.布局.newrectFrameLayer(row, col), itemProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row, col), itemProto.Name);

        }

        void drawItemSelectValueList(int row, int col)
        {

            Rect rect1 = Plugin.实例.布局.newrectFrameLayer(row, col);
            //Plugin.实例.布局.set字体比例(1);
            string valueText = GUI.TextField(rect1, itemValue.ToString());
            int valueInt = 0;
            int.TryParse(valueText, out valueInt);
            if (valueInt <= 0)
                itemValue = 60;
            else
                itemValue = valueInt;

        }

        void drawItemSelectButtonList(int row, int col)
        {

            Rect rect1 = Plugin.实例.布局.newrectFrameLayer(row, col);
            // Plugin.实例.布局.set字体比例(0.5f);
            if (GUI.Button(rect1, "更新\n" + itemProto.Name))
            {
                itemTree.setRootResultValue(itemProto, itemValue);
                item汇总.set树枝(itemTree);
            }

        }





        void drawRecipeTree()
        {

            if (itemTree == null)
                return;

            for (int i = 0; i < itemTree.allNodes.Count; i++)
            {
                NodeIteration node = itemTree.allNodes[i];
                NodeCalculate calc = node.calc;
                int row = node.depth * 8 + 8;
                int col = node.column;

                drawRecipeBox(node, row, col);
                drawRecipeOuports(calc, row, col);
                drawRecipeInfo(calc, row, col);
                drawRecipeImports(node, row, col);


            }
        }

        void drawRecipeOuports(NodeCalculate calc, int row, int col)
        {
            if (calc.recipeProto == null) { return; }

            int bufindex1 = 0;
            int bufcount1 = 0;
            for (int i = 0; i < calc.recipeProto.Results.Length; i++)
            {
                int resultID1 = calc.recipeProto.Results[i];
                if (resultID1 == calc.itemProto.ID)
                {
                    bufindex1 = i;
                    break;
                }

            }

            int resultID = calc.recipeProto.Results[bufindex1];
            int resultCount = calc.recipeProto.ResultCounts[bufindex1];
            float resultValue = calc.ResultValues[bufindex1];

            ItemProto itemProto = LDB.items.Select(resultID);

            GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 0, col + bufcount1), new GUIContent(resultCount.ToString(), itemProto.iconSprite.texture));
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 0, col + bufcount1), itemProto.name);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 0, col + bufcount1), resultValue + " /min");
            bufcount1++;



            for (int i = 0; i < bufindex1; i++)
            {
                int resultID2 = calc.recipeProto.Results[i];
                int resultCount2 = calc.recipeProto.ResultCounts[i];
                float resultValue2 = calc.ResultValues[i];

                ItemProto itemProto2 = LDB.items.Select(resultID2);

                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 0, col + bufcount1), new GUIContent(resultCount2.ToString(), itemProto2.iconSprite.texture));
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 0, col + bufcount1), itemProto2.name);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 0, col + bufcount1), resultValue2 + " /min");
                bufcount1++;
            }

            for (int i = bufindex1 + 1; i < calc.recipeProto.Results.Length; i++)
            {
                int resultID2 = calc.recipeProto.Results[i];
                int resultCount2 = calc.recipeProto.ResultCounts[i];
                float resultValue2 = calc.ResultValues[i];

                ItemProto itemProto2 = LDB.items.Select(resultID2);

                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 0, col + bufcount1), new GUIContent(resultCount2.ToString(), itemProto2.iconSprite.texture));
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 0, col + bufcount1), itemProto2.name);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 0, col + bufcount1), resultValue2 + " /min");
                bufcount1++;
            }


        }

        void drawRecipeInfo(NodeCalculate calc, int row, int col)
        {
            if (calc.recipeProto == null) { return; }
            if (GUI.Button(Plugin.实例.布局.newrectFrameRecipeIcon(row + 2, col + 0), new GUIContent((calc.recipeProto.TimeSpend / 60).ToString(), calc.recipeProto.iconSprite.texture)))
            {

            }
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 2, col + 0), calc.recipeProto.name);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 2, col + 0), calc.配方数量.ToString());

        }

        void drawRecipeImports(NodeIteration node, int row, int col)
        {
            NodeCalculate calc = node.calc;
            if (calc.recipeProto == null) { return; }
            for (int i = 0; i < calc.recipeProto.Items.Length; i++)
            {
                // Debug.Log(node.itemProto.name + "::itrms::" + i);
                int itemID = calc.recipeProto.Items[i];
                int itemCount = calc.recipeProto.ItemCounts[i];
                float itemValue = calc.ItemValues[i];

                ItemProto itemProto = LDB.items.Select(itemID);

                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 4, node.itemColumns[i]), new GUIContent(itemCount.ToString(), itemProto.iconSprite.texture));
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 4, node.itemColumns[i]), itemProto.name);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 4, node.itemColumns[i]), itemValue + " /min");
            }
        }

        void drawRecipeBox(NodeIteration node, int row, int col)
        {
            if (node.recipeProto == null) { return; }
            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);

            int columnIndex = 0;

            int maxResultColumnIndex = node.recipeProto.Results.Length - 1 + col;

            int maxItemColumnIndex = 0;
            foreach (int index1 in node.itemColumns)
            {
                if (index1 > maxItemColumnIndex)
                    maxItemColumnIndex = index1;
            }

            if (maxResultColumnIndex > columnIndex)
                columnIndex = maxResultColumnIndex;
            if (maxItemColumnIndex > columnIndex)
                columnIndex = maxItemColumnIndex;

            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row + 4, columnIndex);
            float width = finalRect.x - startRect.x + finalRect.width; // + Plugin.实例.布局.padx * 5;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");

        }





    }






}







