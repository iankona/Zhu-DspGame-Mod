using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;


namespace 轨道采集器立即建造
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.gasminerinstantbuild";
        public const string NAME = "轨道采集器立即建造";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll(typeof(轨道采集器立即建造));
        }
    }


    [HarmonyPatch(typeof(MechaDroneLogic), "UpdateTargets")]
    class 轨道采集器立即建造
    {
        public static void Postfix(MechaDroneLogic __instance)
        {
            foreach (var prebuild in __instance.factory.prebuildPool)
            {
                if (prebuild.protoId == 2105) // __instance.planet.type == EPlanetType.Gas
                {
                    __instance.factory.BuildFinally(__instance.player, prebuild.id);
                }
                    
            }
                
        }
    }
}

