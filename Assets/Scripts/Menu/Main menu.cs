using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    public GameObject Menu;

    public Canvas MainMenu;

    public GameObject Settings;
    public Canvas SettingsMenu;

    public GameObject Load;

    public GameObject StartGame;

    public GameObject Exit;

    public int sceneID;

    public AudioSource BGM;
    public AudioSource Sounds;

    public AudioClip ARoseInAFieldMusic;
    public AudioClip RadianLockMusic;
    public AudioClip SevconMusic;
    public AudioClip TheEchoMusic;
    public AudioClip TheArtOfWarMusic;
    public AudioClip ClickSound;
    public AudioClip LoadSound;
    public AudioClip PointerEnterSound;
    public AudioClip SaveSound;


    void Start()
    {
        SettingsMenu.enabled = false;
        sceneID = SceneManager.GetActiveScene().buildIndex;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu.enabled = !MainMenu.enabled;
            SettingsMenu.enabled = !SettingsMenu.enabled;
        }  //Эта строчка кода отвечает за открытие и закрытие настроек на Escape

        if (Input.GetKeyDown(KeyCode.S))
            PlayerPrefs.SetInt("Scene", sceneID); // Эта отвечает за сохранение на S
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene(PlayerPrefs.GetInt("Scene")); // Эта отвечает за загрузку на L
        if (Input.GetKeyDown(KeyCode.Equals))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Эта отвечает за переход на следующую чцену на =
        if (Input.GetKeyDown(KeyCode.Minus))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Эта за переход на предыдущую сцену на -
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Эта за перезагрузку сцены на R
    }

    public void PointerDown(GameObject gameObject) // Этот метод отвечает за анимацию кнопки при нажатии (уменьшение размера и прозрачности)
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.35f, 0.35f, 0.45f);
    }

    public void PointerUp(GameObject gameObject) // Этот метод отвечает за возвращение кнопки в исходное состояние после нажатия кнопки
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.45f, 0.45f, 0.45f);
    }

    public void PointerEnter(GameObject gameObject) // Этот метод отвечает за увеличение кнопки при наведении курсора на неё
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.45f);
    }

    public void PointerExit(GameObject gameObject) // Этот медот отвечает за возвращение кноке прежнего размера после наведения курсора
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.45f, 0.45f, 0.45f);
    }

    public void CloseOpenSettingsMenu(Canvas menu) // Этот метод отвечает за открытие/закрытие меню на кнопку "Settings"
    {
        menu.enabled = !menu.enabled;

        Settings.SetActive(!menu.enabled);
        Load.SetActive(!menu.enabled);
        StartGame.SetActive(!menu.enabled);
        Exit.SetActive(!menu.enabled);
    }


    public void LoadGame() // Этот метод отвечат за загрузку игры на кнопку "Load"
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene"));
    }

    public void Play() // Этот метод отвечат за загрузку игры на кнопку "Load"
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
