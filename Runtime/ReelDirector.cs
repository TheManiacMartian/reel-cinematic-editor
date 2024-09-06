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
        [SerializeField] private List<ReelDialogueView> _views;
        [SerializeField] private ReelCamera _reelCamera;

        /// <summary>
        /// This input action indicates the player pressing a "continue" button.
        /// Used for things like continuing dialogue.
        /// </summary>
        public Action OnReelInput;

        public Action OnReelStart, OnReelEnd;

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
            OnReelStart.Invoke();

            // start the reel
            StartCoroutine(reelGraph.DoReel(this, OnReelComplete));

        }

        public void StartAsyncReelNode(ReelNode node)
        {
            StartCoroutine(node.NodeSequence(this));
        }

        private void OnReelComplete()
        {
            Debug.Log("Reel Completed");

            // clear dialogue information
            UpdateDialogueInformation(new Dictionary<string, string>());

            // stop camera
            _reelCamera.gameObject.SetActive(false);

            // invoke event
            OnReelEnd.Invoke();

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

        public bool GetIsReelRunning()
        {
            return _currentGraph != null;
        }

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
    }
}
