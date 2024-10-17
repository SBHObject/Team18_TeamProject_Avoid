using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputContoller : MonoBehaviour
{
    private PlayerInput playerInput;

    public float Dir { get; private set; }

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput ������Ʈ�� �����ϴ�.");
        }
    }

    private void OnMove(InputValue value)
    {
        Dir = value.Get<float>();
    }

    public void isPlayer2(bool isPlayer2)
    {
        if (isPlayer2)
        {
            if (playerInput.actions.FindActionMap("Player2P") == null)
            {
                Debug.LogError("Player2P �׼� ���� ã�� �� �����ϴ�.");
            }
            playerInput.defaultActionMap = "Player2P";
        }
        else
        {
            if (playerInput.actions.FindActionMap("Player1P") == null)
            {
                Debug.LogError("Player1P �׼� ���� ã�� �� �����ϴ�.");
            }
            playerInput.defaultActionMap = "Player1P";
        }
    }
}
