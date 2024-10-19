using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputContoller : MonoBehaviour
{
    private PlayerInput playerInput;

    public UnityAction<float> OnPlayer2Move;

    public float Dir { get; private set; }

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnMove(InputValue value)
    {
        Dir = value.Get<float>();
    }

    public void IsPlayer2(bool isPlayer2)
    {
        if(isPlayer2)
        {
            playerInput.SwitchCurrentActionMap("Player2P");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("Player1P");
        }
    }
}