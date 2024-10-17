using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RtanMovement : CharacterMoveBase
{
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
