using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Martian.Reel.Samples
{
    public abstract class Interaction : MonoBehaviour
    {
        [SerializeField] private KeyCode _interactKey;
        [SerializeField] private GameObject _interactPrompt;
        protected bool _isInInteractDistance;

        private void Start()
        {
            _interactPrompt.SetActive(false);
            ReelDirector.Instance.OnReelStart += () => _interactPrompt.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isInInteractDistance = true;
                _interactPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isInInteractDistance = false;
                _interactPrompt.SetActive(false);

            }
        }

        private void Update()
        {
            // check for input
            if(Input.GetKeyDown(_interactKey) && _isInInteractDistance)
            {
                Interact();
            }
        }

        public abstract void Interact();
    }
}
