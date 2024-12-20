

using Qtool;
using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{


    public class FrameItemInverte
    {


        ItemProto itemProto = null;
        int itemValue = 60;

        UpTree upTree = null;
        int selectItemID = 0;








        void updateUpTree()
        {
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
            updateUpTree();
            draw基础矿物();
            drawInverteSelect();
            drawUpTreeEndItems();
            drawInverteTree();
        }


        void draw基础矿物() 
        {
            int i = 0;
            int row = 0;
            foreach(ItemProto itemProto in LDB.items.dataArray)
            {
                if (itemProto.recipes.Count > 0)
                    continue;
                int col = i;
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIconX(row, col), itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeNameX(row, col), itemProto.Name);
                i++;
            }
        }


        void drawInverteSelect()
        {
            if (upTree == null)
                return;

            UpIteration node = upTree.Select(selectItemID);
            if (node == null)
                return;
            int row = 1;
            int col = 0;
            drawInverteSelectBox(row, col, node.childNodes.Count);

            GUI.Box(Plugin.实例.布局.newrectFrameRecipeIconX(row, col), node.itemProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeNameX(row, col), node.itemProto.Name);




            int i = 0;
            foreach (UpIteration child in node.childNodes)
            {
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIconX(row + 2, i), child.itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeNameX(row + 2, i), child.itemProto.Name);
                i++;
            }
        }

        void drawInverteSelectBox(int row, int col, int length)
        {
            if (length<=1)
                return;

            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIconX(row, col);
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIconX(row + 2, col + length - 1);
            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");

        }




        void drawUpTreeEndItems()
        {
            if (upTree == null)
                return;

            int i= 0;
            int row = 4;
            foreach(ItemProto itemProto in upTree.endItemProtoNoRepeat) 
            {
                GUI.Box(Plugin.实例.布局.newrectFrameLayerColumn(row, i), itemProto.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameLayerColumnName(row, i), itemProto.Name);
                i++;
            }
        }


        void drawInverteTree()
        {

            if (upTree == null)
                return;

            int row = 0;
            int col = 0;
            foreach (UpIteration node in upTree.allNodes)
            {
                row = node.depth * 4 + 10; // 3+1
                col = node.column;
                drawInverteBox(row, col, node);
                drawInverteUpNode(row, col, node);
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

            int column2 = maxcolumn;// 3 维持整体偏移一致
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row + 2, column2);
            float width = finalRect.x - startRect.x + finalRect.width; 
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");

        }


        void drawInverteUpNode(int row, int col, UpIteration node) 
        {
            if (GUI.Button(Plugin.实例.布局.newrectFrameRecipeIcon(row + 0, col), node.itemProto.iconSprite.texture))
                selectItemID = node.itemProto.ID;
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row+0, col), node.itemProto.Name);

            foreach(UpIteration child in node.childNodes)
            {
                int column1 = child.column;// 3 维持整体偏移一致
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

