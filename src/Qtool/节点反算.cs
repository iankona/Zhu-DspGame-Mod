

using Qtool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static Qtool.NodeTree;

namespace Qtool
{

    public class InvertteCalculate 
    {

        ItemProto itemProto;
        float itemValue = 0;
        public List<NodeTree> itemTreeList = new List<NodeTree>(64);
        public List<ItemInfoList> item汇总列表 = new List<ItemInfoList>(64);




        public void setItemProto基础(ItemProto itemProto1) 
        {
            itemProto = itemProto1;
        }

        public void setItemValue基础(float value)
        {
            itemValue = value;
        }


        public void addItemProto目标(ItemProto itemProto2)
        {
            bool 在树列表里 = inTree(itemProto2);
            if (在树列表里)
                return;

            NodeTree itemTree2 = new NodeTree();
            itemTree2.setRootItem(itemProto2, ref Plugin.实例.物品配方索引);
            itemTree2.setRootResultValue(itemProto2, 60);
            itemTreeList.Add(itemTree2);

            ItemInfoList item汇总2 = new ItemInfoList();
            item汇总2.set树枝(itemTree2);
            item汇总列表.Add(item汇总2);
        }

        public float getItemValue目标(ItemProto itemProto2)
        {
            float 基础物品消耗量 = getSumValue(itemProto2);
            float 系数 = itemValue / 基础物品消耗量;
            return 60 * 系数;
        }


        bool inTree(ItemProto itemProto2)
        {
            bool 在树列表里 = false;
            foreach (NodeTree itemTree2 in itemTreeList)
            {
                if (itemProto2.ID == itemTree2.root.itemProto.ID) 
                {
                    在树列表里 = true;
                    break;
                }
            }
            return 在树列表里;

        }


        float getSumValue(ItemProto itemProto2)
        {
            int i = 0;
            foreach (NodeTree itemTree2 in itemTreeList)
            {
                if (itemProto2.ID == itemTree2.root.itemProto.ID)
                    break;
                i++;
            }
            ItemInfoList item汇总1 = item汇总列表[i];
            ItemInfo 消耗 = item汇总1.Select消耗物品(itemProto.ID);
            return 消耗.value;
        }

    }


}




