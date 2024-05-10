

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

    public class RecipeInfoList
    {
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


        void setRecipeDataArray统计(RecipeStatistics item统计)
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


        public void set树枝(NodeTree itemTree) 
        {
            new记录列表();
            setRecipeDataArrayTree(itemTree);
            sumRecipeDataArray();
        }

        public void set统计(RecipeStatistics item统计)
        {
            new记录列表();
            setRecipeDataArray统计(item统计);
            sumRecipeDataArray();
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

        public void setRowStep汇总(RecipeStatistics item统计)
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


        public void copyRowAndRowStep(ref RecipeInfoList 统计汇总) 
        {
            int i = 0;
            foreach (RecipeInfo 记录1 in 统计汇总.记录列表)
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

    public class RecipeStatistics
    {
        public List<RecipeInfoList> 统计列表 = null;
        public RecipeInfoList 统计汇总 = null;

        public List<NodeCalculate> getCalcsByRecipeID(int recipeID)
        {
            List<NodeCalculate> calcs = new List<NodeCalculate>(16);
            foreach (RecipeInfoList 单树统计 in 统计列表)
            {
                RecipeInfo 记录 = 单树统计.Select(recipeID);
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
            foreach (RecipeInfoList 单树统计 in 统计列表)
            {
                RecipeInfo 记录 = 单树统计.Select(recipeID);
                if (记录 == null || 记录.dataArray == null)
                    continue;
                counts.Add(记录.dataArray.Count);
            }
            return counts;

        }

        public void set树枝列表(List<NodeTree> itemTreeList)
        {
            if (统计列表 == null)
                统计列表 = new List<RecipeInfoList>(3);

            统计列表.Clear();
            foreach (NodeTree itemTree in itemTreeList)
            {
                RecipeInfoList 单树统计 = new RecipeInfoList();
                单树统计.set树枝(itemTree);
                统计列表.Add(单树统计);
            }


            统计汇总 = new RecipeInfoList();
            统计汇总.set统计(this);

            统计汇总.setRowStep汇总(this); // 处理记录列表
            foreach (RecipeInfoList 单树统计 in 统计列表)
            {
                单树统计.copyRowAndRowStep(ref 统计汇总);
            }



            foreach (RecipeInfoList 单树统计 in 统计列表)
            {
                单树统计.new显示列表();
            }
            统计汇总.new显示列表();


            统计汇总.update列宽度();// 处理显示列表
            foreach (RecipeInfoList 单树统计 in 统计列表)
            {
                单树统计.update列宽度();
            }




        }

    }


}



