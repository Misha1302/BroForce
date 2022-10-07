using System;
using Interfaces;
using UnityEngine;

public abstract class Movable : MonoBehaviour
{
    private static readonly Quaternion quaternionUpOneHundredEighty = new(0, 1, 0, 0);
    private static readonly Quaternion quaternionZero = new(0, 0, 0, 1);

    [SerializeField] private float speed;
    [SerializeField] private float stopSpeed;
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private Transform _objectToRotate;

    private IStater _stater;
    private IAnimated _animated;
    private IFightable _fightable;
    private float _horizontalStopSpeed;
    private Rigidbody2D _rigidbody2D;
    private float _t;


    private static void KeepHorizontalSpeedLimit(Rigidbody2D rbParameter, float horizontalLimit)
    {
        Vector2 KeepHorizontalSpeedLimitInternal()
        {
            var velocity = rbParameter.velocity;
            return velocity.x > 0
                ? velocity.x < horizontalLimit ? velocity : new Vector2(horizontalLimit, velocity.y)
                : velocity.x > -horizontalLimit ? velocity : new Vector2(-horizontalLimit, velocity.y);
        }

        rbParameter.velocity = KeepHorizontalSpeedLimitInternal();
    }

    private static void SetPlayerFlip(Transform rotateTransform, float speed)
    {
        // 0.04f is necessary so that when meeting with an obstacle the player does not turn back and forth
        rotateTransform.rotation = speed > 0.04f
            ? quaternionUpOneHundredEighty // Y = 180
            : quaternionZero; // X,Y,Z = 0
    }

    private void StopPlayer(float xVelocity)
    {
        xVelocity = Mathf.Lerp(xVelocity, 0, _t);
        _rigidbody2D.velocity = new Vector2(xVelocity, _rigidbody2D.velocity.y);

        _t += 0.5f * Time.deltaTime * stopSpeed;

        if (Math.Abs(_rigidbody2D.velocity.magnitude) > 0.5f) return;

        if (_animated.GetCurrentAnimation() == AnimationsEnum.Run)
            StartIdleAnimation();
    }

    private void StartIdleAnimation()
    {
        _animated.StartAnimation(AnimationsEnum.Idle);
    }

    private void StartRunAnimation()
    {
        _animated.StartAnimation(AnimationsEnum.Run);
    }

    private void MoveInternal()
    {
        var horizontal = HorizontalMovement * speed * Time.deltaTime;

        _rigidbody2D.AddForce(new Vector2(horizontal, 0));

        KeepHorizontalSpeedLimit(_rigidbody2D, maxHorizontalSpeed);
        SetPlayerFlip(_objectToRotate, _rigidbody2D.velocity.x);
        StartRunAnimation();
    }

    private void Move()
    {
        if (HorizontalMovement == 0 || _fightable.IsFighting())
        {
            if (_horizontalStopSpeed == 0)
            {
                _t = 0;
                _horizontalStopSpeed = _rigidbody2D.velocity.x;
            }

            StopPlayer(_horizontalStopSpeed);
            return;
        }

        _horizontalStopSpeed = 0;
        MoveInternal();
    }


    #region IAnimated interface implementation

    public float HorizontalMovement { get; protected set; }

    public void UpdateScript()
    {
        UpdateHorizontalMovement();

        if (!_stater.CanRun) return;

        Move();
    }

    protected abstract void UpdateHorizontalMovement();

    public void Init(IAnimated animated, IFightable fightable, IStater stater)
    {
        _animated = animated;
        _fightable = fightable;
        _stater = stater;

        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    #endregion
}
