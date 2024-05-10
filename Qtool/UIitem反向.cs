

using Qtool;
using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{


    public class FrameItemInverte
    {
        物品配方索引处理 物品配方索引 = null;
        int 配方Index = -1;

        ItemProto itemProto = null;
        int itemValue = 60;

        UpTree upTree = null;
        int selectItemID = 0;





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


        //void updateItem汇总()
        //{
        //    if (Plugin.实例.item汇总 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
        //    {
        //        item汇总 = new RecipeInfoList();
        //        Plugin.实例.item汇总 = item汇总;
        //    }
        //    else
        //    {
        //        item汇总 = Plugin.实例.item汇总;
        //    }

        //}


        void updateupTree()
        {
            if (物品配方索引 == null)
                return;

            if (Plugin.实例.upTree == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
            {
                upTree = new UpTree();
                Plugin.实例.upTree = upTree;
            }
            else
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
                    upTree.Clear(); // List<ItemBool> 显示
                    upTree.setRootItem(itemProto);
                    selectItemID = itemProto.ID;
                    //foreach (UpIteration up in upTree.allNodes)
                    //{
                    //    Debug.Log(up.itemProto.Name);
                    //}
                }

            }


            if (itemProto.ID != Plugin.实例.物品ID) // 处理2~N次点击
            {
                itemProto = LDB.items.Select(Plugin.实例.物品ID);
                upTree.Clear(); // List<ItemBool> 显示
                upTree.setRootItem(itemProto);
                selectItemID = itemProto.ID;
            }


        }



        public void showTree()
        {
            update物品配方索引();
            updateupTree();
            drawInverteTree();
        }






        void drawInverteTree()
        {

            if (upTree == null)
                return;


            drawInverteSelect();



            int row = 0;
            int col = 0;
            foreach(UpIteration node in upTree.allNodes)
            {
                row = node.depth * 4 + 8; // 3+1
                col = node.column + 3;
                drawInverteBox(row, col, node);
                drawInverteUpNode(row, col, node);
            }


        }

        void drawInverteSelectBox(int row, int col, int length)
        {
            if (length<=1)
                return;

            Rect startRect = Plugin.实例.布局.newrectFrameLayer(row, col);
            Rect finalRect = Plugin.实例.布局.newrectFrameLayer(row + 2, col + length - 1);
            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");

        }


        void drawInverteSelect() 
        {
            UpIteration node = upTree.Select(selectItemID);
            if (node == null)
                return;
            int row = 0;
            int col = 0;
            drawInverteSelectBox(row, col, node.childNodes.Count);

            GUI.Box(Plugin.实例.布局.newrectFrameLayer(row, col), node.itemProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameLayerName(row, col), node.itemProto.Name);
            int i = 0;
            foreach (UpIteration child in node.childNodes)
            {
                GUI.Box(Plugin.实例.布局.newrectFrameLayerColumn(row + 1, i), child.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameLayerColumnName(row + 1, i), child.itemProto.Name);
                i++;
            }
        }

        void drawInverteBox(int row, int col, UpIteration node)
        {
            if (node.itemProto == null) 
                return; 

            if (node.childNodes.Count <1) // 防止子集为零时，依然画box导致区域被涂黑的错误。
                return;

            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);
            int maxcolumn = 0;
            foreach (int column in node.itemColumns)
            {
                if (column > maxcolumn)
                    maxcolumn = column;
            }

            int column2 = maxcolumn + 3;// 3 维持整体偏移一致
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row + 2, column2);
            float width = finalRect.x - startRect.x + finalRect.width; 
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");

        }


        void drawInverteUpNode(int row, int col, UpIteration node) 
        {
            GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row+0, col), node.itemProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row+0, col), node.itemProto.Name);

            foreach(UpIteration child in node.childNodes)
            {
                int column1 = child.column + 3;// 3 维持整体偏移一致
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row+2, column1), child.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row+2, column1), child.itemProto.Name);
            }
        }





    }



}





//void drawInverteTreeSimple()
//{

//    if (upTree == null)
//        return;

//    int row = 0;
//    int col = 0;
//    for (int i = 0; i < upTree.allNodes.Count; i++)
//    {
//        UpIteration node = upTree.allNodes[i];
//        row = node.depth + 8;
//        col = node.column;
//        GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row, col), node.itemProto.iconSprite.texture);
//        GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row, col), node.itemProto.Name);
//    }

//}

