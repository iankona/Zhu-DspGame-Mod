using System;
using System.Collections;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace Qtool
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.qtool";
        public const string NAME = "Qtool";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";

        public static 绘制逻辑 实例;



        public void Start()
        {
            实例 = new 绘制逻辑();
            // new Harmony(GUID).PatchAll(typeof(打印));

        }


        public void OnGUI()
        {
            实例.保存字体设置();
            实例._onGUI();
            实例.复位字体设置();
        }

    }

    class 打印
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(UIMilestoneLine), "SetLine")]
        public static bool 函数0(int _milestoneId, bool isHorizontal)
        {
            Console.WriteLine("_milestoneId");
            Console.WriteLine(_milestoneId);
            return true;
        }



    }
}







