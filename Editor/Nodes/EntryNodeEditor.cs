using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using XNodeEditor;

namespace Martian.Reel.Editor
{
    [CustomNodeEditor(typeof(EntryNode))]
    public class EntryNodeEditor : ReelNodeEditor
    {
        public override void OnBodyGUI()
        {
            // Initialization
            EntryNode node = (EntryNode)target;

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Out"));

        }
    }
}
