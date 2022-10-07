using Interfaces;
using UnityEngine;

namespace PlayerClasses
{
    /// <summary>
    ///     Must be a trigger collider on the bottom of the character
    /// </summary>
    public class PlayerJumpBehaviour : Jumpable
    {
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private int jumpPower;

        private void Awake()
        {
            AfterInitEvent.AddListener(() =>
            {
                PlayerRigidbody2D = playerRigidbody;
                JumpPower = new Vector2(0, jumpPower);
            });

            OnTriggerEnterEvent.AddListener(() =>
            {
                var isJumping = Animated.GetCurrentAnimation() == AnimationsEnum.Jump;

                if (IsGrounded() || !isJumping) return;

                var isRunning = Movable.HorizontalMovement != 0;

                if (!isRunning) StartIdleAnimation();
                else StartRunAnimation();
            });
        }

        public override void UpdateScript()
        {
            if (!Stater.CanJump) return;

            Jump();
        }
    }
}