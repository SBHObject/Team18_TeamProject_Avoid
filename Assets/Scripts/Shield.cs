using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Transform target;  // 실드가 따라다닐 단일 타겟

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Start()
    {
        if (target != null)
        {
            Destroy(gameObject, 3.0f);  // 실드는 3초 동안 유지
        }
        else
        {
            Destroy(gameObject);  // 타겟이 없으면 실드 생성되지 않음
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // 타겟을 따라 실드의 위치를 설정
            transform.position = target.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject, 0.5f);  // 장애물에 부딪히면 실드가 0.5초 후 파괴
        }
    }
}
