using System;
using System.Collections;
using System.Collections.Generic;
using Martian.Reel;
using UnityEngine;
using System.Linq;
using XNode;

[CreateAssetMenu(fileName ="New ReelGraph", menuName = "Reel/Graph", order = 1)]
[RequireNode(typeof(EntryNode))]
public class ReelGraph : NodeGraph {
    public ReelNode Current;

    public IEnumerator DoReel(ReelDirector director, Action onCompleteCallback)
    {
        // set current to entry node
        Current = nodes[0] as ReelNode;

        // for each node await its sequence, then get the following node, and await its sequence.
        // repeat those steps.
        while(Current != null)
        {
            if (Current.IsSynchronous)
            {
                yield return Current.NodeSequence(director);

            }
            else
            {
                director.StartAsyncReelNode(Current);    
            }

            Current = Current.GetNextNode();

        }

        // we are done
        onCompleteCallback.Invoke();
    }
}

[Serializable]
public class EmptyPort { }