using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel
{
    /// <summary>
    /// Acts as a way to both trigger reel graphs and access the information in a reel graph through
    /// events
    /// </summary>
    public class ReelDirector : MonoBehaviour
    {
        public static ReelDirector Instance;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] protected KeyCode _reelInput;
        [SerializeField] private List<ReelDialogueView> _views;

        /// <summary>
        /// This input action indicates the player pressing a "continue" button.
        /// Used for things like continuing dialogue.
        /// </summary>
        public Action OnReelInput; 

        private ReelGraph _currentGraph;

        private void Update()
        {
            if(_currentGraph != null)
            {
                // get input
                if (Input.GetKeyDown(_reelInput))
                {
                    OnReelInput?.Invoke();
                }
            }
        }

        /// <summary>
        /// Start a given reel
        /// </summary>
        /// <param name="reelGraph"></param>
        public void StartReel(ReelGraph reelGraph)
        {
            // check if we can do reel
            if(reelGraph == _currentGraph)
            {
                Debug.LogWarning("Cannot play Reel because it is already playing");
                return;
            }

            _currentGraph = reelGraph;

            // start the reel
            StartCoroutine(reelGraph.DoReel(this, OnReelComplete));

        }

        private void OnReelComplete()
        {
            Debug.Log("Reel Completed");

            // clear dialogue information
            UpdateDialogueInformation(new Dictionary<string, string>());

            // clear grpah
            _currentGraph = null;
        }

        public void UpdateDialogueInformation(Dictionary<string, string> dialogueInfo)
        {
            foreach(ReelDialogueView view in _views)
            {
                view.ViewUpdate(dialogueInfo);
            }
        }
    }
}
