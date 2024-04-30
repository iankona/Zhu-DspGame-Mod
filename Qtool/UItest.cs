

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
    //public class FrameTest
    //{
    //    Material linemat = new Material(Shader.Find("Hidden/Internal-Colored"));// 线条为渐变颜色
    //    float x = 0f;
    //    float y = 0f;
    //    float width = 0f;
    //    float height = 0f;

    //    public void showTests()
    //    {


    //        Sprite[] 列表 = (Sprite[])UnityEngine.Resources.FindObjectsOfTypeAll(typeof(Sprite));
    //        Sprite icon = 列表[1398];// 180 x 22
    //        GUI.DrawTextureWithTexCoords(new Rect(50, 50, 2 * icon.texture.width, 2 * icon.texture.height), icon.texture, new Rect(0.5f, 0.5f, 0.1f, 0.1f));
    //        GUI.Box(new Rect(x, y, width, height), "1398");


    //        drawThinLine(new Vector3(50, 50, 0), new Vector3(150, 150, 0));
    //        drawBoldLine(new Vector3(50, 50, 0), new Vector3(250, 350, 0));






    //    }

    //    public void drawThinLine(Vector3 start, Vector3 final)
    //    {
    //        Vector3 beg = GUI屏幕坐标系toGL屏幕坐标系Thin(start);
    //        Vector3 end = GUI屏幕坐标系toGL屏幕坐标系Thin(final);

    //        GL.PushMatrix();
    //        linemat.SetPass(0);
    //        GL.LoadPixelMatrix();
    //        GL.Begin(GL.LINES);

    //        GL.Color(new Color(0.1f, 1.0f, 1.0f, 1.0f));
    //        GL.Vertex3(beg.x, beg.y, 0.0f);

    //        GL.Color(new Color(1.0f, 1.0f, 0.3f, 1.0f));
    //        GL.Vertex3(end.x, end.y, 0.0f);

    //        GL.End();
    //        GL.PopMatrix();
    //    }


    //    public void drawBoldLine(Vector3 start, Vector3 final)
    //    {
    //        Vector3 beg = GUI屏幕坐标系toGL屏幕坐标系Bold(start);
    //        Vector3 end = GUI屏幕坐标系toGL屏幕坐标系Bold(final);

    //        GL.PushMatrix();
    //        linemat.SetPass(0);
    //        GL.LoadOrtho();
    //        GL.Begin(GL.LINES);

    //        GL.Color(new Color(0.1f, 1.0f, 1.0f, 1.0f));
    //        GL.Vertex3(beg.x, beg.y, 0.0f);

    //        GL.Color(new Color(1.0f, 1.0f, 0.3f, 1.0f));
    //        GL.Vertex3(end.x, end.y, 0.0f);

    //        GL.End();
    //        GL.PopMatrix();


    //    }

    //    public Vector3 GUI屏幕坐标系toGL屏幕坐标系Thin(Vector3 vec3)
    //    {
    //        Vector3 result = new Vector3();
    //        result.x = vec3.x;
    //        result.y = Screen.height - vec3.y;
    //        result.z = 0.0f;
    //        return result;

    //    }

    //    public Vector3 GUI屏幕坐标系toGL屏幕坐标系Bold(Vector3 vec3)
    //    {
    //        Vector3 result = new Vector3();
    //        result.x = vec3.x / Screen.width;
    //        result.y = (Screen.height - vec3.y) / Screen.height;
    //        result.z = 0.0f;
    //        return result;

    //    }



    //}


}


//GL.LoadOrtho()：屏幕空间，圆心为屏幕左下角，屏幕区域{(0,1), (0, 1)}
//GL.LoadPixelMatrix()：屏幕空间，圆心为屏幕左下角，屏幕区域{(0, 屏幕宽度), (0, 屏幕高度)}




//GL_POINTS
//将每个顶点视为单个点。 顶点 n 定义点 n。 绘制 N 个点。
//GL_LINES
//将每对顶点视为独立的线段。 顶点 2n - 1 和 2n 定义第 n 行。 绘制 N/2 条线。
//GL_LINE_STRIP
//绘制从第一个顶点到最后一个顶点的连接线段组。 顶点 n 和 n+1 定义第 n 行。 N - 绘制 1 条线。
//GL_LINE_LOOP
//绘制一组连接的线段，从第一个顶点到最后一个顶点，然后回到第一个顶点。 顶点 n 和 n + 1 定义第 n 行。 但是，最后一行由顶点 N 和 1 定义。 绘制 N 条线。
//GL_TRIANGLES
//将顶点的每个三元组视为一个独立的三角形。 顶点 3n - 2、 3n - 1 和 3n 定义三角形 n。 绘制 N/3 个三角形。
//GL_TRIANGLE_STRIP
//绘制一组连接的三角形。 为前两个顶点之后显示的每个顶点定义一个三角形。 对于奇数 n，顶点 n、 n + 1 和 n + 2 定义三角形 n。 对于偶数 n，顶点 n + 1、 n 和 n + 2 定义三角形 n。 N - 绘制 2 个三角形。
//GL_TRIANGLE_FAN
//绘制一组连接的三角形。 为前两个顶点之后显示的每个顶点定义一个三角形。 顶点 1、 n + 1、 n + 2 定义三角形 n。 N - 绘制 2 个三角形。
//GL_QUADS
//将四个顶点的每个组视为独立的四边形。 顶点 4n - 3、 4n - 2、 4n - 1 和 4n 定义四边形 n。 绘制 N/4 个四边形。
//GL_QUAD_STRIP
//绘制一组连接的四边形。 为第一对之后呈现的每一对顶点定义一个四边形。 顶点 2n - 1、 2n、 2n + 2 和 2n + 1 定义四边形 n。 N/2 - 绘制 1 个四边形。 请注意，使用顶点根据条带数据构造四边形的顺序不同于用于独立数据的顺序。
//GL_POLYGON
//绘制单个凸多边形。 顶点 1 到 N 定义此多边形。

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


