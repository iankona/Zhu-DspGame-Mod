using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;



namespace 继承物品
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.additems";
        public const string NAME = "继承物品";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Awake()
        {
            new Harmony(GUID).PatchAll();

        }
    }

    [HarmonyPatch(typeof(UITechNode), "DoStartTech")]
    class 继承物品
    { 
        public static void Postfix(UITechNode __instance)
        {
            // int itemID = 5002; // 5002 星际物流运输船
            // int count = 50;
            if (__instance.techProto.ID == 1001) // 1001 电磁学
            {

                // 2301~2306	机舱容量
                GameMain.history.UnlockTech(2301);
                GameMain.history.UnlockTech(2302);
                GameMain.history.UnlockTech(2303);

                // GameMain.mainPlayer.TryAddItemToPackage(itemID, count, 0, false);
                GameMain.mainPlayer.TryAddItemToPackage(2201, 50, 0, false); // 2201 电力感应塔
                GameMain.mainPlayer.TryAddItemToPackage(2202,  5, 0, false); // 2202 无线输电塔

                // GameMain.mainPlayer.TryAddItemToPackage(2001,300, 0, false); // 2001 传送带
                // GameMain.mainPlayer.TryAddItemToPackage(2011,200, 0, false); // 2011 分拣器
                GameMain.mainPlayer.TryAddItemToPackage(2003, 1500, 0, false); // 2003 极速传送带
                GameMain.mainPlayer.TryAddItemToPackage(2013,  600, 0, false); // 2013 极速分拣器

                GameMain.mainPlayer.TryAddItemToPackage(2101, 25, 0, false); // 2101 小型储物仓
                GameMain.mainPlayer.TryAddItemToPackage(2020, 25, 0, false); // 2020 四向分流器
                GameMain.mainPlayer.TryAddItemToPackage(2107, 25, 0, false); // 2107 物流配送器
                GameMain.mainPlayer.TryAddItemToPackage(5003,250, 0, false); // 5003 配送运输机

                GameMain.mainPlayer.TryAddItemToPackage(2301, 10, 0, false); // 2301 采矿机
                GameMain.mainPlayer.TryAddItemToPackage(2302,100, 0, false); // 2302 电弧熔炉
                GameMain.mainPlayer.TryAddItemToPackage(2304, 50, 0, false); // 2304 制造台Mk.II

                GameMain.mainPlayer.TryAddItemToPackage(2103,  20, 0, false); // 2103 行星内物流运输站
                GameMain.mainPlayer.TryAddItemToPackage(5001, 600, 0, false); // 5001 物流运输机

                GameMain.mainPlayer.TryAddItemToPackage(2104,  5, 0, false); // 2104 星际物流运输站
                GameMain.mainPlayer.TryAddItemToPackage(2105, 80, 0, false); // 2105 轨道采集器
                GameMain.mainPlayer.TryAddItemToPackage(5002, 50, 0, false); // 5002 星际物流运输船

                GameMain.mainPlayer.TryAddItemToPackage(2203, 35, 0, false); // 2203 风力涡轮机
                GameMain.mainPlayer.TryAddItemToPackage(2316, 25, 0, false); // 2316 大型采矿机

                GameMain.mainPlayer.TryAddItemToPackage(1803,100, 0, false); // 1803 反物质燃料棒 // 机甲用


                GameMain.history.UnlockTech(1001); // 1001	电磁学
                GameMain.history.UnlockTech(1002); // 1002  电磁矩阵

                GameMain.history.UnlockTech(1601); // 1601	基础物流系统
                GameMain.history.UnlockTech(1401); // 1401	自动化冶金
                GameMain.history.UnlockTech(1402); // 1402	冶炼提纯

                GameMain.history.UnlockTech(1201); // 1201	基础制造工艺制造台Ⅰ
                GameMain.history.UnlockTech(1202); // 1202	高级制造工艺制造台Ⅱ

                GameMain.history.UnlockTech(1501); // 1501	太阳能收集

                GameMain.history.UnlockTech(1102); // 1102	等离子萃取精炼 // 氢
                GameMain.history.UnlockTech(1121); // 1121  基础化工 // 硫酸
                GameMain.history.UnlockTech(1131); // 1131  应用型超导体 // 可燃冰


                // 2101~2106	机甲核心
                GameMain.history.UnlockTech(2101);
                GameMain.history.UnlockTech(2102);
                // GameMain.history.UnlockTech(2103); // 能量回路前置

                // 2201~2208	机械骨骼
                GameMain.history.UnlockTech(2201);


                // 2401~2407	通讯控制
                GameMain.history.UnlockTech(2401);
                GameMain.history.UnlockTech(2402);

                // 2501~2506	能量回路
                GameMain.history.UnlockTech(2501);
                GameMain.history.UnlockTech(2502);
                // GameMain.history.UnlockTech(2503); // 第3级，烧石墨，机甲滞空+小飞机建造，能量条不会见底

                // 2701~2705	批量建造，蓝图
                GameMain.history.UnlockTech(2701);
                GameMain.history.UnlockTech(2702);


                // 2601~2606	无人机引擎
                GameMain.history.UnlockTech(2601); // 没必要升，到第5级，小飞机飞行速度，才能稍微追上机甲滞空飞行速度

                // 2901~2904	驱动引擎
                GameMain.history.UnlockTech(2901);
                GameMain.history.UnlockTech(2902);

                // 4101~4104	宇宙探索
                GameMain.history.UnlockTech(4101);
                GameMain.history.UnlockTech(4102);




            }
        }
    }

}



