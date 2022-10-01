using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IAnimated
    {
        public AnimationsEnum? GetCurrentAnimation();
        public void StartAnimation(AnimationsEnum animationType);
        public void Init(Animator animator, Dictionary<AnimationsEnum, string> animationsDictionary);
    }
}