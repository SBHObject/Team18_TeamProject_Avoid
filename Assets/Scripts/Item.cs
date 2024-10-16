using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int destroyedObjectCount = 0;
    public GameObject shield;
    SpriteRenderer renderer;
    private string itemName;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        string objectName = gameObject.name;
        itemName = objectName;
        float x = Random.Range(-2.4f, 2.4f);
        float y = Random.Range(5.0f, 6.0f);
        transform.position = new Vector3(x, y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (itemName == "ShieldItem(Clone)")
            {
                Instantiate(shield);
            } else if (itemName == "Bomb(Clone)")
            {
                GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Obstacle");
                foreach (GameObject obj in objectsToDestroy)
                {
                    Destroy(obj);
                    destroyedObjectCount++;
                }
                GameManager.Instance.AddScore(destroyedObjectCount);
            }
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
