using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;

namespace 机甲数据修改
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.mecha_data_set";
        public const string NAME = "机甲数据修改";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(Mecha), "SetForNewGame")]
    class 机甲数据修改
    {
        public static void Postfix(Mecha __instance)
        {
            long GJ = 1000000000;
            __instance.coreEnergyCap = (double)(200 * GJ); //机甲核心能量上限，正常情况3.2GJ
            __instance.reactorPowerGen = (double)(100 * GJ); //护盾充能功率，初始24MW, 2400000; 注意（double）限制
            __instance.energyShieldCapacity = 1024*GJ; // 能量盾上限，初始100MJ，100000000;

        }
    }
}


