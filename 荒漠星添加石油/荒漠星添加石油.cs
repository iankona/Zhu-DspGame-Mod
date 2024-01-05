using System;
using UnityEngine;
using BepInEx;
using HarmonyLib;
using System.Collections.Generic;



//public enum EPlanetType
//{
//    None,
//    Vocano,
//    Ocean,
//    Desert,
//    Ice,
//    Gas,
//}






namespace 荒漠星添加石油
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.desert_add_oil";
        public const string NAME = "荒漠星添加石油";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(荒漠星添加石油));
        }
    }

    class 荒漠星添加石油
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlanetAlgorithm), "GenerateVeins")]
        static void 星球(PlanetAlgorithm __instance) //  , 0~14
        {
            PlanetData planet = (PlanetData)Traverse.Create(__instance).Field("planet").GetValue(); // PlanetData planet = (PlanetData)AccessTools.Field(typeof(PlanetAlgorithm), "planet").GetValue(__instance);
            if (planet.type == EPlanetType.Ocean)
            {
                planet.data.veinPool = new VeinData[1024];// 写0报错，out of range
                planet.data.veinCursor = 1;
                //Console.WriteLine("荒漠星添加石油");
                //添加石油(planet);
                //Console.WriteLine("荒漠星添加煤炭");
                //添加煤炭(planet);

            }
            if (planet.type == EPlanetType.Desert) // 橙晶荒漠
            {
                Console.WriteLine("荒漠星添加石油");
                添加石油(planet);
                Console.WriteLine("荒漠星添加煤炭");
                添加煤炭(planet);
            }
            初始星系冻土星添加矿物(planet); // 冰原冻土，极寒冻土不在这个类
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlanetAlgorithm12), "GenerateVeins")]
        static void 星球12(PlanetAlgorithm12 __instance) // 极寒冻土
        {
            PlanetData planet = (PlanetData)Traverse.Create(__instance).Field("planet").GetValue();
            初始星系冻土星添加矿物(planet); // 极寒冻土
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlanetAlgorithm13), "GenerateVeins")]
        static bool 星球13(PlanetAlgorithm13 __instance)// 潘多拉沼泽
        {
            return false; // 拦截方法
        }


        // 星球14没有GenerateVeins方法


        static void 初始星系冻土星添加矿物(PlanetData planet)
        {

            bool 是初始星系 = 所在星系是初始星系(planet);
            bool 缺荒漠星球 = 所在星系没有荒漠星(planet);
            if (是初始星系 && 缺荒漠星球 && planet.type == EPlanetType.Ice) 
            {
                Console.WriteLine("冻土星添加石油");
                添加石油(planet);
                Console.WriteLine("冻土星添加煤炭");
                添加煤炭(planet);
            }

        }

        static bool 所在星系是初始星系(PlanetData planet)
        {
            bool result = false;
            for (int index1 = 0; index1 < planet.star.planetCount; ++index1)
            {
                PlanetData child_planet = planet.star.planets[index1];
                if (planet.galaxy.birthPlanetId == child_planet.id) 
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        static bool 所在星系没有荒漠星(PlanetData planet)
        {
            bool result = true;
            for (int index1 = 0; index1 < planet.star.planetCount; ++index1)
            {
                PlanetData child_planet = planet.star.planets[index1];
                if (child_planet.type == EPlanetType.Desert) // 橙晶荒漠
                {
                    result = false;
                    break;
                }
            }
            return result;
        }








        static Vector3[] 取原有矿脉位置列表(PlanetData planet)
        {
            if (planet.data.veinCursor == 1)
            {
                Vector3[] result1 = new Vector3[0];
                return result1;
            }

            int count1 = planet.data.veinCursor - 2;
            Vector3[] result2 = new Vector3[count1];

            for (int index1 = 0; index1 < count1; index1++)
            {
                VeinData vein = planet.data.veinPool[index1+1];
                result2[index1] = vein.pos;
            }
            return result2;

        }


        static bool 布尔都大于间隔(Vector3[] 矿脉位置列表, Vector3 vec1, float radius, float limit) // 距离
        {
            double 弧度 = (double)(limit/radius);
            double cos值0 = Math.Cos(弧度); // 在0~180度之间，角度越大，cos值越小。cos值更大，说明角度更小
            bool 都大于 = true;
            for (int index1 = 0; index1 < 矿脉位置列表.Length; index1++)
            {
                Vector3 vec2 = 矿脉位置列表[index1];
                double cos值1 = Vector3.Dot(vec2.normalized, vec1.normalized);
                if (cos值1 > cos值0)
                {
                    都大于 = false;
                    break;
                }
                    
            }
            if (都大于)
                return true;
            else
                return false;
        }


        static bool 最小值小于间隔(Vector3[] 矿脉位置列表, Vector3 vec1, float radius, float limit) // 距离
        {
            double 弧度 = (double)(limit / radius);
            double cos值0 = Math.Cos(弧度); // 在0~180度之间，角度越大，cos值越小。cos值更大，说明角度更小
            double cos值1 = -1.0;
            for (int index1 = 0; index1 < 矿脉位置列表.Length; index1++)
            {
                Vector3 vec2 = 矿脉位置列表[index1];
                double cos值2 = Vector3.Dot(vec2.normalized, vec1.normalized);
                if (cos值2 > cos值1)
                    cos值1 = cos值2;
            }
            if (cos值1 > cos值0)
                return true;
            else
                return false;
        }



        static Vector3[] 置矿组随机位置列表(PlanetData planet, float 距离, int num_left, int num_right)
        {

            Vector3[] 原有矿脉位置列表 = 取原有矿脉位置列表(planet);

            DotNet35Random dotNet35Random1 = new DotNet35Random(Guid.NewGuid().GetHashCode()); // dotNet35Random1.NextDouble() 0~1
            Vector3 位置1 = Vector3.zero;

            System.Random 随机类 = new System.Random(Guid.NewGuid().GetHashCode());
            int 矿组数 = 随机类.Next(num_left, num_right);
            Vector3[] 新增矿组位置列表 = new Vector3[矿组数];
            int count0 = 0;
            for (int index1 = 0; index1 < 矿组数; index1++)
            {
                int count1 = 0;
                while (count1++ < 10000)
                {
                    位置1.x = (float)(dotNet35Random1.NextDouble() * 2.0 - 1.0);
                    位置1.y = (float)(dotNet35Random1.NextDouble() * 2.0 - 1.0);
                    位置1.z = (float)(dotNet35Random1.NextDouble() * 2.0 - 1.0);
                    位置1 = 位置1.normalized * planet.radius;
                    bool 原有都大于 = 布尔都大于间隔(原有矿脉位置列表, 位置1, planet.radius, 距离);
                    bool 新增都大于 = 布尔都大于间隔(新增矿组位置列表, 位置1, planet.radius, 距离);
                    if (原有都大于 && 新增都大于)
                    { 
                        新增矿组位置列表[count0] = 位置1;
                        count0++;
                        break;
                    }

                }
            }
            Vector3[] result1 = new Vector3[count0];
            Array.Copy(新增矿组位置列表, 0, result1, 0, count0);
            return result1;

        }

        static double 取随机范围(int 矿脉数, float 距离)
        {
            double right = (double)距离;
            double 数量 = Math.Sqrt(矿脉数) + 1.0;
            right = 数量 * right;
            return right;
        }



        static Vector3[] 置矿脉随机位置列表(Vector3 position, PlanetData planet, float limit_left, float limit_right, int num_left, int num_right)
        {
            DotNet35Random dotNet35Random1 = new DotNet35Random(Guid.NewGuid().GetHashCode()); // dotNet35Random1.NextDouble() 0~1
            Vector3 位置1 = Vector3.zero;

            System.Random 随机类 = new System.Random(Guid.NewGuid().GetHashCode());
            int 矿脉数 = 随机类.Next(num_left, num_right);
            Vector3[] 新增矿脉位置列表 = new Vector3[矿脉数];
            新增矿脉位置列表[0] = position;
            int count0 = 1;
            double right = 取随机范围(矿脉数, limit_left);
            double half_right = right / 2.0;
            for (int index1 = 1; index1 < 矿脉数; index1++) 
            { 
                int count1 = 0;
                while (count1++ < 10000)
                {
                    位置1.x = (float)(dotNet35Random1.NextDouble() * right - half_right);
                    位置1.y = (float)(dotNet35Random1.NextDouble() * right - half_right);
                    位置1.z = (float)(dotNet35Random1.NextDouble() * right - half_right);
                    位置1 = position + 位置1;
                    位置1 = 位置1.normalized * planet.radius;
                    bool 大于left = 布尔都大于间隔(新增矿脉位置列表, 位置1, planet.radius, limit_left);
                    bool 小于right = 最小值小于间隔(新增矿脉位置列表, 位置1, planet.radius, limit_right);
                    if (大于left && 小于right)
                    {
                        新增矿脉位置列表[count0] = 位置1;
                        count0++;
                        break;
                    }

                }
            }
            Vector3[] result1 = new Vector3[count0];
            Array.Copy(新增矿脉位置列表, 0, result1, 0, count0);
            return result1;
        }









        static void 添加石油(PlanetData planet)
        {
            lock (planet)
            {
                int groupindex = 0;
                if (planet.data.veinCursor == 1)
                    groupindex = 1;
                else
                    groupindex = planet.data.veinPool[planet.data.veinCursor - 1].groupIndex + 1;

                System.Random 随机类 = new System.Random(Guid.NewGuid().GetHashCode());
                VeinData vein = new VeinData();
                Vector3[] 矿组随机位置列表 = 置矿组随机位置列表(planet, 40.0f, 7, 15);
                for (int index1 = 0; index1 < 矿组随机位置列表.Length; index1++)
                {
                    Vector3 zero1 = 矿组随机位置列表[index1];
                    Vector3 zero2 = planet.aux.RawSnap(zero1); // 吸附到网格，单位向量
                    float height2 = planet.data.QueryHeight(zero2);
                    if ((double)height2 >= (double)planet.radius)
                    {
                        int veintypeindex = (int)EVeinType.Oil;
                        vein.type = EVeinType.Oil;
                        vein.groupIndex = (short)groupindex;
                        vein.amount = 随机类.Next(150000, 350000);
                        vein.modelIndex = (short)随机类.Next(PlanetModelingManager.veinModelIndexs[veintypeindex], PlanetModelingManager.veinModelIndexs[veintypeindex] + PlanetModelingManager.veinModelCounts[veintypeindex]);
                        vein.productId = PlanetModelingManager.veinProducts[veintypeindex];
                        vein.minerCount = 0;
                        vein.pos = zero2.normalized * height2;
                        // planet.data.EraseVegetableAtPoint(vein.pos);// 清除位置上的树木等
                        planet.data.AddVeinData(vein);
                        groupindex++;
                    }
                }
            }
        }



        static void 添加煤炭(PlanetData planet)
        {
            lock (planet)
            {
                int groupindex = 0;
                if (planet.data.veinCursor == 1)
                    groupindex = 1;
                else
                    groupindex = planet.data.veinPool[planet.data.veinCursor - 1].groupIndex + 1;

                System.Random 随机类 = new System.Random(Guid.NewGuid().GetHashCode());
                VeinData vein = new VeinData();
                Vector3[] 矿组随机位置列表 = 置矿组随机位置列表(planet, 50.0f, 5, 11);
                for (int index1 = 0; index1 < 矿组随机位置列表.Length; index1++)
                {
                    Vector3 zero1 = 矿组随机位置列表[index1];
                    bool 矿组有添加 = false;
                    Vector3[] 矿脉随机位置列表 = 置矿脉随机位置列表(zero1, planet, 1.9f, 2.1f, 5, 27);
                    for (int index2 = 0; index2 < 矿脉随机位置列表.Length; index2++)
                    {
                        Vector3 zero2 = 矿脉随机位置列表[index2];
                        float height2 = planet.data.QueryHeight(zero2);
                        if ((double)height2 >= (double)planet.radius)
                        {
                            int veintypeindex = (int)EVeinType.Coal;
                            vein.type = EVeinType.Coal;
                            vein.groupIndex = (short)groupindex;
                            vein.amount = 1000000000; // 表示无限储量；
                            // vein.amount = 150; // 矿脉高度与容量有关，越多越高
                            vein.modelIndex = (short)随机类.Next(PlanetModelingManager.veinModelIndexs[veintypeindex], PlanetModelingManager.veinModelIndexs[veintypeindex] + PlanetModelingManager.veinModelCounts[veintypeindex]);
                            vein.productId = PlanetModelingManager.veinProducts[veintypeindex];
                            vein.minerCount = 0;
                            vein.pos = zero2.normalized * height2;
                            planet.data.EraseVegetableAtPoint(vein.pos);
                            planet.data.AddVeinData(vein);
                            矿组有添加 = true;
                        }
                    }

                    if (矿组有添加)
                        groupindex++;
                }
            }
        }
    }



}

