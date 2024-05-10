

using Qtool;
using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{
    public class FrameSelectTree
    {
        物品配方索引处理 物品配方索引 = null;
        ItemSelectList 物品多选 = null;
        List<NodeTree> 树枝列表 = null;
        RecipeStatistics 树枝统计 = null;

        int 配方Index = -1;
        int 多选Index = -1;

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

        void update物品多选()
        {
            if (Plugin.实例.物品多选 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
            {
                物品多选 = new ItemSelectList();
                物品多选.初始化();
                Plugin.实例.物品多选 = 物品多选;

            }
            else
            {
                物品多选 = Plugin.实例.物品多选;
            }
        }
        void update树枝统计()
        {
            if (Plugin.实例.树枝统计 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
            {
                树枝统计 = new RecipeStatistics();
                Plugin.实例.树枝统计 = 树枝统计;
            }
            else
            {
                树枝统计 = Plugin.实例.树枝统计;
            }

        }


        void update树枝列表()
        {
            if (物品配方索引 == null)
                return;

            if (树枝统计 == null)
                return;

            if (Plugin.实例.树枝列表 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
            {
                树枝列表 = new List<NodeTree>(3);
                Plugin.实例.树枝列表 = 树枝列表;
            }
            else
            {
                树枝列表 = Plugin.实例.树枝列表;
            }


            if (树枝列表.Count != 物品多选.显示列表.Count)
            {
                树枝列表.Clear(); // List<ItemBool> 显示
                foreach (ItemBool  显示 in 物品多选.显示列表) 
                {
                    NodeTree itemTree = new NodeTree();
                    itemTree.setRootItem(显示.itemProto, ref 物品配方索引);
                    itemTree.setRootResultValue(显示.itemProto, 显示.itemValue);
                    树枝列表.Add(itemTree);
                }

                树枝统计.set树枝列表(树枝列表);
            }

            if (树枝列表.Count == 物品多选.显示列表.Count)
            {
                bool 有修改 = false;
                for(int i=0; i < 树枝列表.Count; i++)
                {
                    ItemBool 显示 = 物品多选.显示列表[i];
                    NodeTree itemTree = 树枝列表[i];
                    if (itemTree.root.itemProto.ID != 显示.itemProto.ID)
                    {
                        itemTree.Clear();
                        itemTree.setRootItem(显示.itemProto, ref 物品配方索引);
                        itemTree.setRootResultValue(显示.itemProto, 显示.itemValue);
                        有修改 = true;
                    }
                    
                }
                if (有修改)
                    树枝统计.set树枝列表(树枝列表);
            }

        }



        public void showTrees()
        {
            update物品配方索引();
            update物品多选();
            update树枝统计();
            update树枝列表();

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
            drawRecipeSelectTitle(row, col-1);
            drawRecipeSelectItem(row, col);
            drawRecipeSelectRecipe(row+1, col);
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
            foreach(ItemRecipeIndex 显示 in 物品配方索引.显示列表)
            {
                GUI.Box(Plugin.实例.布局.newrectFrameLayer(row, col+i), 显示.itemProto.iconSprite.texture);
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
                if(GUI.Button(Plugin.实例.布局.newrectFrameLayer(row, col+i), 图标))
                    配方Index = i;
                GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row, col+i), "Index：" + 显示.recipeIndex);

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
            if (物品配方索引 == null || 物品多选 == null) // 不知道为什么，或有一次null出现，导致调用null出错
                return;
            int row = 0;
            int col = 2 + (物品配方索引.显示列表.Count - 1) + 2+1;
            drawItemSelectTitle(row, col-1);
            drawItemSelectItemList(row+0, col);
            drawItemSelectValueList(row+1, col);
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
            int i = 0;
            foreach (ItemBool itemSelect in 物品多选.显示列表)
            {
                GUI.Button(Plugin.实例.布局.newrectFrameLayer(row, col + i), itemSelect.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row, col + i), itemSelect.itemProto.Name);
                i++;
            }
        }

        void drawItemSelectValueList(int row, int col)
        {
            int i = 0;
            foreach (ItemBool itemSelect in 物品多选.显示列表)
            {
                Rect rect1 = Plugin.实例.布局.newrectFrameLayer(row, col + i);
                //Plugin.实例.布局.set字体比例(1);
                string valueText = GUI.TextField(rect1, itemSelect.itemValue.ToString());
                int valueInt = 0;
                int.TryParse(valueText, out valueInt);
                if (valueInt <= 0)
                    itemSelect.itemValue = 60;
                else
                    itemSelect.itemValue = valueInt;
                i++;
            }
        }

        void drawItemSelectButtonList(int row, int col)
        {
            int i = 0;
            foreach (ItemBool itemSelect in 物品多选.显示列表)
            {
                Rect rect1 = Plugin.实例.布局.newrectFrameLayer(row, col+i);
                // Plugin.实例.布局.set字体比例(0.5f);
                if (GUI.Button(rect1, "显示\n"+ itemSelect.itemProto.Name))
                {
                    多选Index = i;
                    ItemBool 显示 = 物品多选.显示列表[i];
                    NodeTree itemTree = 树枝列表[i];
                    itemTree.setRootResultValue(显示.itemProto, 显示.itemValue);
                    树枝统计.set树枝列表(树枝列表);
                }
                i++;
            }
        }





        void drawRecipeTree()
        {
            if (多选Index < 0)
                return;

            //Debug.Log("drawRecipeTree::" + 树枝列表.Count + "::" + 多选Index);
            NodeTree itemTree = 树枝列表[多选Index];
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

            for (int i = bufindex1+1; i < calc.recipeProto.Results.Length; i++)
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

                GUI.Box(  Plugin.实例.布局.newrectFrameRecipeIcon(row + 4, node.itemColumns[i]), new GUIContent(itemCount.ToString(), itemProto.iconSprite.texture));
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

            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row+4, columnIndex);
            float width = finalRect.x - startRect.x + finalRect.width; // + Plugin.实例.布局.padx * 5;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");

        }





    }





}







