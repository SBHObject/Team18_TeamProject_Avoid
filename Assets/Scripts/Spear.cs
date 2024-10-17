using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    int score = 1;
    SpriteRenderer renderer;
    float speed;
    float fixedPositionX;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        fixedPositionX = Random.Range(-2.8f, 2.8f);
        float y = 6.0f;

        transform.position = new Vector3(fixedPositionX, y, 0);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(fixedPositionX, transform.position.y - speed, 0);
        speed = Random.Range(0.03f, 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            GameManager.Instance.EndGame();
        }

        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
            GameManager.Instance.AddScore(score);
        }
    }
}
