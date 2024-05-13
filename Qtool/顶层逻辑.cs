

using Compressions;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using UnityEngine;

namespace Qtool
{

    public class 绘制逻辑
    {
        int asize;
        int bsize;
        int csize;

        float time = 0;
        bool 按键按下 = false;
        bool 鼠标按下 = false;

        public int 物品ID = -1;
        public int 配方ID = -1;
        public NodeTree itemTree = null;
        public ItemInfoList item汇总 = null;
        public RecipeInfoList recipe汇总 = null;
        public UpTree upTree = null;


        public 物品配方索引处理 物品配方索引 = null;
        public ItemSelectList 物品多选 = null;
        public List<NodeTree> itemTreeList = null;
        public 物品统计处理 物品统计 = null;
        public 配方统计处理 配方统计 = null;

        public 鼠标更新 事件 = new 鼠标更新();
        public 部件方框 布局 = new 部件方框();
        public 窗口面板 界面 = new 窗口面板();

        public void 保存字体设置()
        {
            asize = GUI.skin.label.fontSize;
            bsize = GUI.skin.button.fontSize;
            csize = GUI.skin.textField.fontSize;
        }

        public void 复位字体设置()
        {
            GUI.skin.label.fontSize = asize;
            GUI.skin.button.fontSize = bsize ;
            GUI.skin.textField.fontSize = csize;
        }

        public void _onGUI()
        {
            if (UIRoot.instance == null || GameMain.instance == null)
                return;

            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                time = 0;
                按键按下 = true;
            }

            if (按键按下)
            {
                time += Time.deltaTime;
                if (time > 1000)
                    time = 0;
            }

            if (Input.GetKeyUp(KeyCode.BackQuote))
                if (time > 0.5) // 处理按键up状态存活时间毫秒，程序刚好被执行2次，导致状态复位，UI没有显示的问题
                    按键按下 = false;

            if (按键按下)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // 鼠标按下 = true;
                    事件.update鼠标左键按下();
                }

                //if (鼠标按下)
                //    事件.update鼠标滚轮滑动();
                事件.update鼠标滚轮滑动();

                if (Input.GetMouseButton(0))
                    事件.update鼠标左键按住();

                if (Input.GetMouseButtonUp(0))
                {
                    // 鼠标按下 = false;
                    事件.update鼠标左键松开();
                }

                界面.showWindown();

            }

        }

    }
}



