using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        input = GetComponent<InputContoller>();
    }

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetSpeed = speed;
        LookLeft();
    }

    private void Update()
    {
        Move();
    }

    protected void Move()
    {
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

}
