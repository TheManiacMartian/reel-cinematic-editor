using System.Collections;
using System.Collections.Generic;
using Martian.Reel.Subject;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using XNodeEditor;

namespace Martian.Reel.Editor
{
    [CustomNodeEditor(typeof(CameraOrbitSubjectNode))]
    public class CameraOrbitSubjectNodeEditor : NodeEditor
    {

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();

            CameraOrbitSubjectNode cameraOrbitSubjectNode = (CameraOrbitSubjectNode)target;

            // copy button
            if (GUILayout.Button("Copy Position and Rotation"))
            {
                EditorGUIUtility.systemCopyBuffer = $"{cameraOrbitSubjectNode.PositionOffset}:{cameraOrbitSubjectNode.RotationOffset}";
            }

            // paste
            if (GUILayout.Button("Paste Position and Rotation"))
            {
                string[] offsets = EditorGUIUtility.systemCopyBuffer.Split(":");
                cameraOrbitSubjectNode.PositionOffset = GetVector3FromString(offsets[0]);
                cameraOrbitSubjectNode.RotationOffset = GetVector3FromString(offsets[1]);
            }

            
        }

        private Vector3 GetVector3FromString(string str)
        {

            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace(" ", "");

            string[] values = str.Split(',');


            return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));


        }

    }
}
