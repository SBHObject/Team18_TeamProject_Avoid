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
                                                      // �� ���� Ÿ�ٸ� �ǵ带 �ο�
                int activeShields = 0;

                // ShieldTarget1 ������Ʈ�� �ִ��� Ȯ��
                ShieldTarget1 target1 = FindObjectOfType<ShieldTarget1>();
                if (target1 != null && activeShields < 2)
                {
                    GameObject shield1 = Instantiate(shield, target1.transform.position, Quaternion.identity);
                    shield1.GetComponent<Shield>().SetTarget(target1.transform);
                    activeShields++;
                }

                // ShieldTarget2 ������Ʈ�� �ִ��� Ȯ��
                ShieldTarget2 target2 = FindObjectOfType<ShieldTarget2>();
                if (target2 != null && activeShields < 2)
                {
                    GameObject shield2 = Instantiate(shield, target2.transform.position, Quaternion.identity);
                    shield2.GetComponent<Shield>().SetTarget(target2.transform);
                    activeShields++;
                }

                // ShieldTarget3 ������Ʈ�� �ִ��� Ȯ��
                ShieldTarget3 target3 = FindObjectOfType<ShieldTarget3>();
                if (target3 != null && activeShields < 2)
                {
                    GameObject shield3 = Instantiate(shield, target3.transform.position, Quaternion.identity);
                    shield3.GetComponent<Shield>().SetTarget(target3.transform);
                }
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
