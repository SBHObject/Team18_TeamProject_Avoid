using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMoveBase : MonoBehaviour
{
    //스프라이트 랜더러
    private SpriteRenderer spriteRenderer;

    //입력을 받을 스크립트
    protected InputContoller input;

    //이동 속도
    private float speed = 5f;
    protected float acceleration = 0.02f;
    protected float targetSpeed;
    protected float nowSpeed;

    //하드모드(미끄러짐 여부)
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

        //맵 탈출 방지
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

        //하드모드 여부에 따라 가속도 여부 결정
        AccelControl(isHardMode);

        //실제 이동 구현부
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
        //하드모드 여부에 따라 가속도 여부 결정
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
