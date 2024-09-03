using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Martian.Reel.Samples
{
    public class SpeakerView : ReelDialogueView
    {
        [SerializeField] private TMP_Text _speakerText;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public override void ViewUpdate(Dictionary<string, string> reelInformation)
        {
            if (reelInformation.ContainsKey("speaker"))
            {
                gameObject.SetActive(true);

                // set the speaker color
                if (reelInformation.ContainsKey("speakerColor"))
                {
                    ColorUtility.TryParseHtmlString("#" + reelInformation["speakerColor"], out Color speakerColor);
                    _speakerText.color = speakerColor;

                }

                // show speaker name
                _speakerText.text = reelInformation["speaker"];
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
