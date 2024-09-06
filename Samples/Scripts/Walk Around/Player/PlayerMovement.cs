using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;

namespace Martian.Reel.Samples
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float MoveSpeed = 7f;

        private bool _canMove = true;

        [Header("References")]
        [SerializeField] private Animator _anim = null;
        private CharacterController _controller = null;
        private Transform _cameraTransform = null;

        private void Start()
        {
            // get references
            _controller = GetComponent<CharacterController>();
            _cameraTransform = Camera.main.transform;

            // subscribe to reel starting and stopping
            ReelDirector.Instance.OnReelStart += () => { _canMove = false; };
            ReelDirector.Instance.OnReelEnd += () => { _canMove = true; };
        }

        private void Update()
        {
            Movement();
        }

        private void LateUpdate()
        {
            // update animation
            if (_canMove)            {
                _anim.SetFloat("Speed", _controller.velocity.normalized.magnitude);

            }
            else
            {
                _anim.SetFloat("Speed", 0f);
            }
        }

        private void Movement()
        {
            if (!_canMove) { return; }

            // get input
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if(moveInput.sqrMagnitude > 1)
            {
                moveInput.Normalize();
            }

            // get input relative to camera for direction
            Vector3 forwardVector = _cameraTransform.forward;
            Vector3 rightVector = _cameraTransform.right;
            forwardVector.y = 0f;
            rightVector.y = 0f;

            Vector3 moveDirection = (forwardVector * moveInput.y) + (rightVector * moveInput.x);

            // move controller with move direction
            _controller.Move(moveDirection * MoveSpeed * Time.deltaTime);

            // rotate player to direction
            RotatePlayer(moveDirection);
        }

        private void RotatePlayer(Vector3 direction)
        {
            if (direction.magnitude < 0.15f) { return; }

            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        }

    }
}
