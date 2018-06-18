using System;
using System.Collections.Generic;
using UnityEngine;

public class SlowSideRotator : MonoBehaviour
{
    public float rotationSpeed = 2;     //1 = 1/4 rotate takes 1 second; 2 = 1/4 turn rotation takes half second.
    public IUIController[] UIControllers { get; set; }
    private GameObject[,,] SolvedLocation { get; set; }
    private GameObject[,,] Pieces { get; set; }
    private SideRotateDirection RotationDirection { get; set; }
    private List<SideRotationGroup> RotationList { get; set; }
    private bool isShuffling = false;

    // Use this for initialization
    void Start()
    {
        SetRubikCubeStart();
        RotationDirection = SideRotateDirection.Clockwise;
        RotationList = new List<SideRotationGroup>();
        this.UIControllers = GameObject.FindWithTag("UIController").GetComponentsInChildren<IUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShuffling)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ShuffleCube();
            }
            GetKeyboardInputs();
            PerformControllerActions();
            StartSlowRotation();
        }
    }

    /// <summary>
    /// Performs Logical Rotation, and perpares Side for Physical Rotation.
    /// </summary>
    private void StartSlowRotation()
    {
        if (this.RotationList.Count > 0)
        {
            GameObject centerPiece = this.RotationList[0].CenterPiece;
            if (!this.RotationList[0].HasStarted())
            {
                PerformLogicalTurn(out centerPiece);
                this.RotationList[0].CenterPiece = centerPiece;
            }
            PerformSlowRotation();
            if (this.RotationList[0].HasFinished())
            {
                this.RotationList.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// Performs a visual rotation.
    /// </summary>
    private void PerformSlowRotation()
    {
        SideRotationGroup firstSide = this.RotationList[0];
        float rate = 90 * Time.deltaTime * rotationSpeed;
        if (firstSide.SideToRotate == Side.White || firstSide.SideToRotate == Side.Green || firstSide.SideToRotate == Side.Orange)
        {
            rate *= -1;
        }
        float minRotation = -90 - firstSide.RotationSoFar;
        float maxRotation = 90 - firstSide.RotationSoFar;
        float x = 0;
        float y = 0;
        float z = 0;
        switch (firstSide.SideToRotate)
        {
            case Side.White:
            case Side.Yellow:
                z = Mathf.Clamp((float)firstSide.DirectionToRotate * rate, minRotation, maxRotation);
                this.RotationList[0].RotationSoFar += z;
                break;
            case Side.Blue:
            case Side.Green:
                y = Mathf.Clamp((float)firstSide.DirectionToRotate * rate, minRotation, maxRotation);
                this.RotationList[0].RotationSoFar += y;
                break;
            case Side.Red:
            case Side.Orange:
                x = Mathf.Clamp((float)firstSide.DirectionToRotate * rate, minRotation, maxRotation);
                this.RotationList[0].RotationSoFar += x;
                break;
        }
        this.RotationList[0].CenterPiece.transform.Rotate(x, y, z);
    }

    /// <summary>
    /// Performs the logical rotation.
    /// </summary>
    /// <param name="centerPiece">Reference to Center of Side to Rotate.</param>
    private void PerformLogicalTurn(out GameObject centerPiece)
    {
        switch (this.RotationList[0].SideToRotate)
        {
            case Side.White:
                RotateWhiteLocially(out centerPiece);
                break;
            case Side.Yellow:
                RotateYellowLocially(out centerPiece);
                break;
            case Side.Orange:
                RotateOrangeLocially(out centerPiece);
                break;
            case Side.Red:
                RotateRedLocially(out centerPiece);
                break;
            case Side.Blue:
                RotateBlueLocially(out centerPiece);
                break;
            case Side.Green:
                RotateGreenLocially(out centerPiece);
                break;
            default:
                centerPiece = new GameObject();
                break;
        }
    }

    /// <summary>
    /// Sets up pieces for physical rotation, and performs logical rotation
    /// </summary>
    /// <param name="centerPiece">Reference to center of Side</param>
    private void RotateGreenLocially(out GameObject centerPiece)
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
        centerPiece = Pieces[1, 2, 1];

        int rotateTimes = 1;
        if (this.RotationList[0].DirectionToRotate == SideRotateDirection.CounterClockwise)
        {
            rotateTimes = 3;
        }
        for (int rotations = 0; rotations < rotateTimes; rotations++)
        {
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
    }

    /// <summary>
    /// Sets up pieces for physical rotation, and performs logical rotation
    /// </summary>
    /// <param name="centerPiece">Reference to center of Side</param>
    private void RotateBlueLocially(out GameObject centerPiece)
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
        centerPiece = Pieces[1, 0, 1];

        int rotateTimes = 1;
        if (this.RotationList[0].DirectionToRotate == SideRotateDirection.CounterClockwise)
        {
            rotateTimes = 3;
        }
        for (int rotations = 0; rotations < rotateTimes; rotations++)
        {
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
    }

    /// <summary>
    /// Sets up pieces for physical rotation, and performs logical rotation
    /// </summary>
    /// <param name="centerPiece">Reference to center of Side</param>
    private void RotateRedLocially(out GameObject centerPiece)
    {
        for (int f = 0; f < 3; f++)
        {
            for (int s = 0; s < 3; s++)
            {
                if (!(f == 1 && s == 1))
                {
                    Pieces[f, s, 2].transform.parent = Pieces[1, 1, 2].transform;
                }
            }
        }
        centerPiece = Pieces[1, 1, 2];

        int rotateTimes = 1;
        if (this.RotationList[0].DirectionToRotate == SideRotateDirection.CounterClockwise)
        {
            rotateTimes = 3;
        }
        for (int rotations = 0; rotations < rotateTimes; rotations++)
        {
            //Update Array with new locations
            //Rotate Corners in Array
            GameObject tl = Pieces[0, 0, 2];
            Pieces[0, 0, 2] = Pieces[0, 2, 2];
            Pieces[0, 2, 2] = Pieces[2, 2, 2];
            Pieces[2, 2, 2] = Pieces[2, 0, 2];
            Pieces[2, 0, 2] = tl;
            //Rotate Edges in Array
            GameObject tm = Pieces[1, 0, 2];
            Pieces[1, 0, 2] = Pieces[0, 1, 2];
            Pieces[0, 1, 2] = Pieces[1, 2, 2];
            Pieces[1, 2, 2] = Pieces[2, 1, 2];
            Pieces[2, 1, 2] = tm;
        }
    }

    /// <summary>
    /// Sets up pieces for physical rotation, and performs logical rotation
    /// </summary>
    /// <param name="centerPiece">Reference to center of Side</param>
    private void RotateOrangeLocially(out GameObject centerPiece)
    {
        for (int f = 0; f < 3; f++)
        {
            for (int s = 0; s < 3; s++)
            {
                if (!(f == 1 && s == 1))
                {
                    Pieces[f, s, 0].transform.parent = Pieces[1, 1, 0].transform;
                }
            }
        }
        centerPiece = Pieces[1, 1, 0];

        int rotateTimes = 1;
        if (this.RotationList[0].DirectionToRotate == SideRotateDirection.CounterClockwise)
        {
            rotateTimes = 3;
        }
        for (int rotations = 0; rotations < rotateTimes; rotations++)
        {
            //Update Array with new locations
            //Rotate Corners in Array
            GameObject tl = Pieces[2, 0, 0];
            Pieces[2, 0, 0] = Pieces[2, 2, 0];
            Pieces[2, 2, 0] = Pieces[0, 2, 0];
            Pieces[0, 2, 0] = Pieces[0, 0, 0];
            Pieces[0, 0, 0] = tl;
            //Rotate Edges in Array
            GameObject tm = Pieces[1, 0, 0];
            Pieces[1, 0, 0] = Pieces[2, 1, 0];
            Pieces[2, 1, 0] = Pieces[1, 2, 0];
            Pieces[1, 2, 0] = Pieces[0, 1, 0];
            Pieces[0, 1, 0] = tm;
        }
    }

    /// <summary>
    /// Sets up pieces for physical rotation, and performs logical rotation
    /// </summary>
    /// <param name="centerPiece">Reference to center of Side</param>
    private void RotateYellowLocially(out GameObject centerPiece)
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
        centerPiece = Pieces[2, 1, 1];

        int rotateTimes = 1;
        if (this.RotationList[0].DirectionToRotate == SideRotateDirection.CounterClockwise)
        {
            rotateTimes = 3;
        }
        for (int rotations = 0; rotations < rotateTimes; rotations++)
        {
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
    }

    /// <summary>
    /// Sets up pieces for physical rotation, and performs logical rotation
    /// </summary>
    /// <param name="centerPiece">Reference to center of Side</param>
    private void RotateWhiteLocially(out GameObject centerPiece)
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
        centerPiece = Pieces[0, 1, 1];

        int rotateTimes = 1;
        if (this.RotationList[0].DirectionToRotate == SideRotateDirection.CounterClockwise)
        {
            rotateTimes = 3;
        }
        for (int rotations = 0; rotations < rotateTimes; rotations++)
        {
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
    }

    /// <summary>
    /// Performs all of the Side rotate actions for all the controllers
    /// </summary>
    private void PerformControllerActions()
    {
        foreach (IUIController controller in UIControllers)
        {
            if (controller.ShuffleDesired())
            {
                ShuffleCube();
                controller.CubeShuffled();
            }
            else if (controller.GetSideToRotate() != Side.None)
            {
                RotationList.Add(new SideRotationGroup(controller.GetSideToRotate(), controller.GetDirectionToRotate()));
                controller.FinishedSideRotate();
            }
        }
    }

    /// <summary>
    /// Get Keyboard Inputs for rotation
    /// </summary>
    private void GetKeyboardInputs()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RotationList.Add(new SideRotationGroup(Side.White, RotationDirection));
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            RotationList.Add(new SideRotationGroup(Side.Yellow, RotationDirection));
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            RotationList.Add(new SideRotationGroup(Side.Orange, RotationDirection));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            RotationList.Add(new SideRotationGroup(Side.Red, RotationDirection));
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            RotationList.Add(new SideRotationGroup(Side.Blue, RotationDirection));
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            RotationList.Add(new SideRotationGroup(Side.Green, RotationDirection));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (RotationDirection == SideRotateDirection.Clockwise)
            {
                RotationDirection = SideRotateDirection.CounterClockwise;
            }
            else
            {
                RotationDirection = SideRotateDirection.Clockwise;
            }
        }
    }

    /// <summary>
    /// Shuffles the cube.
    /// </summary>
    private void ShuffleCube()
    {
        isShuffling = true;
        float currentRotationSpeed = this.rotationSpeed;
        System.Random rng = new System.Random();
        int numberOfShuffleTurns = 25 + rng.Next() % 500;   //Make 25-524 turns to shuffle cube.
        for (int i = 0; i < numberOfShuffleTurns; i++)
        {
            this.RotationList.Add(new SideRotationGroup((Side)(1 + rng.Next() % 6),(SideRotateDirection)(((rng.Next() % 2) * 2) - 1)));
        }
        this.rotationSpeed = 10;
        while(this.RotationList.Count > 0)
        {
            StartSlowRotation();
        }
        this.rotationSpeed = currentRotationSpeed;
        isShuffling = false;
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
                    if (f == 1 && s == 1 && t == 1)  //Avoid the core of the cube
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

[Serializable]
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

[Serializable]
public enum SideRotateDirection
{
    CounterClockwise = -1,
    Clockwise = 1
}