using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace 超级蓄电池建筑
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.superxudianchiacc";
        public const string NAME = "超级蓄电池建筑";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll(typeof(超级蓄电池建筑));
        }
    }



    class 超级蓄电池建筑
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PowerSystem), "NewAccumulatorComponent")]
        static void Postfix(PowerSystem __instance, ref int __result)
        {
            // 需要把蓄电池拍地上成为建筑物才有用，对能量枢纽不起作用；
            PowerAccumulatorComponent acc = __instance.accPool[__result]; // 是复制
            int 系数 = 15;
            __instance.accPool[__result].inputEnergyPerTick = 系数 * acc.inputEnergyPerTick; // 是引用
            __instance.accPool[__result].outputEnergyPerTick = 系数 * acc.outputEnergyPerTick;
            __instance.accPool[__result].maxEnergy = 系数 * acc.maxEnergy;
        }
    }
}

// 小太阳功率72MW
// 枢纽功率 45MW
// 蓄电池拍地上成为建筑，参数13.5MV, 4.05GJ
// 原始蓄电池参数 0.9MV, 270MJ