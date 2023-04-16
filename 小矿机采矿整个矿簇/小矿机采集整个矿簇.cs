using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;

namespace 小矿机采集整个矿簇
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.miningveingroup";
        public const string NAME = "小矿机采集整个矿簇";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(小矿机采集整个矿簇));
        }
    }


    [HarmonyPatch(typeof(MinerComponent), "ArrangeVeinArray")]
    class 小矿机采集整个矿簇
    {
        public static void Postfix(ref MinerComponent __instance)
        {
            int entityid = __instance.entityId;
            EntityData entity = GameMain.mainPlayer.factory.entityPool[entityid];
            if (entity.protoId != 2301)
            {
                return;
            }
            int groupindex = GameMain.mainPlayer.factory.veinPool[__instance.veins[0]].groupIndex;
            VeinGroup group = GameMain.mainPlayer.factory.veinGroups[groupindex];
            __instance.veins = new int[group.count];
            __instance.veinCount = group.count;
            __instance.currentVeinIndex = group.count-1;

            int currentindex = -1;
            foreach (var vein in GameMain.mainPlayer.factory.veinPool)
            {
                if (vein.groupIndex == groupindex)
                {
                    ++currentindex;
                    __instance.veins[currentindex] = vein.id;  
                }
            }

        }
    }
}


