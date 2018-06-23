using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputColorChanger : MonoBehaviour {

    public string SelectedColorAsString = "FFFFFF";
    private Color SelectedColor = Color.white;
    public string[] WhiteSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] YellowSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFF00", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] BlueSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "0000FF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] GreenSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "00FF00", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] OrangeSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FEA100", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] RedSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FF0000", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };

    public void OnColorSelected(string color)
    {
        this.SelectedColorAsString = color;
        float r = (float)int.Parse(this.SelectedColorAsString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        float g = (float)int.Parse(this.SelectedColorAsString.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        float b = (float)int.Parse(this.SelectedColorAsString.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        this.SelectedColor = new Color(r/255, g/255, b/255);
    }

    public void OnPieceSelected(int piece)
    {
        Image clickedPiece = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        clickedPiece.color = this.SelectedColor;
        int index = piece % 10;
        switch (piece / 10)
        {
            case 0:
                this.WhiteSideColors[index] = this.SelectedColorAsString;
                break;
            case 1:
                this.RedSideColors[index] = this.SelectedColorAsString;
                break;
            case 2:
                this.OrangeSideColors[index] = this.SelectedColorAsString;
                break;
            case 3:
                this.BlueSideColors[index] = this.SelectedColorAsString;
                break;
            case 4:
                this.GreenSideColors[index] = this.SelectedColorAsString;
                break;
            case 5:
                this.YellowSideColors[index] = this.SelectedColorAsString;
                break;
        }
    }

    public void OnSolveButtonClicked()
    {

    }
}