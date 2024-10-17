using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BlurryChildMovement : CharacterMoveBase
{
    protected override void Start()
    {
        base.Start();
        acceleration = acceleration * 2;
    }

    protected override void MoveDirection()
    {
        if (input.Dir < 0)
        {
            LookLeft();
        }
        else if (input.Dir > 0)
        {
            LookRight();
        }
    }
}
