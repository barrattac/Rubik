using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputColorChanger : MonoBehaviour {

    #region "Properties"
    public OutputController Output;
    public string SelectedColorAsString = "FFFFFF";
    private Color SelectedColor = Color.white;
    public string[] WhiteSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] YellowSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFF00", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] BlueSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "0000FF", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] GreenSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "40C040", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] OrangeSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FFA500", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    public string[] RedSideColors = new string[] { "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF", "FF0000", "FFFFFF", "FFFFFF", "FFFFFF", "FFFFFF" };
    #endregion

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
        if (IsValid())
        {
            Output.ShowSolutionDisplay(GetSolution());
        }
        else
        {
            Output.ShowErrorDisplay();
        }
    }

    private string GetSolution()
    {
        throw new NotImplementedException();
    }

    #region "Validation"
    /// <summary>
    /// Checks if the puzzle is valid
    /// </summary>
    /// <returns>True if valid</returns>
    private bool IsValid()
    {
        return CheckForAllPieces();
    }

    /// <summary>
    /// Checks if all pieces are inputed
    /// </summary>
    /// <returns>Returns true if all pieces are inputed</returns>
    private bool CheckForAllPieces()
    {
        bool allPieces = CheckForEdgePieces();
        if (allPieces)
        {
            allPieces = CheckForCornerPieces();
        }
        return allPieces;
    }

    /// <summary>
    /// Checks if all corner pieces are inputed
    /// </summary>
    /// <returns>Returns true if all corner pieces are inputed</returns>
    private bool CheckForCornerPieces()
    {
        List<string> comparePieces = new List<string>();
        List<string> pieces = new List<string>();
        pieces.Add(NamePiece(new List<string> { WhiteSideColors[0], BlueSideColors[6], OrangeSideColors[2] }));
        pieces.Add(NamePiece(new List<string> { WhiteSideColors[2], BlueSideColors[8], RedSideColors[0] }));
        pieces.Add(NamePiece(new List<string> { WhiteSideColors[6], GreenSideColors[0], OrangeSideColors[8] }));
        pieces.Add(NamePiece(new List<string> { WhiteSideColors[8], GreenSideColors[2], RedSideColors[6] }));

        pieces.Add(NamePiece(new List<string> { YellowSideColors[0], BlueSideColors[2], RedSideColors[2] }));
        pieces.Add(NamePiece(new List<string> { YellowSideColors[2], BlueSideColors[0], OrangeSideColors[0] }));
        pieces.Add(NamePiece(new List<string> { YellowSideColors[6], GreenSideColors[8], RedSideColors[8] }));
        pieces.Add(NamePiece(new List<string> { YellowSideColors[8], GreenSideColors[6], OrangeSideColors[6] }));

        while (pieces.Count > 0)
        {
            if (comparePieces.Contains(pieces[0]))
            {
                return false;
            }
            else
            {
                comparePieces.Add(pieces[0]);
                pieces.RemoveAt(0);
            }
        }
        return true;
    }

    /// <summary>
    /// Checks if all edge pieces are inputed
    /// </summary>
    /// <returns>Returns true if all edge pieces are inputed</returns>
    private bool CheckForEdgePieces()
    {
        List<string> comparePieces = new List<string>();
        List<string> pieces = new List<string>();
        pieces.Add(NamePiece(new List<string> { WhiteSideColors[1], BlueSideColors[7] }));
        pieces.Add(NamePiece(new List<string> { WhiteSideColors[3], OrangeSideColors[5] }));
        pieces.Add(NamePiece(new List<string> { WhiteSideColors[5], RedSideColors[3] }));
        pieces.Add(NamePiece(new List<string> { WhiteSideColors[7], GreenSideColors[2] }));

        pieces.Add(NamePiece(new List<string> { YellowSideColors[1], BlueSideColors[1] }));
        pieces.Add(NamePiece(new List<string> { YellowSideColors[5], OrangeSideColors[3] }));
        pieces.Add(NamePiece(new List<string> { YellowSideColors[3], RedSideColors[5] }));
        pieces.Add(NamePiece(new List<string> { YellowSideColors[7], GreenSideColors[7] }));

        pieces.Add(NamePiece(new List<string> { RedSideColors[1], BlueSideColors[5] }));
        pieces.Add(NamePiece(new List<string> { BlueSideColors[3], OrangeSideColors[1] }));
        pieces.Add(NamePiece(new List<string> { RedSideColors[7], GreenSideColors[5] }));
        pieces.Add(NamePiece(new List<string> { GreenSideColors[3], OrangeSideColors[7] }));

        while(pieces.Count > 0)
        {
            if (comparePieces.Contains(pieces[0]))
            {
                return false;
            }
            else
            {
                comparePieces.Add(pieces[0]);
                pieces.RemoveAt(0);
            }
        }
        return true;
    }
    
    /// <summary>
    /// Gives the pieces a name
    /// </summary>
    /// <param name="colors">Color that was inputed</param>
    /// <returns>Name for the piece</returns>
    private string NamePiece(List<string> colors)
    {
        colors.Sort();
        string name = colors[0];
        for(int i = 1; i < colors.Count; i++)
        {
            name += ", " + colors[i];
        }
        return name;
    }
    #endregion
}