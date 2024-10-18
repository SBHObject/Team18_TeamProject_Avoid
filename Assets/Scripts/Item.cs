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
                                                      // 두 개의 타겟만 실드를 부여
                int activeShields = 0;

                // ShieldTarget1 컴포넌트가 있는지 확인
                ShieldTarget1 target1 = FindObjectOfType<ShieldTarget1>();
                if (target1 != null && activeShields < 2)
                {
                    GameObject shield1 = Instantiate(shield, target1.transform.position, Quaternion.identity);
                    shield1.GetComponent<Shield>().SetTarget(target1.transform);
                    activeShields++;
                }

                // ShieldTarget2 컴포넌트가 있는지 확인
                ShieldTarget2 target2 = FindObjectOfType<ShieldTarget2>();
                if (target2 != null && activeShields < 2)
                {
                    GameObject shield2 = Instantiate(shield, target2.transform.position, Quaternion.identity);
                    shield2.GetComponent<Shield>().SetTarget(target2.transform);
                    activeShields++;
                }

                // ShieldTarget3 컴포넌트가 있는지 확인
                ShieldTarget3 target3 = FindObjectOfType<ShieldTarget3>();
                if (target3 != null && activeShields < 2)
                {
                    GameObject shield3 = Instantiate(shield, target3.transform.position, Quaternion.identity);
                    shield3.GetComponent<Shield>().SetTarget(target3.transform);
                }
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
