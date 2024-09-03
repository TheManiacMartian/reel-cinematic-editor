using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel
{
    [CreateAssetMenu(fileName = "New Reel Character", menuName = "Reel/Character")]
    public class ReelCharacter : ScriptableObject
    {
        public string CharacterName = "Character Name";
        public Color NameColor = Color.white;
    }
}
