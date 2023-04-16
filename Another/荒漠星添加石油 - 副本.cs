using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace 荒漠星添加石油
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.desertaddoil";
        public const string NAME = "荒漠星添加石油";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll(typeof(荒漠星添加石油));
        }
    }


    class 荒漠星添加石油
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlanetAlgorithm), "GenerateVeins")]
        static bool Prefix(PlanetAlgorithm __instance)

        {
            PlanetData planet = (PlanetData)AccessTools.Field(typeof(PlanetAlgorithm), "planet").GetValue(__instance);
            if (planet.type != EPlanetType.Ocean)
            {
                __instance.GenerateVeins();
                函数(__instance);
            }
            return false;

        }





        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(PlanetAlgorithm11), "GenerateVeins")]
        //static bool Prefix11(PlanetAlgorithm11 __instance)// 橙晶荒漠
        //{
        //    // 函数(__instance);
        //    return false;
        //}



        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(PlanetAlgorithm12), "GenerateVeins")]
        //static bool Prefix12(PlanetAlgorithm12 __instance)// 极寒冻土
        //{
        //    // 函数(__instance);
        //    return false;
        //}


        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlanetAlgorithm13), "GenerateVeins")]
        static bool Prefix13(PlanetAlgorithm13 __instance)// 潘多拉沼泽
        {
            return false;
        }



        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(PlanetAlgorithm7), "GenerateVeins")]
        //static bool Prefix7(PlanetAlgorithm7 __instance)// 未知星球使用
        //{
        //    // 函数(__instance);
        //    return false;
        //}



        static void 函数(PlanetAlgorithm __instance)
        {
            PlanetData planet = (PlanetData)AccessTools.Field(typeof(PlanetAlgorithm), "planet").GetValue(__instance);
            lock (planet)
            {
                DotNet35Random dotNet35Random1 = new DotNet35Random(planet.seed);
                DotNet35Random dotNet35Random2 = new DotNet35Random(dotNet35Random1.Next());


                EVeinType 矿物类型 = EVeinType.Oil;
                System.Random 随机类 = new System.Random();
                int 矿簇数 = 随机类.Next(7, 15);
                int 矿脉数 = 1;
                int 矿脉容量 = 1;
                if (矿物类型 == EVeinType.Oil)
                {
                    矿脉数 = 随机类.Next(1, 1);
                    矿脉容量 = 随机类.Next(100000, 170000);
                }
                else
                {
                    矿脉数 = 随机类.Next(7, 25);
                    矿脉容量 = 随机类.Next(15000000, 27000000);
                }
                

                for (int zhu_index0 = 0; zhu_index0 < 矿簇数; ++zhu_index0)
                {
                    Vector3 矿簇位置 = Vector3.zero;

                    int zhu_num1 = 0;
                    while (zhu_num1++ < 200)
                    {
                        矿簇位置.x = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                        矿簇位置.y = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                        矿簇位置.z = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                        矿簇位置.Normalize();
                        float 矿簇高度 = planet.data.QueryHeight(矿簇位置);
                        if (矿物类型 == EVeinType.Oil)
                        {
                            if ((double)矿簇高度 >= (double)planet.radius)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if ((double)矿簇高度 >= (double)planet.radius + 0.5)
                            {
                                break;
                            }
                        }
                    }
                    for (int zhu_index1 = 0; zhu_index1 < 矿簇数; ++zhu_index1)
                    {
                        生成矿脉(planet, 矿物类型, 矿簇位置, 矿脉容量, dotNet35Random2);
                    }
                }
            }
        }

        static void 生成矿脉(PlanetData planet, EVeinType 矿物类型, Vector3 矿簇位置, int 矿脉容量, DotNet35Random dotNet35Random2)
        {
            VeinData vein = new VeinData();
            vein.type = 矿物类型;
            vein.groupIndex = (short)(planet.data.veinPool.Length + 1);
            if (vein.type == EVeinType.Oil)
            {
                vein.amount = (int)(矿脉容量 * DSPGame.GameDesc.oilAmountMultiplier);
            }
            else
            {
                vein.amount = (int)(矿脉容量 * DSPGame.GameDesc.resourceMultiplier);
            }

            int[] veinModelIndexs = PlanetModelingManager.veinModelIndexs;
            int[] veinModelCounts = PlanetModelingManager.veinModelCounts;
            int[] veinProducts = PlanetModelingManager.veinProducts;
            int 矿类型索引 = (int)矿物类型;
            vein.modelIndex = (short)dotNet35Random2.Next(veinModelIndexs[矿类型索引], veinModelIndexs[矿类型索引] + veinModelCounts[矿类型索引]);
            vein.productId = veinProducts[矿类型索引];
            vein.minerCount = 0;

            if (vein.type == EVeinType.Oil)
            {
                vein.pos = 矿簇位置;
                vein.pos = planet.aux.RawSnap(vein.pos);
            }
            else
            {
                Vector2 矿脉位置 = Vector2.zero;
                Vector2 vector2_1 = 矿脉位置;
                // if ((double)vector2_1.sqrMagnitude <= 36.0)
                double 矿簇范围 = dotNet35Random2.NextDouble() * Math.PI * 2.0;
                Vector2 vector2_2 = new Vector2((float)Math.Cos(矿簇范围), (float)Math.Sin(矿簇范围));
                vector2_2 += 矿脉位置 * 0.2f;
                vector2_2.Normalize();
                Vector2 vector2_3 = 矿脉位置 + vector2_2;

                Vector3 矿簇位置_normalized = 矿簇位置.normalized;
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, 矿簇位置_normalized);
                Vector3 vector3_1 = rotation * Vector3.right;
                Vector3 vector3_2 = rotation * Vector3.forward;
                float num1 = 2.1f / planet.radius;
                Vector3 vector3_3 = (vector2_3.x * vector3_1 + vector2_3.y * vector3_2) * num1;

                vein.pos = 矿簇位置_normalized + vector3_3;
            }

            float 矿脉高度 = planet.data.QueryHeight(vein.pos);
            planet.data.EraseVegetableAtPoint(vein.pos);
            vein.pos = vein.pos.normalized * 矿脉高度;
            if (planet.waterItemId == 0 || (double)矿脉高度 >= (double)planet.radius)
                planet.data.AddVeinData(vein);
        }

    }
}