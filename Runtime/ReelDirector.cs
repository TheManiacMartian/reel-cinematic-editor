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
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] protected KeyCode _reelInput;

        [SerializeField] private GameObject _dialogueInputPrompt;

        [SerializeField] private List<ReelDialogueView> _views;
        [SerializeField] private ReelCamera _reelCamera;

        private List<Coroutine> _reelGraphCoroutines = new List<Coroutine>();

        private Dictionary<string, ReelNode> _eventPortals = new Dictionary<string, ReelNode>();

        /// <summary>
        /// This input action indicates the player pressing a "continue" button.
        /// Used for things like continuing dialogue.
        /// </summary>
        public Action OnReelInput;

        public Action OnReelStart, OnReelEnd;

        public Action<string> OnEventTriggered;

        private ReelGraph _currentGraph;

        private Dictionary<string, ReelSubject> _reelSubjects = new Dictionary<string, ReelSubject>();

        private void Start()
        {
            _reelCamera.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_currentGraph != null)
            {
                // get input
                if (Input.GetKeyDown(_reelInput))
                {
                    OnReelInput?.Invoke();
                }

            }

            /*foreach(var pair in _eventPortals)
            {
                Debug.Log(pair.Key + ": " + pair.Value);
            }*/

        }

        /// <summary>
        /// Start a given reel
        /// </summary>
        /// <param name="reelGraph"></param>
        public void StartReel(ReelGraph reelGraph)
        {
            // check if we can do reel
            if (reelGraph == _currentGraph)
            {
                Debug.LogWarning("Cannot play Reel because it is already playing");
                return;
            }

            _currentGraph = reelGraph;

            if(_reelSubjects != null)
            {
                _reelSubjects.Clear();
            }
            
            // set reel camera position
            _reelCamera.transform.position = Camera.main.transform.position;
            _reelCamera.transform.rotation = Camera.main.transform.rotation;
            _reelCamera.GetComponent<Camera>().fieldOfView = 60;

            // invoke event
            OnReelStart?.Invoke();

            // start the reel
            _reelGraphCoroutines.Add(StartCoroutine(reelGraph.DoReel(this, OnReelComplete)));

        }

        /// <summary>
        /// Stops the current reel graph
        /// </summary>
        public void StopReel()
        {
            if(_currentGraph != null)
            {
                OnReelComplete();
            }
        }

        public void StartAsyncReelNode(ReelNode node)
        {
            _reelGraphCoroutines.Add(StartCoroutine(node.NodeSequence(this)));
        }

        private void OnReelComplete()
        {
            Debug.Log("Reel Completed");

            // clear dialogue information
            UpdateDialogueInformation(new Dictionary<string, string>());

            // stop camera
            _reelCamera.gameObject.SetActive(false);

            // clear all coroutines

            foreach(var coroutine in _reelGraphCoroutines)
            {
                StopCoroutine(coroutine);
            }

            _reelGraphCoroutines.Clear();

            // invoke event
            OnReelEnd?.Invoke();

            // remove from the event portals
            _eventPortals.Clear();

            // clear graph
            _currentGraph = null;
        }

        public void UpdateDialogueInformation(Dictionary<string, string> dialogueInfo)
        {
            foreach (ReelDialogueView view in _views)
            {
                view.ViewUpdate(dialogueInfo);
            }
        }

        public ReelGraph GetCurrentReelRunning()
        {
            return _currentGraph;
        }

        #region Events

        public void TriggerEvent(string eventName)
        {
            OnEventTriggered?.Invoke(eventName);

            // if the event is apart of a portal, we will activate the portal
            if(_eventPortals.ContainsKey(eventName) && _currentGraph != null)
            {
                TriggerEventPortal(eventName);
            }
        }

        public void AddEventPortal(string eventName, ReelNode node)
        {
            if(_eventPortals.ContainsKey(eventName))
            {
                _eventPortals[eventName] = node;
            }
            else
            {
                _eventPortals.Add(eventName, node);
            }
        }

        private void TriggerEventPortal(string eventName)
        {
            foreach(var coroutine in _reelGraphCoroutines)
            {
                StopCoroutine(coroutine);
            }

            // clear all coroutines
            _reelGraphCoroutines.Clear();

            // clear dialogue information
            UpdateDialogueInformation(new Dictionary<string, string>());

            // stop camera
            _reelCamera.gameObject.SetActive(false);

            _reelGraphCoroutines.Add(StartCoroutine(_currentGraph.DoReel(_eventPortals[eventName], this, OnReelComplete)));

            // remove from the event portals
            _eventPortals.Remove(eventName);
        }

        #endregion

        #region Camera

        public IEnumerator FocusCamera(ReelSubject subject, Vector3 positionOffset, Vector3 rotationOffset, float fov, float blendTime)
        {
            // turn on camera
            _reelCamera.gameObject.SetActive(true);
            yield return _reelCamera.FocusSubject(subject, positionOffset, rotationOffset, fov, blendTime);
        }

        #endregion

        #region Subject


        /// <summary>
        /// Add subject to the reel subjects list
        /// </summary>
        /// <param name="subject"></param>
        public void AddReelSubject(ReelSubject subject)
        {
            _reelSubjects.Add(subject.SubjectId, subject);
        }

        /// <summary>
        /// Get a current reel subject with their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReelSubject GetSubjectFromId(string id)
        {
            if (_reelSubjects.ContainsKey(id))
            {
                return _reelSubjects[id];

            }


            return null;
        }

        #endregion

        #region Dialogue

        public void SetPromptShow(bool show)
        {
            if(_dialogueInputPrompt)
            {
                _dialogueInputPrompt.SetActive(show);
            }
        }

        #endregion
    }
}
