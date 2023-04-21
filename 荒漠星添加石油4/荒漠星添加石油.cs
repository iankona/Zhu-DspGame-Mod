using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using Compressions;
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


        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlanetAlgorithm), "GenerateVeins")]
        static void Postfix(PlanetAlgorithm __instance)
        {
            PlanetData planet = (PlanetData)AccessTools.Field(typeof(PlanetAlgorithm), "planet").GetValue(__instance);
            if (planet.type == EPlanetType.Ocean)
            {
                planet.data.veinPool = new VeinData[1024];
                planet.data.veinCursor = 1;
                // 添加石油(planet);
                // 添加煤炭(planet);
            }
            if (planet.type == EPlanetType.Desert)
            {
                添加石油(planet);
                添加煤炭(planet);
            }
            //if (planet.type == EPlanetType.Ice)
            //{
            //    添加石油(planet);
            //    添加煤炭(planet);
            //}

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

        static Vector3[] 取星球划分区域列表(PlanetData planet)
        {
            // blender3.5 ,正二十面体， 细分级别1；
            Vector3[] temp1 = new Vector3[62];
            temp1[0] = new Vector3(0.000000f, -0.742034f, -0.000000f);
            temp1[1] = new Vector3(0.536936f, -0.331848f, 0.390103f);
            temp1[2] = new Vector3(-0.205089f, -0.331848f, 0.631202f);
            temp1[3] = new Vector3(-0.663691f, -0.331848f, -0.000000f);
            temp1[4] = new Vector3(-0.205089f, -0.331848f, -0.631202f);
            temp1[5] = new Vector3(0.536936f, -0.331848f, -0.390103f);
            temp1[6] = new Vector3(0.205089f, 0.331848f, 0.631202f);
            temp1[7] = new Vector3(-0.536936f, 0.331848f, 0.390103f);
            temp1[8] = new Vector3(-0.536936f, 0.331848f, -0.390103f);
            temp1[9] = new Vector3(0.205089f, 0.331848f, -0.631202f);
            temp1[10] = new Vector3(0.663691f, 0.331848f, 0.000000f);
            temp1[11] = new Vector3(-0.000000f, 0.742034f, 0.000000f);
            temp1[12] = new Vector3(-0.120992f, -0.633535f, 0.372377f);
            temp1[13] = new Vector3(0.316765f, -0.633535f, 0.230141f);
            temp1[14] = new Vector3(0.195772f, -0.391546f, 0.602517f);
            temp1[15] = new Vector3(0.633529f, -0.391546f, -0.000000f);
            temp1[16] = new Vector3(0.316765f, -0.633535f, -0.230141f);
            temp1[17] = new Vector3(-0.391543f, -0.633535f, 0.000000f);
            temp1[18] = new Vector3(-0.512535f, -0.391546f, 0.372377f);
            temp1[19] = new Vector3(-0.120992f, -0.633535f, -0.372377f);
            temp1[20] = new Vector3(-0.512535f, -0.391546f, -0.372377f);
            temp1[21] = new Vector3(0.195772f, -0.391546f, -0.602517f);
            temp1[22] = new Vector3(0.708308f, -0.000000f, 0.230141f);
            temp1[23] = new Vector3(0.708308f, 0.000000f, -0.230141f);
            temp1[24] = new Vector3(0.000000f, -0.000000f, 0.744753f);
            temp1[25] = new Vector3(0.437756f, 0.000000f, 0.602517f);
            temp1[26] = new Vector3(-0.708308f, -0.000000f, 0.230141f);
            temp1[27] = new Vector3(-0.437756f, 0.000000f, 0.602517f);
            temp1[28] = new Vector3(-0.437756f, -0.000000f, -0.602517f);
            temp1[29] = new Vector3(-0.708308f, 0.000000f, -0.230141f);
            temp1[30] = new Vector3(0.437756f, 0.000000f, -0.602517f);
            temp1[31] = new Vector3(0.000000f, 0.000000f, -0.744753f);
            temp1[32] = new Vector3(0.512535f, 0.391546f, 0.372377f);
            temp1[33] = new Vector3(-0.195772f, 0.391546f, 0.602517f);
            temp1[34] = new Vector3(-0.633529f, 0.391546f, 0.000000f);
            temp1[35] = new Vector3(-0.195772f, 0.391546f, -0.602517f);
            temp1[36] = new Vector3(0.512535f, 0.391546f, -0.372377f);
            temp1[37] = new Vector3(0.120992f, 0.633535f, 0.372377f);
            temp1[38] = new Vector3(0.391543f, 0.633535f, 0.000000f);
            temp1[39] = new Vector3(-0.316765f, 0.633535f, 0.230141f);
            temp1[40] = new Vector3(-0.316765f, 0.633535f, -0.230141f);
            temp1[41] = new Vector3(0.120992f, 0.633535f, -0.372377f);
            temp1[42] = new Vector3(0.140893f, -0.596832f, 0.433616f);
            temp1[43] = new Vector3(0.455934f, -0.596832f, -0.000000f);
            temp1[44] = new Vector3(-0.368858f, -0.596832f, 0.267990f);
            temp1[45] = new Vector3(-0.368858f, -0.596832f, -0.267990f);
            temp1[46] = new Vector3(0.140893f, -0.596832f, -0.433616f);
            temp1[47] = new Vector3(0.737718f, -0.140893f, -0.000000f);
            temp1[48] = new Vector3(0.227967f, -0.140893f, 0.701606f);
            temp1[49] = new Vector3(-0.596826f, -0.140893f, 0.433616f);
            temp1[50] = new Vector3(-0.596826f, -0.140893f, -0.433616f);
            temp1[51] = new Vector3(0.227967f, -0.140893f, -0.701606f);
            temp1[52] = new Vector3(0.596826f, 0.140893f, 0.433616f);
            temp1[53] = new Vector3(-0.227967f, 0.140893f, 0.701606f);
            temp1[54] = new Vector3(-0.737718f, 0.140893f, -0.000000f);
            temp1[55] = new Vector3(-0.227967f, 0.140893f, -0.701606f);
            temp1[56] = new Vector3(0.596826f, 0.140893f, -0.433616f);
            temp1[57] = new Vector3(0.368858f, 0.596832f, 0.267990f);
            temp1[58] = new Vector3(-0.140893f, 0.596832f, 0.433616f);
            temp1[59] = new Vector3(-0.455934f, 0.596832f, 0.000000f);
            temp1[60] = new Vector3(-0.140893f, 0.596832f, -0.433616f);
            temp1[61] = new Vector3(0.368858f, 0.596832f, -0.267990f);

            Vector3[] result = new Vector3[62];
            for (int index1=0; index1 < 62; index1++)
            {
                result[index1] = temp1[index1].normalized * planet.radius;
            }
            return result;
        }

        static Vector3[] 取星球剩余区域列表(Vector3[] 星球划分区域列表, Vector3[] 原有矿组位置列表, PlanetData planet)
        {
            if (原有矿组位置列表.Length == 0)
            {
                Vector3[] result1 = new Vector3[星球划分区域列表.Length];
                Array.Copy(星球划分区域列表, 0, result1, 0, 星球划分区域列表.Length);
                return result1;
            }

            Vector3[] temp1 = new Vector3[星球划分区域列表.Length];
            int count1 = 0;
            for (int index1 = 0; index1 < 星球划分区域列表.Length; index1++)
            {
                bool 都大于 = true;
                Vector3 vec1 = 星球划分区域列表[index1];
                for (int index2 = 0; index2 < 原有矿组位置列表.Length; index2++)
                {
                    Vector3 vec2 = 原有矿组位置列表[index2];
                    double 弧长 = Vector3.Angle(vec1, vec2) / 180 * Math.PI * planet.radius;
                    if (弧长 < 70)
                    {
                        都大于 = false;
                        break;
                    }
                }
                if (都大于)
                {
                    temp1[count1] = vec1;
                    count1++;
                }
            }

            Vector3[] result2 = new Vector3[count1];
            Array.Copy(temp1, 0, result2, 0, count1);
            return result2;
        }


        static Vector3[] 取随机矿组位置列表(PlanetData planet, int left, int right)
        {
            System.Random 随机类 = new System.Random(Guid.NewGuid().GetHashCode());
            int 矿组数 = 随机类.Next(left, right);

            Vector3[] 星球划分区域列表 = 取星球划分区域列表(planet);
            Vector3[] 原有矿脉位置列表 = 取原有矿脉位置列表(planet);
            Vector3[] 星球剩余区域列表 = 取星球剩余区域列表(星球划分区域列表, 原有矿脉位置列表, planet);

            Vector3[] temp1 = new Vector3[星球划分区域列表.Length];
            for (int index1 = 0; index1 < 矿组数; index1++)
            {
                int id1 = 随机类.Next(0, 星球剩余区域列表.Length);
                Vector3 position = 星球剩余区域列表[id1];
                Vector3[] temp2 = new Vector3[星球划分区域列表.Length-1];
                Array.Copy(星球剩余区域列表, 0, temp2, 0, id1);
                int count2 = 星球剩余区域列表.Length - (id1 + 1);
                Array.Copy(星球剩余区域列表, id1 + 1, temp2, id1, count2);
                星球剩余区域列表 = temp2;

                Vector3 vec1 = 置星球随机位置(position, planet, 0.1, 4.2);
                temp1[index1] = vec1;
            }
            Vector3[] result = new Vector3[矿组数];
            Array.Copy(temp1, 0, result, 0, 矿组数);
            return result;
        }

        static Vector3[] 取随机矿脉位置列表(Vector3 position, PlanetData planet)
        {
            System.Random 随机类 = new System.Random(Guid.NewGuid().GetHashCode());
            int 矿脉数 = 随机类.Next(11, 25);
            Vector3[] temp1 = new Vector3[矿脉数];
            temp1[0] = position;
            int count1 = 1;
            for (int index1 = 1; index1 < 矿脉数; index1++)
            {
                int zeroindex = 随机类.Next(0, count1);
                Vector3 zero = temp1[zeroindex];
                int count2 = 0;
                while (count2++ < 10240)
                {
                    Vector3 vec1 = 置星球随机位置(zero, planet, 1.70, 1.70+0.05);
                    if (vec1.sqrMagnitude < planet.radius * planet.radius-0.5)
                        continue;
                    bool 都大于 = 布尔都大于间隔(temp1, count1, vec1, planet, 1.70);
                    if (都大于)
                    {
                        temp1[count1] = vec1;
                        count1++;
                        break;
                    }

                }

            }
            Vector3[] result = new Vector3[count1];
            Array.Copy(temp1, 0, result, 0, count1);
            return result;
        }

        static Vector3 置星球随机位置(Vector3 position, PlanetData planet, double left, double right)
        {
            DotNet35Random dotNet35Random1 = new DotNet35Random(Guid.NewGuid().GetHashCode());
            Vector3 zero = Vector3.zero;

            double half = right;
            double range = 2 * right;

            int count1 = 0;
            while (count1++ < 100)
            {
                zero.x = (float)(dotNet35Random1.NextDouble() * range - half);
                zero.y = (float)(dotNet35Random1.NextDouble() * range - half);
                zero.z = (float)(dotNet35Random1.NextDouble() * range - half);
            }

            Vector3 vec1 = Vector3.zero;
            if (left * left < zero.sqrMagnitude && zero.sqrMagnitude < half * half)
                vec1 = zero;
            else
                vec1 = zero.normalized * (float)right;

            return (position + vec1).normalized * planet.radius;

        }

        static bool 布尔都大于间隔(Vector3[] temp1, int currutindex1, Vector3 vec1, PlanetData planet, double limit)
        {
            bool 都大于 = true;
            for (int index1 = 0; index1 < currutindex1; index1++)
            {
                Vector3 vec2 = temp1[index1];
                double 弧长 = (Vector3.Angle(vec1, vec2) / 180) * Math.PI * planet.radius;
                if (弧长 < limit) // 1.65
                    都大于 = false;
            }
            if (都大于)
                return true;
            else 
                return false;
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
                Vector3[] 随机矿组位置列表 = 取随机矿组位置列表(planet, 7, 15);
                for (int index1 = 0; index1 < 随机矿组位置列表.Length; index1++)
                {
                    Vector3 zero1 = 随机矿组位置列表[index1];
                    Vector3 zero2 = planet.aux.RawSnap(zero1); // 吸附到网格，单位向量
                    float height2 = planet.data.QueryHeight(zero2); 
                    if (  (double)height2 >= (double)planet.radius)
                    {
                        int veintypeindex = (int)EVeinType.Oil;
                        vein.type = EVeinType.Oil;
                        vein.groupIndex = (short)groupindex;
                        vein.amount = 随机类.Next(150000, 350000);
                        vein.modelIndex = (short)随机类.Next(PlanetModelingManager.veinModelIndexs[veintypeindex], PlanetModelingManager.veinModelIndexs[veintypeindex] + PlanetModelingManager.veinModelCounts[veintypeindex]);
                        vein.productId = PlanetModelingManager.veinProducts[veintypeindex];
                        vein.minerCount = 0;
                        vein.pos = zero2.normalized * height2;
                        planet.data.EraseVegetableAtPoint(vein.pos);// 清除位置上的树木等
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
                Vector3[] 随机矿组位置列表 = 取随机矿组位置列表(planet, 5, 11);
                for (int index1 = 0; index1 < 随机矿组位置列表.Length; index1++)
                {
                    Vector3 zero1 = 随机矿组位置列表[index1];
                    bool 矿组有添加 = false;
                    Vector3[] 随机矿脉位置列表 = 取随机矿脉位置列表(zero1, planet);
                    for (int index2 = 0; index2 < 随机矿脉位置列表.Length; index2++)
                    {
                        Vector3 zero2 = 随机矿脉位置列表[index2];
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