using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {

    public AudioSource BGM { get; set; }
    public AudioSource SFX { get; set; }
    public Toggle BGMCheckbox { get; set; }
    public Toggle SFXCheckbox { get; set; }

    // Use this for initialization
    void Start () {
        this.BGM = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        this.SFX = GameObject.FindWithTag("Cube").GetComponent<AudioSource>();
        this.BGMCheckbox = GameObject.FindWithTag("BGMCheckbox").GetComponent<Toggle>();
        this.SFXCheckbox = GameObject.FindWithTag("SFXCheckbox").GetComponent<Toggle>();
    }

    public void OnBGMCheck()
    {
        this.BGM.mute = !this.BGMCheckbox.isOn;
    }

    public void OnSFXCheck()
    {
        this.SFX.mute = !this.SFXCheckbox.isOn;
    }
}
