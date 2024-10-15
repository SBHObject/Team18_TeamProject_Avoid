using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    int score = 1;

    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

       float x = Random.Range(-2.4f, 2.4f);
       float y = Random.Range(5.0f, 6.0f);

       transform.position = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
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
