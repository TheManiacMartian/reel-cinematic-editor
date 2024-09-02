using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Martian.Reel
{
    //[NodeTint(0f, 0.5f, 0.75f)]
    [NodeWidth(260)]
    public class DialogueNode : Node
    {
        [Input] public EmptyPort In;
        [Output] public EmptyPort Out;

        [Header("Dialogue Settings")]
        [TextArea(3, 10)]
        [Input] public string Line;

        [Header("Character Settings")]
        [Input] public ReelCharacter Speaker;
        public string Emotion = "Default";


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