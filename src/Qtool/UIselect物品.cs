

using NGPT;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

namespace Qtool
{





    public class ItemBool
    {
        public bool 是否选上 = false;
        public ItemProto itemProto = null;
        public int itemValue = 0;
    }

    public class ItemSelectList
    {
        public List<ItemBool> 记录列表 = new List<ItemBool>(4); // 此时LDB还是null
        public List<ItemBool> 显示列表 = new List<ItemBool>(4); // 此时LDB还是null

        public void 初始化()
        {
            记录列表.Clear();
            foreach (ItemProto itemProto in LDB.items.dataArray)
            {
                if (itemProto.recipes.Count < 1)
                    continue;
                ItemBool itemBool = new ItemBool();
                itemBool.是否选上 = false;
                itemBool.itemProto = itemProto;
                itemBool.itemValue = 60; 
                记录列表.Add(itemBool);
            }
        }

        public void setItemProtos(List<ItemProto> dataArray)
        {
            记录列表.Clear();
            foreach (ItemProto itemProto1 in dataArray)
            {
                ItemBool itemBool = new ItemBool();
                itemBool.是否选上 = false;
                itemBool.itemProto = itemProto1;
                itemBool.itemValue = 60;
                记录列表.Add(itemBool);
            }
        }

        public void set全选()
        {
            foreach(ItemBool itemBool in 记录列表)
            {
                itemBool.是否选上 = true;
            }
            //foreach (ItemBool itemBool in 记录列表)
            //{
            //    Debug.Log(itemBool.是否选上);
            //}
        }

        public void set反选()
        {
            foreach (ItemBool itemBool in 记录列表)
            {
                itemBool.是否选上 = !itemBool.是否选上;
            }
        }
        public void set全不选()
        {
            foreach (ItemBool itemBool in 记录列表)
            {
                itemBool.是否选上 = false;
            }
        }

        public void 更新显示列表()
        {
            显示列表.Clear();
            foreach (ItemBool 记录 in 记录列表)
            {
                if (!记录.是否选上)
                    continue;
                显示列表.Add(记录);
            }
        }

    }

    public class FrameSelectItem
    {
        public ItemSelectList 物品多选 = null;

        void update物品多选()
        {
            if (Plugin.实例.物品多选 == null) // 延迟到游戏加载完成后，再初始化，防止调用游戏对象时出现null错误
            {
                物品多选 = new ItemSelectList();
                物品多选.初始化();
                Plugin.实例.物品多选 = 物品多选;

            }
            else
            {
                物品多选 = Plugin.实例.物品多选;
            }
        }



        public void showItems()
        {
            update物品多选();

            int i = 0;
            foreach (ItemProto itemProto in LDB.items.dataArray)
            {
                if (itemProto.recipes.Count < 1)
                    continue;
                GUI.Box(Plugin.实例.布局.newrectFrameLayer(i), itemProto.iconSprite.texture);
                物品多选.记录列表[i].是否选上 = GUI.Toggle(Plugin.实例.布局.newrectFrameLayer(i), 物品多选.记录列表[i].是否选上, itemProto.Name );
                if (物品多选.记录列表[i].是否选上)
                    物品多选.更新显示列表();
                i++;

            }




        }
    }
}



