using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;

namespace DSPFastMiningMachine
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.fastminingmachine";
        public const string NAME = "FastMiningMachine";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(FactorySystem), "NewMinerComponent")]
    class FastMiningMachine
    {
        public static void Postfix(FactorySystem __instance, int __result)
        {
            __instance.minerPool[__result].speed = 20000; //默认为10000，对应采矿30个/矿点
        }
    }
}


