using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMainSceneController : MonoBehaviour {

    private GameObject SceneMenu { get; set; }
    public PovRotator PoV;
    public SlowSideRotator Cube;
    public AudioSource BGM;
    public AudioSource SFX;

    public Slider BGMSlider;
    public Slider SFXSlider;
    public Slider PoVRotateSlider;
    public Slider CubeRotateSlider;

    // Use this for initialization
    void Start () {
        this.SceneMenu = GameObject.FindGameObjectWithTag("MenuMainScene");
        //this.BGM = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        //this.SFX = GameObject.FindWithTag("Cube").GetComponent<AudioSource>();
        //this.PoV = GameObject.FindWithTag("PointOfView").GetComponent<PovRotator>();
        //this.Cube = GameObject.FindWithTag("Cube").GetComponent<SlowSideRotator>();
        this.SceneMenu.SetActive(false);
    }



    public void OnResumeButtonClick()
    {
        this.SceneMenu.SetActive(false);
    }

    public void OnBGMSliderChange()
    {
        this.BGM.volume = this.BGMSlider.value;
    }

    public void OnSFXSliderChange()
    {
        this.SFX.volume = this.SFXSlider.value;
    }

    public void OnPoVSpeedSliderChange()
    {
        this.PoV.rotationSpeed = this.PoVRotateSlider.value;
    }

    public void OnRotateSpeedSliderChange()
    {
        this.Cube.rotationSpeed = this.CubeRotateSlider.value;
    }

    public void OnMainMenuButtonClick()
    {
        OnResumeButtonClick();
    }
}
