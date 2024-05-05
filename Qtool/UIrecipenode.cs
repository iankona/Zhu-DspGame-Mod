

using NGPT;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Policy;
using UnityEngine;

namespace Qtool
{



    public class FrameRecipeNode
    {
        public int column = 0; 
        public int columnstep = 0;
        public int columnmini = 0;
        public List<RecipeProto> recipes = new List<RecipeProto>(4);
        //public bool 更新物品配方 = false;
        //public bool 更新配方 = false;

        void setItemRecipes(int itemID)
        {
            ItemProto itemProto = LDB.items.Select(itemID);
            if (itemProto == null)
                return;

            recipes.Clear();

            if (itemProto.recipes.Count < 1)
                return;

            foreach (RecipeProto recipeProto in itemProto.recipes)
                recipes.Add(recipeProto);

        }
        void set配方(int recipeID)
        {
            RecipeProto recipeProto = LDB.recipes.Select(recipeID);
            if (recipeProto == null) { return; }
            recipes.Clear();
            recipes.Add(recipeProto);
        }







        public void showRecipeNodes()
        {
            ItemProto itemProto = LDB.items.Select(Plugin.实例.物品ID);
            if (itemProto == null)
                return;

            setItemRecipes(itemProto.ID);

            columnmini = 8;
            int i = 0;
            foreach (RecipeProto recipeProto in recipes)
            {
                if (recipeProto == null)
                    continue;
                int row = i + 1;
                int col = 4;
                drawRecipeColumn(recipeProto, row, col);
                i++;
                i++;
            }

            column = 0;
            foreach (RecipeProto recipeProto in recipes)
            {
                int row = 10;
                int col = columnmini + column;
                drawRecipeBox(recipeProto, row, col);
                drawRecipeNode(recipeProto, row, col);
                column = column + columnstep + 2;
            }

        }


        void drawRecipeColumnBox(int row, int col, int length)
        { 
            if (length <= 1)
            { return; }

            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);
            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col + length - 1);

            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");

        }

        void drawRecipeColumn(RecipeProto recipeProto, int row, int col)
        {
            if ((col - recipeProto.Results.Length -1) < columnmini)
                columnmini = col - recipeProto.Results.Length - 1;

            drawRecipeColumnBox(row, columnmini, recipeProto.Results.Length);

            for (int k = 0; k < recipeProto.Results.Length; k++)
            {
                int itemID1 = recipeProto.Results[k];
                int resultCount1 = recipeProto.ResultCounts[k];

                ItemProto itemProto1 = LDB.items.Select(itemID1);
                int column1 = col - recipeProto.Results.Length + k;

                GUI.Box(  Plugin.实例.布局.newrectFrameRecipeIcon(row, column1 - 1), itemProto1.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row, column1 - 1), resultCount1.ToString());
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row, column1 - 1), itemProto1.Name);
            }

            GUI.Box(  Plugin.实例.布局.newrectFrameRecipeIcon(row, col), recipeProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row, col), (recipeProto.TimeSpend / 60) + "S");
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row, col), recipeProto.Name);


            drawRecipeColumnBox(row, col+2, recipeProto.Items.Length);
            for (int m = 0; m < recipeProto.Items.Length; m++)
            {

                int itemID2 = recipeProto.Items[m];
                int itemCount2 = recipeProto.ItemCounts[m];

                ItemProto itemProto2 = LDB.items.Select(itemID2);
                int column2 = col + 1 + m;

                GUI.Box(  Plugin.实例.布局.newrectFrameRecipeIcon(row, column2 + 1), itemProto2.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row, column2 + 1), itemCount2.ToString());
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row, column2 + 1), itemProto2.Name);
            }


        }



        void drawRecipeBox(RecipeProto recipeProto, int row, int col)
        {

            Rect startRect = Plugin.实例.布局.newrectFrameRecipeIcon(row, col);

            int columnIndex = 0;
            int maxResultColumnIndex = recipeProto.Results.Length - 1;
            int maxItemColumnIndex = recipeProto.Items.Length - 1;

            if (maxResultColumnIndex > columnIndex)
                columnIndex = maxResultColumnIndex;
            if (maxItemColumnIndex > columnIndex)
                columnIndex = maxItemColumnIndex;

            columnstep = columnIndex;

            Rect finalRect = Plugin.实例.布局.newrectFrameRecipeIcon(row + 4, col + columnIndex);

            float width = finalRect.x - startRect.x + finalRect.width;
            float height = finalRect.y - startRect.y + finalRect.height;
            GUI.Box(new Rect(startRect.x, startRect.y, width, height), "");
        }



        void drawRecipeNode(RecipeProto recipeProto, int row, int col)
        {
            for (int k = 0; k < recipeProto.Results.Length; k++)
            {
                int itemID1 = recipeProto.Results[k];
                int resultCount1 = recipeProto.ResultCounts[k];

                ItemProto itemProto1 = LDB.items.Select(itemID1);
                int column1 = col + k;

                GUI.Box(  Plugin.实例.布局.newrectFrameRecipeIcon(row + 0, column1), itemProto1.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 0, column1), resultCount1.ToString());
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 0, column1), itemProto1.name);
            }


            GUI.Box(  Plugin.实例.布局.newrectFrameRecipeIcon(row + 2, col), recipeProto.iconSprite.texture);
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 2, col), (recipeProto.TimeSpend / 60) + "S");
            GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 2, col), recipeProto.name);


            for (int i = 0; i < recipeProto.Items.Length; i++)
            {

                int itemID2 = recipeProto.Items[i];
                int itemCount2 = recipeProto.ItemCounts[i];
                ItemProto itemProto2 = LDB.items.Select(itemID2);
                int column2 = col + i;

                GUI.Box(  Plugin.实例.布局.newrectFrameRecipeIcon(row + 4, column2), itemProto2.iconSprite.texture);
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeName(row + 4, column2), itemCount2.ToString());
                GUI.Label(Plugin.实例.布局.newrectFrameRecipeText(row + 4, column2), itemProto2.name);
            }
        }



    }

}




