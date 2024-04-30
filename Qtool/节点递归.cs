

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static Qtool.NodeTree;

namespace Qtool
{
    public class NodeRecipeIndex
    {
        public int itemID = 0;
        public int recipeIndex = 0;
    }

    public class ListRecipeIndex
    {
        public List<NodeRecipeIndex> listNodes = new List<NodeRecipeIndex>(1024);
        public ListRecipeIndex()
        {
            // Debug.Log("物品的配方索引");
            for (int i = 0; i < LDB.items.Length; i++)
            {
                NodeRecipeIndex nodeRecipeIndex = new NodeRecipeIndex();
                nodeRecipeIndex.itemID = LDB.items.dataArray[i].ID; // 必须是dataArray
                nodeRecipeIndex.recipeIndex = 0;
                listNodes.Add(nodeRecipeIndex);
                // Debug.Log(i+ "::" + LDB.items[i].Name + "::" + LDB.items[i].ID + "::" + LDB.items.dataArray[i].recipes.Count);
                // Debug.Log("// " + LDB.items[i].ID + ", //" + LDB.items[i].Name);
            }
            // Debug.Log("==============");

        }

        public NodeRecipeIndex Select(int ID)
        {
            for (int i = 0; i < listNodes.Count; i++)
            {
                if (listNodes[i].itemID == ID)
                    return listNodes[i];
            }
            return (NodeRecipeIndex)null;
        }

        public int getRecipeIndex(int ID)
        {
            for (int i = 0; i < listNodes.Count; i++)
            {
                if (listNodes[i].itemID == ID)
                    return listNodes[i].recipeIndex;
            }
            return 0;
        }
    }

    public class NodeTree
    {
        public ListRecipeIndex listRecipeIndexs = new ListRecipeIndex();
        public NodeIteration root;
        public List<NodeIteration> allNodes = new List<NodeIteration>(2048);
        public List<NodeIteration> endNodes = new List<NodeIteration>(1024);


        public void setRootItem(ItemProto itemProto)
        {
            root = NodeIteration.setRootItem(itemProto, ref listRecipeIndexs);
            if (root == null)
            {
                return;
            }
            getAllNodes();
            getEndNodes();
            setEndNodesColumn();
            NodeIteration.setRootValue(root, 61, ref listRecipeIndexs);
            物品层级.setLayerNodesDepth(ref allNodes);

        }

        public void updateTree()
        {

            if (root == null)
            {
                return;
            }
            root = NodeIteration.setRootItem(root.itemProto, ref listRecipeIndexs);
            getAllNodes();
            getEndNodes();
            setEndNodesColumn();
        }



        void getAllNodes()
        {
            if (root == null)
                return;
            allNodes.Clear();
            allNodes.Add(root);
            root.children递归深度(ref allNodes);

        }

        void getEndNodes()
        {
            endNodes.Clear();
            for (int i = 0; i < allNodes.Count; i++)
            {
                NodeIteration node = allNodes[i];
                if (node.children.Count <= 0)
                {
                    endNodes.Add(node);
                }
            }

        }

        void setEndNodesColumn()
        {
            int i = 0;
            foreach (NodeIteration end in endNodes)
            {
                NodeIteration.setEndItemColumn(end, i);
                i++;
                //Debug.Log("End节点::" + end.itemProto.ID + "::" + end.itemProto.Name);
                //[Info: Unity Log] End节点::1007::原油
                //[Info: Unity Log] End节点::1006::煤矿
                //[Info: Unity Log] End节点::1007::原油
                //[Info: Unity Log] End节点::1000::水
                if (end.itemProto.ID == 1007 || end.itemProto.ID == 1011)
                {
                    i++;
                    //Debug.Log("有精炼油或石墨烯::" + end.itemProto.ID + "::" + end.itemProto.Name);
                }
                    
            }

            // 1114, //精炼油
            // 1007, //原油

            // 1123, //石墨烯
            // 1011, //可燃冰

        }

    }
    public class NodeIteration
    {
        public NodeIteration parent;
        public int depth = -1;
        public int column = -1;
        public ItemProto itemProto;
        public List<NodeIteration> children = new List<NodeIteration>(8);

        public float itemValue;
        public List<float> resultMinCounts = new List<float>(8);
        public RecipeProto recipeProto;
        public float 单个配方分钟产量 = 0;
        public float 配方数量 = 0;
        public int 设备ID;
        public List<int> itemColumns = new List<int>(8);
        public List<float> itemMinCounts = new List<float>(8);


        public static void setEndItemColumn(NodeIteration endNodeIteration, int columnindex)
        {
            if (endNodeIteration == null)
                return;
            endNodeIteration.children逆归树枝(columnindex);
        }

        public static NodeIteration setRootItem(ItemProto itemProto, ref ListRecipeIndex listRecipeIndexs)
        {
            if (itemProto == null)
            {
                return (NodeIteration)null;
            }
            int recipeIndex = listRecipeIndexs.getRecipeIndex(itemProto.ID);
            // Debug.Log("新建树::"+ itemProto.Name);
            NodeIteration rootNodeIteration = new NodeIteration();
            // rootNodeIteration.uuid = Guid.NewGuid();
            rootNodeIteration.depth = 0;
            rootNodeIteration.itemProto = itemProto;
            rootNodeIteration.children递归更新(ref listRecipeIndexs);
            return rootNodeIteration;
        }

        public void children递归更新(ref ListRecipeIndex listRecipeIndexs)
        {
            if (itemProto.recipes.Count <= 0)
                return;

            int recipeIndex = listRecipeIndexs.getRecipeIndex(itemProto.ID);
            RecipeProto recipeProto = itemProto.recipes[recipeIndex];

            children.Clear();
            for (int i = 0; i < recipeProto.Items.Length; i++) // Result会死循环
            {
                int itemID = recipeProto.Items[i];
                NodeIteration childNodeIteration = new NodeIteration();
                childNodeIteration.parent = this;
                childNodeIteration.depth = depth + 1;
                childNodeIteration.itemProto = LDB.items.Select(itemID);
                children.Add(childNodeIteration);
            }

            for (int i = 0; i < children.Count; i++)
            {
                // Debug.Log(children[i].depth+"::"+children[i].itemProto.Name);
                children[i].children递归更新(ref listRecipeIndexs);
            }

        }
        public void children递归深度(ref List<NodeIteration> allNodes)
        {
            for (int i = 0; i < children.Count; i++)
            {
                NodeIteration childNodeIteration = children[i];
                allNodes.Add(childNodeIteration);
                childNodeIteration.children递归深度(ref allNodes);
            }
        }

        public void children递归广度(ref List<NodeIteration> allNodes)
        {
            for (int i = 0; i < children.Count; i++)
            {
                NodeIteration childNodeIteration = children[i];
                allNodes.Add(childNodeIteration);
            }

            for (int i = 0; i < children.Count; i++)
            {
                NodeIteration childNodeIteration = children[i];
                childNodeIteration.children递归深度(ref allNodes);
            }
        }


        public void children逆归树枝(int columnindex)
        {
            if (column >= 0)
                return;
            column = columnindex;
            if (parent == null)
                return;
            parent.children逆归树枝(columnindex);
        }





        public static void setRootValue(NodeIteration root, float itemValue, ref ListRecipeIndex listRecipeIndexs)
        {

            if (root == null || itemValue < 0)
            {
                return;
            }
            root.itemValue = itemValue;
            root.calculate递归更新(ref listRecipeIndexs);

        }

        public void calculate递归更新(ref ListRecipeIndex listRecipeIndexs)
        {

            int recipeIndex = listRecipeIndexs.getRecipeIndex(this.itemProto.ID);
            // Debug.Log("新建树::"+ itemProto.Name);





            if (this.itemProto.recipes.Count <= 0)
            {
                recipeProto = null;
            }
            else
            {
                recipeProto = this.itemProto.recipes[recipeIndex];
                float 每分钟执行次数 = 3600 / recipeProto.TimeSpend; // (60 / (TimeSpend / 60)

                int index1 = 0;
                for (int i = 0; i < recipeProto.Results.Length; i++)
                {
                    int itemID = recipeProto.Results[i];
                    if (itemID == this.itemProto.ID)
                    {
                        index1 = i;
                        break;
                    }
                }
                单个配方分钟产量 = recipeProto.ResultCounts[index1] * 每分钟执行次数;
                配方数量 = itemValue / 单个配方分钟产量;

                this.resultMinCounts.Clear();
                for (int i = 0; i < recipeProto.ResultCounts.Length; i++)
                {
                    float minValue = recipeProto.ResultCounts[i] * 每分钟执行次数 * 配方数量;
                    this.resultMinCounts.Add(minValue);
                }
                this.itemMinCounts.Clear();
                for (int i = 0; i < recipeProto.ItemCounts.Length; i++)
                {
                    float minValue = recipeProto.ItemCounts[i] * 每分钟执行次数 * 配方数量;
                    this.itemMinCounts.Add(minValue);
                    this.children[i].itemValue = minValue;
                }

                // Debug.Log("节点::" + recipeProto.Name + "::" + itemValue);
                this.itemColumns.Clear();
                for (int i = 0; i < recipeProto.ItemCounts.Length; i++)
                {
                    this.itemColumns.Add(this.children[i].column);
                    this.children[i].calculate递归更新(ref listRecipeIndexs);
                }


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