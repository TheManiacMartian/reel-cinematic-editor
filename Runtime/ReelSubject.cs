using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Martian.Reel
{
    /// <summary>
    /// A reel subject is a way to link objects in the scene and access them in a reel graph.
    /// </summary>
    public class ReelSubject : MonoBehaviour
    {
        public string SubjectId = "Empty";

        public ReelSubject(string subjectId)
        {
            SubjectId = subjectId;
        }

        private void Start()
        {
            // subscribe to add itself to reel director
            ReelDirector.Instance.OnReelStart += AddToReel;
        }

        private void OnDisable()
        {
            ReelDirector.Instance.OnReelStart -= AddToReel;
        }

        private void AddToReel()
        {
            ReelDirector.Instance.AddReelSubject(this);
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.red;
            style.fontSize = 18;
            Handles.Label(transform.position + new Vector3(0f, 0.25f, 0f), SubjectId, style);

            Handles.color = Color.cyan;
            Handles.DrawWireDisc(transform.position, transform.up, 0.5f);

            Handles.color = Color.red;
            Handles.ArrowHandleCap(0, transform.position, transform.rotation * Quaternion.LookRotation(Vector3.forward), 0.5f, EventType.Repaint);
        }

#endif
    }
}
