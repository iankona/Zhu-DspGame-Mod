

using UnityEngine;

namespace Qtool
{
    public class FrameRecipeTree
    {

        public NodeTree itemTree;


        void updateTree()
        {
            if (!Plugin.实例.物品树需要更新)
                return;

            ItemProto itemProto = LDB.items.Select(Plugin.实例.物品ID);
            if (itemProto == null)
            {
                return;
            }
            itemTree = new NodeTree();
            itemTree.setRootItem(itemProto);

            //Debug.Log("选择物品ID::" + itemProto.Name + "::" + itemProto.ID);
            //Debug.Log(itemTree.allNodes.Count);
            //Debug.Log(itemTree.endNodes.Count);

            Plugin.实例.物品树需要更新 = false;

        }
        public void showRecipeTree()
        {

            updateTree();

            if (itemTree == null)
            {
                return;
            }

            for (int i = 0; i < itemTree.allNodes.Count; i++)
            {
                NodeIteration node = itemTree.allNodes[i];
                int row = node.depth * 8;
                int col = node.column;


                // GUI.Box(Plugin.实例.布局.newrectFrameIconXY(node.depth, node.column), node.itemProto.iconSprite.texture);
                showRecipeOuports(node, row, col);
                showRecipeInfo(node, row, col);
                showRecipeImports(node, row, col);

            }
        }

        void showRecipeOuports(NodeIteration node, int row, int col)
        {
            if (node.recipeProto == null) { return; }

            int bufindex1 = 0;
            int bufcount1 = 0;
            for (int i = 0; i < node.recipeProto.Results.Length; i++)
            {
                int resultID1 = node.recipeProto.Results[i];
                if (resultID1 == node.itemProto.ID)
                {
                    bufindex1 = i;
                    break;
                }

            }

            int resultID = node.recipeProto.Results[bufindex1];
            int resultCount = node.recipeProto.ResultCounts[bufindex1];
            float resultMinCount = node.resultMinCounts[bufindex1];
            ItemProto itemProto = LDB.items.Select(resultID);
            GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 0, col + bufcount1), new GUIContent(resultCount.ToString(), itemProto.iconSprite.texture));
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 0, col + bufcount1), itemProto.name);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 0, col + bufcount1), resultMinCount + " /min");
            bufcount1++;



            for (int i = 0; i < bufindex1; i++)
            {
                int resultID2 = node.recipeProto.Results[i];
                int resultCount2 = node.recipeProto.ResultCounts[i];
                float resultMinCount2 = node.resultMinCounts[i];
                ItemProto itemProto2 = LDB.items.Select(resultID2);
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 0, col + bufcount1), new GUIContent(resultCount2.ToString(), itemProto2.iconSprite.texture));
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 0, col + bufcount1), itemProto2.name);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 0, col + bufcount1), resultMinCount2 + " /min");
                bufcount1++;
            }

            for (int i = bufindex1+1; i < node.recipeProto.Results.Length; i++)
            {
                int resultID2 = node.recipeProto.Results[i];
                int resultCount2 = node.recipeProto.ResultCounts[i];
                float resultMinCount2 = node.resultMinCounts[i];
                ItemProto itemProto2 = LDB.items.Select(resultID2);
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 0, col + bufcount1), new GUIContent(resultCount2.ToString(), itemProto2.iconSprite.texture));
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 0, col + bufcount1), itemProto2.name);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 0, col + bufcount1), resultMinCount2 + " /min");
                bufcount1++;
            }


        }
        void showRecipeInfo(NodeIteration node, int row, int col)
        {
            if (node.recipeProto == null) { return; }
            if (GUI.Button(Plugin.实例.布局.newrectFrameRecipeIcon(row + 2, col + 0), new GUIContent((node.recipeProto.TimeSpend / 60).ToString(), node.recipeProto.iconSprite.texture)))
            {

            }
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 2, col + 0), node.recipeProto.name);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 2, col + 0), "设备数量：" + node.配方数量.ToString());

        }

        void showRecipeImports(NodeIteration node, int row, int col)
        {

            if (node.recipeProto == null) { return; }
            for (int i = 0; i < node.children.Count; i++)
            {
                int itemID = node.recipeProto.Items[i];
                int itemCount = node.recipeProto.ItemCounts[i];
                ItemProto itemProto = LDB.items.Select(itemID);

                //int itemIndex = col;
                int itemIndex = node.itemColumns[i];

                float itemMinCount = node.itemMinCounts[i];
                GUI.Box(Plugin.实例.布局.newrectFrameRecipeIcon(row + 4, itemIndex), new GUIContent(itemCount.ToString(), itemProto.iconSprite.texture));
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 4, itemIndex), itemProto.name);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 4, itemIndex), itemMinCount + " /min");
            }
        }
    }
}


