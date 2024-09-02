using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;

namespace Martian.Reel.Editor
{
    [CustomNodeGraphEditor(typeof(ReelGraph))]
    public class ReelGraphEditor : NodeGraphEditor
    {

        /// <summary>
        /// We are going to override the menu name to only include nodes under the Martian.Reel namespace.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override string GetNodeMenuName(Type type)
        {
            if (type.Namespace == "Martian.Reel")
            {
                return base.GetNodeMenuName(type).Replace("Martian/Reel/", "");
            }

            else return null;
        }
    }
}
