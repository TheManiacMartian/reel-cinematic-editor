using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Martian.Reel;

namespace Martian.Reel.Samples
{
    public class ReelInteraction : Interaction
    {
        [SerializeField] private ReelGraph _selectedGraph;


        override public void Interact()
        {
            if (ReelDirector.Instance.GetCurrentReelRunning() != null)
            {
                ReelDirector.Instance.StartReel(_selectedGraph);

            }
        }
    }
}
