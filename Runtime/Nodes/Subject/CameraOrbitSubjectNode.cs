using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Martian.Reel.Subject
{
    public class CameraOrbitSubjectNode : ReelNode
    {

        [Input] public ReelSubject Subject;
        [Input] public Vector3 PositionOffset;
        [Input] public Vector3 RotationOffset;
        [Input] public float FOV = 60f;
        [Input] public float BlendTime = 1f;

        [HideInInspector] public GameObject PreviewObject;
        

        public override IEnumerator NodeSequence(ReelDirector director)
        {
            // clear dialogue information
            director.UpdateDialogueInformation(new Dictionary<string, string>());

            _reelDirector = director;

            if (GetPort("Subject").IsConnected)
            {
                Subject = GetPort("Subject").GetInputValue<ReelSubject>();
            }

            if (GetPort("PositionOffset").IsConnected)
            {
                PositionOffset = GetPort("PositionOffset").GetInputValue<Vector3>();
            }

            if (GetPort("RotationOffset").IsConnected)
            {
                RotationOffset = GetPort("RotationOffset").GetInputValue<Vector3>();
            }

            if (GetPort("BlendTime").IsConnected)
            {
                BlendTime = GetPort("BlendTime").GetInputValue<float>();
            }

            if (GetPort("FOV").IsConnected)
            {
                FOV = GetPort("FOV").GetInputValue<float>();
            }

            // do the camera focus
            yield return director.FocusCamera(
                Subject,
                PositionOffset,
                RotationOffset,
                FOV,
                BlendTime);

        }

        // Use this for initialization
        protected override void Init()
        {
            base.Init();

        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}