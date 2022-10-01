using System.Collections;
using Interfaces;
using UnityEngine;

namespace PlayerClasses
{
    public class PlayerFightBehaviour : MonoBehaviour, IFightable
    {
        private const float ANIMATION_TIME = 0.583f;

        [SerializeField] private Transform shootingPoint;
        [SerializeField] private float strokeLength;
        [SerializeField] private int makingDamage;


        private IAnimated _animated;
        private float _disableFightStateTime;

        private bool _fighting;
        private IStater _stater;


        private void Update()
        {
            if (!_fighting || Time.time < _disableFightStateTime) return;

            if (_animated.GetCurrentAnimation() == AnimationsEnum.Attack)
                _animated.StartAnimation(AnimationsEnum.Idle);
            StartCoroutine(StopFighting());
        }

        private IEnumerator StopFighting(float timeOffset = 0.1f)
        {
            yield return new WaitForSeconds(timeOffset);
            _fighting = false;
        }

        private void StartAttackAnimation()
        {
            _animated.StartAnimation(AnimationsEnum.Attack);
        }


        private void MakeDamage()
        {
            var position = shootingPoint.position;
            var direction = new Vector2(position.x - transform.position.x, 0);
            var hit = Physics2D.Raycast(position, direction, strokeLength);

            if (!hit) return;
            if (hit.transform.TryGetComponent<IDamageable>(out var iDamageable))
                iDamageable.GetDamage(makingDamage);
        }

        private void Attack()
        {
            _fighting = true;
            _disableFightStateTime = Time.time + ANIMATION_TIME;

            MakeDamage();
            StartAttackAnimation();
        }

        #region IAnimated interface implementation

        public bool IsFighting()
        {
            return _fighting;
        }

        public void UpdateScript()
        {
            if (!_stater.CanFight) return;

            Attack();
        }

        public void Init(IAnimated animated, IStater stater)
        {
            _animated = animated;
            _stater = stater;
        }

        #endregion
    }
}