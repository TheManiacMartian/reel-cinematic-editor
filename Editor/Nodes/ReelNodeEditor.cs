using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using XNodeEditor;

namespace Martian.Reel.Editor
{
    [CustomNodeEditor(typeof(ReelNode))]
    public class ReelNodeEditor : NodeEditor
    {
        public override void OnHeaderGUI()
        {
            GUI.color = Color.white;
            ReelNode node = target as ReelNode;
            ReelGraph graph = node.graph as ReelGraph;
            if (graph.Current == node) GUI.color = Color.green;
            string title = target.name;
            GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
            GUI.color = Color.white;
        }
    }
}
