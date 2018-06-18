using System;
using UnityEngine;

public class SideRotationGroup
{
    public GameObject CenterPiece { get; set; }
    public Side SideToRotate { get; set; }
    public SideRotateDirection DirectionToRotate { get; set; }
    public float RotationSoFar { get; set; }

    public SideRotationGroup(Side side, SideRotateDirection direction)
    {
        this.SideToRotate = side;
        this.DirectionToRotate = direction;
        this.RotationSoFar = 0;
    }

    internal bool HasStarted()
    {
        return this.RotationSoFar != 0;
    }

    internal bool HasFinished()
    {
        return Math.Abs(this.RotationSoFar) == 90 ;
    }
}
