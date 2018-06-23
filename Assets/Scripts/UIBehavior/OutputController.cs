using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputController : MonoBehaviour {

    public GameObject ErrorDisplay;
    public GameObject SolutionDisplay;
    public GameObject SolutionTextArea;
    private Text SolutionText;


    // Use this for initialization
    void Start () {
        this.SolutionText = SolutionTextArea.GetComponent<Text>();
    }

    public void ShowErrorDisplay()
    {
        ErrorDisplay.SetActive(true);
    }

    public void ShowSolutionDisplay(string solution)
    {
        this.SolutionText.text = solution;
        SolutionDisplay.SetActive(true);
    }

    public void OnErrorOKClicked()
    {
        ErrorDisplay.SetActive(false);
    }

    public void OnSolutionOKClicked()
    {
        SolutionDisplay.SetActive(false);
    }
}
