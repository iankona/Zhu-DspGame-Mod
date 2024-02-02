using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using Steamworks;

namespace 继承物品
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.new_add_items";
        public const string NAME = "继承物品";
        public const string VERSION = "1.0.7";
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
            添加物品(__instance);
            //打印科技(__instance);
            //打印物品(__instance);
            //打印配方(__instance);

        }

        public static void 添加物品(UITechNode __instance)
        {


            if (__instance.techProto.ID == 1001) // 1001 电磁学
            {
                GameMain.mainPlayer.TryAddItemToPackage(2301,   3, 0, false); // 2301  采矿机
                GameMain.mainPlayer.TryAddItemToPackage(2203,   3, 0, false); // 2203  风力涡轮机
                GameMain.mainPlayer.TryAddItemToPackage(2001, 100, 0, false); // 2001  传送带
                GameMain.mainPlayer.TryAddItemToPackage(2302,   3, 0, false); // 2302  电弧熔炉
                GameMain.mainPlayer.TryAddItemToPackage(2303,   5, 0, false); // 2303  制造台 Mk.I
                GameMain.mainPlayer.TryAddItemToPackage(2101,  40, 0, false); // 2101  小型储物仓
                GameMain.mainPlayer.TryAddItemToPackage(2107,  20, 0, false); // 2107  物流配送器
                GameMain.mainPlayer.TryAddItemToPackage(5003, 200, 0, false); // 5003  配送运输机

                GameMain.history.UnlockTech(1001); // 1001	电磁学

                GameMain.history.UnlockTech(1601); // 1601	基础物流系统
                GameMain.history.UnlockTech(1401); // 1401	自动化冶金
                GameMain.history.UnlockTech(1002); // 1002  电磁矩阵
                GameMain.history.UnlockTech(1201); // 1201	基础制造

                GameMain.history.UnlockTech(4101); // 4101~4104 宇宙探索
                GameMain.history.UnlockTech(2101); // 2101~2106	机甲核心
                GameMain.history.UnlockTech(2201); // 2201~2208	机械骨骼
                GameMain.history.UnlockTech(2301); // 2301~2307	机舱容量
                GameMain.history.UnlockTech(2701); // 2701~2705	批量建造，蓝
                GameMain.history.UnlockTech(2501); // 2501~2506	能量回路
                GameMain.history.UnlockTech(2901); // 2901~2904	驱动引擎
            }
        }



        public static void 打印科技(UITechNode __instance) 
        {
            StreamWriter file = new StreamWriter("techs.py", false, Encoding.UTF8);

            file.WriteLine("LDB_techs_name = {");
            foreach (TechProto techProto in LDB.techs.dataArray)
            {
                string linestr = "    " +  "'" + techProto.name + "'" + ": " + (object)techProto.ID + ",";
                file.WriteLine(linestr);
            }
            file.WriteLine("}");

            file.WriteLine("LDB_techs_id = {");
            foreach (TechProto techProto in LDB.techs.dataArray)
            {
                string linestr = "    " + (object)techProto.ID + ":" + "'" + techProto.name + "'" + ",";
                file.WriteLine(linestr);
            }
            file.WriteLine("}");

            foreach (TechProto techProto in LDB.techs.dataArray)
                file.WriteLine((object)techProto.ID + " " + techProto.name);

            file.Close();
        }


        public static void 打印物品(UITechNode __instance)
        {
            StreamWriter file = new StreamWriter("items.py", false, Encoding.UTF8);

            file.WriteLine("LDB_items_name = {");
            foreach (ItemProto itemProto in LDB.items.dataArray)
            {
                string linestr = "    " + "'" + itemProto.name + "'" + ": " + (object)itemProto.ID + ",";
                file.WriteLine(linestr);
            }
            file.WriteLine("}");

            file.WriteLine("LDB_items_id = {");
            foreach (ItemProto itemProto in LDB.items.dataArray)
            {
                string linestr = "    " + (object)itemProto.ID + ":" + "'" + itemProto.name + "'" + ",";
                file.WriteLine(linestr);
            }
            file.WriteLine("}");

            foreach (ItemProto itemProto in LDB.items.dataArray)
                file.WriteLine((object)itemProto.ID + " " + itemProto.name);

            file.Close();
        }

        public static void 打印配方(UITechNode __instance)
        {
            StreamWriter file = new StreamWriter("recipes.py", false, Encoding.UTF8);
            ItemProto[] dataArray1 = LDB.items.dataArray;
            RecipeProto[] dataArray2 = LDB.recipes.dataArray;


            file.WriteLine("LDB_recipes_name = {");
            for (int index2 = 0; index2 < dataArray2.Length; ++index2)
            {
                RecipeProto recipeProto = dataArray2[index2];
                string idstr = (object)recipeProto.ID + ", ";
                string namestr = "'" + recipeProto.name + "', ";
                string spendstr = (object)((float)recipeProto.TimeSpend / 60.0f) + ", ";

                string itemstr = "[";
                foreach (int itemID in recipeProto.Items)
                {
                    ItemProto item1 = LDB.items.Select(itemID);
                    itemstr += "'" + item1.name + "', ";
                }
                itemstr += "], ";

                string itemcountstr = "[";
                foreach (int itemcount in recipeProto.ItemCounts) itemcountstr += (object)itemcount + ", ";
                itemcountstr += "], ";

                string resultstr = "[";
                foreach (int resultID in recipeProto.Results)
                {
                    ItemProto item2 = LDB.items.Select(resultID);
                    resultstr += "'" + item2.name + "', ";
                }
                resultstr += "], ";

                string resultcountstr = "[";
                foreach (int resultcount in recipeProto.ResultCounts) resultcountstr += (object)resultcount + ", ";
                resultcountstr += "], ";

                string linestr = "    '" + recipeProto.name + "': [" + idstr + namestr + spendstr + itemstr + itemcountstr + resultstr + resultcountstr + "],";
                file.WriteLine(linestr);
            }
            file.WriteLine("}");


            file.WriteLine("LDB_recipes_id = {");
            for (int index2 = 0; index2 < dataArray2.Length; ++index2)
            {
                RecipeProto recipeProto = dataArray2[index2];
                string idstr = (object)recipeProto.ID + ", ";
                string namestr = "'" + recipeProto.name + "', ";
                string spendstr = (object)((float)recipeProto.TimeSpend / 60.0f) + ", ";

                string itemstr = "[";
                foreach (int itemID in recipeProto.Items) itemstr += (object)itemID + ", ";
                itemstr += "], ";

                string itemcountstr = "[";
                foreach (int itemcount in recipeProto.ItemCounts) itemcountstr += (object)itemcount + ", ";
                itemcountstr += "], ";

                string resultstr = "[";
                foreach (int resultID in recipeProto.Results) resultstr += (object)resultID + ", ";
                resultstr += "], ";

                string resultcountstr = "[";
                foreach (int resultcount in recipeProto.ResultCounts) resultcountstr += (object)resultcount + ", ";
                resultcountstr += "], ";

                string linestr = "    '" + recipeProto.name +  "': [" + idstr + namestr + spendstr+ itemstr + itemcountstr + resultstr  + resultcountstr + "],";
                file.WriteLine(linestr);
            }
            file.WriteLine("}");




            file.Close();
        }
            






    }

}

//1001  铁矿
//1002  铜矿
//1003  硅石
//1004  钛石
//1005  石矿
//1006  煤矿
//1030  木材
//1031  植物燃料
//1011  可燃冰
//1012  金伯利矿石
//1013  分形硅石
//1014  光栅石
//1015  刺笋结晶
//1016  单极磁石
//1101  铁块
//1104  铜块
//1105  高纯硅块
//1106  钛块
//1108  石材
//1109  高能石墨
//1103  钢材
//1107  钛合金
//1110  玻璃
//1119  钛化玻璃
//1111  棱镜
//1112  金刚石
//1113  晶格硅
//1201  齿轮
//1102  磁铁
//1202  磁线圈
//1203  电动机
//1204  电磁涡轮
//1205  超级磁场环
//1206  粒子容器
//1127  奇异物质
//1301  电路板
//1303  处理器
//1305  量子芯片
//1302  微晶元件
//1304  位面过滤器
//1402  粒子宽带
//1401  电浆激发器
//1404  光子合并器
//1501  太阳帆
//1000  水
//1007  原油
//1114  精炼油
//1116  硫酸
//1120  氢
//1121  重氢
//1122  反物质
//1208  临界光子
//1801  液氢燃料棒
//1802  氘核燃料棒
//1803  反物质燃料棒
//1804  奇异湮灭燃料棒
//1115  塑料
//1123  石墨烯
//1124  碳纳米管
//1117  有机晶体
//1118  钛晶石
//1126  卡西米尔晶体
//1128  燃烧单元
//1129  爆破单元
//1130  晶石爆破单元
//1209  引力透镜
//1210  空间翘曲器
//1403  湮灭约束球
//1407  动力引擎
//1405  推进器
//1406  加力推进器
//5003  配送运输机
//5001  物流运输机
//5002  星际物流运输船
//1125  框架材料
//1502  戴森球组件
//1503  小型运载火箭
//1131  地基
//1141  增产剂 Mk.I
//1142  增产剂 Mk.II
//1143  增产剂 Mk.III
//1601  机枪弹箱
//1602  钛化弹箱
//1603  超合金弹箱
//1604  炮弹组
//1605  高爆炮弹组
//1606  晶石炮弹组
//1607  等离子胶囊
//1608  反物质胶囊
//1609  导弹组
//1610  超音速导弹组
//1611  引力导弹组
//1612  干扰胶囊
//1613  压制胶囊
//5101  原型机
//5102  精准无人机
//5103  攻击无人机
//5111  护卫舰
//5112  驱逐舰
//5201  黑雾矩阵
//5202  硅基神经元
//5203  物质重组器
//5204  负熵奇点
//5205  核心素
//5206  能量碎片
//2001  传送带
//2002  高速传送带
//2003  极速传送带
//2011  分拣器
//2012  高速分拣器
//2013  极速分拣器
//2014  集装分拣器
//2020  四向分流器
//2040  自动集装机
//2030  流速监测器
//2313  喷涂机
//2107  物流配送器
//2101  小型储物仓
//2102  大型储物仓
//2106  储液罐
//2303  制造台 Mk.I
//2304  制造台 Mk.II
//2305  制造台 Mk.III
//2318  重组式制造台
//2201  电力感应塔
//2202  无线输电塔
//2212  卫星配电站
//2203  风力涡轮机
//2204  火力发电厂
//2211  微型聚变发电站
//2213  地热发电站
//2301  采矿机
//2316  大型采矿机
//2306  抽水站
//2302  电弧熔炉
//2315  位面熔炉
//2319  负熵熔炉
//2307  原油萃取站
//2308  原油精炼厂
//2309  化工厂
//2317  量子化工厂
//2314  分馏塔
//2205  太阳能板
//2206  蓄电器
//2207  蓄电器（满）
//2311  电磁轨道弹射器
//2208  射线接收站
//2312  垂直发射井
//2209  能量枢纽
//2310  微型粒子对撞机
//2210  人造恒星
//2103  行星内物流运输站
//2104  星际物流运输站
//2105  轨道采集器
//2901  矩阵研究站
//2902  自演化研究站
//3001  高斯机枪塔
//3002  高频激光塔
//3003  聚爆加农炮
//3004  磁化电浆炮
//3005  导弹防御塔
//3006  干扰塔
//3007  信号塔
//3008  行星护盾发生器
//3009  战场分析基站
//3010  近程电浆塔
//6001  电磁矩阵
//6002  能量矩阵
//6003  结构矩阵
//6004  信息矩阵
//6005  引力矩阵
//6006  宇宙矩阵
//1099  沙土


//1 戴森球计划
//1001 电磁学
//1002 电磁矩阵
//1101 高效电浆控制
//1102 等离子萃取精炼
//1103 X射线裂解
//1104 重整精炼
//1111 能量矩阵
//1112 氢燃料棒
//1113 推进器
//1114 加力推进器
//1120 流体储存封装
//1121 基础化工
//1122 高分子化工
//1123 高强度晶体
//1124 结构矩阵
//1125 卡西米尔晶体
//1126 高强度玻璃
//1131 应用型超导体
//1132 高强度材料
//1133 粒子可控
//1134 重氢分馏
//1141 波函数干扰
//1142 微型粒子对撞机
//1143 奇异物质
//1144 人造恒星
//1145 可控湮灭反应
//1151 增产剂 Mk.I
//1152 增产剂 Mk.II
//1153 增产剂 Mk.III
//1201 基础制造
//1202 高速制造
//1203 量子打印
//1302 处理器
//1303 量子芯片
//1304 光子聚束采矿
//1305 亚微观量子纠缠
//1311 半导体材料
//1312 信息矩阵
//1401 自动化冶金
//1402 冶炼提纯
//1403 晶体冶炼
//1411 钢材冶炼
//1412 火力发电
//1413 钛矿冶炼
//1414 高强度钛合金
//1415 移山填海工程
//1416 微型核聚变发电
//1417 位面冶金
//1501 太阳能收集
//1502 光子变频
//1503 太阳帆轨道系统
//1504 射线接收站
//1505 行星电离层利用
//1506 狄拉克逆变机制
//1507 宇宙矩阵
//1508 任务完成 !
//1511 能量储存
//1512 星际电力运输
//1513 地热开采
//1521 高强度轻质结构
//1522 垂直发射井
//1523 戴森球应力系统
//1601 基础物流系统
//1602 改良物流系统
//1603 高效物流系统
//1604 行星物流系统
//1605 星际物流系统
//1606 气态行星开采
//1607 集装物流系统
//1608 配送物流系统
//1701 电磁驱动
//1702 磁悬浮
//1703 粒子磁力阱
//1704 引力波折射
//1705 引力矩阵
//1711 超级磁场发生器
//1712 卫星配电系统
//1801 武器系统
//1802 燃烧单元
//1803 爆破单元
//1804 晶石爆破单元
//1805 动力引擎
//1806 导弹防御塔
//1807 聚爆加农炮
//1808 信号塔
//1809 行星防御系统
//1810 干扰塔
//1811 磁化电浆炮
//1812 钛化弹箱
//1813 超合金弹箱
//1814 高爆炮弹组
//1815 超音速导弹组
//1816 晶石炮弹组
//1817 引力导弹组
//1818 反物质胶囊
//1819 原型机
//1820 精准无人机
//1821 攻击无人机
//1822 护卫舰
//1823 驱逐舰
//1824 压制胶囊
//1826 战场分析基站
//1901 数字模拟计算
//1902 物质重组
//1903 负熵递归
//1904 高密度可控湮灭

//2101 机甲核心
//2102 机甲核心
//2103 机甲核心
//2104 机甲核心
//2105 机甲核心
//2106 机甲核心

//2201 机械骨骼
//2202 机械骨骼
//2203 机械骨骼
//2204 机械骨骼
//2205 机械骨骼
//2206 机械骨骼
//2207 机械骨骼
//2208 机械骨骼

//2301 机舱容量
//2302 机舱容量
//2303 机舱容量
//2304 机舱容量
//2305 机舱容量
//2306 机舱容量
//2307 机舱容量

//2401 通讯控制
//2402 通讯控制
//2403 通讯控制
//2404 通讯控制
//2405 通讯控制
//2406 通讯控制
//2407 通讯控制

//2501 能量回路
//2502 能量回路
//2503 能量回路
//2504 能量回路
//2505 能量回路
//2506 能量回路

//2601 无人机引擎
//2602 无人机引擎
//2603 无人机引擎
//2604 无人机引擎
//2605 无人机引擎
//2606 无人机引擎

//2701 批量建造
//2702 批量建造
//2703 批量建造
//2704 批量建造
//2705 批量建造

//2801 能量护盾
//2802 能量护盾
//2803 能量护盾
//2804 能量护盾
//2805 能量护盾
//2806 能量护盾
//2807 能量护盾

//2901 驱动引擎
//2902 驱动引擎
//2903 驱动引擎
//2904 驱动引擎
//2905 驱动引擎
//2906 驱动引擎

//2951 自动标记重建
//2952 自动标记重建
//2953 自动标记重建
//2954 自动标记重建
//2955 自动标记重建
//2956 自动标记重建

//3101 太阳帆寿命
//3102 太阳帆寿命
//3103 太阳帆寿命
//3104 太阳帆寿命
//3105 太阳帆寿命
//3106 太阳帆寿命

//3201 射线传输效率
//3202 射线传输效率
//3203 射线传输效率
//3204 射线传输效率
//3205 射线传输效率
//3206 射线传输效率
//3207 射线传输效率
//3208 射线传输效率

//3301 分拣器货物叠加
//3302 分拣器货物叠加
//3303 分拣器货物叠加
//3304 分拣器货物叠加
//3305 分拣器货物叠加
//3306 分拣器货物集装

//3311 集装分拣器改良
//3312 集装分拣器改良
//3313 集装分拣器改良
//3314 集装分拣器改良
//3315 集装分拣器改良
//3316 集装分拣器改良

//4001 配送范围
//4002 配送范围
//4003 配送范围
//4004 配送范围
//4005 配送范围

//3401 运输船引擎
//3402 运输船引擎
//3403 运输船引擎
//3404 运输船引擎
//3405 运输船引擎
//3406 运输船引擎
//3407 运输船引擎

//3501 运输机舱扩容
//3502 运输机舱扩容
//3503 运输机舱扩容
//3504 运输机舱扩容
//3505 运输机舱扩容
//3506 运输机舱扩容
//3507 运输机舱扩容
//3508 运输机舱扩容
//3509 运输机舱扩容
//3510 运输机舱扩容

//3801 运输站集装物流
//3802 运输站集装物流
//3803 运输站集装物流

//3601 矿物利用
//3602 矿物利用
//3603 矿物利用
//3604 矿物利用
//3605 矿物利用
//3606 矿物利用

//3701 垂直建造
//3702 垂直建造
//3703 垂直建造
//3704 垂直建造
//3705 垂直建造
//3706 垂直建造

//3901 研究速度
//3902 研究速度
//3903 研究速度
//3904 研究速度

//4101 宇宙探索
//4102 宇宙探索
//4103 宇宙探索
//4104 宇宙探索

//5001 动能武器伤害
//5002 动能武器伤害
//5003 动能武器伤害
//5004 动能武器伤害
//5005 动能武器伤害
//5006 动能武器伤害

//5101 能量武器伤害
//5102 能量武器伤害
//5103 能量武器伤害
//5104 能量武器伤害
//5105 能量武器伤害
//5106 能量武器伤害

//5201 爆破武器伤害
//5202 爆破武器伤害
//5203 爆破武器伤害
//5204 爆破武器伤害
//5205 爆破武器伤害
//5206 爆破武器伤害

//5301 战斗无人机伤害
//5302 战斗无人机伤害
//5303 战斗无人机伤害
//5304 战斗无人机伤害
//5305 战斗无人机伤害

//5401 战斗无人机射速
//5402 战斗无人机射速
//5403 战斗无人机射速
//5404 战斗无人机射速
//5405 战斗无人机射速

//5601 战斗无人机耐久
//5602 战斗无人机耐久
//5603 战斗无人机耐久
//5604 战斗无人机耐久
//5605 战斗无人机耐久

//5701 行星护盾
//5702 行星护盾
//5703 行星护盾
//5704 行星护盾
//5705 行星护盾

//5801 地面编队扩容
//5802 地面编队扩容
//5803 地面编队扩容
//5804 地面编队扩容
//5805 地面编队扩容
//5806 地面编队扩容
//5807 地面编队扩容

//5901 太空编队扩容
//5902 太空编队扩容
//5903 太空编队扩容
//5904 太空编队扩容
//5905 太空编队扩容
//5906 太空编队扩容
//5907 太空编队扩容

//6001 结构强化
//6002 结构强化
//6003 结构强化
//6004 结构强化
//6005 结构强化
//6006 结构强化

//6101 电磁武器效果
//6102 电磁武器效果
//6103 电磁武器效果
//6104 电磁武器效果
//6105 电磁武器效果
//6106 电磁武器效果