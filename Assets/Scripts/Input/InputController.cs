using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputContoller : MonoBehaviour
{
    private PlayerInput playerInput;

    private CharacterMoveBase player2Move;

    public UnityAction<float> OnPlayer2Move;

    public float Dir { get; private set; }
    private float player2Dir;

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnMove(InputValue value)
    {
        Dir = value.Get<float>();
    }

    private void OnMove2P(InputValue value)
    {
        player2Dir = value.Get<float>();
        OnPlayer2Move?.Invoke(player2Dir);
    }

    public void IsPlayer2(CharacterMoveBase _player2Move)
    {
        player2Move = _player2Move;

        player2Move.SetThis2P(this);
    }
}