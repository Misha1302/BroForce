using Interfaces;
using UnityEngine;

namespace PlayerClasses
{
    public class PlayerStater : MonoBehaviour, IStater
    {
        private IFightable _fightable;
        private Jumpable _jumpable;

        public bool CanFight => _fightable.IsFighting() && Input.GetMouseButton(0);
        public bool CanRun => _jumpable.IsGrounded();
        public bool CanJump => Input.GetKeyDown(KeyCode.Space) && _jumpable.IsGrounded();

        public void Init(IFightable fightable, Jumpable jumpable)
        {
            _fightable = fightable;
            _jumpable = jumpable;
        }
    }
}