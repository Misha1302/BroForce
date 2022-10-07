using UnityEngine;

public abstract class MobVision : MonoBehaviour
{
    [SerializeField] private Rigidbody2D thisRigidbody2D;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float maxHeigth;
    [SerializeField] private float maxWigth;

    [HideInInspector]
    public bool SeePlayer
    {
        get
        {
            var dist = transform.position - target.position;

            return Mathf.Abs(dist.x) <= maxWigth && Mathf.Abs(dist.y) <= maxHeigth;
        }
    }
}
