using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float parallaxStrength;
    [SerializeField] private bool disableVerticalParallax;

    private Vector3 targetPreviousPosition;

    private void Start()
    {
        if (!target) target = Camera.main.transform;

        targetPreviousPosition = target.position;
    }

    private void FixedUpdate()
    {
        var delta = target.position - targetPreviousPosition;
        delta.y = 0;

        targetPreviousPosition = target.position;
        transform.Translate(delta * parallaxStrength);
    }
}
