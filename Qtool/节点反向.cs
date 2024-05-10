

using Qtool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static Qtool.NodeTree;

namespace Qtool
{

    public class UpIteration
    {
        public UpIteration parent = null;
        public int depth = -1;
        public int column = -1;
        public ItemProto itemProto = null;
        public List<RecipeProto> recipes = new List<RecipeProto>(16);
        public List<ItemProto> childItems = new List<ItemProto>(16);
        public List<int> itemColumns = new List<int>(16);
        public List<UpIteration> childNodes = new List<UpIteration>(16);


        public static UpIteration setRootItem(ItemProto itemProto1)
        {
            if (itemProto1 == null)
                return null;

            UpIteration root = new UpIteration();
            root.parent = null;
            root.depth = 0;
            root.itemProto = itemProto1;
            root.getAllRecipes();
            root.getChildItems();
            root.childNodes递归更新();
            return root;
        }

        public static void setEndItemColumn(UpIteration end, int columnindex)
        {
            if (end == null)
                return;
            end.childNodes逆归树枝(columnindex);
        }

        public void childNodes逆归树枝(int columnindex)
        {
            if (column >= 0)
                return;
            column = columnindex;
            if (parent == null)
                return;
            parent.childNodes逆归树枝(columnindex);
        }



        public void childNodes递归更新()
        {
            childNodes.Clear();
            foreach (ItemProto itemProto2 in childItems)
            {
                UpIteration child = new UpIteration();
                child.parent = this;
                child.depth = depth+1;
                child.itemProto = itemProto2;
                child.getAllRecipes();
                child.getChildItems();
                childNodes.Add(child);
            }
            foreach (UpIteration child in childNodes) 

            {
                child.childNodes递归更新();
            }

        }

        public void getAllRecipes() 
        {
            recipes.Clear();
            foreach(RecipeProto recipeProto in LDB.recipes.dataArray)
            {
                if (recipeProto.ID == 121) // 选择配方::重整精炼::121, 防止无线递归
                    continue;
                bool 在配方里 = inRecipe(itemProto.ID, recipeProto);
                if (在配方里)
                    recipes.Add(recipeProto);
            }
        }


        public void getChildItems()
        {
            List<int> result = new List<int>(16);
            foreach (RecipeProto recipeProto in recipes)
            {
                foreach(int resultID in recipeProto.Results)
                {
                    bool 在列表里 = inResult(resultID, result);
                    if (在列表里)
                        continue;
                    result.Add(resultID);
                }
            }

            childItems.Clear();
            foreach (int itemID1 in result)
            {
                ItemProto itemProto1 = LDB.items.Select(itemID1);
                childItems.Add(itemProto1);
            }
        }

        public bool inRecipe(int itemID1, RecipeProto recipeProto)
        {
            bool 在配方里 = false;
            foreach (int itemID2 in recipeProto.Items) 
            { 
                if (itemID1 == itemID2) 
                { 
                    在配方里 = true; 
                }
            }
            return 在配方里;
        }
        
        public bool inResult(int itemID, List<int> 列表)
        {
            bool 在列表里 = false;
            foreach (int ID in 列表)
            {
                if (itemID == ID)
                {
                    在列表里 = true;
                }
            }
            return 在列表里;
        }




        public void childNodes递归深度(ref List<UpIteration> allNodes)
        {
            for (int i = 0; i < childNodes.Count; i++)
            {
                allNodes.Add(childNodes[i]);
                childNodes[i].childNodes递归深度(ref allNodes);
            }
        }

        public void childNodes递归广度(ref List<UpIteration> allNodes)
        {
            for (int i = 0; i < childNodes.Count; i++)
            {
                allNodes.Add(childNodes[i]);
            }

            for (int i = 0; i < childNodes.Count; i++)
            {
                childNodes[i].childNodes递归深度(ref allNodes);
            }
        }


    }

    public class UpTree
    {
        public UpIteration root;
        public List<UpIteration> allNodes = new List<UpIteration>(2048);
        public List<UpIteration> endNodes = new List<UpIteration>(1024);

        public void Clear()
        {
            root = null;
            allNodes.Clear();
            endNodes.Clear();
        }

        public UpIteration Select(int itemID) 
        {
            foreach (UpIteration node in allNodes)
            {
                if (node.itemProto.ID == itemID)
                    return node;
            }
            return null;
        }


        public void setRootItem(ItemProto itemProto)
        {
            root = UpIteration.setRootItem(itemProto);
            if (root == null)
            {
                return;
            }
            getAllNodes();
            getEndNodes();
            setEndNodesColumn();
            setEndNodesDepth();
            setAllNodesChildColumn();

        }



        void getAllNodes()
        {
            if (root == null)
                return;
            allNodes.Clear();
            allNodes.Add(root);
            root.childNodes递归深度(ref allNodes);

        }

        void getEndNodes()
        {
            endNodes.Clear();
            for (int i = 0; i < allNodes.Count; i++)
            {
                UpIteration node = allNodes[i];
                if (node.childNodes.Count <= 0)
                {
                    endNodes.Add(node);
                }
            }

        }

        void setEndNodesColumn()
        {
            int i = 2;
            foreach (UpIteration end in endNodes)
            {
                UpIteration.setEndItemColumn(end, i);
                i++;
                //if (end.itemProto.ID == 1007 || end.itemProto.ID == 1011) // 都是矿物
                //{
                //    i++;
                //}
                    
            }

        }

        void setEndNodesDepth()
        {
            int maxdepth = 0;
            foreach (UpIteration end in endNodes)
            {
                if(end.depth> maxdepth)
                    maxdepth = end.depth;
            }
            foreach (UpIteration end in endNodes)
                end.depth = maxdepth;

        }

        void setAllNodesChildColumn()
        {

            foreach (UpIteration node in allNodes)
            {
                node.itemColumns.Clear();
                foreach (UpIteration child in node.childNodes)
                {
                    node.itemColumns.Add(child.column);
                }
            }

        }

    }
}




