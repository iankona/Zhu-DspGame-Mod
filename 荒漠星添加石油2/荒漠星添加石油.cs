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
        static Vector3[] 取原有矿组位置列表(PlanetData planet)
        {
            if (planet.data.veinCursor == 1)
            {
                Vector3[] result1 = new Vector3[0];
                return result1;
            }

            int numgroup = planet.data.veinPool[planet.data.veinCursor-1].groupIndex;
            Vector3[] result2 = new Vector3[numgroup];

            for (int index1 = 1; index1 < planet.data.veinCursor; index1++)
            {
                VeinData vein = planet.data.veinPool[index1];
                result2[vein.groupIndex - 1] = vein.pos;
            }
            return result2;

        }

        static Vector3[] 取随机矿组位置列表(Vector3[] 原有矿组位置列表, PlanetData planet)
        {
            System.Random 随机类 = new System.Random(Guid.NewGuid().GetHashCode());
            int 矿组数 = 随机类.Next(7, 15);
            int num1 = 1000;
            Vector3[] temp1 = new Vector3[num1];
            for (int index1= 0; index1 < num1; index1++)
            {
                Vector3 zero = Vector3.zero;

                int count1 = 0;
                while (count1++ < 10)
                {
                    zero.x = (float)(随机类.Next(1, (int)(2 * planet.radius)) - (int)(planet.radius));
                    zero.y = (float)(随机类.Next(1, (int)(2 * planet.radius)) - (int)(planet.radius));
                    zero.z = (float)(随机类.Next(1, (int)(2 * planet.radius)) - (int)(planet.radius));
                }
                temp1[index1] = zero.normalized * planet.radius;
            }
            int num2 = 原有矿组位置列表.Length;
            int num3 = num1 + num2;
            Vector3[] temp2 = new Vector3[num3];
            Array.Copy(原有矿组位置列表, 0, temp2, 0, num2);
            int count2 = num2;
            for (int index2 = 0; index2 < num1; index2++)
            {
                Vector3 vec1 = temp1[index2];
                bool 距离都大于 = true;
                for (int index3 = 0; index3 < count2; index3++)
                {
                    Vector3 vec2 = temp2[index3];
                    float 弧长 = Mathf.Acos(Vector3.Dot(vec1.normalized, vec2.normalized)) * planet.radius;
                    if ( 弧长 < 75.0f)
                        距离都大于 = false;
                }
                if (距离都大于)
                {
                    temp2[count2] = temp1[index2];
                    count2++;
                }

                if (count2 - num2 > 矿组数)
                    break;
            }

            int count3 = count2 - num2;
            Vector3[] result = new Vector3[count3];
            Array.Copy(temp2, num2, result, 0, count3);
            return result;
        }

        static Vector3[] 取随机矿脉位置列表(Vector3 position, PlanetData planet)
        {
            System.Random 随机类 = new System.Random(Guid.NewGuid().GetHashCode());
            int 矿脉数 = 随机类.Next(7, 21);
            int num = 1024;
            Vector3[] temp1 = new Vector3[num];
            for (int index1 = 0; index1 < num; index1++)
            {
                Vector3 zero = Vector3.zero;

                int range = 10; // vector2_1.sqrMagnitude <= 36.0
                int half = 5;
                temp1[index1] = position;

                int count1 = 0;
                while (count1++ < 10)
                {
                    zero.x = 随机类.Next(1, range) - half;
                    zero.y = 随机类.Next(1, range) - half;
                    zero.z = 随机类.Next(1, range) - half;
                }
                temp1[index1] = (position + zero).normalized * planet.radius;
            }
            Vector3[] temp2 = new Vector3[num];
            temp2[0] = position;
            int count2 = 1;
            for (int index2 = 1; index2 < num; index2++)
            {
                Vector3 vec1 = temp1[index2];
                bool 距离都大于 = true;
                for (int index3 = 0; index3 < count2; index3++)
                {
                    Vector3 vec2 = temp2[index3];
                    float 弧长 = Mathf.Acos(Vector3.Dot(vec1.normalized, vec2.normalized)) * planet.radius;
                    if ( 弧长 < 1.6f)
                        距离都大于 = false;
                }
                if (距离都大于)
                {
                    temp2[count2] = temp1[index2];
                    count2++;
                }

                if (count2 > 矿脉数)
                    break;
            }
            Vector3[] result = new Vector3[count2];
            Array.Copy(temp2, 0, result, 0, count2);
            return result;
        }





        static void 添加石油(PlanetData planet)
        {
            lock (planet)
            {
                System.Random 随机类 = new System.Random();
                int veintypeindex = (int)EVeinType.Oil;
                VeinData vein = new VeinData();

                Vector3[] 原有矿组位置列表 = 取原有矿组位置列表(planet);
                Vector3[] 随机矿组位置列表 = 取随机矿组位置列表(原有矿组位置列表, planet);

                int groupindex = 0;
                if (原有矿组位置列表.Length == 0)
                    groupindex = 1;
                else
                    groupindex = 原有矿组位置列表.Length + 2;

                for (int index1 = 0; index1 < 随机矿组位置列表.Length; index1++)
                {
                    Vector3 zero1 = 随机矿组位置列表[index1];
                    Vector3 zero2 = planet.aux.RawSnap(zero1);
                    float height2 = planet.data.QueryHeight(zero2); // 吸附到网格，单位向量
                    if (  (double)height2 >= (double)planet.radius)
                    {
                        vein.type = EVeinType.Oil;
                        vein.groupIndex = (short)groupindex;
                        vein.amount = 随机类.Next(70000, 350000);
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
                Vector3[] 原有矿组位置列表 = 取原有矿组位置列表(planet);
                Vector3[] 随机矿组位置列表 = 取随机矿组位置列表(原有矿组位置列表, planet);

                int groupindex = 0;
                if (原有矿组位置列表.Length == 0)
                    groupindex = 1;
                else
                    groupindex = 原有矿组位置列表.Length + 2;

                System.Random 随机类 = new System.Random();
                VeinData vein = new VeinData();

                for (int index1 = 0; index1 < 随机矿组位置列表.Length; index1++)
                {
                    Vector3 zero1 = 随机矿组位置列表[index1];
                    bool 矿组有添加 = false;
                    Vector3[] 随机矿脉位置列表 = 取随机矿脉位置列表(zero1, planet);
                    for (int index2 = 0; index2 < 随机矿脉位置列表.Length; index2++ )
                    {
                        Vector3 zero2 = 随机矿脉位置列表[index2];
                        float height2 = planet.data.QueryHeight(zero2);
                        if ((double)height2 >= (double)planet.radius)
                        {
                            int veintypeindex = (int)EVeinType.Coal;
                            vein.type = EVeinType.Coal;
                            vein.groupIndex = (short)groupindex;
                            vein.amount = 1000000000; // 表示无限储量；
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