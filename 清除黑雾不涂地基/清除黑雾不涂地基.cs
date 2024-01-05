using System;
using BepInEx;
using HarmonyLib;

namespace 摧毁黑雾不留坑洞
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.destroy_no_heidong";
        public const string NAME = "摧毁黑雾不留坑洞";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(摧毁黑雾不留坑洞));
        }
    }



    class 摧毁黑雾不留坑洞
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(BuildTool_Reform), "RemoveBasePit")]
        public static bool 函数1(
            BuildTool_Reform __instance,
            ref int removeBasePitRuinId,
            ref bool __result)
        {
            // Console.WriteLine("移除按钮方法体有运行");
            __instance.factory.enemySystem.RemoveBasePit(removeBasePitRuinId);
            __result = true;
            return false; // 拦截原方法
        }
    }
}


//[HarmonyPostfix]
//[HarmonyPatch(typeof(UIRemoveBasePitButton), "OnRemoveButtonClick")]
//public static void 函数0(UIRemoveBasePitButton __instance)
//{
//    Console.WriteLine("移除按钮有运行");
//}