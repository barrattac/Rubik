public interface IUIController
{
    /// <summary>
    /// Desided direction to rotate PoV.
    /// </summary>
    /// <returns>Direction to rotate PoV</returns>
    PoVRotateDirection GetDirectionToRotatePoV();

    /// <summary>
    /// Desided side of Cube to rotate.
    /// </summary>
    /// <returns>Side of Cube to rotate</returns>
    Side GetSideToRotate();

    /// <summary>
    /// Desided direction to rotate Side of Cube.
    /// </summary>
    /// <returns>Direction to rotate Side of Cube.</returns>
    SideRotateDirection GetDirectionToRotate();

    /// <summary>
    /// Called from input to tell the controller to rotate the PoV.
    /// </summary>
    /// <param name="povRotateDirection">Which direction to rotate PoV</param>
    void RotatePoV(PoVRotateDirection povRotateDirection);

    /// <summary>
    /// Called from input to tell the controller to rotate a Side of the Cube.
    /// </summary>
    /// <param name="side">Side to rotate</param>
    /// <param name="direction">Direction of Rotation.</param>
    void RotateSide(Side side, SideRotateDirection direction);

    /// <summary>
    /// Notifies the controller that the UI is finished with the PoV rotation.
    /// </summary>
    void FinishedPoVRotate();

    /// <summary>
    /// Notifies the controller that the UI is finished with the Side rotation.
    /// </summary>
    void FinishedSideRotate();

    /// <summary>
    /// Called from the input to tell the controller to shuffle the cube
    /// </summary>
    void ShuffleCube();

    /// <summary>
    /// Lets the UI know to whether shuffle the Cube or not.
    /// </summary>
    /// <returns>True if Cube needs to be shuffled, False if Cube does not need to be shuffled</returns>
    bool ShuffleDesired();

    /// <summary>
    /// Notifies the controller that the UI shuffled the Cube. 
    /// </summary>
    void CubeShuffled();
}
