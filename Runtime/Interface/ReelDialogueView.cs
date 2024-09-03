using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel
{
    public abstract class ReelDialogueView : MonoBehaviour
    {
        public abstract void ViewUpdate(Dictionary<string, string> dialogueInfo);
    }
}
