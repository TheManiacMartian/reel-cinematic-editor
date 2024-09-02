using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu(fileName ="New_ReelGraph", menuName = "Reel/Graph", order = 1)]
[RequireNode(typeof(EntryNode))]
public class ReelGraph : NodeGraph {

}

[Serializable]
public class EmptyPort { }