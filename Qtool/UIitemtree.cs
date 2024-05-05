

using Qtool;
using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{
    public class FrameItemTree
    {

        public NodeTree itemTree = null;
        public List<bool> 按钮bool列表 = new List<bool>(32);
        public int 按钮index = 0;

        public void updateTree()
        {
            if (!Plugin.实例.物品树需要更新)
                return;

            ItemProto itemProto = LDB.items.Select(Plugin.实例.物品ID);
            if (itemProto == null)
            {
                return;
            }
            //itemTree.aaaClear();
            if (itemTree == null)
            { 
                itemTree = new NodeTree(); 
            }
            else
            { 
                itemTree.Clear(); 
            }



            itemTree.setRootItem(itemProto);

            //Debug.Log("选择物品ID::" + itemProto.Name + "::" + itemProto.ID);
            //Debug.Log(itemTree.allNodes.Count);
            //Debug.Log(itemTree.endNodes.Count);

            按钮bool列表.Clear();
            foreach (ItemRecipeIndex 显示 in itemTree.物品配方索引.显示列表)
                按钮bool列表.Add(false);


            Plugin.实例.物品树需要更新 = false;

        }

        public void showRecipeSelect()
        {
            if (itemTree == null)
                return;

            // Fix Error
            if (按钮bool列表.Count < itemTree.物品配方索引.显示列表.Count)
            {
                int num1 = 按钮bool列表.Count;
                int num2 = itemTree.物品配方索引.显示列表.Count;
                int num3 = num2 - num1;
                for (int i = 0; i < num3; i++) 
                {
                    按钮bool列表.Add(false);
                }
            }

            for (int i=0; i < itemTree.物品配方索引.显示列表.Count; i++)
            {
                ItemRecipeIndex 显示 = itemTree.物品配方索引.显示列表[i];
                按钮index = i;
                drawRecipeSelect(显示,  0, i+2);
                 
            }

        }


        void drawRecipeSelect(ItemRecipeIndex 显示, int row, int col)
        {

            GUI.Button(Plugin.实例.布局.newrectFrameLayer(row+0, col), 显示.itemProto.iconSprite.texture);


            //if (!按钮bool列表[按钮index])
            //{ }

            Texture 图标 = null;
            if (显示.recipeIndex < 0) // 处理氢索引为负数的情况
            {
                图标 = 显示.itemProto.iconSprite.texture;
            }
            else
            {
                图标 = 显示.recipes[显示.recipeIndex].iconSprite.texture;
            }
            if (GUI.Button(Plugin.实例.布局.newrectFrameLayer(row + 1, col), 图标))
            {
                // 只运行一次
                for (int i = 0; i < 按钮bool列表.Count; i++)
                    按钮bool列表[i] = false;
                按钮bool列表[按钮index] = true;
            }
            GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row + 1, col), "Index：" + 显示.recipeIndex);



            if (!按钮bool列表[按钮index])
                return;


            int j = 0;
            for (j = 0; j < 显示.recipes.Count; j++)
            {
                RecipeProto recipeProto = 显示.recipes[j];
                drawRecipeColumn(recipeProto, row+2+j, col, 显示, j);
            }

            if (显示.itemProto.ID == 1120) // 氢
            {
                if (GUI.Button(Plugin.实例.布局.newrectFrameLayer(row + 2 + j, col), 显示.itemProto.iconSprite.texture))
                {
                    按钮bool列表[按钮index] = false;
                    itemTree.物品配方索引.setRecipeIndex(显示.itemProto.ID, -1);
                }
            }

        }




        void drawRecipeColumn(RecipeProto recipeProto, int row, int col, ItemRecipeIndex 显示, int index)
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
                按钮bool列表[按钮index] = false;
                itemTree.物品配方索引.setRecipeIndex(显示.itemProto.ID, index);
                if (recipeProto.ID == 121) // 选择配方::重整精炼::121
                    itemTree.物品配方索引.setRecipeIndex(显示.itemProto.ID, 0); // 防止物品树无限递归
            }
            GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row, col), recipeProto.Name+"："+(recipeProto.TimeSpend / 60)+"S");


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













        public void showRecipeTree()
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
            float resultValue = calc.resultValues[bufindex1];

            ItemProto itemProto = LDB.items.Select(resultID);

            GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 0, col + bufcount1), new GUIContent(resultCount.ToString(), itemProto.iconSprite.texture));
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 0, col + bufcount1), itemProto.name);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 0, col + bufcount1), resultValue + " /min");
            bufcount1++;



            for (int i = 0; i < bufindex1; i++)
            {
                int resultID2 = calc.recipeProto.Results[i];
                int resultCount2 = calc.recipeProto.ResultCounts[i];
                float resultValue2 = calc.resultValues[i];

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
                float resultValue2 = calc.resultValues[i];

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
                float itemValue = calc.itemValues[i];

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







