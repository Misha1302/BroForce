using UnityEngine;

namespace PlayerClasses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMoveBehaviour : Movable
    {
        protected override void UpdateHorizontalMovement()
        {
            // System.Math returns -1 if number < 0, 0 if number == 0, 1, if number > 0
            HorizontalMovement = System.Math.Sign(Input.GetAxis("Horizontal"));
            // BUT MATHF returns -1 if number < 0 and 1 if number >= 0                  DON't USE IT
        }
    }
}