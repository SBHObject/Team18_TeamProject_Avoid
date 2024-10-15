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

        // 왼쪽 방향 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = -1f;
            renderer.flipX = true;  // 왼쪽으로 향할 때 스프라이트를 뒤집음
        }

        // 오른쪽 방향 이동
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1f;
            renderer.flipX = false;  // 오른쪽으로 향할 때 스프라이트를 정상 방향으로 설정
        }

        // 이동
        transform.position += Vector3.right * moveDirection * direction;

        // 화면 경계 체크
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
