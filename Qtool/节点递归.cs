

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qtool
{
    public class NodeRecipeIndex
    {
        public int itemID = 0;
        public int recipeIndex = 0;
    }

    public class ListRecipeIndex
    {
        public List<NodeRecipeIndex> listNodes = new List<NodeRecipeIndex>(1024);
        public ListRecipeIndex()
        {
            // Debug.Log("物品的配方索引");
            for (int i = 0; i < LDB.items.Length; i++)
            {
                NodeRecipeIndex nodeRecipeIndex = new NodeRecipeIndex();
                nodeRecipeIndex.itemID = LDB.items.dataArray[i].ID; // 必须是dataArray
                nodeRecipeIndex.recipeIndex = 0;
                listNodes.Add(nodeRecipeIndex);
                // Debug.Log(i+ "::" + LDB.items[i].Name + "::" + LDB.items[i].ID + "::" + LDB.items.dataArray[i].recipes.Count);
            }
            // Debug.Log("==============");

        }

        public NodeRecipeIndex Select(int ID)
        {
            for (int i = 0; i < listNodes.Count; i++)
            {
                if (listNodes[i].itemID == ID)
                    return listNodes[i];
            }
            return (NodeRecipeIndex)null;
        }

        public int getRecipeIndex(int ID)
        {
            for (int i = 0; i < listNodes.Count; i++)
            {
                if (listNodes[i].itemID == ID)
                    return listNodes[i].recipeIndex;
            }
            return 0;
        }
    }

    public class NodeTree
    {
        public ListRecipeIndex listRecipeIndexs = new ListRecipeIndex();
        public NodeIteration root;
        public List<NodeIteration> allNodes = new List<NodeIteration>(2048);
        public List<NodeIteration> endNodes = new List<NodeIteration>(1024);


        public void setRootItem(ItemProto itemProto)
        {
            root = NodeIteration.setRootItem(itemProto, ref listRecipeIndexs);
            if (root == null)
            {
                return;
            }
            getAllNodes();
            getEndNodes();
            setEndNodesColumn();
            NodeIteration.setRootValue(root, 61, ref listRecipeIndexs);

        }

        public void updateTree()
        {

            if (root == null)
            {
                return;
            }
            root = NodeIteration.setRootItem(root.itemProto, ref listRecipeIndexs);
            getAllNodes();
            getEndNodes();
            setEndNodesColumn();
        }



        void getAllNodes()
        {
            if (root == null)
                return;
            allNodes.Clear();
            allNodes.Add(root);
            root.children递归深度(ref allNodes);

        }

        void getEndNodes()
        {
            endNodes.Clear();
            for (int i = 0; i < allNodes.Count; i++)
            {
                NodeIteration node = allNodes[i];
                if (node.children.Count <= 0)
                {
                    endNodes.Add(node);
                }
            }

        }

        void setEndNodesColumn()
        {
            for (int i = 0; i < endNodes.Count; i++)
            // for (int i = endNodes.Count - 1; i>=0 ; i--)
            {
                NodeIteration end = endNodes[i];
                NodeIteration.setEndItemColumn(end, i);
            }

        }


    }
    public class NodeIteration
    {
        public NodeIteration parent;
        public int depth = -1;
        public int column = -1;
        public ItemProto itemProto;
        public List<NodeIteration> children = new List<NodeIteration>(8);

        public float itemValue;
        public List<float> resultMinCounts = new List<float>(8);
        public RecipeProto recipeProto;
        public float 单个配方分钟产量 = 0;
        public float 配方数量 = 0;
        public int 设备ID;
        public List<int> itemColumns = new List<int>(8);
        public List<float> itemMinCounts = new List<float>(8);


        public static void setEndItemColumn(NodeIteration endNodeIteration, int columnindex)
        {
            if (endNodeIteration == null)
                return;
            endNodeIteration.children逆归树枝(columnindex);
        }

        public static NodeIteration setRootItem(ItemProto itemProto, ref ListRecipeIndex listRecipeIndexs)
        {
            if (itemProto == null)
            {
                return (NodeIteration)null;
            }
            int recipeIndex = listRecipeIndexs.getRecipeIndex(itemProto.ID);
            // Debug.Log("新建树::"+ itemProto.Name);
            NodeIteration rootNodeIteration = new NodeIteration();
            // rootNodeIteration.uuid = Guid.NewGuid();
            rootNodeIteration.depth = 0;
            rootNodeIteration.itemProto = itemProto;
            rootNodeIteration.children递归更新(ref listRecipeIndexs);
            return rootNodeIteration;
        }

        public void children递归更新(ref ListRecipeIndex listRecipeIndexs)
        {
            if (itemProto.recipes.Count <= 0)
                return;

            int recipeIndex = listRecipeIndexs.getRecipeIndex(itemProto.ID);
            RecipeProto recipeProto = itemProto.recipes[recipeIndex];

            children.Clear();
            for (int i = 0; i < recipeProto.Items.Length; i++) // Result会死循环
            {
                int itemID = recipeProto.Items[i];
                NodeIteration childNodeIteration = new NodeIteration();
                childNodeIteration.parent = this;
                childNodeIteration.depth = depth + 1;
                childNodeIteration.itemProto = LDB.items.Select(itemID);
                children.Add(childNodeIteration);
            }

            for (int i = 0; i < children.Count; i++)
            {
                // Debug.Log(children[i].depth+"::"+children[i].itemProto.Name);
                children[i].children递归更新(ref listRecipeIndexs);
            }

        }
        public void children递归深度(ref List<NodeIteration> allNodes)
        {
            for (int i = 0; i < children.Count; i++)
            {
                NodeIteration childNodeIteration = children[i];
                allNodes.Add(childNodeIteration);
                childNodeIteration.children递归深度(ref allNodes);
            }
        }

        public void children递归广度(ref List<NodeIteration> allNodes)
        {
            for (int i = 0; i < children.Count; i++)
            {
                NodeIteration childNodeIteration = children[i];
                allNodes.Add(childNodeIteration);
            }

            for (int i = 0; i < children.Count; i++)
            {
                NodeIteration childNodeIteration = children[i];
                childNodeIteration.children递归深度(ref allNodes);
            }
        }


        public void children逆归树枝(int columnindex)
        {
            if (column >= 0)
                return;
            column = columnindex;
            if (parent == null)
                return;
            parent.children逆归树枝(columnindex);
        }





        public static void setRootValue(NodeIteration root, float itemValue, ref ListRecipeIndex listRecipeIndexs)
        {

            if (root == null || itemValue < 0)
            {
                return;
            }
            root.itemValue = itemValue;
            root.calculate递归更新(ref listRecipeIndexs);

        }

        public void calculate递归更新(ref ListRecipeIndex listRecipeIndexs)
        {

            int recipeIndex = listRecipeIndexs.getRecipeIndex(this.itemProto.ID);
            // Debug.Log("新建树::"+ itemProto.Name);





            if (this.itemProto.recipes.Count <= 0)
            {
                recipeProto = null;
            }
            else
            {
                recipeProto = this.itemProto.recipes[recipeIndex];
                float 每分钟执行次数 = 3600 / recipeProto.TimeSpend; // (60 / (TimeSpend / 60)

                int index1 = 0;
                for (int i = 0; i < recipeProto.Results.Length; i++)
                {
                    int itemID = recipeProto.Results[i];
                    if (itemID == this.itemProto.ID) 
                    {
                        index1 = i; 
                        break;
                    }
                }
                单个配方分钟产量 = recipeProto.ResultCounts[index1] * 每分钟执行次数; 
                配方数量 = itemValue / 单个配方分钟产量;

                this.resultMinCounts.Clear();
                for (int i = 0; i < recipeProto.ResultCounts.Length; i++)
                {
                    float minValue = recipeProto.ResultCounts[i] * 每分钟执行次数 * 配方数量;
                    this.resultMinCounts.Add(minValue);
                }
                this.itemMinCounts.Clear();
                for (int i = 0; i < recipeProto.ItemCounts.Length; i++)
                {
                    float minValue = recipeProto.ItemCounts[i] * 每分钟执行次数 * 配方数量;
                    this.itemMinCounts.Add(minValue);
                    this.children[i].itemValue = minValue;
                }

                // Debug.Log("节点::" + recipeProto.Name + "::" + itemValue);
                this.itemColumns.Clear();
                for (int i = 0; i < recipeProto.ItemCounts.Length; i++)
                {
                    this.itemColumns.Add(this.children[i].column);
                    this.children[i].calculate递归更新(ref listRecipeIndexs);
                }


            }

        }






    }


}



