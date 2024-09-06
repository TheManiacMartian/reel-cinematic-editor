using System.Collections;
using System.Collections.Generic;
using Codice.CM.Client.Differences.Merge;
using UnityEngine;
using XNode;

namespace Martian.Reel.Dialogue
{
    //[NodeTint(0f, 0.5f, 0.75f)]
    [NodeWidth(260)]
    public class DialogueNode : ReelNode
    {
        
        [Header("Dialogue Settings")]
        [TextArea(3, 10)]
        [Input] public string Line;

        [Header("Character Settings")]
        [Input] public ReelCharacter Speaker;
        public string Emotion = "Default";

        private bool _inputRecieved;

        public override IEnumerator NodeSequence(ReelDirector director)
        {
            // set input recieved to false
            _inputRecieved = false;

            // subscribe to reel input
            director.OnReelInput += GetReelInput;

            // update dialogue information
            Dictionary<string, string> dialogueInformation = new Dictionary<string, string>
            {
                { "dialogue", Line },
                { "speaker", Speaker.CharacterName },
                { "speakerColor", ColorUtility.ToHtmlStringRGB(Speaker.NameColor) }
            };

            director.UpdateDialogueInformation(dialogueInformation);

            while(_inputRecieved == false)
            {
                yield return null;
            }

            // unsubscribe to reel input
            director.OnReelInput -= GetReelInput;

        }

        private void GetReelInput()
        {
            _inputRecieved = true;
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