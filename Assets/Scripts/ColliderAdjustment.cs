using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAdjustment : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;

    void Start()
    {
        // Get the Box Collider 2D component
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        // Adjust the position if needed
        boxCollider.transform.position = transform.position;
        circleCollider.transform.position = transform.position;

        // Adjust the offset if needed
        boxCollider.offset = new Vector2(0.0f, 0.57f);
        circleCollider.offset = new Vector2(0.0f, -0.51f);
    }
}
