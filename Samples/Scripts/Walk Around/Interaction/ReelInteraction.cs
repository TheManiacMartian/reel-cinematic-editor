using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel.Samples
{
    public class ReelInteraction : Interaction
    {
        [SerializeField] private ReelGraph _selectedGraph;


        override public void Interact()
        {
            if (!ReelDirector.Instance.GetIsReelRunning())
            {
                ReelDirector.Instance.StartReel(_selectedGraph);

            }
        }
    }
}
