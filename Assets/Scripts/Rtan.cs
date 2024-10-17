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

    // �÷��̾� Ű �Է� ����
    public KeyCode leftKey;  // ���� �̵� Ű
    public KeyCode rightKey; // ������ �̵� Ű

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
        // ���� ���� (�Ҵ�� Ű�� �̵�)
        if (Input.GetKeyDown(leftKey))
        {
            LookLeft();
        }

        if (Input.GetKeyDown(rightKey))
        {
            LookRight();
        }

        // �� Ż�� ����
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

        // �ϵ��� ���ο� ���� ���ӵ� ó��
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

        // �̵� ó��
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
