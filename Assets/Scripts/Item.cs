using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int destroyedObjectCount = 0;
    public GameObject shield;
    SpriteRenderer renderer;
    private string itemName;
    public GameObject shieldEffect;  // �ǵ� ȿ���� ��Ÿ�� �̹���(����Ʈ) ������
    public GameObject bombEffect;  // ��ź ȿ���� ��Ÿ�� �̹���(����Ʈ) ������

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
                // �ǵ� ȿ�� ���� �� 0.5�� �� ����
                GameObject shieldEffectInstance = Instantiate(shieldEffect, transform.position, Quaternion.identity);
                Destroy(shieldEffectInstance, 0.5f);  // �ǵ� ȿ�� 0.5�� �� ����
                Instantiate(shield);
            } else if (itemName == "Bomb(Clone)")
            {
                // ��ź ȿ�� ���� �� 0.5�� �� ����
                GameObject bombEffectInstance = Instantiate(bombEffect, transform.position, Quaternion.identity);
                Destroy(bombEffectInstance, 0.5f);  // ��ź ȿ�� 0.5�� �� ����

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
