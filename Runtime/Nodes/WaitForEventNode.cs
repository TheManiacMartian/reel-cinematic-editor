using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel
{
    public class WaitForEventNode : ReelNode
    {
        [Input] public string EventName;
        [Input] public bool ClearDialogue;


        private bool _eventTriggered = false;

        protected override void Init()
        {
            base.Init();

            
        }

        private void EventTriggered(string eventName)
        {
            if(eventName == EventName)
            {
                _eventTriggered = true;
            }
        }


        public override IEnumerator NodeSequence(ReelDirector director)
        {

            _eventTriggered = false;
            ReelDirector.Instance.OnEventTriggered += EventTriggered;

            if (ClearDialogue)
            {
                // clear dialogue information
                director.UpdateDialogueInformation(new Dictionary<string, string>());
            }

            yield return new WaitWhile(() => _eventTriggered == false);

            ReelDirector.Instance.OnEventTriggered -= EventTriggered;

        }

    }
}
