using UnityEngine;

public class OnClickActionController : MonoBehaviour, IUIController
{
    public void Start()
    {
        this.PovRotateDirection = PoVRotateDirection.None;
        this.Side = Side.None;
        this.Direction = SideRotateDirection.Clockwise;
        this.NeedsShuffle = false;
        //this.SceneMenu = GameObject.FindGameObjectWithTag("MenuMainScene");
    }

    private PoVRotateDirection PovRotateDirection { get; set; }
    private Side Side { get; set; }
    private SideRotateDirection Direction { get; set; }
    private bool NeedsShuffle { get; set; }
    public GameObject SceneMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.SceneMenu.SetActive(!this.SceneMenu.activeSelf);
        }
    }

    /// <summary>
    /// Actions called from the input device
    /// </summary>
    #region "OnClick Actions"
    public void RotatePoVRightClick()
    {
        RotatePoV(PoVRotateDirection.Right);
    }
    public void RotatePoVLeftClick()
    {
        RotatePoV(PoVRotateDirection.Left);
    }
    public void RotatePoVUpClick()
    {
        RotatePoV(PoVRotateDirection.Up);
    }
    public void RotatePoVDownClick()
    {
        RotatePoV(PoVRotateDirection.Down);
    }

    public void RotateWhiteClockwiseClick()
    {
        RotateSide(Side.White, SideRotateDirection.Clockwise);
    }
    public void RotateWhiteCounterClockwiseClick()
    {
        RotateSide(Side.White, SideRotateDirection.CounterClockwise);
    }

    public void RotateYellowClockwiseClick()
    {
        RotateSide(Side.Yellow, SideRotateDirection.Clockwise);
    }
    public void RotateYellowCounterClockwiseClick()
    {
        RotateSide(Side.Yellow, SideRotateDirection.CounterClockwise);
    }

    public void RotateBlueClockwiseClick()
    {
        RotateSide(Side.Blue, SideRotateDirection.Clockwise);
    }
    public void RotateBlueCounterClockwiseClick()
    {
        RotateSide(Side.Blue, SideRotateDirection.CounterClockwise);
    }

    public void RotateGreenClockwiseClick()
    {
        RotateSide(Side.Green, SideRotateDirection.Clockwise);
    }
    public void RotateGreenCounterClockwiseClick()
    {
        RotateSide(Side.Green, SideRotateDirection.CounterClockwise);
    }

    public void RotateRedClockwiseClick()
    {
        RotateSide(Side.Red, SideRotateDirection.Clockwise);
    }
    public void RotateRedCounterClockwiseClick()
    {
        RotateSide(Side.Red, SideRotateDirection.CounterClockwise);
    }

    public void RotateOrangeClockwiseClick()
    {
        RotateSide(Side.Orange, SideRotateDirection.Clockwise);
    }
    public void RotateOrangeCounterClockwiseClick()
    {
        RotateSide(Side.Orange, SideRotateDirection.CounterClockwise);
    }

    /// <summary>
    /// Called from the input to tell the controller to shuffle the cube
    /// </summary>
    public void ShuffleCube()
    {
        this.NeedsShuffle = true;
    }
    #endregion

    #region "Methods"
    /// <summary>
    /// Notifies the controller that the UI is finished with the PoV rotation.
    /// </summary>
    public void FinishedPoVRotate()
    {
        this.PovRotateDirection = PoVRotateDirection.None;
    }

    /// <summary>
    /// Notifies the controller that the UI is finished with the Side rotation.
    /// </summary>
    public void FinishedSideRotate()
    {
        this.Side = Side.None;
    }

    /// <summary>
    /// Desided direction to rotate Side of Cube.
    /// </summary>
    /// <returns>Direction to rotate Side of Cube.</returns>
    public SideRotateDirection GetDirectionToRotate()
    {
        return this.Direction;
    }

    /// <summary>
    /// Desided direction to rotate PoV.
    /// </summary>
    /// <returns>Direction to rotate PoV</returns>
    public PoVRotateDirection GetDirectionToRotatePoV()
    {
        return this.PovRotateDirection;

    }

    /// <summary>
    /// Desided side of Cube to rotate.
    /// </summary>
    /// <returns>Side of Cube to rotate</returns>
    public Side GetSideToRotate()
    {
        return this.Side;
    }

    /// <summary>
    /// Called from input to tell the controller to rotate the PoV.
    /// </summary>
    /// <param name="povRotateDirection">Which direction to rotate PoV</param>
    public void RotatePoV(PoVRotateDirection povRotateDirection)
    {
        this.PovRotateDirection = povRotateDirection;
    }

    /// <summary>
    /// Called from input to tell the controller to rotate a Side of the Cube.
    /// </summary>
    /// <param name="side">Side to rotate</param>
    /// <param name="direction">Direction of Rotation.</param>
    public void RotateSide(Side side, SideRotateDirection direction)
    {
        this.Side = side;
        this.Direction = direction;
    }

    /// <summary>
    /// Lets the UI know to whether shuffle the Cube or not.
    /// </summary>
    /// <returns>True if Cube needs to be shuffled, False if Cube does not need to be shuffled</returns>
    public bool ShuffleDesired()
    {
        return this.NeedsShuffle;
    }

    /// <summary>
    /// Notifies the controller that the UI shuffled the Cube. 
    /// </summary>
    public void CubeShuffled()
    {
        this.NeedsShuffle = false;
    }
    #endregion
}
