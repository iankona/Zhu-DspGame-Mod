using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace 黑雾不登录海洋星
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.ocean_no_heiwu";
        public const string NAME = "黑雾不登录海洋星";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(黑雾不登录海洋星));
        }
    }



    class 黑雾不登录海洋星
    {


        [HarmonyPrefix]
        [HarmonyPatch(typeof(SpaceSector), "CreateEnemyFinal", new Type[] { typeof(EnemyDFHiveSystem), typeof(int), typeof(int), typeof(VectorLF3), typeof(Quaternion) })]
        public static bool 函数0(
            ref EnemyDFHiveSystem hive,
            ref int protoId,
            ref int astroId,
            ref VectorLF3 lpos,
            ref Quaternion lrot,
            ref int __result)
        {
            
            if (protoId != 8116) // 中继站
                return true;
            PlanetData planetData = hive.galaxy.PlanetById(astroId);
            if (planetData == null)
                return true;
            if (planetData.type == EPlanetType.Ocean)
            {
                __result = 0;
                return false;
            }
            return true;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(DFRelayComponent), "SearchTargetPlaceProcess")]
        public static void 函数1(DFRelayComponent __instance, ref bool __result)
        {
            PlanetData planetData = __instance.hive.galaxy.PlanetById(__instance.searchAstroId);
            if (planetData != null && planetData.type == EPlanetType.Ocean)
            {
                __instance.ResetSearchStates();
                __result = false;
            }
                
        }



    }


}


