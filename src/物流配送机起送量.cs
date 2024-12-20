using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace 物流配送机起送量
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.courier_carries";
        public const string NAME = "物流配送机起送量";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll(typeof(物流配送机起送量));
        }
    }



    class 物流配送机起送量
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(GameHistoryData), "UnlockTechFunction")]
        private static void 运载量与配送角度(GameHistoryData __instance)
        {
            if (__instance.logisticCourierCarries < 100)
                __instance.logisticCourierCarries += 100;
            __instance.dispenserDeliveryMaxAngle = 180f;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DispenserComponent), "PickFromStoragePrecalc")]
        private static bool 起送量(int itemId, int needCnt, ref int __result)
        {
            if (needCnt < 100)
            {
                __result = 0;
                return false;
            }
            return true;
        }
    }
}
