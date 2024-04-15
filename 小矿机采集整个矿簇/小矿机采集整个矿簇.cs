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



        [HarmonyPrefix]
        [HarmonyPatch(typeof(NearColliderLogic), "GetVeinsInAreaNonAlloc")]
        public static bool 函数2(NearColliderLogic __instance, ref Vector3 center, ref float areaRadius, ref int[] veinIds)
        {
            areaRadius = 18.0f; // 10 -> 18
            return true;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(MinerComponent), "IsTargetVeinInRange")] // 静态方法, 超过扇形范围返回false
        public static void 函数3(Vector3 vPos, Pose lPose, PrefabDesc desc, ref bool __result)
        {
            __result = true;
            Vector3 forward = lPose.forward;
            if (desc.veinMiner)
            {
                if (desc.isVeinCollector)
                {
                    Vector3 vector3 = lPose.position + forward * -10f;
                    Vector3 rhs = -forward;
                    Vector3 right = lPose.right;
                    Vector3 lhs = vPos - vector3;
                    double sqrMagnitude = (double)lhs.sqrMagnitude;
                    float num1 = Mathf.Abs(Vector3.Dot(lhs, rhs));
                    float num2 = Mathf.Abs(Vector3.Dot(lhs, right));
                    if (sqrMagnitude > 100.0 || (double)num1 > 7.75 || (double)num2 > 6.25) //  
                        __result = false;

                }
                else
                {
                    Vector3 vector3_1 = lPose.position + forward * -1.2f;
                    Vector3 rhs1 = -forward;
                    Vector3 up = lPose.up;
                    Vector3 rhs2 = vPos - vector3_1;
                    float f = Vector3.Dot(up, rhs2);
                    Vector3 vector3_2 = rhs2 - up * f;
                    double sqrMagnitude = (double)vector3_2.sqrMagnitude;
                    float num = Vector3.Dot(vector3_2.normalized, rhs1);
                    // 小采矿机的判断代码
                    if ( (double)num < 0.73000001907348633 || (double)Mathf.Abs(f) > 2.0) // sqrMagnitude > 961.0 / 16.0 ||
                        __result = false;
                }
            }

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