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

        private float _timer;


        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer < -3.4028235E+38F) _timer = Time.deltaTime; // -3.4028235E+38F = float.MinValue - 100
            if (!_fighting || _timer < _disableFightStateTime) return;

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
            _disableFightStateTime = _timer + ANIMATION_TIME;

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
            if (_fighting || !Input.GetMouseButton(0)) return;

            Attack();
        }

        public void Init(IAnimated animated)
        {
            _animated = animated;
        }

        #endregion
    }
}