using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Transform target;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject, 0.5f);
        }
    }
    void Start()
    {
        GameObject targetObject = GameObject.FindWithTag("Player");

        if (targetObject != null)
        {
            target = targetObject.transform;

            Destroy(gameObject, 3.0f);
        }
    }
    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
