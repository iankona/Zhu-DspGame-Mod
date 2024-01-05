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

namespace 矿机采矿留根
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.miner_leave_vein";
        public const string NAME = "矿机采矿留根";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(矿机采矿留根));
        }
    }



    class 矿机采矿留根
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MinerComponent), "InternalUpdate")]
        static bool Prefix(MinerComponent __instance, VeinData[] veinPool)
        {
            // VeinData[] veinPool = GameMain.mainPlayer.factory.veinPool;
            lock (veinPool) 
            {
                bool 都小于 = true;
                for (int index1 = 0; index1 < __instance.veinCount; index1++)
                {
                    VeinData vein = veinPool[__instance.veins[index1]];
                    if (vein.amount > 101)
                    {
                        都小于 = false;
                        __instance.currentVeinIndex = index1;
                    }
 
                }

                if (都小于 & GameMain.history.techStates[3606].curLevel < 157)
                    return false; // 禁用原函数
                else
                    return true; // 原函数正常运行



            }
        }
    }
}


