using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerClasses
{
    public class PlayerAnimatedBehaviour : MonoBehaviour, IAnimated
    {
        private Dictionary<AnimationsEnum, string> _animationsDictionary;
        private Animator _animator;
        private AnimationsEnum? _currentAnimation;

        #region IAnimated interface implementation

        public AnimationsEnum? GetCurrentAnimation()
        {
            return _currentAnimation;
        }

        public void StartAnimation(AnimationsEnum animationType)
        {
            if (_currentAnimation == animationType) return;
            _animator.SetTrigger(_animationsDictionary[AnimationsEnum.Hub]);

            if (animationType != AnimationsEnum.Hub)
                _animator.SetTrigger(_animationsDictionary[animationType]);
            _currentAnimation = animationType;
        }

        public void Init(Animator animator, Dictionary<AnimationsEnum, string> animationsDictionary)
        {
            _animator = animator;
            _animationsDictionary = animationsDictionary;

            StartAnimation(AnimationsEnum.Idle);
        }

        #endregion
    }
}