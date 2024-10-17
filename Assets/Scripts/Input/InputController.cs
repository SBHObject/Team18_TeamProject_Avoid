using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputContoller : MonoBehaviour
{
    public float Dir { get; private set; }

    private void OnMove(InputValue value)
    {
        Dir = value.Get<float>();
    }
}
