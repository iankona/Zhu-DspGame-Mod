using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace 物品堆叠倍数
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.player_package_item_stacksize_multiplier";
        public const string NAME = "背包物品堆叠倍数";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll(typeof(背包物品堆叠倍数));
        }
    }

    class 背包物品堆叠倍数
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "TryAddItemToPackage")]
        static bool 函数(Player __instance, 
                        ref int itemId,
                        ref int count,
                        ref int inc,
                        ref bool throwTrash,
                        ref int objId,
                        ref bool trashLife)
        {
            if (itemId <= 0 || itemId == 1099)
                return true;

            int multiplier = 10;
            StorageComponent package = __instance.package;

            bool 存在物品 = false;
            for (int index1 = 0; index1 < package.size; ++index1)
            {
                if (package.grids[index1].itemId == itemId)
                    存在物品 = true;        
            }
            if (存在物品)
                return true;

            for (int index2 = 0; index2 < package.size; ++index2)
            {
                if (package.grids[index2].itemId == 0)
                {
                    package.grids[index2].itemId = itemId;
                    package.grids[index2].count = 0;
                    package.grids[index2].filter = 0;
                    package.grids[index2].stackSize = multiplier * StorageComponent.itemStackCount[itemId];
                    break;
                }
            }
            return true;
        }
    }

    //class 物流和背包物品堆叠倍数
    //{
    //    [HarmonyPostfix]
    //    [HarmonyPatch(typeof(StorageComponent), "LoadStatic")]
    //    static void 函数(StorageComponent __instance)
    //    {
    //        int multiplier = 10;
    //        ItemProto[] dataArray = LDB.items.dataArray;
    //        for (int index = 0; index < dataArray.Length; ++index)
    //        {
    //            StorageComponent.itemStackCount[dataArray[index].ID] = multiplier * dataArray[index].StackSize;
    //        }
    //    }
    //}

}
