

using Qtool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static Qtool.NodeTree;

namespace Qtool
{
    public class ItemRecipeIndex
    {
        public ItemProto itemProto;
        public int recipeIndex;
        public List<RecipeProto> recipes; 
        public RecipeProto recipeProto ;
        public List<ItemProto> itemsProto ;

        public ItemRecipeIndex() 
        {
            itemProto = null;
            recipeIndex = -1;

            recipes = new List<RecipeProto>();
            recipes.Clear();

            recipeProto = null;

            itemsProto = new List<ItemProto>();
            itemsProto.Clear();
        }
    }

    public class 物品配方索引处理
    {
        public List<ItemRecipeIndex> 记录列表 = new List<ItemRecipeIndex>(1024);
        public List<ItemRecipeIndex> 显示列表 = new List<ItemRecipeIndex>(1024);
        public void 初始化()
        {
            记录列表.Clear();
            for (int i = 0; i < LDB.items.Length; i++)
            {
                ItemProto itemProto = LDB.items.dataArray[i];
                ItemRecipeIndex 记录 = new ItemRecipeIndex();
                记录.itemProto = itemProto; // 必须是dataArray
                记录列表.Add(记录); // 保证铁矿等矿物items也加进来

                if (itemProto.recipes.Count < 1)
                    continue;
                
                记录.recipeIndex = 0;
                记录.recipeProto = itemProto.recipes[0];

                记录.recipes.Clear();
                foreach (RecipeProto recipe in itemProto.recipes)
                    记录.recipes.Add(recipe);

                记录.itemsProto.Clear();
                foreach (int itemID in 记录.recipeProto.Items)
                    记录.itemsProto.Add(LDB.items.Select(itemID));
            }

            显示列表.Clear();
            foreach (ItemRecipeIndex 记录 in 记录列表)
            {
                if (记录.recipes.Count > 1)
                {
                    显示列表.Add(记录);
                }
            }





        }

        public ItemRecipeIndex Select(int itemID)
        {
            foreach (ItemRecipeIndex 记录 in 记录列表)
            {
                if (记录.itemProto.ID != itemID)
                    continue;
                return 记录;
            }
            return null;
        }




        public void setRecipeIndex(int itemID, int index)
        {

            ItemRecipeIndex 记录 = Select(itemID);

            if (记录 == null || index < -1 || index >= 记录.recipes.Count)
                return;

            if (index < 0)
            {
                记录.recipeIndex = -1;
                记录.recipeProto = null;
                记录.itemsProto.Clear();
                return;
            }

            记录.recipeIndex = -1;
            记录.recipeProto = null;
            记录.itemsProto.Clear();

            记录.recipeIndex = index;
            if (index >= 0)// Fix 未知的编译错误
                记录.recipeProto = 记录.recipes[index];

            foreach (int itemID1 in 记录.recipeProto.Items)
            {
                ItemProto itemProto1 = LDB.items.Select(itemID1);
                记录.itemsProto.Add(itemProto1);
            }
            Debug.Log("物品配方索引处理::setRecipeIndex::" + 记录.itemProto.Name + "::" + index);




        }


    }

    public class NodeTree
    {
        public NodeIteration root;
        public List<NodeIteration> allNodes = new List<NodeIteration>(2048);
        public List<NodeIteration> endNodes = new List<NodeIteration>(1024);

        public void Clear()
        {
            root = null;
            allNodes.Clear();
            endNodes.Clear();
        }




        public void setRootItem(ItemProto itemProto, ref 物品配方索引处理 物品配方索引)
        {
            root = NodeIteration.setRootItem(itemProto, ref 物品配方索引);
            if (root == null)
            {
                return;
            }
            getAllNodes();
            getEndNodes();
            setEndNodesColumn(); // == setAllNodesColumn();
            setAllNodesChildColumn();
            // 物品层级.setLayerNodesDepth(ref allNodes);

        }


        public void setRootResultValue(ItemProto itemProto, int itemValue)
        {
            if (root == null || root.itemProto.ID != itemProto.ID)
            {
                return;
            }
            NodeIteration.setRootResultValue(root, itemValue);
        }


        public List<NodeCalculate> getCalcsByRecipeID(int recipeID)
        {

            List<NodeCalculate> calcs = new List<NodeCalculate> (16);
            foreach (NodeIteration node in allNodes) 
            {
                if (node.calc.recipeProto == null) { continue; } // allNodes是有包含矿物配方,需要处理, calc有new, calc.recipeProto默认未null;
                if (node.calc.recipeProto.ID == recipeID)
                    calcs.Add(node.calc);
            }
            if (calcs.Count > 0) 
                return calcs;
            return null;
        }



        void getAllNodes()
        {
            if (root == null)
                return;
            allNodes.Clear();
            allNodes.Add(root);
            root.childNodes递归深度(ref allNodes);

        }

        void getEndNodes()
        {
            endNodes.Clear();
            for (int i = 0; i < allNodes.Count; i++)
            {
                NodeIteration node = allNodes[i];
                if (node.childNodes.Count <= 0)
                {
                    endNodes.Add(node);
                }
            }

        }

        void setEndNodesColumn()
        {
            int i = 2;
            foreach (NodeIteration end in endNodes)
            {
                NodeIteration.setEndItemColumn(end, i);
                i++;
                if (end.itemProto.ID == 1007 || end.itemProto.ID == 1011) // 都是矿物
                {
                    i++;
                }
                    
            }

        }

        void setAllNodesChildColumn()
        {
            
            foreach (NodeIteration node in allNodes)
            {

                node.itemColumns.Clear();
                foreach (NodeIteration child in node.childNodes)
                {
                    node.itemColumns.Add(child.column);
                }
            }
           
        }

        public NodeIteration getNodeFromItemID(int itemID)
        {
            foreach(NodeIteration node in allNodes)
            {
                if (node.itemProto.ID == itemID)
                    return node;
            }
            return null;
        }





    }
    public class NodeIteration
    {
        public NodeIteration parent = null;
        public int depth = -1;
        public int column = -1;
        public ItemProto itemProto = null;
        public int recipeIndex = -1;
        public RecipeProto recipeProto = null;
        public List<ItemProto> childItems = new List<ItemProto>(8);
        public List<int> itemColumns = new List<int>(8);
        public List<NodeIteration> childNodes = new List<NodeIteration>(8);

        //public List<int> resultColumns = new List<int>(8);

        public NodeCalculate calc = new NodeCalculate();




        public static void setEndItemColumn(NodeIteration endNodeIteration, int columnindex)
        {
            if (endNodeIteration == null)
                return;
            endNodeIteration.childNodes逆归树枝(columnindex);
        }

        public static NodeIteration setRootItem(ItemProto itemProto1, ref 物品配方索引处理 物品配方索引)
        {
            if (itemProto1 == null)
                return null;
            
            NodeIteration root = new NodeIteration();
            root.itemProto = itemProto1;
            root.getRecipeIndex(itemProto1.ID, ref 物品配方索引);
            root.depth = 0;
            root.childNodes递归更新(ref 物品配方索引);
            return root;
        }

        void getRecipeIndex(int itemID, ref 物品配方索引处理 物品配方索引) 
        {
            ItemRecipeIndex 记录 = 物品配方索引.Select(itemID);
            recipeIndex = 记录.recipeIndex;
            recipeProto = 记录.recipeProto;
            childItems.Clear();
            foreach (ItemProto itemProto1 in 记录.itemsProto)
                childItems.Add(itemProto1);

        }
        public void childNodes递归更新(ref 物品配方索引处理 物品配方索引)
        {
            if (recipeProto == null)
                return;

            childNodes.Clear();
            foreach (ItemProto itemProto1 in childItems) // Result会死循环
            {
                NodeIteration child = new NodeIteration();
                child.itemProto = itemProto1;
                child.getRecipeIndex(itemProto1.ID, ref 物品配方索引);
                child.parent = this;
                child.depth = depth + 1;
                childNodes.Add(child);
            }

            foreach(NodeIteration child in childNodes)
            {
                child.childNodes递归更新(ref 物品配方索引);
            }

        }
        public void childNodes递归深度(ref List<NodeIteration> allNodes)
        {
            for (int i = 0; i < childNodes.Count; i++)
            {
                allNodes.Add(childNodes[i]);
                childNodes[i].childNodes递归深度(ref allNodes);
            }
        }

        public void childNodes递归广度(ref List<NodeIteration> allNodes)
        {
            for (int i = 0; i < childNodes.Count; i++)
            {
                allNodes.Add(childNodes[i]);
            }

            for (int i = 0; i < childNodes.Count; i++)
            {
                childNodes[i].childNodes递归深度(ref allNodes);
            }
        }


        public void childNodes逆归树枝(int columnindex)
        {
            if (column >= 0)
                return;
            column = columnindex;
            if (parent == null)
                return;
            parent.childNodes逆归树枝(columnindex);
        }





        public static void setRootResultValue(NodeIteration root, float resultValue)
        {

            if (root == null || resultValue < 0)
            {
                return;
            }
            root.calc.resultValue = resultValue;
            root.calculate递归更新();

        }

        public void calculate递归更新()
        {

            if (recipeProto == null)
                return;

            calc.itemProto = itemProto;
            calc.recipeProto = recipeProto;
            calc.计算();

            for (int i = 0; i < childNodes.Count; i++)
            {
                childNodes[i].calc.resultValue = calc.ItemValues[i];
                childNodes[i].calculate递归更新();
            }

        }

    }


    public class 物品层级
    {
        public static int[] 一级物品ID = new int[] {
                        1101, //铁块
                        1102, //磁铁
                        1104, //铜块
                        1105, //高纯硅块
                        1106, //钛块
                        1108, //石材
                        1109, //高能石墨
                        1110, //玻璃
                        1114, //精炼油
                    };

        public static int[] 二级物品ID = new int[] {
                        1103, //钢材
                        1111, //棱镜
                        1113, //晶格硅
                        1201, //齿轮

                    };


        public static void setLayerNodesDepth(ref List<NodeIteration> allNodes)
        {
            List<NodeIteration> findNodes = findNodes一级(ref allNodes);
            int maxdepth = getMaxDepth(ref findNodes);
            setMaxDepth(ref findNodes, maxdepth);

            List<NodeIteration> findNodes2 = findNodes二级(ref allNodes);
            int maxdepth2 = getMaxDepth(ref findNodes2);
            setMaxDepth(ref findNodes2, maxdepth2);
        }


        static List<NodeIteration> findNodes一级(ref List<NodeIteration> allNodes)
        {
            List<NodeIteration> findNodes = new List<NodeIteration>(1024);
            foreach (int itemID in 一级物品ID)
            {
                foreach (NodeIteration node in allNodes)
                {
                    if (node.itemProto.ID == itemID)
                        findNodes.Add(node);
                }
            }
            return findNodes;
        }

        static List<NodeIteration> findNodes二级(ref List<NodeIteration> allNodes)
        {
            List<NodeIteration> findNodes = new List<NodeIteration>(1024);
            foreach (int itemID in 二级物品ID)
            {
                foreach (NodeIteration node in allNodes)
                {
                    if (node.itemProto.ID == itemID)
                        findNodes.Add(node);
                }
            }
            return findNodes;
        }

        static int getMaxDepth(ref List<NodeIteration> findNodes)
        {
            int maxdepth = 0;
            foreach (NodeIteration node in findNodes)
            {
                if (node.depth > maxdepth)
                    maxdepth = node.depth;
            }

            return maxdepth;

        }


        static void setMaxDepth(ref List<NodeIteration> findNodes, int maxdepth)
        {
            foreach (NodeIteration node in findNodes)
            {
                node.depth = maxdepth;
            }
        }
    }
    }





// 1001, //铁矿
// 1002, //铜矿
// 1003, //硅石
// 1004, //钛石
// 1005, //石矿
// 1006, //煤矿
// 1030, //木材
// 1031, //植物燃料
// 1011, //可燃冰
// 1012, //金伯利矿石
// 1013, //分形硅石
// 1014, //光栅石
// 1015, //刺笋结晶
// 1016, //单极磁石
// 1101, //铁块
// 1104, //铜块
// 1105, //高纯硅块
// 1106, //钛块
// 1108, //石材
// 1109, //高能石墨
// 1103, //钢材
// 1107, //钛合金
// 1110, //玻璃
// 1119, //钛化玻璃
// 1111, //棱镜
// 1112, //金刚石
// 1113, //晶格硅
// 1201, //齿轮
// 1102, //磁铁
// 1202, //磁线圈
// 1203, //电动机
// 1204, //电磁涡轮
// 1205, //超级磁场环
// 1206, //粒子容器
// 1127, //奇异物质
// 1301, //电路板
// 1303, //处理器
// 1305, //量子芯片
// 1302, //微晶元件
// 1304, //位面过滤器
// 1402, //粒子宽带
// 1401, //电浆激发器
// 1404, //光子合并器
// 1501, //太阳帆
// 1000, //水
// 1007, //原油
// 1114, //精炼油
// 1116, //硫酸
// 1120, //氢
// 1121, //重氢
// 1122, //反物质
// 1208, //临界光子
// 1801, //氢燃料棒
// 1802, //氘核燃料棒
// 1803, //反物质燃料棒
// 1804, //金色燃料棒
// 1115, //塑料
// 1123, //石墨烯
// 1124, //碳纳米管
// 1117, //有机晶体
// 1118, //钛晶石
// 1126, //卡西米尔晶体
// 1128, //燃烧单元
// 1129, //爆破单元
// 1130, //晶石爆破单元
// 1209, //引力透镜
// 1210, //空间翘曲器
// 1403, //湮灭约束球
// 1407, //动力引擎
// 1405, //推进器2
// 1406, //加力推进器
// 5003, //配送运输机
// 5001, //物流运输机
// 5002, //星际物流运输船
// 1125, //框架材料
// 1502, //戴森球组件
// 1503, //小型运载火箭
// 1131, //地基
// 1141, //增产剂 Mk.I
// 1142, //增产剂 Mk.II
// 1143, //增产剂 Mk.III
// 1601, //机枪弹箱
// 1602, //钛化弹箱
// 1603, //超合金弹箱
// 1604, //炮弹组
// 1605, //高爆炮弹组
// 1606, //晶石炮弹组
// 1607, //等离子胶囊
// 1608, //反物质胶囊
// 1609, //导弹组
// 1610, //超音速导弹组
// 1611, //引力导弹组
// 1612, //电磁干扰胶囊
// 1613, //电磁压制胶囊
// 5101, //地面战斗机-E型
// 5102, //地面战斗机-A型
// 5103, //地面战斗机-F型
// 5111, //太空战斗机-A型
// 5112, //太空战斗机-F型
// 5201, //存储单元
// 5202, //硅基神经元
// 5203, //物质重组器
// 5204, //负熵奇点
// 5205, //虚粒子
// 5206, //能量碎片
// 2001, //低速传送带
// 2002, //高速传送带
// 2003, //极速传送带
// 2011, //低速分拣器
// 2012, //高速分拣器
// 2013, //极速分拣器
// 2014, //集装分拣器
// 2020, //四向分流器
// 2040, //自动集装机
// 2030, //流速器
// 2313, //喷涂机
// 2107, //物流配送器
// 2101, //小型储物仓
// 2102, //大型储物仓
// 2106, //储液罐
// 2303, //制造台 Mk.I
// 2304, //制造台 Mk.II
// 2305, //制造台 Mk.III
// 2318, //制造台 Mk.IV
// 2201, //电力感应塔
// 2202, //无线输电塔
// 2212, //卫星配电站
// 2203, //风力涡轮机
// 2204, //火力发电厂
// 2211, //微型聚变发电站
// 2213, //地热发电站
// 2301, //采矿机
// 2316, //大型采矿机
// 2306, //抽水站
// 2302, //电弧熔炉
// 2315, //位面熔炉
// 2319, //熔炉 Mk.III
// 2307, //原油萃取站
// 2308, //原油精炼厂
// 2309, //化工厂
// 2317, //化工厂 Mk.II
// 2314, //分馏塔
// 2205, //太阳能板
// 2206, //蓄电器
// 2207, //蓄电器（满）
// 2311, //电磁轨道弹射器
// 2208, //射线接收站
// 2312, //垂直发射井
// 2209, //能量枢纽
// 2310, //微型粒子对撞机
// 2210, //人造恒星
// 2103, //物流运输站
// 2104, //星际物流运输站
// 2105, //轨道采集器
// 2901, //矩阵研究站
// 2902, //矩阵研究站 Mk.II
// 3001, //高斯机枪塔
// 3002, //高频激光塔
// 3003, //聚爆加农炮
// 3004, //磁化电浆炮
// 3005, //导弹防御塔
// 3006, //干扰塔
// 3007, //信标
// 3008, //护盾发生器
// 3009, //战场分析基站
// 3010, //地面电浆炮
// 6001, //电磁矩阵
// 6002, //能量矩阵
// 6003, //结构矩阵
// 6004, //信息矩阵
// 6005, //引力矩阵
// 6006, //宇宙矩阵
// 1099, //沙土


//List<int> itemDepths = new List<int>(1024);
//        for (int i = 0; i<allNodes.Count; i++)
//            itemDepths.Add(allNodes[i].depth);
//        HashSet<int> itemDepthsSet = new HashSet<int>(itemDepths);
//        foreach (var n in itemDepthsSet)
//        {

//        }



//string[] names = new string[] {
//                "mahesh",
//                "vikram",
//                "mahesh",
//                "mayur",
//                "suprotim",
//                "saket",
//                "manish"
//            };



//void setAllNodesDepth()
//{
//    List<int> itemIDs = new List<int>(1024);
//    for (int i = 0; i < allNodes.Count; i++)
//        itemIDs.Add(allNodes[i].itemProto.ID);
//    HashSet<int> itemIDSet = new HashSet<int>(itemIDs);
//    foreach (int itemID1 in itemIDSet)
//    {
//        List<NodeIteration> depthNodes = new List<NodeIteration>(1024);
//        foreach (NodeIteration node in allNodes)
//        {
//            if (node.itemProto.ID == itemID1)
//            {
//                depthNodes.Add(node);
//            }
//        }
//        int maxdepth = 0;
//        foreach (NodeIteration node in depthNodes)
//        {
//            if (node.depth > maxdepth)
//                maxdepth = node.depth;
//        }
//        foreach (NodeIteration node in depthNodes)
//        {
//            node.depth = maxdepth;
//        }
//    }

//}


//Debug.Log("物品的配方索引");
//for (int i = 0; i < LDB.items.Length; i++)
//{
//    ItemProto itemProto = LDB.items.dataArray[i];
//    Debug.Log(i + "::" + LDB.items[i].Name + "::" + LDB.items[i].ID + "::" + LDB.items.dataArray[i].recipes.Count);
//    Debug.Log("// " + LDB.items[i].ID + ", //" + LDB.items[i].Name);
//}
//Debug.Log("==============");