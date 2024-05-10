

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
            foreach (ItemProto itemProto in LDB.items.dataArray)
            {
                if (itemProto.recipes.Count < 1)
                    continue;

                if (GUI.Button(Plugin.实例.布局.newrectFrameLayer(i), itemProto.iconSprite.texture))
                {
                    Plugin.实例.界面.guilayerindex = 2;
                    Plugin.实例.物品ID = itemProto.ID;
                    Debug.Log("选择物品ID::"+ itemProto.Name + "::" + itemProto.ID);
                }
                i++;

            }




        }
    }
}



