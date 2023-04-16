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
            // 函数(__instance);
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
            // 取得类型实例的私有变量
            PlanetData planet = (PlanetData)AccessTools.Field(typeof(PlanetAlgorithm), "planet").GetValue(__instance);
            //// int seed = (int)AccessTools.Field(type, "seed").GetValue(__instance);
            Vector3[] veinVectors = (Vector3[])Traverse.Create(__instance).Field("veinVectors").GetValue();
            EVeinType[] veinVectorTypes = (EVeinType[])Traverse.Create(__instance).Field("veinVectorTypes").GetValue();
            int veinVectorCount = (int)Traverse.Create(__instance).Field("veinVectorCount").GetValue();
            List<Vector2> tmp_vecs = (List<Vector2>)Traverse.Create(__instance).Field("tmp_vecs").GetValue();

            lock (planet)
            {
                ThemeProto themeProto = LDB.themes.Select(planet.theme);
                if (themeProto == null)
                {
                    return ;
                }
                DotNet35Random dotNet35Random1 = new DotNet35Random(planet.seed);
                dotNet35Random1.Next();
                dotNet35Random1.Next();
                dotNet35Random1.Next();
                dotNet35Random1.Next();
                int _birthSeed = dotNet35Random1.Next();
                DotNet35Random dotNet35Random2 = new DotNet35Random(dotNet35Random1.Next());

                PlanetRawData data = planet.data;
                float num1 = 2.1f / planet.radius;
                VeinProto[] veinProtos = PlanetModelingManager.veinProtos;
                int[] veinModelIndexs = PlanetModelingManager.veinModelIndexs;
                int[] veinModelCounts = PlanetModelingManager.veinModelCounts;
                int[] veinProducts = PlanetModelingManager.veinProducts;
                int[] destinationArray1 = new int[veinProtos.Length];
                float[] destinationArray2 = new float[veinProtos.Length];
                float[] destinationArray3 = new float[veinProtos.Length];
                if (themeProto.VeinSpot != null)
                    Array.Copy((Array)themeProto.VeinSpot, 0, (Array)destinationArray1, 1, Math.Min(themeProto.VeinSpot.Length, destinationArray1.Length - 1));
                if (themeProto.VeinCount != null)
                    Array.Copy((Array)themeProto.VeinCount, 0, (Array)destinationArray2, 1, Math.Min(themeProto.VeinCount.Length, destinationArray2.Length - 1));
                if (themeProto.VeinOpacity != null)
                    Array.Copy((Array)themeProto.VeinOpacity, 0, (Array)destinationArray3, 1, Math.Min(themeProto.VeinOpacity.Length, destinationArray3.Length - 1));
                float p = 1f;
                ESpectrType spectr = planet.star.spectr;
                switch (planet.star.type)
                {
                    case EStarType.MainSeqStar:
                        switch (spectr)
                        {
                            case ESpectrType.M:
                                p = 2.5f;
                                break;
                            case ESpectrType.K:
                                p = 1f;
                                break;
                            case ESpectrType.G:
                                p = 0.7f;
                                break;
                            case ESpectrType.F:
                                p = 0.6f;
                                break;
                            case ESpectrType.A:
                                p = 1f;
                                break;
                            case ESpectrType.B:
                                p = 0.4f;
                                break;
                            case ESpectrType.O:
                                p = 1.6f;
                                break;
                        }
                        break;
                    case EStarType.GiantStar:
                        p = 2.5f;
                        break;
                    case EStarType.WhiteDwarf:
                        p = 3.5f;
                        ++destinationArray1[9];
                        ++destinationArray1[9];
                        for (int index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.44999998807907104; ++index)
                            ++destinationArray1[9];
                        destinationArray2[9] = 0.7f;
                        destinationArray3[9] = 1f;
                        ++destinationArray1[10];
                        ++destinationArray1[10];
                        for (int index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.44999998807907104; ++index)
                            ++destinationArray1[10];
                        destinationArray2[10] = 0.7f;
                        destinationArray3[10] = 1f;
                        ++destinationArray1[12];
                        for (int index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.5; ++index)
                            ++destinationArray1[12];
                        destinationArray2[12] = 0.7f;
                        destinationArray3[12] = 0.3f;
                        break;
                    case EStarType.NeutronStar:
                        p = 4.5f;
                        ++destinationArray1[14];
                        for (int index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.64999997615814209; ++index)
                            ++destinationArray1[14];
                        destinationArray2[14] = 0.7f;
                        destinationArray3[14] = 0.3f;
                        break;
                    case EStarType.BlackHole:
                        p = 5f;
                        ++destinationArray1[14];
                        for (int index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.64999997615814209; ++index)
                            ++destinationArray1[14];
                        destinationArray2[14] = 0.7f;
                        destinationArray3[14] = 0.3f;
                        break;
                }
                for (int index1 = 0; index1 < themeProto.RareVeins.Length; ++index1)
                {
                    int rareVein = themeProto.RareVeins[index1];
                    float num2 = planet.star.index == 0 ? themeProto.RareSettings[index1 * 4] : themeProto.RareSettings[index1 * 4 + 1];
                    float rareSetting1 = themeProto.RareSettings[index1 * 4 + 2];
                    float rareSetting2 = themeProto.RareSettings[index1 * 4 + 3];
                    float num3 = rareSetting2;
                    float num4 = 1f - Mathf.Pow(1f - num2, p);
                    float num5 = 1f - Mathf.Pow(1f - rareSetting2, p);
                    float num6 = 1f - Mathf.Pow(1f - num3, p);
                    if (dotNet35Random1.NextDouble() < (double)num4)
                    {
                        ++destinationArray1[rareVein];
                        destinationArray2[rareVein] = num5;
                        destinationArray3[rareVein] = num5;
                        for (int index2 = 1; index2 < 12 && dotNet35Random1.NextDouble() < (double)rareSetting1; ++index2)
                            ++destinationArray1[rareVein];
                    }
                }
                bool 是出生星球 = planet.galaxy.birthPlanetId == planet.id;
                if (是出生星球)
                    planet.GenBirthPoints(data, _birthSeed);
                float f = planet.star.resourceCoef;
                bool infiniteResource = GameMain.data.gameDesc.isInfiniteResource;
                bool isRareResource = GameMain.data.gameDesc.isRareResource;
                if (是出生星球)
                    f *= 0.6666667f;
                else if (isRareResource)
                {
                    if ((double)f > 1.0)
                        f = Mathf.Pow(f, 0.8f);
                    f *= 0.7f;
                }
                float num7 = 1f * 1.1f;
                Array.Clear((Array)veinVectors, 0, veinVectors.Length);
                Array.Clear((Array)veinVectorTypes, 0, veinVectorTypes.Length);
                veinVectorCount = 0;
                Vector3 birthPoint;
                if (是出生星球)
                {
                    birthPoint = planet.birthPoint;
                    birthPoint.Normalize();
                    birthPoint *= 0.75f;
                }
                else
                {
                    birthPoint.x = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                    birthPoint.y = (float)dotNet35Random2.NextDouble() - 0.5f;
                    birthPoint.z = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                    birthPoint.Normalize();
                    birthPoint *= (float)(dotNet35Random2.NextDouble() * 0.4 + 0.2);
                }
                if (是出生星球)
                {
                    veinVectorTypes[0] = EVeinType.Iron;
                    veinVectors[0] = planet.birthResourcePoint0;
                    veinVectorTypes[1] = EVeinType.Copper;
                    veinVectors[1] = planet.birthResourcePoint1;
                    veinVectorCount = 2;
                }
                if (planet.type == EPlanetType.Desert)
                {
                    System.Random 随机数类例 = new System.Random();
                    int 随机数 = 随机数类例.Next(7, 15);
                    for (int 索引1 = 0; 索引1 < 随机数; ++索引1)
                    {
                        Vector3 zero1 = Vector3.zero;
                        int 计数1 = 0;
                        while (计数1++ < 200)
                        {
                            zero1.x = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                            zero1.y = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                            zero1.z = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                            zero1.Normalize();
                            float 高度1 = data.QueryHeight(zero1);
                            if ((double)高度1 >= (double)planet.radius + 0.5)
                            {
                                break;
                            }
                        }
                        veinVectorTypes[索引1] = EVeinType.Oil;
                        veinVectors[索引1] = zero1;
                    }

                    for (int 索引2 = 随机数; 索引2 < (随机数 + 随机数); ++索引2)
                    {
                        Vector3 zero2 = Vector3.zero;
                        int 计数2 = 0;
                        while (计数2++ < 200)
                        {
                            zero2.x = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                            zero2.y = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                            zero2.z = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                            zero2.Normalize();
                            float 高度2 = data.QueryHeight(zero2);
                            if ((double)高度2 >= (double)planet.radius)
                            {
                                break;
                            }
                        }
                        veinVectorTypes[索引2] = EVeinType.Coal;
                        veinVectors[索引2] = zero2;

                    }
                    
                    veinVectorCount = (int)随机数 * 2;
                }

                for (int index3 = 1; index3 < 15 && veinVectorCount < veinVectors.Length; ++index3)
                {
                    EVeinType eveinType = (EVeinType)index3;
                    int num8 = destinationArray1[index3];
                    if (num8 > 1)
                        num8 += dotNet35Random2.Next(-1, 2);
                    for (int index4 = 0; index4 < num8; ++index4)
                    {
                        int num9 = 0;
                        Vector3 zero = Vector3.zero;
                        bool flag2 = false;
                        while (num9++ < 200)
                        {
                            zero.x = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                            zero.y = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                            zero.z = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                            if (eveinType != EVeinType.Oil)
                                zero += birthPoint;
                            zero.Normalize();
                            float num10 = data.QueryHeight(zero);
                            if ((double)num10 >= (double)planet.radius && (eveinType != EVeinType.Oil || (double)num10 >= (double)planet.radius + 0.5))
                            {
                                bool flag3 = false;
                                float num11 = eveinType == EVeinType.Oil ? 100f : 196f;
                                for (int index5 = 0; index5 < veinVectorCount; ++index5)
                                {
                                    if ((double)(veinVectors[index5] - zero).sqrMagnitude < (double)num1 * (double)num1 * (double)num11)
                                    {
                                        flag3 = true;
                                        break;
                                    }
                                }
                                if (!flag3)
                                {
                                    flag2 = true;
                                    break;
                                }
                            }
                        }
                        if (flag2)
                        {
                            veinVectors[veinVectorCount] = zero;
                            veinVectorTypes[veinVectorCount] = eveinType;
                            ++veinVectorCount;
                            if (veinVectorCount == veinVectors.Length)
                                break;
                        }
                    }
                }
                data.veinCursor = 1;
                tmp_vecs.Clear();
                VeinData vein = new VeinData();
                for (int index6 = 0; index6 < veinVectorCount; ++index6)
                {
                    tmp_vecs.Clear();
                    Vector3 normalized = veinVectors[index6].normalized;
                    EVeinType veinVectorType = veinVectorTypes[index6];
                    int index7 = (int)veinVectorType;
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normalized);
                    Vector3 vector3_1 = rotation * Vector3.right;
                    Vector3 vector3_2 = rotation * Vector3.forward;
                    tmp_vecs.Add(Vector2.zero);
                    int num12 = Mathf.RoundToInt(destinationArray2[index7] * (float)dotNet35Random2.Next(20, 25));
                    if (veinVectorType == EVeinType.Oil)
                        num12 = 1;
                    float num13 = destinationArray3[index7];
                    if (是出生星球 && index6 < 2)
                    {
                        num12 = 6;
                        num13 = 0.2f;
                    }
                    int num14 = 0;
                    while (num14++ < 20)
                    {
                        int count = tmp_vecs.Count;
                        for (int index8 = 0; index8 < count && tmp_vecs.Count < num12; ++index8)
                        {
                            Vector2 vector2_1 = tmp_vecs[index8];
                            if ((double)vector2_1.sqrMagnitude <= 36.0)
                            {
                                double num15 = dotNet35Random2.NextDouble() * Math.PI * 2.0;
                                Vector2 vector2_2 = new Vector2((float)Math.Cos(num15), (float)Math.Sin(num15));
                                vector2_2 += tmp_vecs[index8] * 0.2f;
                                vector2_2.Normalize();
                                Vector2 vector2_3 = tmp_vecs[index8] + vector2_2;
                                bool flag4 = false;
                                for (int index9 = 0; index9 < tmp_vecs.Count; ++index9)
                                {
                                    vector2_1 = tmp_vecs[index9] - vector2_3;
                                    if ((double)vector2_1.sqrMagnitude < 0.85000002384185791)
                                    {
                                        flag4 = true;
                                        break;
                                    }
                                }
                                if (!flag4)
                                    tmp_vecs.Add(vector2_3);
                            }
                        }
                        if (tmp_vecs.Count >= num12)
                            break;
                    }
                    float num16 = f;
                    if (veinVectorType == EVeinType.Oil)
                        num16 = Mathf.Pow(f, 0.5f);
                    int num17 = Mathf.RoundToInt(num13 * 100000f * num16);
                    if (num17 < 20)
                        num17 = 20;
                    int num18 = num17 < 16000 ? Mathf.FloorToInt((float)num17 * (15f / 16f)) : 15000;
                    int minValue = num17 - num18;
                    int maxValue = num17 + num18 + 1;
                    for (int index10 = 0; index10 < tmp_vecs.Count; ++index10)
                    {
                        Vector3 vector3_3 = (tmp_vecs[index10].x * vector3_1 + tmp_vecs[index10].y * vector3_2) * num1;
                        vein.type = veinVectorType;
                        vein.groupIndex = (short)(index6 + 1);
                        vein.modelIndex = (short)dotNet35Random2.Next(veinModelIndexs[index7], veinModelIndexs[index7] + veinModelCounts[index7]);
                        vein.amount = Mathf.RoundToInt((float)dotNet35Random2.Next(minValue, maxValue) * num7);
                        vein.amount = veinVectorType == EVeinType.Oil ? Mathf.RoundToInt((float)vein.amount * DSPGame.GameDesc.oilAmountMultiplier) : Mathf.RoundToInt((float)vein.amount * DSPGame.GameDesc.resourceMultiplier);
                        if (vein.amount < 1)
                            vein.amount = 1;
                        if (infiniteResource && vein.type != EVeinType.Oil)
                            vein.amount = 1000000000;
                        if (planet.type == EPlanetType.Desert)
                        {
                            System.Random 随机数类例 = new System.Random();
                            int 随机数 = 随机数类例.Next(100000, 170000);
                            vein.amount = (int)(随机数 * DSPGame.GameDesc.oilAmountMultiplier);
                        }
                        vein.productId = veinProducts[index7];
                        vein.pos = normalized + vector3_3;
                        if (vein.type == EVeinType.Oil)
                            vein.pos = planet.aux.RawSnap(vein.pos);
                        vein.minerCount = 0;
                        float num19 = data.QueryHeight(vein.pos);
                        data.EraseVegetableAtPoint(vein.pos);
                        vein.pos = vein.pos.normalized * num19;
                        if (planet.waterItemId == 0 || (double)num19 >= (double)planet.radius)
                            data.AddVeinData(vein);
                    }
                }
                tmp_vecs.Clear();
            }
        }


    }







}

