using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace PlayerClasses
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private IAnimated _animated;
        private IFightable _fightable;
        private IJumpable _jumpable;
        private IMovable _movable;

        private void Start()
        {
            SetVariables();
            InitInterfaces();
        }

        private void Update()
        {
            _fightable.UpdateScript();
            _movable.UpdateScript();
            _jumpable.UpdateScript();
        }

        private void InitInterfaces()
        {
            var animationsNames = new Dictionary<AnimationsEnum, string>
            {
                { AnimationsEnum.Attack, "Attack" },
                { AnimationsEnum.Jump, "Jump" },
                { AnimationsEnum.Die, "Die" },
                { AnimationsEnum.Idle, "Idle" },
                { AnimationsEnum.Run, "Run" },
                { AnimationsEnum.Sit, "Sit" },
                { AnimationsEnum.Hub, "Hub" }
            };

            _movable.Init(_animated, _jumpable, _fightable);
            _animated.Init(animator, animationsNames);
            _jumpable.Init(_animated, _movable);
            _fightable.Init(_animated);
        }


        private void SetVariables()
        {
            _fightable = GetComponent<IFightable>();
            _movable = GetComponent<IMovable>();
            _jumpable = GetComponent<IJumpable>();
            _animated = GetComponent<IAnimated>();

            _fightable ??= GetComponentInChildren<IFightable>();
            _movable ??= GetComponentInChildren<IMovable>();
            _jumpable ??= GetComponentInChildren<IJumpable>();
            _animated ??= GetComponentInChildren<IAnimated>();
        }
    }
}