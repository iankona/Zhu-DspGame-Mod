using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace 取物范围
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.selectdistance";
        public const string NAME = "取物范围";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(PlayerAction_Inspect), "GetObjectSelectDistance")]
    class 取物范围
    {
        public static void Postfix(ref float __result)
        {
            var range = 600;
            if (GameMain.localPlanet != null && GameMain.localPlanet.realRadius > 201)
            {
                range = (int)(GameMain.localPlanet.realRadius * Mathf.PI);
            }
            __result = range; // 默认35f
        }
    }
}

