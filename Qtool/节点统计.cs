

using Qtool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using static Qtool.NodeTree;

namespace Qtool
{


    public class ItemInfo
    {
        public int row = -1;

        public ItemProto itemProto = null;
        public float value = 0;
    }


    public class RecipeInfo
    {
        public int rowSum = -1;
        public int rowStepMax = -1;

        public RecipeProto recipeProto;
        public List<NodeCalculate> dataArray;
        public NodeCalculate calcSum;


        public void calculateSum()
        {
            if (dataArray == null || dataArray.Count < 1) {
                dataArray = null;
                calcSum = null;
                return; 
            }

            List<NodeCalculate> dataArray1 = dataArray;
            List<NodeCalculate> dataArray2 = new List<NodeCalculate>(dataArray.Count);
            foreach (NodeCalculate calc in dataArray1)
            {
                if (calc == null)
                    continue;
                dataArray2.Add(calc);
            }

            if (dataArray2.Count < 1)
            {
                dataArray = null;
                calcSum = null;
                return;
            }

            dataArray = dataArray2;
            calcSum = new NodeCalculate();
            calcSum.recipeProto = recipeProto;
            calcSum.ResultValues.Clear();
            for (int i = 0; i < recipeProto.Results.Length; i++) 
            {
                float tempResultValue = 0;
                foreach(NodeCalculate calc in dataArray)
                {
                    if(calc == null || calc.ResultValues.Count < 1) { continue; } // 处理汇总计算Error
                    tempResultValue += calc.ResultValues[i];
                }
                calcSum.ResultValues.Add(tempResultValue);
            }

            calcSum.ItemValues.Clear();
            for (int i = 0; i < recipeProto.Items.Length; i++)
            {
                float tempItemValue = 0;
                foreach (NodeCalculate calc in dataArray)
                {
                    if (calc == null || calc.ItemValues.Count < 1) { continue; }
                    tempItemValue += calc.ItemValues[i];
                }
                calcSum.ItemValues.Add(tempItemValue);
            }

            calcSum.resultValue = 0.0f;
            foreach (NodeCalculate calc in dataArray)
            {
                if (calc == null)
                { continue; }
                calcSum.resultValue += calc.resultValue;
            }

        }



    }


    public class ItemInfoList
    {
        public NodeTree itemTree = null;
        List<ItemInfo> 产生列表 = null;
        List<ItemInfo> 消耗列表 = null;

        public List<ItemInfo> 显示产生列表 = null;
        public List<ItemInfo> 显示消耗列表 = null;

        public int maxRow = -1;


        public void set树枝(NodeTree itemTree1)
        {
            itemTree = itemTree1;
            new物品列表();
            sumItemDataArray(itemTree1);
        }


        public void set树枝汇总(List<ItemInfoList> item汇总列表)
        {
            new物品列表();
            sumItemDataArray汇总(item汇总列表);
        }



        public void updateForShow()
        {
            setRow();
            new显示列表();
        }

        public void updateForShow汇总(List<ItemInfoList> item汇总列表)
        {
            setRow汇总(item汇总列表);
            new显示列表();
            foreach (ItemInfoList item汇总 in item汇总列表)
            {
                item汇总.new显示列表();
            }
        }



        void new物品列表()
        {
            if (产生列表 == null)
                产生列表 = new List<ItemInfo>(LDB.items.Length);

            产生列表.Clear();
            foreach (ItemProto itemProto in LDB.items.dataArray)
            {
                ItemInfo 物品 = new ItemInfo();
                物品.itemProto = itemProto;
                产生列表.Add(物品);
            }

            if (消耗列表 == null)
                消耗列表 = new List<ItemInfo>(LDB.items.Length);

            消耗列表.Clear();
            foreach (ItemProto itemProto in LDB.items.dataArray)
            {
                ItemInfo 物品 = new ItemInfo();
                物品.itemProto = itemProto;
                消耗列表.Add(物品);
            }

        }
        public ItemInfo Select产生物品(int itemID)
        {
            foreach (ItemInfo 产生 in 产生列表)
            {
                if (产生.itemProto.ID == itemID)
                    return 产生;
            }
            return null;
        }

        public ItemInfo Select消耗物品(int itemID)
        {
            foreach (ItemInfo 消耗 in 消耗列表)
            {
                if (消耗.itemProto.ID == itemID)
                    return 消耗;
            }
            return null;
        }


        void sumItemDataArray(NodeTree itemTree)
        {
            foreach (NodeIteration node in itemTree.allNodes)
            {
                
                if (node.calc == null)
                    continue;

                if (node.calc.recipeProto == null) { continue; } // allNodes是有包含矿物配方,需要处理, calc有new, calc.recipeProto默认未null;

                NodeCalculate calc = node.calc;
                for (int i = 0; i < calc.recipeProto.Items.Length; i++)
                {
                    int itemID = calc.recipeProto.Items[i];
                    float itemValue1 = calc.ItemValues[i];
                    ItemInfo 消耗 = Select消耗物品(itemID);
                    消耗.value += itemValue1;
                }

                for (int i = 0; i < calc.recipeProto.Results.Length; i++)
                {
                    int resultID = calc.recipeProto.Results[i];
                    float resultValue2 = calc.ResultValues[i];
                    ItemInfo 产生 = Select产生物品(resultID);
                    产生.value += resultValue2;
                }

            }
        }


        void sumItemDataArray汇总(List<ItemInfoList> item汇总列表)
        {
            foreach (ItemProto itemProto in LDB.items.dataArray)
            {
                ItemInfo 产生1 = Select产生物品(itemProto.ID);
                ItemInfo 消耗1 = Select消耗物品(itemProto.ID);

                foreach (ItemInfoList item汇总 in item汇总列表)
                {
                    ItemInfo 产生2 = item汇总.Select产生物品(itemProto.ID);
                    ItemInfo 消耗2 = item汇总.Select消耗物品(itemProto.ID);
                    if (产生2.value > 0)
                        产生1.value += 产生2.value;
                    if (消耗2.value > 0)
                        消耗1.value += 消耗2.value;
                }

            }
        }




        void setRow()
        {
            int i = 0;
            foreach (ItemProto itemProto1 in LDB.items.dataArray)
            {
                ItemInfo 产生 = Select产生物品(itemProto1.ID);
                ItemInfo 消耗 = Select消耗物品(itemProto1.ID);

                if (产生.value > 0 || 消耗.value > 0)
                {
                    产生.row = i;
                    消耗.row = i;
                    i++;
                }
                
            }
            maxRow = i;
        }
        void setRow汇总(List<ItemInfoList> item汇总列表)
        {
            int i = 0;
            foreach (ItemProto itemProto2 in LDB.items.dataArray)
            {
                ItemInfo 产生 = Select产生物品(itemProto2.ID);
                ItemInfo 消耗 = Select消耗物品(itemProto2.ID);
                if (产生.value > 0 || 消耗.value > 0)
                {
                    产生.row = i;
                    消耗.row = i;

                    foreach(ItemInfoList item汇总 in item汇总列表)
                    {
                        item汇总.Select产生物品(itemProto2.ID).row = i;
                        item汇总.Select消耗物品(itemProto2.ID).row = i;
                    }

                    i++;
                }

            }
            maxRow = i;
            foreach (ItemInfoList item汇总 in item汇总列表)
                item汇总.maxRow = i;

        }

        public void new显示列表()
        {
            if (显示产生列表 == null)
                显示产生列表 = new List<ItemInfo>(LDB.items.Length);

            if (显示消耗列表 == null)
                显示消耗列表 = new List<ItemInfo>(LDB.items.Length);

            显示产生列表.Clear();
            foreach (ItemInfo 产生 in 产生列表)
            {
                if (产生.value > 0)
                    显示产生列表.Add(产生);
            }

            显示消耗列表.Clear();
            foreach (ItemInfo 消耗 in 消耗列表)
            {
                if (消耗.value > 0)
                    显示消耗列表.Add(消耗);
            }

        }
    }


    public class RecipeInfoList
    {
        public NodeTree itemTree = null;
        public List<RecipeInfo> 记录列表 = null;
        public List<RecipeInfo> 显示列表 = null;
        public int maxResultsCount = 0;
        public int maxItemsCount = 0;


        void new记录列表()
        {
            if (记录列表 == null)
            {
                记录列表 = new List<RecipeInfo>(LDB.recipes.Length);
            }
            记录列表.Clear();
            foreach (RecipeProto recipeProto in LDB.recipes.dataArray)
            {
                if (recipeProto.Items.Length < 1)
                    continue;
                RecipeInfo 记录 = new RecipeInfo();
                记录.recipeProto = recipeProto;
                记录列表.Add(记录);
            }

        }

        void setRecipeDataArrayTree(NodeTree itemTree) 
        {
            foreach (RecipeInfo 记录 in 记录列表)
            {
                记录.dataArray = itemTree.getCalcsByRecipeID(记录.recipeProto.ID);
            }
        }


        void setRecipeDataArray统计(配方统计处理 item统计)
        {
            foreach (RecipeInfo 记录 in 记录列表)
            {
                记录.dataArray = item统计.getCalcsByRecipeID(记录.recipeProto.ID);
            }
        }

        void sumRecipeDataArray()
        {
            foreach (RecipeInfo 记录 in 记录列表)
            {
                记录.calculateSum();
            }
        }


        public void set树枝(NodeTree itemTree2) 
        {
            itemTree = itemTree2;
            new记录列表();
            setRecipeDataArrayTree(itemTree2);
            sumRecipeDataArray();

        }

        public void set统计(配方统计处理 item统计)
        {
            new记录列表();
            setRecipeDataArray统计(item统计);
            sumRecipeDataArray();
        }

        public void updateForShow()
        {
            setRowStep();
            new显示列表();
            update列宽度();
        }

        public RecipeInfo Select(int recipeID) 
        {
            foreach (RecipeInfo 记录 in 记录列表)
            {
                if (记录.recipeProto.ID == recipeID)
                    return 记录;
            }
            return null;
        }


        public void setRowStep()
        {
            foreach (RecipeInfo 记录 in 记录列表)
            {
                if (记录.calcSum == null)
                    continue;
                记录.rowStepMax = 0;
            }

            foreach (RecipeInfo 记录 in 记录列表)
            {
                if (记录.calcSum == null)
                    continue;
                记录.rowStepMax = 记录.dataArray.Count;
            }

            int 累加行数 = 0;
            foreach (RecipeInfo 记录 in 记录列表)
            {
                if (记录.calcSum == null)
                    continue;
                记录.rowSum = 累加行数;
                累加行数 += 记录.rowStepMax + 1 + 1; // 空格1, 总和1
            }

        }

        public void setRowStep汇总(配方统计处理 item统计)
        {
            foreach (RecipeInfo 记录 in 记录列表)
            {
                if (记录.calcSum == null)
                    continue;
                记录.rowStepMax = 0;
            }

            foreach (RecipeInfo 记录 in 记录列表)
            {
                if (记录.calcSum == null)
                    continue;

                int stepMax = 记录.dataArray.Count;
                List<int> 数量列表 = item统计.getCountsByRecipeID(记录.recipeProto.ID);
                foreach (int step in 数量列表)
                {
                    if (step > stepMax) 
                        stepMax = step;
                }
                记录.rowStepMax = stepMax;
            }



            int 累加行数 = 0;
            foreach (RecipeInfo 记录 in 记录列表)
            {
                if (记录.calcSum == null)
                    continue;
                记录.rowSum = 累加行数;
                累加行数 += 记录.rowStepMax +1 +1; // 空格1, 总和1
                
            }

        }
        
        public void copyRowAndRowStep(ref RecipeInfoList 汇总) 
        {
            int i = 0;
            foreach (RecipeInfo 记录1 in 汇总.记录列表)
            {
                RecipeInfo 记录2 = 记录列表[i];
                i++;
                记录2.rowSum = 记录1.rowSum;
                记录2.rowStepMax = 记录1.rowStepMax;
            }
        }

        public void new显示列表()
        {

            显示列表 = new List<RecipeInfo>(LDB.recipes.Length);
            foreach (RecipeInfo 记录 in 记录列表)
            {
                if (记录.calcSum == null)
                    continue;
                显示列表.Add(记录);
                // Debug.Log(记录.recipeProto.Name+"::"+记录.calcSum.resultValue);
            }
        }


        public void update列宽度()
        {
            foreach (RecipeInfo 显示 in 显示列表)
            {
                if (显示.recipeProto.Results.Length > maxResultsCount)
                    maxResultsCount = 显示.recipeProto.Results.Length;
            }

            foreach (RecipeInfo 显示 in 显示列表)
            {
                if (显示.recipeProto.Items.Length > maxItemsCount)
                    maxItemsCount = 显示.recipeProto.Items.Length;
            }

        }

    }

    public class 配方统计处理
    {
        public List<RecipeInfoList> recipe汇总列表 = null;
        public RecipeInfoList 汇总 = null;

        public List<NodeCalculate> getCalcsByRecipeID(int recipeID)
        {
            List<NodeCalculate> calcs = new List<NodeCalculate>(16);
            foreach (RecipeInfoList recipe汇总 in recipe汇总列表)
            {
                RecipeInfo 记录 = recipe汇总.Select(recipeID);
                if (记录 == null || 记录.calcSum == null)
                    continue;
                calcs.Add(记录.calcSum);

            }
            if (calcs.Count > 0)
                return calcs;
            return null;

        }

        public List<int> getCountsByRecipeID(int recipeID)
        {
            List<int> counts = new List<int>(16);
            foreach (RecipeInfoList recipe汇总 in recipe汇总列表)
            {
                RecipeInfo 记录 = recipe汇总.Select(recipeID);
                if (记录 == null || 记录.dataArray == null)
                    continue;
                counts.Add(记录.dataArray.Count);
            }
            return counts;

        }

        public void set树枝列表(List<NodeTree> itemTreeList)
        {
            if (recipe汇总列表 == null)
                recipe汇总列表 = new List<RecipeInfoList>(3);

            recipe汇总列表.Clear();
            foreach (NodeTree itemTree in itemTreeList)
            {
                RecipeInfoList recipe汇总 = new RecipeInfoList();
                recipe汇总.set树枝(itemTree);
                recipe汇总列表.Add(recipe汇总);
            }


            汇总 = new RecipeInfoList();
            汇总.set统计(this);

            汇总.setRowStep汇总(this); // 处理记录列表
            foreach (RecipeInfoList recipe汇总 in recipe汇总列表)
            {
                recipe汇总.copyRowAndRowStep(ref 汇总);
            }



            foreach (RecipeInfoList recipe汇总 in recipe汇总列表)
            {
                recipe汇总.new显示列表();
            }
            汇总.new显示列表();


            汇总.update列宽度();// 处理显示列表
            foreach (RecipeInfoList recipe汇总 in recipe汇总列表)
            {
                recipe汇总.update列宽度();
            }




        }

    }



    public class 物品统计处理
    {
        public List<ItemInfoList> item汇总列表 = null;
        public ItemInfoList 汇总 = null;

        public void set树枝列表(List<NodeTree> itemTreeList)
        {
            if (item汇总列表 == null)
                item汇总列表 = new List<ItemInfoList>(3);

            item汇总列表.Clear();
            foreach (NodeTree itemTree in itemTreeList)
            {
                ItemInfoList item汇总 = new ItemInfoList();
                item汇总.set树枝(itemTree);
                item汇总列表.Add(item汇总);
            }


            汇总 = new ItemInfoList();
            汇总.set树枝汇总(item汇总列表);
            汇总.updateForShow汇总(item汇总列表);

        }

    }





}



