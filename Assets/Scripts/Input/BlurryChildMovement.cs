using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurryChildMovement : CharacterMoveBase
{
    protected override void Start()
    {
        base.Start();
        SetAccel(isHardMode);
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

    private void SetAccel(bool isHardMode)
    {
        if(isHardMode)
        {
            acceleration = acceleration * 0.5f;
        }
    }

    protected override void AccelControl(bool isHardMode)
    {
        nowSpeed = Mathf.Lerp(nowSpeed, targetSpeed, acceleration);
        if (Mathf.Abs(nowSpeed) >= Mathf.Abs(targetSpeed) - 0.0001)
        {
            nowSpeed = targetSpeed;
        }
    }
}
