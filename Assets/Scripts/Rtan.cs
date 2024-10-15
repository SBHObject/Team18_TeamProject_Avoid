using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rtan : MonoBehaviour
{
    float direction = 0.05f;
    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = 0f;

        // ���� ���� �̵�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = -1f;
            renderer.flipX = true;  // �������� ���� �� ��������Ʈ�� ������
        }

        // ������ ���� �̵�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1f;
            renderer.flipX = false;  // ���������� ���� �� ��������Ʈ�� ���� �������� ����
        }

        // �̵�
        transform.position += Vector3.right * moveDirection * direction;

        // ȭ�� ��� üũ
        if (transform.position.x > 2.6f)
        {
            transform.position = new Vector3(2.6f, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -2.6f)
        {
            transform.position = new Vector3(-2.6f, transform.position.y, transform.position.z);
        }
    }
}
