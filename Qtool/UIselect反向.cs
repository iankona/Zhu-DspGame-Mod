

using UnityEngine;

namespace Qtool
{

    public class FrameRecipe
    {

        public void showRecipes()
        {

            int i = 0;
            foreach (RecipeProto recipeProto in LDB.recipes.dataArray)
            {
                if (recipeProto.Results.Length < 1)
                    continue;

                if (GUI.Button(Plugin.实例.布局.newrectFrameLayer(i), recipeProto.iconSprite.texture))
                {
                    Plugin.实例.界面.guilayerindex = 22;
                    Plugin.实例.配方ID = recipeProto.ID;
                    Debug.Log("选择配方::" + recipeProto.Name + "::" + recipeProto.ID);
                }
                i++;

            }


        }
    }




}



