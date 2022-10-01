using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerClasses
{
    /// <summary>
    ///     Must be a trigger collider on the bottom of the character
    /// </summary>
    public class PlayerJumpBehaviour : MonoBehaviour, IJumpable
    {
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private int jumpPower;

        private readonly UnityEvent _onTriggerEnter = new();

        private IAnimated _animated;
        private IMovable _movable;
        private int _numberOfContacts;

        private void OnTriggerEnter2D(Collider2D col)
        {
            _onTriggerEnter?.Invoke();

            _numberOfContacts++;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _numberOfContacts--;
        }

        private void Jump()
        {
            playerRigidbody.AddForce(new Vector2(0, jumpPower));
            StartJumpAnimation();
        }

        private void StartIdleAnimation()
        {
            _animated.StartAnimation(AnimationsEnum.Idle);
        }

        private void StartRunAnimation()
        {
            _animated.StartAnimation(AnimationsEnum.Run);
        }

        private void StartJumpAnimation()
        {
            _animated.StartAnimation(AnimationsEnum.Jump);
        }

        #region IAnimated interface implementation

        public void UpdateScript()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            if (!IsGrounded()) return;

            Jump();
        }

        public void Init(IAnimated animated, IMovable movable)
        {
            _animated = animated;
            _movable = movable;

            _onTriggerEnter.AddListener(() =>
            {
                var isJumping = _animated.GetCurrentAnimation() == AnimationsEnum.Jump;

                if (IsGrounded() || !isJumping) return;

                var isRunning = _movable.HorizontalMovement != 0;

                if (!isRunning) StartIdleAnimation();
                else StartRunAnimation();
            });
        }

        public bool IsGrounded()
        {
            return _numberOfContacts > 0;
        }

        #endregion
    }
}