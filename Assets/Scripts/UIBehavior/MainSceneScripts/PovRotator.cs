using System;
using UnityEngine;

public class PovRotator : MonoBehaviour
{
    public float rotationSpeed = 2;     //1 = 90 degrees per second; 2 = 90 degrees per half second.
    public IUIController[] UIControllers { get; set; }
    private Rigidbody PoV { get; set; }
    private PoVRotateDirection PoVRotateDirection { get; set; }
    private Vector3 CurrentCubeRotation { get; set; }   //<---This is needed, because Unity translates rotation angles at runtime making it impossible to get the current rotation.
    private Vector3 DesiredPoVRotatePosition { get; set; }

    //Intialize
    public void Start()
    {
        PoV = GetComponent<Rigidbody>();
        CurrentCubeRotation = new Vector3(0, 0, 0);
        DesiredPoVRotatePosition = new Vector3(0, 0, 0);
        this.UIControllers = GameObject.FindWithTag("UIController").GetComponentsInChildren<IUIController>();
    }

    //Called every frame
    public void Update()
    {
        SetPoVRotateDirection();
        PoVRotate(GetPoVRotateDirection());
        PerformControllerActions();
        SlowRotate();
    }

    /// <summary>
    /// Performs all of the PoV rotate actions for all the controllers
    /// </summary>
    private void PerformControllerActions()
    {
        foreach (IUIController controller in this.UIControllers)
        {
            PoVRotate(controller.GetDirectionToRotatePoV());
            controller.FinishedPoVRotate();
        }
    }

    /// <summary>
    /// Changes the PoV to the desired position
    /// </summary>
    /// <param name="rotateDirection">Desired direction of rotate</param>
    private void PoVRotate(PoVRotateDirection rotateDirection)
    {
        switch (rotateDirection)    //nothing needs to happen on no rotate
        {
            case PoVRotateDirection.Right:
                DesiredPoVRotatePosition = new Vector3(DesiredPoVRotatePosition.x, DesiredPoVRotatePosition.y + 90, 0);
                break;
            case PoVRotateDirection.Left:
                DesiredPoVRotatePosition = new Vector3(DesiredPoVRotatePosition.x, DesiredPoVRotatePosition.y - 90, 0);
                break;
            case PoVRotateDirection.Up:
                DesiredPoVRotatePosition = new Vector3(DesiredPoVRotatePosition.x - 90, DesiredPoVRotatePosition.y, 0);
                break;
            case PoVRotateDirection.Down:
                DesiredPoVRotatePosition = new Vector3(DesiredPoVRotatePosition.x + 90, DesiredPoVRotatePosition.y, 0);
                break;
        }
    }

    /// <summary>
    /// Makes a slow rotation instead of instant.
    /// </summary>
    private void SlowRotate()
    {
        float x = 0;
        float y = 0;
        float rate = 90 * Time.deltaTime * rotationSpeed;
        if(CurrentCubeRotation.x < DesiredPoVRotatePosition.x)
        {
            x = Mathf.Clamp(rate, 0, DesiredPoVRotatePosition.x - CurrentCubeRotation.x);
        }
        else if (CurrentCubeRotation.x > DesiredPoVRotatePosition.x)
        {
            x = Mathf.Clamp(0 - rate, DesiredPoVRotatePosition.x - CurrentCubeRotation.x, 0);
        }
        if (CurrentCubeRotation.y < DesiredPoVRotatePosition.y)
        {
            y = Mathf.Clamp(rate, 0, DesiredPoVRotatePosition.y - CurrentCubeRotation.y);
        }
        else if (CurrentCubeRotation.y > DesiredPoVRotatePosition.y)
        {
            y = Mathf.Clamp(0 - rate, DesiredPoVRotatePosition.y - CurrentCubeRotation.y, 0);
        }
        PoV.transform.Rotate(x, y, 0);
        CurrentCubeRotation = new Vector3(CurrentCubeRotation.x + x, CurrentCubeRotation.y + y, 0);
    }

    /// <summary>
    /// Returns the desired rotation for the PoV
    /// </summary>
    /// <returns>Desired Rotation for the PoV</returns>
    private PoVRotateDirection GetPoVRotateDirection()
    {
        return this.PoVRotateDirection;
    }

    /// <summary>
    /// Sets the desired rotation for the PoV
    /// </summary>
    private void SetPoVRotateDirection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.PoVRotateDirection = PoVRotateDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.PoVRotateDirection = PoVRotateDirection.Right;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.PoVRotateDirection = PoVRotateDirection.Down;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.PoVRotateDirection = PoVRotateDirection.Up;
        }
        else
        {
            this.PoVRotateDirection = PoVRotateDirection.None;
        }
    }
}

[Serializable]
public enum PoVRotateDirection
{
    None,
    Right,
    Left,
    Up,
    Down
}
