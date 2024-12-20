

// Decompiled with JetBrains decompiler
// Type: UIGameLoadingSplash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3DEEF376-CF20-4F97-BB1A-3F3B433E8784
// Assembly location: D:\Program_disport\StreamApps\common\Dyson Sphere Program\DSPGAME_Data\Managed\Assembly-CSharp.dll

using NGPT;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace Qtool
{
    public class FrameLineTexture
    {

        RectInfoList 贴图布局 = null;
        int 列数 = 5;

        public void showLines()
        {

            for (int i = 1; i < 43 ; i++)
            {
                Sprite sprite = UnityEngine.Resources.Load<Sprite>("UI/Textures/Sprites/Milestone/lines/" + i);
                GUI.DrawTexture(Plugin.实例.布局.newrectFrameLayerY(i), sprite.texture);
                GUI.Box(Plugin.实例.布局.newrectFrameLayerY(i), i+ sprite.texture.name);
                i++;
            }




        }





        public void showLineTextures()
        {
            updateTextures();
            drawTextures();

        }


        void updateTextures()
        {
            if (贴图布局 == null)
            {
                贴图布局 = new RectInfoList();

                for (int i = 1; i < 43; i++)
                {
                    int row = i / 列数;
                    int col = i % 列数;
                    Sprite sprite = UnityEngine.Resources.Load<Sprite>("UI/Textures/Sprites/Milestone/lines/" + i);
                    贴图布局.addTexture(row, col, sprite.texture);

                }

                贴图布局.网格分布();
            }
        }


        public void drawTextures()
        {

            int i = 0;
            foreach (RectInfo rectInfo in 贴图布局.iconRectList)
            {
                GUI.DrawTexture(Plugin.实例.布局.newRectInfoIcon(rectInfo.x, rectInfo.y, rectInfo.width, rectInfo.height), rectInfo.texture);
                GUI.Box(Plugin.实例.布局.newRectInfoIcon(rectInfo.x, rectInfo.y, rectInfo.width, rectInfo.height), i + "::" + rectInfo.texture.name);
                i++;
            }




        }






    }




}




//Unity引擎GUI之Image
//UGUI的Image等价于NGUI的Sprite组件，用于显示图片。




//[Info: Console] _milestoneId
//[Info: Console] 1
//[Info: Console] _milestoneId
//[Info: Console] 2
//[Info: Console] _milestoneId
//[Info: Console] 3
//[Info: Console] _milestoneId
//[Info: Console] 4
//[Info: Console] _milestoneId
//[Info: Console] 5
//[Info: Console] _milestoneId
//[Info: Console] 6
//[Info: Console] _milestoneId
//[Info: Console] 7
//[Info: Console] _milestoneId
//[Info: Console] 8
//[Info: Console] _milestoneId
//[Info: Console] 9
//[Info: Console] _milestoneId
//[Info: Console] 10
//[Info: Console] _milestoneId
//[Info: Console] 11
//[Info: Console] _milestoneId
//[Info: Console] 12
//[Info: Console] _milestoneId
//[Info: Console] 13
//[Info: Console] _milestoneId
//[Info: Console] 14
//[Info: Console] _milestoneId
//[Info: Console] 15
//[Info: Console] _milestoneId
//[Info: Console] 16
//[Info: Console] _milestoneId
//[Info: Console] 17
//[Info: Console] _milestoneId
//[Info: Console] 18
//[Info: Console] _milestoneId
//[Info: Console] 19
//[Info: Console] _milestoneId
//[Info: Console] 20
//[Info: Console] _milestoneId
//[Info: Console] 21
//[Info: Console] _milestoneId
//[Info: Console] 22
//[Info: Console] _milestoneId
//[Info: Console] 23
//[Info: Console] _milestoneId
//[Info: Console] 24
//[Info: Console] _milestoneId
//[Info: Console] 25
//[Info: Console] _milestoneId
//[Info: Console] 26
//[Info: Console] _milestoneId
//[Info: Console] 27
//[Info: Console] _milestoneId
//[Info: Console] 28
//[Info: Console] _milestoneId
//[Info: Console] 29
//[Info: Console] _milestoneId
//[Info: Console] 30
//[Info: Console] _milestoneId
//[Info: Console] 31
//[Info: Console] _milestoneId
//[Info: Console] 32
//[Info: Console] _milestoneId
//[Info: Console] 33
//[Info: Console] _milestoneId
//[Info: Console] 34
//[Info: Console] _milestoneId
//[Info: Console] 35
//[Info: Console] _milestoneId
//[Info: Console] 36
//[Info: Console] _milestoneId
//[Info: Console] 37
//[Info: Console] _milestoneId
//[Info: Console] 38
//[Info: Console] _milestoneId
//[Info: Console] 39
//[Info: Console] _milestoneId
//[Info: Console] 40
//[Info: Console] _milestoneId
//[Info: Console] 41
//[Info: Console] _milestoneId
//[Info: Console] 42


