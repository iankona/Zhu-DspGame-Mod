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

namespace 黑雾不登录初始星系
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.birthstar_no_heiwu";
        public const string NAME = "黑雾不登录初始星系";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(黑雾不登录初始星系));
        }
    }



    class 黑雾不登录初始星系
    {


        [HarmonyPrefix]
        [HarmonyPatch(typeof(SpaceSector), "SetForNewGame")]
        public static bool 函数0(SpaceSector __instance)
        {
            int starCount = __instance.galaxy.starCount;
            for (int index = 0; index < starCount; ++index)
            {
                StarData star = __instance.galaxy.stars[index];
                int birthstarindex = __instance.galaxy.birthStarId - 1;
                if (index == birthstarindex)
                {
                    star.initialHiveCount = 0;
                    break;
                }
            }
            return true;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(SpaceSector), "TryCreateNewHive")]
        public static bool 函数1(SpaceSector __instance, ref StarData star)
        {
            if (star == null)
                return true;
            int birthstarindex = __instance.galaxy.birthStarId - 1;
            if (star.index == birthstarindex)
                return false;
            return true;
        }
    }


}


//Debug.Log((object)"SetForNewGame调试");
//Debug.Log((object)"出生星系ID: ");
//Debug.Log((object)__instance.galaxy.birthStarId);