using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using XNode;
using Node = XNode.Node;

namespace Martian.Reel
{
    public class ReelNode : Node
    {
        [Input] public EmptyPort In;
        [Output] public EmptyPort Out;

        /// <summary>
        /// The actual functionality in a coroutine of the node.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator NodeSequence(ReelDirector director)
        {
            // clear dialogue information
            director.UpdateDialogueInformation(new Dictionary<string, string>());
            yield return null;
        }

        public virtual ReelNode GetNextNode()
        {
            NodePort outPort = GetOutputPort("Out");

            if (!outPort.IsConnected)
            {
                return null;
            }
            else
            {
                ReelNode node = outPort.Connection.node as ReelNode;

                return node;
            }
        }

    }
}