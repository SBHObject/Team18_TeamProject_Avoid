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
    }

    private void OnMove(InputValue value)
    {
        Dir = value.Get<float>();
    }

    public void isPlayer2(bool isPlayer2)
    {
        if(isPlayer2)
        {
            playerInput.defaultActionMap = "Player2P";
        }
        else
        {
            playerInput.defaultActionMap = "Player1P";
        }
    }
}
