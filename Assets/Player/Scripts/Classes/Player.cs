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
        private Jumpable _jumpable;
        private IMovable _movable;
        private IStater _stater;

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

            _animated.Init(animator, animationsNames);
            _stater.Init(_fightable, _jumpable);
            _movable.Init(_animated, _jumpable, _fightable, _stater);
            _jumpable.Init(_animated, _movable, _stater);
            _fightable.Init(_animated, _stater);
        }


        private void SetVariables()
        {
            _fightable = GetComponent<IFightable>();
            _movable = GetComponent<IMovable>();
            _jumpable = GetComponent<Jumpable>();
            _animated = GetComponent<IAnimated>();
            _stater = GetComponent<IStater>();

            _fightable ??= GetComponentInChildren<IFightable>();
            _movable ??= GetComponentInChildren<IMovable>();
            _jumpable ??= GetComponentInChildren<Jumpable>();
            _animated ??= GetComponentInChildren<IAnimated>();
            _stater ??= GetComponentInChildren<IStater>();
        }
    }
}