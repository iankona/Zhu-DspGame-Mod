using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace 超级能量枢纽
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.more_power_exc";
        public const string NAME = "超级能量枢纽";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll(typeof(超级能量枢纽));
        }
    }



    class 超级能量枢纽
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PowerSystem), "NewExchangerComponent")]
        static void Postfix(PowerSystem __instance, ref int __result)
        {
            long MW = 1000020L;
            long GJ = 1000000000L;
            long MW_PerTick = (long)(MW / 60);
            __instance.excPool[__result].energyPerTick = 315 * MW_PerTick;
            __instance.excPool[__result].maxPoolEnergy = (long)(10.26*GJ); 
        }
    }
}

// 小太阳功率75MW,黑棒储能7.20GJ
// 枢纽功率 45MW
