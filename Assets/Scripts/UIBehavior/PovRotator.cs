using System;
using UnityEngine;

public class PovRotator : MonoBehaviour
{
    public IUIController[] UIControllers { get; set; }
    private Rigidbody PoV { get; set; }
    private PoVRotateDirection PoVRotateDirection { get; set; }
    private Vector3 DesiredPoVRotatePosition { get; set; }

    //Intialize
    public void Start()
    {
        PoV = GetComponent<Rigidbody>();
        DesiredPoVRotatePosition = new Vector3(0, 0, 0);
        this.UIControllers = GameObject.FindWithTag("UIController").GetComponentsInChildren<IUIController>();
    }

    //Called every frame
    public void Update()
    {
        SetPoVRotateDirection();
        PoVRotate(GetPoVRotateDirection());
        PerformControllerActions();
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
                PoV.transform.Rotate(0, 90, 0);
                break;
            case PoVRotateDirection.Left:
                PoV.transform.Rotate(0, -90, 0);
                break;
            case PoVRotateDirection.Up:
                PoV.transform.Rotate(-90, 0, 0);
                break;
            case PoVRotateDirection.Down:
                PoV.transform.Rotate(90, 0, 0);
                break;
        }
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
