using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;


namespace 轨道采集器立即建造
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.gasminer_instant_build";
        public const string NAME = "轨道采集器立即建造";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(ConstructionSystem), "ExecuteDroneTasks")]
    class 轨道采集器立即建造
    {
        public static bool Prefix(ConstructionSystem __instance)
        {
            PrebuildData[] prebuildPool = __instance.factory.prebuildPool;
            for (int index1 = 1; index1 < __instance.drones.cursor; ++index1)
            {
                ref DroneComponent drone = ref __instance.drones.buffer[index1];
                int prebuildId = -drone.targetObjectId;
                if (drone.targetObjectId < 0 && prebuildPool[prebuildId].protoId == 2105) // __instance.planet.type == EPlanetType.Gas
                    drone.stage = 3; // 到达位置
            }
            return true; // 运行原方法
        }
    }
}

