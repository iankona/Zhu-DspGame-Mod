using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;


namespace DSPInstantBuild
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.instantbuild";
        public const string NAME = "InstantBuild";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(MechaDroneLogic), "UpdateTargets")]
    class InstantBuild
    {
        public static void Postfix(MechaDroneLogic __instance)
        {
            foreach (var prebuild in __instance.factory.prebuildPool)
                __instance.factory.BuildFinally(__instance.player, prebuild.id);
        }
    }
}

