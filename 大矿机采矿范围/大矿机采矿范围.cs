using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;

namespace 大矿机采矿范围
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.mkminerrange";
        public const string NAME = "大矿机采矿范围";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(FactorySystem), "NewMinerComponent")]
    class 大矿机采矿范围
    {
        public static void Postfix(FactorySystem __instance, int __result)
        {
            // __instance.minerPool[__result].speed = 20000; //默认为10000，对应采矿30个/矿点
            // __instance.minerPool[__result].kFanDot = 0.73f;
            __instance.minerPool[__result].kFanRadius += 5;

            __instance.minerPool[__result].kMK2FanLength += 2;
            __instance.minerPool[__result].kMK2FanWidth  += 2;
        }
    }
}


