﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Martian.Reel
{

    /// <summary>
    /// The entry point of all reel graphs
    /// </summary>
    [NodeTint(0.2f, 0.7f, 0.2f)]
    public class EntryNode : ReelNode
    {
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