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
                GameMain.mainPlayer.TryAddItemToPackage(2206,200, 0, false); // 2206 蓄电器
                GameMain.mainPlayer.TryAddItemToPackage(2209,  5, 0, false); // 2209 能量枢纽
                GameMain.mainPlayer.TryAddItemToPackage(2210,  5, 0, false); // 2210 人造恒星


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
                GameMain.history.UnlockTech(4103);
                // GameMain.history.UnlockTech(4104);

            }
        }
    }

}



//1001 铁矿
//1002 铜矿
//1003 硅石
//1004 钛石
//1005 石矿
//1006 煤矿
//1030 木材
//1031 植物燃料
//1011 可燃冰
//1012 金伯利矿石
//1013 分形硅石
//1014 光栅石
//1015 刺笋结晶
//1016 单极磁石
//1101 铁块
//1102 磁铁
//1103 钢材
//1104 铜块
//1105 高纯硅块
//1106 钛块
//1107 钛合金
//1108 石材
//1109 高能石墨
//1110 玻璃
//1119 钛化玻璃
//1111 棱镜
//1112 金刚石
//1113 晶格硅
//1201 齿轮
//1202 磁线圈
//1203 电动机
//1204 电磁涡轮
//1205 超级磁场环
//1206 粒子容器
//1127 奇异物质
//1301 电路板
//1303 处理器
//1305 量子芯片
//1302 微晶元件
//1304 位面过滤器
//1402 粒子宽带
//1401 电浆激发器
//1404 光子合并器
//1501 太阳帆
//1000 水
//1007 原油
//1114 精炼油
//1116 硫酸
//1120 氢
//1121 重氢
//1122 反物质
//1208 临界光子
//1801 液氢燃料棒
//1802 氘核燃料棒
//1803 反物质燃料棒
//1115 塑料
//1123 石墨烯
//1124 碳纳米管
//1117 有机晶体
//1118 钛晶石
//1126 卡西米尔晶体
//1209 引力透镜
//1210 空间翘曲器
//1403 湮灭约束球
//1405 推进器
//1406 加力推进器
//5001 物流运输机
//5002 星际物流运输船
//1125 框架材料
//1502 戴森球组件
//1503 小型运载火箭
//1131 地基
//1141 加速剂Mk.I
//1142 加速剂Mk.II
//1143 加速剂Mk.III
//2001 传送带
//2002 高速传送带
//2003 极速传送带
//2011 分拣器

//2012 高速分拣器
//2013 极速分拣器
//2020 四向分流器
//2101 小型储物仓
//2102 大型储物仓
//2106 储液罐
//2303 制造台Mk.I
//2304 制造台Mk.II
//2305 制造台Mk.III
//2201 电力感应塔
//2202 无线输电塔
//2212 卫星配电站
//2203 风力涡轮机
//2204 火力发电厂
//2211 微型聚变发电站
//2301 采矿机
//2302 电弧熔炉

//2315 位面熔炉
//2307 原油萃取站
//2308 原油精炼厂
//2306 抽水站
//2309 化工厂
//2314 分馏塔
//2313 喷涂机
//2205 太阳能板
//2206 蓄电器
//2207 蓄电器（满）
//2311 电磁轨道弹射器
//2208 射线接收站
//2312 垂直发射井
//2209 能量枢纽
//2310 微型粒子对撞机
//2210 人造恒星
//2103 行星内物流运输站
//2104 星际物流运输站
//2105 轨道采集器
//2901 矩阵研究站

//6001 电磁矩阵
//6002 能量矩阵
//6003 结构矩阵
//6004 信息矩阵
//6005 引力矩阵
//6006 宇宙矩阵