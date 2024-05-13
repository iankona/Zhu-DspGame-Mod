

// Decompiled with JetBrains decompiler
// Type: UIGameLoadingSplash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3DEEF376-CF20-4F97-BB1A-3F3B433E8784
// Assembly location: D:\Program_disport\StreamApps\common\Dyson Sphere Program\DSPGAME_Data\Managed\Assembly-CSharp.dll

using Compressions;
using HarmonyLib;
using NGPT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace Qtool
{
    public class FrameLineWhite
    {
        Material linemat = new Material(Shader.Find("Hidden/Internal-Colored"));// 线条为渐变颜色
        ArrayList pool = new ArrayList();

        public void showLines()
        {

            pool.Add(new Vector3(50, 50, 0));
            pool.Add(new Vector3(250, 250, 0));

            pool.Add(new Vector3(50, 50, 0));
            pool.Add(new Vector3(350, 450, 0));

            drawBoldLine(pool);
            pool.Clear();



        }





        public void drawThinLine(ArrayList pool)
        {


            GL.PushMatrix();
            linemat.SetPass(0);
            GL.LoadPixelMatrix();
            GL.Begin(GL.LINES);

            for (int index = 0; index < pool.Count; index++)
            {
                Vector3 beg = GUI屏幕坐标系toGL屏幕坐标系Thin((Vector3)pool[index]);
                Vector3 end = GUI屏幕坐标系toGL屏幕坐标系Thin((Vector3)pool[index + 1]);

                //GL.Color(new Color(0.1f, 1.0f, 1.0f, 1.0f));
                GL.Vertex3(beg.x, beg.y, 0.0f);

                //GL.Color(new Color(1.0f, 1.0f, 0.3f, 1.0f));
                GL.Vertex3(end.x, end.y, 0.0f);

                index++;
            }
            GL.End();
            GL.PopMatrix();
        }


        public void drawBoldLine(ArrayList pool)
        {
            GL.PushMatrix();
            linemat.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            for (int index = 0; index < pool.Count; index++)
            {
                Vector3 beg = GUI屏幕坐标系toGL屏幕坐标系Bold((Vector3)pool[index]);
                Vector3 end = GUI屏幕坐标系toGL屏幕坐标系Bold((Vector3)pool[index + 1]);

                //GL.Color(new Color(0.1f, 1.0f, 1.0f, 1.0f));
                GL.Vertex3(beg.x, beg.y, 0.0f);

                //GL.Color(new Color(1.0f, 1.0f, 0.3f, 1.0f));
                GL.Vertex3(end.x, end.y, 0.0f);

                index++;
            }

            GL.End();
            GL.PopMatrix();


        }

        public Vector3 GUI屏幕坐标系toGL屏幕坐标系Thin(Vector3 vec3)
        {
            Vector3 result = new Vector3();
            result.x = vec3.x;
            result.y = Screen.height - vec3.y;
            result.z = 0.0f;
            return result;

        }

        public Vector3 GUI屏幕坐标系toGL屏幕坐标系Bold(Vector3 vec3)
        {
            Vector3 result = new Vector3();
            result.x = vec3.x / Screen.width;
            result.y = (Screen.height - vec3.y) / Screen.height;
            result.z = 0.0f;
            return result;

        }

    }
}





