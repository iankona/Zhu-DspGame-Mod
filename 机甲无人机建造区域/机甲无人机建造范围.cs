using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace 机甲无人机建造范围
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.mecha.buildarea";
        public const string NAME = "机甲无人机建造范围";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(BuildTool_Click), "UpdateRaycast")]
    class 机甲无人机建造范围
    {
        public static void Postfix(BuildTool_Click __instance)
        {   // 小飞机建造限制
            var range = 600; // 200 * 3.14159 = 628.318
            if (GameMain.localPlanet != null && GameMain.localPlanet.realRadius > 201)
            {
                range = (int)(GameMain.localPlanet.realRadius * Mathf.PI);
            }

            GameMain.mainPlayer.mecha.buildArea = range;
        }
    }
}

        



