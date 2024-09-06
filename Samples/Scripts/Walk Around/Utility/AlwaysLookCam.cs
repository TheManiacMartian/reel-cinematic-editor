using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel.Samples
{
    public class AlwaysLookCam : MonoBehaviour
    {
        private Transform camTransform;

        private void Start()
        {
            camTransform = Camera.main?.transform;
        }


        private void LateUpdate()
        {
            if (camTransform != null)
            {
                transform.rotation = camTransform.rotation;
            }
            else
            {
                camTransform = Camera.main?.transform;

            }
        }
    }
}
