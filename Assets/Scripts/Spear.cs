using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    int score = 1;

    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

       float x = Random.Range(-2.8f, 2.8f);
       float y = 6.0f;

       transform.position = new Vector3(x, y, 0);
    }

    void Update()
    {
        float speed = Random.Range(0.03f, 0.1f);
        transform.position += Vector3.down * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            GameManager.Instance.EndGame();
        }

        if(collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.AddScore(score);
            Destroy(this.gameObject );
        }
    }
}
