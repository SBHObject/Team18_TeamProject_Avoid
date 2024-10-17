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
            Debug.LogError("PlayerInput 컴포넌트가 없습니다.");
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
                Debug.LogError("Player2P 액션 맵을 찾을 수 없습니다.");
            }
            playerInput.defaultActionMap = "Player2P";
        }
        else
        {
            if (playerInput.actions.FindActionMap("Player1P") == null)
            {
                Debug.LogError("Player1P 액션 맵을 찾을 수 없습니다.");
            }
            playerInput.defaultActionMap = "Player1P";
        }
    }
}
