using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public GameObject Menu;

    public Slider SoundsVolume;
    public Slider BGMVolume;

    void Start()
    {
        BGMVolume.value = PlayerPrefs.GetFloat("BGM Volume");
        SoundsVolume.value = PlayerPrefs.GetFloat("Sounds Volume");
    }



    void Update()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        if(Input.GetKeyDown(KeyCode.Mouse0))
        Menu.GetComponent<Menumanadger>().Sounds.PlayOneShot(Menu.GetComponent<Menumanadger>().ClickSound);
        SoundsVolumeChange();
        BGMVolumeChange();
    }

    void BGMVolumeChange()
    {
        Menu.GetComponent<Menumanadger>().BGM.volume = BGMVolume.value;
    }
    void SoundsVolumeChange() 
    {
        Menu.GetComponent<Menumanadger>().Sounds.volume = SoundsVolume.value;
    }
}