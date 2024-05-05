

using UnityEngine;

namespace Qtool
{
    public class FrameItem统计
    {

        public NodeTree itemTree;


        void updateTree()
        {
            if (!Plugin.实例.物品树需要更新)
                return;

            ItemProto itemProto = LDB.items.Select(Plugin.实例.物品ID);
            if (itemProto == null)
            {
                return;
            }
            itemTree = new NodeTree();
            itemTree.setRootItem(itemProto);

            //Debug.Log("选择物品ID::" + itemProto.Name + "::" + itemProto.ID);
            //Debug.Log(itemTree.allNodes.Count);
            //Debug.Log(itemTree.endNodes.Count);

            Plugin.实例.物品树需要更新 = false;

        }
        public void showRecipeTree()
        {



            
        }



    }
}


