using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace 超级蓄电池
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.super_xudianchi_acc";
        public const string NAME = "超级蓄电池";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll(typeof(超级蓄电池));
        }
    }



    class 超级蓄电池
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PowerSystem), "NewAccumulatorComponent")]
        static void Postfix(PowerSystem __instance, ref int __result)
        {
            // 需要把蓄电池拍地上成为建筑物才有用，对能量枢纽不起作用；
            long MW = 1000020L;
            long GJ = 1000000000L;
            long MW_PerTick = (long)(MW/60);
            __instance.accPool[__result].inputEnergyPerTick = 315 * MW_PerTick;     // 原始，25000 -> 1.5MW
            __instance.accPool[__result].outputEnergyPerTick = 315 * MW_PerTick;    // 原始，37500 -> 2.25MW
            __instance.accPool[__result].maxEnergy = (long)(10.26 * GJ);            // 原始，540000000 -> 540MJ
        }
    }
}
