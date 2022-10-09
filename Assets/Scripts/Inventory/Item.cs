using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 maxDistance;
    [SerializeField] private int force;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform item;

    private Rigidbody2D thisRigidbody2D;

    private void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var vector = player.position - transform.position;
        if (Mathf.Abs(vector.x) > maxDistance.x || Mathf.Abs(vector.y) > maxDistance.y) return;

        thisRigidbody2D.AddForce(vector * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag(player.tag)) return;

        var tempPos = inventory.GetFreePosition();
        if (tempPos == null) return;
        var freePos = (Vector2Int)tempPos;

        var newItem = Instantiate(item);
        inventory.SetItem(newItem, freePos.x, freePos.y);
        Destroy(gameObject);
    }
}
