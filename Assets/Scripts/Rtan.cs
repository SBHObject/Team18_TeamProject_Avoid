using System.Collections;
using UnityEngine;

public class Rtan : MonoBehaviour
{
    private float speed = 0.05f;
    private SpriteRenderer spriteRenderer;

    private float acceleration = 0.05f;
    private float targetSpeed;
    private float nowSpeed = 0;
    public bool isDead = false;

    [SerializeField]
    private bool isHardMode;

    // 플레이어 키 입력 설정
    public KeyCode leftKey;  // 왼쪽 이동 키
    public KeyCode rightKey; // 오른쪽 이동 키

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    private void Move()
    {
        // 방향 결정 (할당된 키로 이동)
        if (Input.GetKeyDown(leftKey))
        {
            LookLeft();
        }

        if (Input.GetKeyDown(rightKey))
        {
            LookRight();
        }

        // 맵 탈출 방지
        if (transform.position.x > 2.8f)
        {
            nowSpeed = 0;
            LookLeft();
            transform.position = new Vector3(2.8f, transform.position.y, 0);
        }

        if (transform.position.x < -2.8f)
        {
            nowSpeed = 0;
            LookRight();
            transform.position = new Vector3(-2.8f, transform.position.y, 0);
        }

        // 하드모드 여부에 따른 가속도 처리
        if (isHardMode)
        {
            nowSpeed = Mathf.Lerp(nowSpeed, targetSpeed, acceleration);
            if (Mathf.Abs(nowSpeed) >= Mathf.Abs(targetSpeed) - 0.0001f)
            {
                nowSpeed = targetSpeed;
            }
        }
        else
        {
            nowSpeed = targetSpeed;
        }

        // 이동 처리
        transform.position += Vector3.right * nowSpeed;
    }

    private void LookLeft()
    {
        targetSpeed = speed * -1;
        spriteRenderer.flipX = true;
    }

    private void LookRight()
    {
        targetSpeed = speed;
        spriteRenderer.flipX = false;
    }
}
