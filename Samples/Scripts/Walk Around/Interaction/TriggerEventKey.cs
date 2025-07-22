using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel.Samples
{
    public class TriggerEventKey : MonoBehaviour
    {
        public KeyCode Key;
        public string EventName;

        private void Update()
        {
            if(Input.GetKeyDown(Key))
            {
                ReelDirector.Instance.TriggerEvent(EventName);
            }
        }
    }
}
