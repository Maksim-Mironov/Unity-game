using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Menu.View;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Menumanadger : MonoBehaviour
{
    public GameObject Menu;
    public GameObject MenuButtons;

    public GameObject Game;
    public GameObject Objects;

    public Canvas SettingsMenu;
    public GameObject Settings;

    public GameObject Load;

    public GameObject Save;

    public Canvas ArticleMenu;

    public GameObject DialogWindow;

    public GameObject Music;
    public GameObject PlayMusicButton;
    public GameObject StopMusicButton;

    public GameObject Table;
    public GameObject TestTubesGraber;
    public TetsTubeGrabs GraberScript;
    public GameObject CloseButton;
    public GameObject OpenButton;

    public GameObject PANA9600s;

    public int SceneID;

    public AudioSource BGM;
    public AudioSource Sounds;

    public Canvas MusicMenu;

    public AudioClip ARoseInAFieldMusic;
    public AudioClip RadianLockMusic;
    public AudioClip SevconMusic;
    public AudioClip TheEchoMusic;
    public AudioClip TheArtOfWarMusic;

    public AudioClip ClickSound;
    public AudioClip LoadSound;
    public AudioClip PointerEnterSound;
    public AudioClip SaveSound;

    public VideoPlayer TVScrene;
    public RawImage TV;
    public VideoClip TVClip;

    public float TimerTime = 0.5f;
    public bool StartTimer = false;

    public GameObject Article;
    public GameObject Arc;
    public GameObject[] ArticleButtons;
    public GameObject[] ArticleTexts;
    GameObject _activeText;

    public Slider Slider;
    bool _bool = false;

    public Canvas Tutorial;


    void Start()
    {
        SettingsMenu.enabled = false;
        MusicMenu.enabled = false;
        SceneID = SceneManager.GetActiveScene().buildIndex;
        TV.enabled = false;
        ArticleMenu.enabled = false;
        for (int i = 0; i < ArticleTexts.Length; i++)
            ArticleTexts[i].SetActive(false);
        Table.SetActive(false);
        TestTubesGraber.SetActive(false);
        GraberScript = TestTubesGraber.GetComponent<TetsTubeGrabs>();
        Tutorial.enabled = true;


        if (PlayerPrefs.GetInt("BGM") == 1)
            ChangeMusic(ARoseInAFieldMusic);
        if (PlayerPrefs.GetInt("BGM") == 2)
            ChangeMusic(RadianLockMusic);
        if (PlayerPrefs.GetInt("BGM") == 3)
            ChangeMusic(SevconMusic);
        if (PlayerPrefs.GetInt("BGM") == 4)
            ChangeMusic(TheEchoMusic);
        if (PlayerPrefs.GetInt("BGM") == 5)
            ChangeMusic(TheArtOfWarMusic);

    }


    void Update()
    {
        PANA9600s.GetComponent<BoxCollider2D>().enabled = !Tutorial;
        if (!TestTubesGraber.GetComponent<TetsTubeGrabs>().IsPosFree.Last()) OpenButton.SetActive(false);

        _bool = ArticleMenu.enabled;


        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); //Ёта строчка кода отвечает за открытие и закрытие настроек на Escape
        if (Input.GetKeyDown(KeyCode.M)) OpenCloseWindow(MusicMenu);
        if (Input.GetKeyDown(KeyCode.A))
        {
            ArticleTVOpen();
            OpenCloseWindow(ArticleMenu);
        }
        if (Input.GetKeyDown(KeyCode.S)) OpenCloseWindow(SettingsMenu); // Ёта отвечает за открытие/закрытие настроек на кнопку на S
        if (Input.GetKeyDown(KeyCode.L))
            LoadGame(); // Ёта отвечает за загрузку на L
        if (Input.GetKeyDown(KeyCode.Equals))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Ёта отвечает за переход на следующую сцену на =
        if (Input.GetKeyDown(KeyCode.Minus))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Ёта за переход на предыдущую сцену на -
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Ёта за перезагрузку сцены на R

        if (BGM.clip == ARoseInAFieldMusic)
        {
            PlayerPrefs.SetInt("BGM", 1);
        }
        else if (BGM.clip == RadianLockMusic)
        {
            PlayerPrefs.SetInt("BGM", 2);
        }
        else if (BGM.clip == SevconMusic)
        {
            PlayerPrefs.SetInt("BGM", 3);
        }
        else if (BGM.clip == TheEchoMusic)
        {
            PlayerPrefs.SetInt("BGM", 4);
        }
        else if (BGM.clip == TheArtOfWarMusic)
        {
            PlayerPrefs.SetInt("BGM", 5);
        }


        if (Input.GetKeyDown(KeyCode.D)) // ¬ывод сообщений в Debug console
        {
            Debug.Log("Article 0 = " + PlayerPrefs.GetInt("Article 0") + " Article 1 = " + PlayerPrefs.GetInt("Article 1") + " Article 2 = " + PlayerPrefs.GetInt("Article 2"));
        }

        TVOn();

    }

    public void PointerDown(GameObject gameObject) // Ётот метод отвечает за анимацию кнопки при нажатии (уменьшение размера и прозрачности)
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1);
    }

    public void PointerUp(GameObject gameObject) // Ётот метод отвечает за возвращение кнопки в исходное состо€ние после нажати€ кнопки
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void PointerEnter(GameObject gameObject) // Ётот метод отвечает за увеличение кнопки при наведении курсора на неЄ
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
        Sounds.PlayOneShot(PointerEnterSound);
    }

    public void PointerExit(GameObject gameObject) // Ётот медот отвечает за возвращение кноке прежнего размера после наведени€ курсора
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void OpenCloseWindow(Canvas menu) // Ётот метод отвечает за открытие/закрытие меню на кнопку "Settings"
    {
        if (menu.enabled == false)
        {
            PANA9600s.SetActive(false);
            SettingsMenu.enabled = false;
            ArticleMenu.enabled = false;
            MusicMenu.enabled = false;
        }
        else if (menu.enabled == true)
            PANA9600s.SetActive(true);
        menu.enabled = !menu.enabled;
        StartTimer = menu.enabled;
    }

    public void SaveGame() // Ётот метод отвечат за сохранение игры на кнопку "Save"
    {
        Sounds.PlayOneShot(SaveSound);
    }

    public void LoadGame() // Ётот метод отвечат за загрузку игры на кнопку "Load"
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene"));


        Sounds.PlayOneShot(LoadSound);

    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeSettingsToDeffalt()
    {
        SettingsMenu.GetComponent<Settings>().BGMVolume.value = 0.4f;
        SettingsMenu.GetComponent<Settings>().SoundsVolume.value = 0.6f;

        PlayerPrefs.SetFloat("BGM Volume", SettingsMenu.GetComponent<Settings>().BGMVolume.value);
        PlayerPrefs.SetFloat("Sounds Volume", SettingsMenu.GetComponent<Settings>().SoundsVolume.value);

        Sounds.PlayOneShot(LoadSound);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("BGM Volume", SettingsMenu.GetComponent<Settings>().BGMVolume.value);
        PlayerPrefs.SetFloat("Sounds Volume", SettingsMenu.GetComponent<Settings>().SoundsVolume.value);


        Sounds.PlayOneShot(SaveSound);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayMusic()
    {
        BGM.Play();
    }

    public void StopMusic()
    {
        BGM.Stop();
    }

    public void ChangeMusic(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.Play();
    }

    public void TVOn()
    {
        TVScrene.clip = TVClip;
        TVScrene.Play();
        TV.enabled = StartTimer;

        if (StartTimer)
        {
            TimerTime -= Time.deltaTime;
        }

        if (TimerTime <= 0)
        {
            TVScrene.Stop();
            StartTimer = false;
            TV.enabled = StartTimer;
            TimerTime = 0.5f;
        }

    }

    public void ArticleOpen(int index)
    {
        Arc.SetActive(false);
        ArticleTexts[index].SetActive(true);
        _activeText = ArticleTexts[index];
        _activeText.transform.localPosition = new Vector3(0, 0);
        StartTimer = true;

        Slider.value = 0;
    }

    public void ArticleTVOpen()
    {
        for (int i = 0; i < ArticleTexts.Length; i++)
            ArticleTexts[i].SetActive(false);
        Arc.SetActive(true);
        PANA9600s.SetActive(false);
    }

    public void OnScroll()
    {
        Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && _activeText.transform.localPosition.y >= 0)
        {
            _activeText.transform.position -= transform.up * 25;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _activeText.transform.position += transform.up * 25;
        }
    }

    public void TableOpenClose(bool _setActive)
    {
        if (GraberScript.IsPosFree.Last())
            TestTubesGraber.SetActive(_setActive);
        else
            TestTubesGraber.SetActive(!_setActive);
        Table.SetActive(_setActive);
        CloseButton.SetActive(_setActive);
        OpenButton.SetActive(!_setActive);
        DialogWindow.SetActive(!_setActive);
        Objects.SetActive(!_setActive);
        Settings.SetActive(!_setActive);
        Music.SetActive(!_setActive);
        Article.SetActive(!_setActive);
    }

    public void ScrollArticleOnTelephone()
    {
        _activeText.transform.localPosition = new Vector2(_activeText.transform.localPosition.x, Slider.value);
    }

    public void DestroyObject(GameObject destroyObject)
    {
        Destroy(destroyObject);
    }

}
