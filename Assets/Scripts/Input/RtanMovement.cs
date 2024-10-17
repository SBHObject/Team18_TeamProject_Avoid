using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RtanMovement : CharacterMoveBase
{
    protected override void MoveDirection()
    {
        if (moveDir < 0)
        {
            LookLeft();
        }
        else if (moveDir > 0)
        {
            LookRight();
        }
    }
}
