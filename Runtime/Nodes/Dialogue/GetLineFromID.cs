using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Martian.Reel.Dialogue
{
    public class GetLineFromID : Node
    {
        [Input] public CSVDialogueDictionary Dialogues;
        [Input] public string LineID;

        [Output] public string Line;

        // Use this for initialization
        protected override void Init()
        {
            base.Init();
        }

        

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "Line")
            {
                if (GetPort("Dialogues").IsConnected)
                {
                    return GetPort("Dialogues").GetInputValue<CSVDialogueDictionary>().GetDialogueAtID(LineID);
                }
            }

            return "No line input.";
        }
    }
}
