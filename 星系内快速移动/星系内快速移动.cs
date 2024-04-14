using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using static UnityEngine.RectTransform;
using static VFInput;

namespace 星系内快速移动
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.default_fast_move";
        public const string NAME = "星系内快速移动";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";



        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(拦截机甲位置判定));
            new Harmony(GUID).PatchAll(typeof(全球视图启用沙盒工具));
            new Harmony(GUID).PatchAll(typeof(星系视图启用沙盒工具));
        }
    }



    class 拦截机甲位置判定
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ABN_MechaPosition), "OnGameTick")]
        public static bool 函数(ABN_MechaPosition __instance)
        {
            return false; // 拦截机甲位置判定
        }
    }


    class 全球视图启用沙盒工具
    {
        public static bool sandboxToolsEnabled;
        public static bool skiprtscancel = false;


        [HarmonyPrefix]
        [HarmonyPatch(typeof(UIGlobemap), "_OnUpdate")]
        public static bool 函数01(UIGlobemap __instance)
        {
            sandboxToolsEnabled = (bool)Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").GetValue();
            if (!sandboxToolsEnabled)
                Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").SetValue(true);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIGlobemap), "_OnUpdate")]
        public static void 函数02(UIGlobemap __instance)
        {
            Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").SetValue(sandboxToolsEnabled);
        }



        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIBuildMenu), "UpdateUXPanelHotKey")]
        public static void 函数03(UIBuildMenu __instance) 
        {
            UIGame uiGame = UIRoot.instance.uiGame;
            if (UIGame.viewMode == EViewMode.Build)
                uiGame.globemap.TeleportLogic();
            if (__instance.isDismantleMode || __instance.isUpgradeMode)
                skiprtscancel = true;
            else
            { 
                if (Input.GetMouseButtonUp(0))
                    skiprtscancel = false;
            }
            if (Input.GetMouseButtonUp(1))
                skiprtscancel = true;



        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(VFInput), "rtsCancel", MethodType.Getter)]
        public static bool 函数04()
        {

            if (skiprtscancel)
            {
                VFInput.InputValue inputValue = new VFInput.InputValue();
                return inputValue;
            }
            else
            {
                VFInput.InputValue inputValue = new VFInput.InputValue();
                VFInput.InputAxis axis = VFInput.axis_button;
                int axisIdx = 1;
                if (axisIdx >= 0 && axisIdx < axis.value.Length)
                {
                    inputValue.pressing = (double)axis.value[axisIdx] > 0.5;
                    inputValue.onDown = axis.down[axisIdx];
                    inputValue.onUp = axis.up[axisIdx];
                    inputValue.value = axis.value[axisIdx];
                }
                return inputValue;
            }

        }

    }


    class 星系视图启用沙盒工具
    {
        public static bool sandboxToolsEnabled;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(UIStarmap), "_OnUpdate")] // 会显示快速移动按钮，但点击无作用
        public static bool 函数01(UIStarmap __instance)
        {
            sandboxToolsEnabled = (bool)Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").GetValue();
            if (!sandboxToolsEnabled)
                Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").SetValue(true);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIStarmap), "_OnUpdate")]
        public static void 函数02(UIStarmap __instance)
        {
            Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").SetValue(sandboxToolsEnabled);
        }


        // 启用星系内快速移动按钮功能
        [HarmonyPrefix]
        [HarmonyPatch(typeof(UIStarmap), "OnFastTravelButtonClick")]
        public static bool 函数11(UIStarmap __instance)
        {
            sandboxToolsEnabled = (bool)Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").GetValue();
            if (!sandboxToolsEnabled)
                Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").SetValue(true);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIStarmap), "OnFastTravelButtonClick")]
        public static void 函数12(UIStarmap __instance)
        {
            Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").SetValue(sandboxToolsEnabled);
        }


        // 启用星系团右键快速移动功能
        [HarmonyPrefix]
        [HarmonyPatch(typeof(UIStarmap), "OnScreenClick")]
        public static bool 函数21(UIStarmap __instance)
        {
            sandboxToolsEnabled = (bool)Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").GetValue();
            if (!sandboxToolsEnabled)
                Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").SetValue(true);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIStarmap), "OnScreenClick")]
        public static void 函数22(UIStarmap __instance)
        {
            Traverse.Create(GameMain.instance).Field("_sandboxToolsEnabled").SetValue(sandboxToolsEnabled);
        }

    }
}


