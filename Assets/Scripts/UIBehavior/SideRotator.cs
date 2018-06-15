using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRotator : MonoBehaviour
{
    private GameObject[,,] SolvedLocation { get; set; }
    private GameObject[,,] Pieces { get; set; }
    private Side SideToRotate { get; set; }
    private SideRotateDirection DirectionToRotate { get; set; }

    // Use this for initialization
    void Start()
    {
        SetRubikCubeStart();
        DirectionToRotate = SideRotateDirection.Clockwise;
    }

    // Update is called once per frame
    void Update()
    {
        SetSideRotateDirection();
        RotateSide();
    }

    /// <summary>
    /// Determines which side to rotate
    /// </summary>
    private void RotateSide()
    {
        int howManyTimes = 1;
        if (DirectionToRotate == SideRotateDirection.CounterClockwise)
        {
            howManyTimes = 3;
        }
        for (int c = 0; c < howManyTimes; c++)
        {
            switch (SideToRotate)
            {
                case Side.White:
                    RotateWhiteSide();
                    break;
                case Side.Yellow:
                    RotateYellowSide();
                    break;
                case Side.Orange:
                    RotateOrangeSide();
                    break;
                case Side.Red:
                    RotateRedSide();
                    break;
                case Side.Blue:
                    RotateBlueSide();
                    break;
                case Side.Green:
                    RotateGreenSide();
                    break;
            }
        }
    }

    /// <summary>
    /// Rotates the Green Side of the Cube
    /// </summary>
    private void RotateGreenSide()
    {
        for (int f = 0; f < 3; f++)
        {
            for (int t = 0; t < 3; t++)
            {
                if (!(f == 1 && t == 1))
                {
                    Pieces[f, 2, t].transform.parent = Pieces[1, 2, 1].transform;
                }
            }
        }
        Pieces[1, 2, 1].transform.Rotate(0, -90, 0);    //Rotate Clockwise

        //Update Array with new locations
        //Rotate Corners in Array
        GameObject fl = Pieces[0, 2, 0];
        Pieces[0, 2, 0] = Pieces[2, 2, 0];
        Pieces[2, 2, 0] = Pieces[2, 2, 2];
        Pieces[2, 2, 2] = Pieces[0, 2, 2];
        Pieces[0, 2, 2] = fl;
        //Rotate Edges in Array
        GameObject fm = Pieces[0, 2, 1];
        Pieces[0, 2, 1] = Pieces[1, 2, 0];
        Pieces[1, 2, 0] = Pieces[2, 2, 1];
        Pieces[2, 2, 1] = Pieces[1, 2, 2];
        Pieces[1, 2, 2] = fm;
    }

    /// <summary>
    /// Rotates the Blue Side of the Cube
    /// </summary>
    private void RotateBlueSide()
    {
        for (int f = 0; f < 3; f++)
        {
            for (int t = 0; t < 3; t++)
            {
                if (!(f == 1 && t == 1))
                {
                    Pieces[f, 0, t].transform.parent = Pieces[1, 0, 1].transform;
                }
            }
        }
        Pieces[1, 0, 1].transform.Rotate(0, 90, 0);    //Rotate Clockwise

        //Update Array with new locations
        //Rotate Corners in Array
        GameObject fl = Pieces[0, 0, 0];
        Pieces[0, 0, 0] = Pieces[0, 0, 2];
        Pieces[0, 0, 2] = Pieces[2, 0, 2];
        Pieces[2, 0, 2] = Pieces[2, 0, 0];
        Pieces[2, 0, 0] = fl;
        //Rotate Edges in Array
        GameObject fm = Pieces[0, 0, 1];
        Pieces[0, 0, 1] = Pieces[1, 0, 2];
        Pieces[1, 0, 2] = Pieces[2, 0, 1];
        Pieces[2, 0, 1] = Pieces[1, 0, 0];
        Pieces[1, 0, 0] = fm;
    }

    /// <summary>
    /// Rotates the Red Side of the Cube
    /// </summary>
    private void RotateRedSide()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Rotates the Orange Side of the Cube
    /// </summary>
    private void RotateOrangeSide()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Rotates the Yellow Side of the Cube
    /// </summary>
    private void RotateYellowSide()
    {
        for (int s = 0; s < 3; s++)
        {
            for (int t = 0; t < 3; t++)
            {
                if (!(s == 1 && t == 1))
                {
                    Pieces[2, s, t].transform.parent = Pieces[2, 1, 1].transform;
                }
            }
        }
        Pieces[2, 1, 1].transform.Rotate(0, 0, 90);    //Rotate Clockwise

        //Update Array with new locations
        //Rotate Corners in Array
        GameObject tr = Pieces[2, 0, 2];
        Pieces[2, 0, 2] = Pieces[2, 2, 2];
        Pieces[2, 2, 2] = Pieces[2, 2, 0];
        Pieces[2, 2, 0] = Pieces[2, 0, 0];
        Pieces[2, 0, 0] = tr;
        //Rotate Edges in Array
        GameObject tm = Pieces[2, 0, 1];
        Pieces[2, 0, 1] = Pieces[2, 1, 2];
        Pieces[2, 1, 2] = Pieces[2, 2, 1];
        Pieces[2, 2, 1] = Pieces[2, 1, 0];
        Pieces[2, 1, 0] = tm;
    }

    /// <summary>
    /// Rotates the White Side of the Cube
    /// </summary>
    private void RotateWhiteSide()
    {
        for (int s = 0; s < 3; s++)
        {
            for (int t = 0; t < 3; t++)
            {
                if (!(s == 1 && t == 1))
                {
                    Pieces[0, s, t].transform.parent = Pieces[0, 1, 1].transform;
                }
            }
        }
        Pieces[0, 1, 1].transform.Rotate(0, 0, -90);    //Rotate Clockwise

        //Update Array with new locations
        //Rotate Corners in Array
        GameObject tr = Pieces[0, 0, 0];
        Pieces[0, 0, 0] = Pieces[0, 2, 0];
        Pieces[0, 2, 0] = Pieces[0, 2, 2];
        Pieces[0, 2, 2] = Pieces[0, 0, 2];
        Pieces[0, 0, 2] = tr;
        //Rotate Edges in Array
        GameObject tm = Pieces[0, 0, 1];
        Pieces[0, 0, 1] = Pieces[0, 1, 0];
        Pieces[0, 1, 0] = Pieces[0, 2, 1];
        Pieces[0, 2, 1] = Pieces[0, 1, 2];
        Pieces[0, 1, 2] = tm;
    }

    /// <summary>
    /// Sets the side to rotate and direction to rotate based on user input.
    /// </summary>
    private void SetSideRotateDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SideToRotate = Side.White;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            SideToRotate = Side.Yellow;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            SideToRotate = Side.Orange;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SideToRotate = Side.Red;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            SideToRotate = Side.Blue;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            SideToRotate = Side.Green;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DirectionToRotate == SideRotateDirection.Clockwise)
            {
                DirectionToRotate = SideRotateDirection.CounterClockwise;
            }
            else
            {
                DirectionToRotate = SideRotateDirection.Clockwise;
            }
            SideToRotate = Side.None;
        }
        else
        {
            SideToRotate = Side.None;
        }
    }

    /// <summary>
    /// Places the Rubik Cube pieces in an array
    /// </summary>
    private void SetRubikCubeStart()
    {
        Pieces = new GameObject[3, 3, 3];
        for (int f = 0; f < 3; f++)
        {
            for (int s = 0; s < 3; s++)
            {
                for (int t = 0; t < 3; t++)
                {
                    if(f == 1 && s == 1 && t == 1)  //Avoid the core of the cube
                    {
                        t++;
                    }
                    int value = f * 9 + s * 3 + t;
                    Pieces pieces = (Pieces)value;
                    Pieces[f, s, t] = GameObject.FindWithTag(pieces.ToString());
                }
            }
        }
        SolvedLocation = (GameObject[,,])Pieces.Clone();
    }
}

public enum Pieces
{
    BOW,
    BW,
    BRW,
    OW,
    W,
    RW,
    GOW,
    GW,
    GRW,
    BO,
    B,
    BR,
    O,
    None,
    R,
    GO,
    G,
    GR,
    BOY,
    BY,
    BRY,
    OY,
    Y,
    RY,
    GOY,
    GY,
    GRY
}

public enum Side
{
    None,
    White,
    Yellow,
    Orange,
    Red,
    Blue,
    Green
}

public enum SideRotateDirection
{
    Clockwise,
    CounterClockwise
}
