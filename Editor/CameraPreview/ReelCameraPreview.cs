using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Martian.Reel.Editor
{
    public class ReelCameraPreview : EditorWindow
    {
        public Vector3 PositionOffset;
        public Vector3 RotationOffset;

        public float CubeSize = 1;

        [MenuItem("Window/Reel/Camera Preview")]
        public static void ShowWindow()
        {
            GetWindow<ReelCameraPreview>("Reel Camera Preview");
        }

        private PreviewRenderUtility _previewUtility;
        private GameObject _targetObject;

        private void OnEnable()
        {
            _previewUtility = new PreviewRenderUtility();
            SetupPreviewScene();
        }

        private void OnDisable()
        {
            if (_previewUtility != null)
                _previewUtility.Cleanup();

            if (_targetObject != null)
                Object.DestroyImmediate(_targetObject);
        }

        private void SetupPreviewScene()
        {
            _targetObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _targetObject.transform.position = Vector3.zero;
            _targetObject.transform.localScale = Vector3.one * CubeSize;

            // Since we want to manage this instance ourselves, hide it
            // from the current active scene, but remember to also destroy it.
            _targetObject.hideFlags = HideFlags.HideAndDontSave;
            _previewUtility.AddSingleGO(_targetObject);

            // camera attributes
            _previewUtility.camera.transform.position = PositionOffset;
            _previewUtility.camera.transform.rotation = Quaternion.Euler(RotationOffset);

            // This is usually set very small for good performance, but
            // we need to shift the range to something our cube can fit between.
            _previewUtility.camera.nearClipPlane = 0.1f;
            _previewUtility.camera.farClipPlane = 20f;
        }

        private void Update()
        {

            // camera attributes
            _previewUtility.camera.transform.position = PositionOffset;
            _previewUtility.camera.transform.rotation = Quaternion.Euler(RotationOffset);

            // cube attributes
            _targetObject.transform.localScale = Vector3.one * CubeSize;

            // Since this is the most important window in the editor, let's use our
            // resources to make this nice and smooth, even when running in the background.
            Repaint();
        }

        private void OnGUI()
        {
            PositionOffset = EditorGUILayout.Vector3Field("Position Offset", PositionOffset);
            RotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", RotationOffset);

            // copy button
            if (GUILayout.Button("Copy Position and Rotation"))
            {
                EditorGUIUtility.systemCopyBuffer = $"{PositionOffset}:{RotationOffset}";
            }

            // paste
            if (GUILayout.Button("Paste Position and Rotation"))
            {
                string[] offsets = EditorGUIUtility.systemCopyBuffer.Split(":");
                PositionOffset = GetVector3FromString(offsets[0]);
                RotationOffset = GetVector3FromString(offsets[1]);
            }



            // Render the preview scene into a texture and stick it
            // onto the current editor window. It'll behave like a custom game view.

            float size = position.width / 1.5f;
            Rect rect = new Rect(position.width / 2 - (size / 2), 200, size, size);

            _previewUtility.BeginPreview(rect, previewBackground: GUIStyle.none);
            _previewUtility.Render();

            var texture = _previewUtility.EndPreview();
            GUI.DrawTexture(rect, texture);

            CubeSize = EditorGUILayout.FloatField("Cube Size", CubeSize);

        }

        private Vector3 GetVector3FromString(string str)
        {

            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace(" ", "");

            string[] values =  str.Split(',');


            return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));


        }

    }
}
