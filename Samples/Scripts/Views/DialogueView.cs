using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Martian.Reel.Samples
{
    public class DialogueView : ReelDialogueView
    {
        [SerializeField] private TMP_Text _dialogueText;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public override void ViewUpdate(Dictionary<string, string> reelInformation)
        {
            if (reelInformation.ContainsKey("dialogue"))
            {
                gameObject.SetActive(true);

                // show dialogue
                _dialogueText.text = reelInformation["dialogue"];
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
