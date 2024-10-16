using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int destroyedObjectCount = 0;
    public GameObject shield;
    SpriteRenderer renderer;
    private string itemName;
    public GameObject shieldEffect;  // 실드 효과로 나타날 이미지(이펙트) 프리팹
    public GameObject bombEffect;  // 폭탄 효과로 나타날 이미지(이펙트) 프리팹

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
                // 실드 효과 생성 및 0.5초 후 제거
                GameObject shieldEffectInstance = Instantiate(shieldEffect, transform.position, Quaternion.identity);
                Destroy(shieldEffectInstance, 0.5f);  // 실드 효과 0.5초 후 제거
                Instantiate(shield);
            } else if (itemName == "Bomb(Clone)")
            {
                // 폭탄 효과 생성 및 0.5초 후 제거
                GameObject bombEffectInstance = Instantiate(bombEffect, transform.position, Quaternion.identity);
                Destroy(bombEffectInstance, 0.5f);  // 폭탄 효과 0.5초 후 제거

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
