using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using BepInEx;
using HarmonyLib;
using UnityEngine;





namespace 小矿机采集整个矿簇
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.mining_vein_group";
        public const string NAME = "小矿机采集整个矿簇";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(小矿机采集整个矿簇));
        }
    }



    class 小矿机采集整个矿簇
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MinerComponent), "InitVeinArray")]
        public static bool 函数0(ref int vcnt)
        {
            vcnt = 32; // 常见的矿脉数量在20个左右
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(MinerComponent), "ArrangeVeinArray")]
        public static bool 函数1(MinerComponent __instance)
        {
            EntityData entity = GameMain.mainPlayer.factory.entityPool[__instance.entityId];
            if (entity.protoId == 2301)
            {
                VeinData[] veinPool = GameMain.mainPlayer.factory.veinPool;
                int veinCursor = GameMain.mainPlayer.factory.veinCursor;
                VeinGroup[] veinGroups = GameMain.mainPlayer.factory.veinGroups;

                int index0 = __instance.veins[0];
                VeinData vein = veinPool[index0];
                VeinGroup group = veinGroups[vein.groupIndex];
                int[] result = new int[group.count];
                int count0 = 0;
                for (int index1 = 1; index1 < veinCursor; ++index1)
                {
                    VeinData child_vein = veinPool[index1];
                    if (vein.groupIndex == child_vein.groupIndex)
                    {
                        result[count0] = child_vein.id;
                        count0++;
                    }
                }
                Array.Clear(__instance.veins, 0, 32);
                Array.Copy(result, 0, __instance.veins, 0, group.count);
            }
            return true;
        }

    }
 
}

//int[] 原先的矿脉索引列表 = new int[32];
//Array.Copy(__instance.veins, 0, 原先的矿脉索引列表, 0, 32);

//PlanetFactory factory = GameMain.mainPlayer.factory;
//for (int count1 = 0; count1 < group.count; ++count1)
//{
//    int index2 = result[count1];
//    if (原先的矿脉索引列表.Contains(index2))
//        continue;
//    factory.RefreshVeinMiningDisplay(index2, __instance.entityId, 0);
//}
//unsafe
//{
//    int* addr = &__instance.veinCount;
//    *addr = group.count;
//}
//Console.WriteLine(__instance.veinCount);