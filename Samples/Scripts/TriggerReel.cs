using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel.Samples
{
    public class TriggerReel : MonoBehaviour
    {
        [SerializeField] private ReelGraph _selectedGraph;

        public void PlayReel()
        {
            ReelDirector.Instance.StartReel(_selectedGraph);
        }
    }
}