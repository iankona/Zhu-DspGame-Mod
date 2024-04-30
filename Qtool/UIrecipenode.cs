

using NGPT;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Policy;
using UnityEngine;

namespace Qtool
{

    //public class FrameRecipeNode
    //{
    //    public List<RecipeProto> recipeProto列表 = new List<RecipeProto>(4); // this.recipes = new List<RecipeProto>(4); // RecipeProto[] dataArray = LDB.recipes.dataArray;

    //    WidgetRecipeNode 节点区域;




    //    public void setRecipe(int recipeProtoID)
    //    {
    //        recipeProto列表.Clear();
    //        RecipeProto recipeProto = LDB.recipes.Select(recipeProtoID);
    //        recipeProto列表.Add(recipeProto);
    //    }

    //    public void setItem(int itemProtoID)
    //    {
    //        recipeProto列表.Clear();
    //        ItemProto itemProto = LDB.items.Select(itemProtoID);
    //        foreach (RecipeProto recipeProto in itemProto.recipes)
    //        {
    //            if (recipeProto == null)
    //                continue;
    //            recipeProto列表.Add(recipeProto);

    //        }
    //    }


    //    public void showRecipeNode()
    //    {

    //        int i = 0;
    //        foreach (RecipeProto recipeProto in recipeProto列表)
    //        {
    //            if (recipeProto == null)
    //                continue;
    //            节点区域.showRecipeNode(recipeProto, 0, i);
    //            i++;

    //        }

    //    }
    //}

    //class WidgetRecipeNode 
    //{
    //    RecipeProto recipeProto;
 
    //    void showRecipeNode(RecipeProto recipeProto1, int rowindex, int colindex)
    //    {
    //        if (recipeProto == null)
    //            return;
    //        recipeProto = recipeProto1;

    //        Plugin.实例.position.setwidgetRectClear();
    //        setOutputsIndex();
    //        setRecipeIndex();
    //        setInputsIndex();
    //        Plugin.实例.position.updateWidgetRect();

    //        showOutputs();
    //        showRecipe();
    //        showInputs();
    //    }



    //    void setOutputsIndex()
    //    {
    //        for (int i = 0; i < recipeProto.Results.Length; i++)
    //            Plugin.实例.position.setIconIndex(0, i);
    //    }
    //    void setRecipeIndex()
    //    {
    //        Plugin.实例.position.setIconIndex(2, 0);
    //    }

    //    void setInputsIndex()
    //    {
    //        for (int i = 0; i < recipeProto.Items.Length; i++)
    //            Plugin.实例.position.setIconIndex(4, i);
    //    }


    //    void showOutputs()
    //    {
    //        for (int i = 0; i < recipeProto.Results.Length; i++)
    //        {
    //            int resultID = recipeProto.Results[i];
    //            int resultCount = recipeProto.ResultCounts[i];
    //            ItemProto itemProto = LDB.items.Select(resultID);
    //            GUI.Box(Plugin.实例.position.newrectFrameRecipeIcon(0, i), new GUIContent(resultCount.ToString(), itemProto.iconSprite.texture));
    //            GUI.Label(Plugin.实例.position.newrectFrameRecipeName(0, i), itemProto.name);
    //            GUI.Label(Plugin.实例.position.newrectFrameRecipeText(0, i), "123456789");
    //        }
    //    }
    //    void showRecipe()
    //    {
    //        if (GUI.Button(Plugin.实例.position.newrectFrameRecipeIcon(2, 0), new GUIContent((recipeProto.TimeSpend / 60).ToString(), recipeProto.iconSprite.texture)))
    //        {

    //        }
    //        GUI.Label(Plugin.实例.position.newrectFrameRecipeName(2,0), recipeProto.name);
    //        GUI.Label(Plugin.实例.position.newrectFrameRecipeText(2,0), "123456789/min");
    //    }

    //    void showInputs()
    //    {
    //        for (int i = 0; i < recipeProto.Items.Length; i++)
    //        {
    //            int itemID = recipeProto.Items[i];
    //            int itemCount = recipeProto.ItemCounts[i];
    //            ItemProto itemProto = LDB.items.Select(itemID);
    //            GUI.Box(Plugin.实例.position.newrectFrameRecipeIcon(4, i), new GUIContent(itemCount.ToString(), itemProto.iconSprite.texture));
    //            GUI.Label(Plugin.实例.position.newrectFrameRecipeName(4, i), itemProto.name);
    //            GUI.Label(Plugin.实例.position.newrectFrameRecipeText(4, i), "123456789/min");
    //        }
    //    }
    //}


}






//int columnindex = 0;
//foreach (int resultID in recipeProto.Results)
//{
//    ItemProto itemProto = LDB.items.Select(resultID);
//    if (GUI.Button(Plugin.实例.position.newrectFrameRecipe(0, columnindex), itemProto.iconSprite.texture))
//    {

//    }
//    columnindex++;
//}
//foreach (int itemID in recipeProto.Items)
//    ItemProto itemProto = LDB.items.Select(itemID);