using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace 更高功率能量枢纽
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.morepowerexc";
        public const string NAME = "更高功率能量枢纽";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll(typeof(更高功率能量枢纽));
        }
    }



    class 更高功率能量枢纽
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PowerSystem), "NewExchangerComponent")]
        static void Postfix(PowerSystem __instance, ref int __result)
        {
            // PowerExchangerComponent exc = __instance.excPool[__result];
            __instance.excPool[__result].energyPerTick *= 7;  //* exc.energyPerTick;  // 额定功率调整到315MW， 默认是45MV // *= 号不起作用
            __instance.excPool[__result].maxPoolEnergy *= 38; //* exc.maxPoolEnergy; // 蓄电量调整到10.26GJ, 默认270MJ
        }
    }
}

// 小太阳功率72MW,黑棒储能7.20GJ
// 枢纽功率 45MW
// 蓄电池拍地上成为建筑，参数13.5MV, 4.05GJ，对应15倍
// 原始蓄电池参数 0.9MV, 270MJ