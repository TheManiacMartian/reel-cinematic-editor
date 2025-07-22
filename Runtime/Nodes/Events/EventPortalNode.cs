using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Martian.Reel.Events
{
    public class EventPortalNode : ReelNode
    {
        [Input] public float Delay;
        [Input] public string EventName;
        [Output] public EmptyPort PortalNode;

        public override IEnumerator NodeSequence(ReelDirector director)
        {
            // get node 
            NodePort outPort = GetOutputPort("PortalNode");

            if (outPort.IsConnected)
            {
                // trigger event
                director.AddEventPortal(EventName, outPort.Connection.node as ReelNode);
            }

            // wait for the delay seconds
            yield return new WaitForSeconds(Delay);
        }

    }
}
