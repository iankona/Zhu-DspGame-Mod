

using NGPT;
using System;
using System.Security.Policy;
using UnityEngine;

namespace Qtool
{



    public class FrameItem
    {
        public void showItems()
        {

            int i = 0;
            foreach (ItemProto tempItemProto in LDB.items.dataArray)
            {
                if (tempItemProto.recipes.Count < 1)
                    continue;

                if (GUI.Button(Plugin.实例.布局.newrectFrameLayerXY(i), tempItemProto.iconSprite.texture))
                {
                    Plugin.实例.界面.guilayerindex = 4;
                    Plugin.实例.物品ID = tempItemProto.ID;
                    Plugin.实例.物品树需要更新 = true;
                    // Debug.Log("选择物品ID::"+ tempItemProto.Name + "::" + tempItemProto.ID);
                }
                i++;

            }




        }
    }
}



