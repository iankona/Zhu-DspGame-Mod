using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;

namespace 小矿机采矿速度
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.mkonefastminer";
        public const string NAME = "小矿机采矿速度";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(小矿机采矿速度));
        }
    }


    [HarmonyPatch(typeof(FactorySystem), "NewMinerComponent")]
    class 小矿机采矿速度
    {
        public static void Postfix(FactorySystem __instance, ref int __result)
        {
            // __instance.minerPool[__result].speed = 20000; //默认为10000，对应采矿30个/矿点
            var miner = __instance.minerPool[__result];
            var entity = __instance.factory.entityPool[miner.entityId];
            if (entity.protoId == 2301)
            {
                // miner.speed = 20000; // 引用和复制的区别
                // __instance.minerPool[__result].speed = 20000; 
            }
        }
    }
}


