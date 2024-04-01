using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace 星系数量
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.set_star_count";
        public const string NAME = "星系数量";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";



        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(星系数量));
        }
    }



    class 星系数量
    {
        public static int minstarcount = 7;
        public static int maxstarcount = 128;


        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIGalaxySelect), "_OnOpen")]
        public static void 函数0(UIGalaxySelect __instance)
        {
            Slider starCountSlider = (Slider)Traverse.Create(__instance).Field("starCountSlider").GetValue();
            starCountSlider.minValue = minstarcount;
            starCountSlider.maxValue = maxstarcount;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(UIGalaxySelect), "OnStarCountSliderValueChange")]
        public static bool 函数1(UIGalaxySelect __instance, ref float val)
        {
            GameDesc gameDesc = (GameDesc)Traverse.Create(__instance).Field("gameDesc").GetValue();
            Slider starCountSlider = (Slider)Traverse.Create(__instance).Field("starCountSlider").GetValue();
            int num = (int)((double)starCountSlider.value + 0.10000000149011612);
            if (num < minstarcount)
                num = minstarcount;
            else if (num > maxstarcount)
                num = maxstarcount;
            if (num == gameDesc.starCount)
                return false;
            gameDesc.starCount = num;
            __instance.SetStarmapGalaxy();

            return false; // 拦截原函数
        }
    }

}


