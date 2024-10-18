using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Transform target;  // �ǵ尡 ����ٴ� ���� Ÿ��

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Start()
    {
        if (target != null)
        {
            Destroy(gameObject, 3.0f);  // �ǵ�� 3�� ���� ����
        }
        else
        {
            Destroy(gameObject);  // Ÿ���� ������ �ǵ� �������� ����
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // Ÿ���� ���� �ǵ��� ��ġ�� ����
            transform.position = target.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject, 0.5f);  // ��ֹ��� �ε����� �ǵ尡 0.5�� �� �ı�
        }
    }
}
