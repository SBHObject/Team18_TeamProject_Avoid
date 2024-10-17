using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGhostMovement : CharacterMoveBase
{
    protected override void MoveDirection()
    {
        if (moveDir < 0)
        {
            LookRight();
        }
        else if (moveDir > 0)
        {
            LookLeft();
        }
    }
}
