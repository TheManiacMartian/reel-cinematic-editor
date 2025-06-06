using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using XNode;
using System;

namespace Martian.Reel.Dialogue
{
    public class GetCSVFile : Node
    {
        [Input] public TextAsset File;

        [Output] public CSVDialogueDictionary Dialogues;

        // Use this for initialization
        protected override void Init()
        {
            base.Init();

        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "Dialogues")
            {
                if(File)
                {
                    return CSVDialogueDictionary.CreateFromFile(File);
                }
            }

            return null;
        }


    }

    /// <summary>
    /// A dictionary of dialogues from the csv file
    /// </summary>
    [Serializable]
    public class CSVDialogueDictionary
    {
        private Dictionary<string, string> _idDialogues;

        public static CSVDialogueDictionary CreateFromFile(TextAsset file)
        {
            if(file == null) { return null; }

            string[] allLines = file.text.Split('\n');

            string[] ids = new string[allLines.Length - 1];
            string[] lines = new string[allLines.Length - 1];

            // start at 1 to skip the column line
            for (int i = 1; i < allLines.Length; i++)
            {
                // replace "
                allLines[i] = allLines[i].Replace("\"", string.Empty);

                // find the first comma on the line, that is our line id spliut
                int commaIndex = allLines[i].IndexOf(',');

                // split on that comma
                string id = allLines[i].Substring(0, commaIndex);
                string line = allLines[i].Substring(commaIndex + 1);

                // add to seperate arrays
                ids[i - 1] = id;
                lines[i - 1] = line;
            }

            return new CSVDialogueDictionary(ids, lines, ids.Length);
        }

        public CSVDialogueDictionary(string[] ids, string[] dialogues, int length)
        {
            _idDialogues = new Dictionary<string, string>();

            for (int i = 0; i < length; i++)
            {
                _idDialogues.Add(ids[i], dialogues[i]);
            }
        }

        public string GetDialogueAtID(string id)
        {
            if(_idDialogues == null)
            {
                return "No dictionary found.";
            }

            if(!_idDialogues.ContainsKey(id))
            {
                return "No dialogue file found.";
            }

            return _idDialogues[id];
        }
    }

}