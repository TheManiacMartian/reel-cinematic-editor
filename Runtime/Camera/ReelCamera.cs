using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel
{

    [RequireComponent(typeof(Camera))]
    public class ReelCamera : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _cameraTransitionCurve;
        private Camera _camera;

        private void OnEnable()
        {
            _camera = GetComponent<Camera>();
        }

        public IEnumerator FocusSubject(ReelSubject subject, Vector3 positionOffset, Vector3 rotationOffset, float fov, float blendTime)
        {
            // clear the parent (effectively)
            transform.parent = null;

            // start variables
            float endTime = Time.time + blendTime;
            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            float startFOV = _camera.fieldOfView;

            // transition to new position
            while (Time.time < endTime)
            {
                // move camera towards goal based on how long we have left
                transform.position = Vector3.Lerp(startPos, subject.transform.TransformPoint(positionOffset), _cameraTransitionCurve.Evaluate(1 - (endTime - Time.time) / blendTime));
                transform.rotation = Quaternion.Lerp(startRot, Quaternion.Euler(subject.transform.rotation.eulerAngles + rotationOffset), _cameraTransitionCurve.Evaluate(1 - (endTime - Time.time) / blendTime));
                _camera.fieldOfView = Mathf.Lerp(startFOV, fov, _cameraTransitionCurve.Evaluate(1 - (endTime - Time.time) / blendTime));
                yield return null;
            }

            // set parent and offset
            transform.parent = subject.transform;
            transform.localPosition = positionOffset;
            transform.localRotation = Quaternion.Euler(rotationOffset);
            _camera.fieldOfView = fov;
        }
    }
}
