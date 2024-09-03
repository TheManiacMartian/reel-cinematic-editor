using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Martian.Reel
{
    public class DelayNode : ReelNode
    {
        [Input] public float Delay;

        public override IEnumerator NodeSequence(ReelDirector director)
        {
            // clear dialogue information
            director.UpdateDialogueInformation(new Dictionary<string, string>());

            // wait for the delay seconds
            yield return new WaitForSeconds(Delay);
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