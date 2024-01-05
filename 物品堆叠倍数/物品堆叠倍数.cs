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
        public const string GUID = "cn.zhufile.dsp.item_stacksize_multiplier";
        public const string NAME = "物品堆叠倍数";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll(typeof(物品堆叠倍数));
        }
    }



    class 物品堆叠倍数
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StorageComponent), "LoadStatic")]
        static void 函数(StorageComponent __instance)
        {
            int multiplier = 10;
            ItemProto[] dataArray = LDB.items.dataArray;
            for (int index = 0; index < dataArray.Length; ++index)
            {
                StorageComponent.itemStackCount[dataArray[index].ID] = multiplier * dataArray[index].StackSize;
            }
        }
    }
}
