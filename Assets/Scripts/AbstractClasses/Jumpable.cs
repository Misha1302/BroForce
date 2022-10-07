using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Interfaces
{
    public abstract class Jumpable : MonoBehaviour
    {
        [SerializeField] private string[] ignoredTags;
        protected readonly UnityEvent AfterInitEvent = new();
        protected readonly UnityEvent OnTriggerEnterEvent = new();

        private int _numberOfContacts;

        protected IStater Stater;
        protected IAnimated Animated;
        protected Vector2 JumpPower;
        protected Movable Movable;
        protected Rigidbody2D PlayerRigidbody2D;

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnTriggerEnterEvent?.Invoke();

            if (!ignoredTags.Contains(col.tag))
                _numberOfContacts++;
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (!ignoredTags.Contains(col.tag))
                _numberOfContacts--;
        }

        protected void Jump()
        {
            PlayerRigidbody2D.AddForce(JumpPower);
            StartCoroutine(StartJumpAnimation(0.2f));
        }

        protected void StartIdleAnimation()
        {
            Animated.StartAnimation(AnimationsEnum.Idle);
        }

        protected void StartRunAnimation()
        {
            Animated.StartAnimation(AnimationsEnum.Run);
        }

        private IEnumerator StartJumpAnimation(float timeOffset)
        {
            yield return new WaitForSeconds(timeOffset);
            Animated.StartAnimation(AnimationsEnum.Jump);
        }

        #region IAnimated interface implementation

        public abstract void UpdateScript();

        public void Init(IAnimated animated, Movable movable, IStater stater)
        {
            Animated = animated;
            Movable = movable;
            Stater = stater;

            AfterInitEvent?.Invoke();
        }

        public bool IsGrounded()
        {
            return _numberOfContacts > 0;
        }

        #endregion
    }
}