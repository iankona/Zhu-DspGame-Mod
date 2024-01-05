using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using UnityEngine;


namespace 背包物品堆叠倍数
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.dsp.player_package_item_stacksize_multiplier";
        public const string NAME = "背包物品堆叠倍数";
        public const string VERSION = "1.0.7";
        private const string GAME_PROCESS = "DSPGAME.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll(typeof(背包物品堆叠倍数));
            // new Harmony(GUID).PatchAll(typeof(AddItem1专项修改));
            new Harmony(GUID).PatchAll(typeof(AddItem2专项修改));
        }
    }

    class 背包物品堆叠倍数
    {
        public static int[] olditemStackCount;
        public static int[] newitemStackCount;


        [HarmonyPostfix]
        [HarmonyPatch(typeof(StorageComponent), "LoadStatic")]
        static void 函数0()
        {
            olditemStackCount = StorageComponent.itemStackCount;

            newitemStackCount = new int[12000];
            for (int index1 = 0; index1 < 12000; ++index1)
                newitemStackCount[index1] = 1000;

            int multiplier = 10;

            ItemProto[] dataArray = LDB.items.dataArray;
            for (int index2 = 0; index2 < dataArray.Length; ++index2)
            {
                newitemStackCount[dataArray[index2].ID] = multiplier * dataArray[index2].StackSize;
            }

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(StorageComponent), "AddItemStacked")]
        static bool 函数1(StorageComponent __instance)
        {
            if (__instance == GameMain.data.mainPlayer.package)
                StorageComponent.itemStackCount = newitemStackCount;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StorageComponent), "AddItemStacked")]
        static void 函数2(StorageComponent __instance)
        {
            if (__instance == GameMain.data.mainPlayer.package)
                StorageComponent.itemStackCount = olditemStackCount;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(StorageComponent), "Sort")]
        static bool 函数3(StorageComponent __instance)
        {
            if (__instance == GameMain.data.mainPlayer.package)
                StorageComponent.itemStackCount = newitemStackCount;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(StorageComponent), "Sort")]
        static void 函数4(StorageComponent __instance)
        {
            if (__instance == GameMain.data.mainPlayer.package)
                StorageComponent.itemStackCount = olditemStackCount;
        }
    }


    // 物流系统用，不是背包用
    //[HarmonyPatch]
    //class AddItem1专项修改
    //{
    //    public static MethodInfo TargetMethod()
    //    {
    //        var methods = typeof(StorageComponent).GetMethods();
    //        MethodInfo method_to_patch = methods[0];
    //        foreach (var method in methods) 
    //        {
    //            // Console.WriteLine(method);
    //            if (method.Name == "AddItem")
    //            {
    //                method_to_patch = method;
    //                break;
    //            }
                    
    //        }
    //        return method_to_patch;
    //    }
    //    public static bool Prefix(StorageComponent __instance)
    //    {
    //        if (__instance == GameMain.data.mainPlayer.package)
    //            StorageComponent.itemStackCount = 背包物品堆叠倍数.newitemStackCount;
    //        Console.WriteLine("Prefix... ...");
    //        return true;
    //    }
    //    public static void Postfix(StorageComponent __instance)
    //    {
    //        if (__instance == GameMain.data.mainPlayer.package)
    //            StorageComponent.itemStackCount = 背包物品堆叠倍数.olditemStackCount;
    //    }
        
    //}


    // 背包用
    // 不知道为什么，补丁一直访问不到函数，可能形参中的 out int 类型有关系，感恩上苍恩赐，最终找到迂回的方式，可以访问到想要打补丁修改的函数
    [HarmonyPatch]
    class AddItem2专项修改
    {
        public static MethodInfo TargetMethod()
        {
            var methods = typeof(StorageComponent).GetMethods();
            MethodInfo method_to_patch = methods[0];
            foreach (var method in methods)
            {
                // Console.WriteLine(method);
                if (method.Name == "AddItem")
                    method_to_patch = method;
            }
            return method_to_patch;
        }
        public static bool Prefix(StorageComponent __instance)
        {
            if (__instance == GameMain.data.mainPlayer.package)
                StorageComponent.itemStackCount = 背包物品堆叠倍数.newitemStackCount;
            // Console.WriteLine("Prefix... ...");
            return true;
        }
        public static void Postfix(StorageComponent __instance)
        {
            if (__instance == GameMain.data.mainPlayer.package)
                StorageComponent.itemStackCount = 背包物品堆叠倍数.olditemStackCount;
        }

    }

}

// 参考 [C#][HarmonyPatch]Manual patch internal class/anonymous method/d
// 参考 https://www.bilibili.com/read/cv22698875/
// 参考 https://github.com/pardeike/Harmony/issues/393 ， Neutron3529 commented on May 1, 2021
// 参考 https://github.com/pardeike/Harmony/issues/393#issuecomment-830340953