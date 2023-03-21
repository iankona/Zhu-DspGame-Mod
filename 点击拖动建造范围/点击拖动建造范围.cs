using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace 点击拖动建造范围
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.clicktool.dotsSnapped";
        public const string NAME = "点击拖动建造范围";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(BuildTool_Click), "UpdateRaycast")]
    class 点击拖动建造范围
    {
        public static void Postfix(BuildTool_Click __instance)
        {
            // __instance.dotsSnapped = new Vector3[36];
            GameMain.mainPlayer.controller.actionBuild.clickTool.dotsSnapped = new Vector3[50];
        }
    }
}

        



