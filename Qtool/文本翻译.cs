

using System.Collections;

namespace Qtool
{
    public class TextTranslate
    {
        Hashtable translateMap = new Hashtable();//翻译用的HashTable
        public void InitTranslateMap()
        {
            translateMap.Clear();
            translateMap.Add("等待游戏资源加载!", "Wait for game resources to load!");
            translateMap.Add("关闭", "Close");
            translateMap.Add("返回", "Return");
            translateMap.Add("原料", "raw");
            translateMap.Add("自提供", "bySelf");
            translateMap.Add("副产物", "byProducts");
            translateMap.Add("目标", "target");
            translateMap.Add("临时物", "temp");
            translateMap.Add("加工厂", "factory");
            translateMap.Add("未知", "unknown");
        }
        string translate(string text)
        {
            if (translateMap.ContainsKey(text) && !Localization.isZHCN)
            {
                return translateMap[text].ToString();
            }
            else
            {
                return text;
            }
        }
    }
    }



