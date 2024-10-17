using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGhostMovement : CharacterMoveBase
{
    protected override void MoveDirection()
    {
        if (input.Dir < 0)
        {
            LookRight();
        }
        else if (input.Dir > 0)
        {
            LookLeft();
        }
    }
}
