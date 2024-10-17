using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMoveBase : MonoBehaviour
{
    //��������Ʈ ������
    private SpriteRenderer spriteRenderer;

    //�Է��� ���� ��ũ��Ʈ
    protected InputContoller input;

    //�̵� �ӵ�
    private float speed = 5f;
    protected float acceleration = 0.02f;
    protected float targetSpeed;
    protected float nowSpeed;

    //�ϵ���(�̲����� ����)
    protected bool isHardMode = false;

    private float mapLimit = 2.8f;

    public bool IsDead {  get; set; } = false;

    private bool is2P = false;

    protected float moveDir;

    private void Awake()
    {
        input = GetComponent<InputContoller>();
    }

    protected virtual void Start()
    {
        input.OnPlayer2Move += Player2Dir;
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetSpeed = speed;
        LookLeft();
    }

    private void Update()
    {
        if(is2P == false)
        {
            moveDir = input.Dir;
        }

        Move();
    }

    protected void Move()
    {
        if(IsDead == true)
        {
            return;
        }

        MoveDirection();

        //�� Ż�� ����
        if (transform.position.x > mapLimit)
        {
            nowSpeed = 0;
            LookLeft();
            transform.position = new Vector3(2.8f, transform.position.y, 0);
        }

        if (transform.position.x < mapLimit * -1)
        {
            nowSpeed = 0;
            LookRight();
            transform.position = new Vector3(-2.8f, transform.position.y, 0);
        }

        //�ϵ��� ���ο� ���� ���ӵ� ���� ����
        AccelControl(isHardMode);

        //���� �̵� ������
        transform.position += Vector3.right * nowSpeed * Time.deltaTime;
    }

    protected void LookLeft()
    {
        targetSpeed = speed * -1;
        spriteRenderer.flipX = true;
    }

    protected void LookRight()
    {
        targetSpeed = speed * 1f;
        spriteRenderer.flipX = false;
    }

    protected virtual void MoveDirection()
    {
        
    }

    protected virtual void AccelControl(bool isHardMode)
    {
        //�ϵ��� ���ο� ���� ���ӵ� ���� ����
        if (isHardMode)
        {
            nowSpeed = Mathf.Lerp(nowSpeed, targetSpeed, acceleration);
            if (Mathf.Abs(nowSpeed) >= Mathf.Abs(targetSpeed) - 0.0001)
            {
                nowSpeed = targetSpeed;
            }
        }
        else
        {
            nowSpeed = targetSpeed;
        }
    }

    public void SetHardMode(bool _isHardMode)
    {
        isHardMode = _isHardMode;
    }

    public void SetThis2P(InputContoller contoller)
    {
        is2P = true;

        input = contoller;
    }

    private void Player2Dir(float value)
    {
        moveDir = value;
    }
}
