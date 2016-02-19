using UnityEngine;
using System.Collections;
using System;

public class InGameMenu : MonoBehaviour
{
    private bool pauseEnabled;
    public GameObject playerGO;

    public bool isInGameMainMenu;
    public bool isInGameOptions;
    public bool isInGameOptionsAudio;
    public bool isInGameOptionsDisplay;

    Font pauseMenuFont;
    public Texture inGameMenu_Resume;
    public Texture inGameMenu_Load;
    public Texture inGameMenu_Save;
    public Texture inGameMenu_Options;
    public Texture inGameMenu_ReturnToMenu;
    public Texture inGameMenu_Quit;
    public Texture inGameMenu_Audio;
    public Texture inGameMenu_Display;
    public Texture inGameMenu_Back;

    public GUISkin inGameMenuSkin;
    AudioSource playerAS;

    void Start()
    {
        pauseEnabled = false;
        isInGameMainMenu = true;
        isInGameOptions = false;
        isInGameOptionsAudio = false;
        isInGameOptionsDisplay = false;
        Time.timeScale = 1;
        AudioListener.volume = GUIMainMenu.currentVolume;
        AudioListener.pause = false;
        Cursor.visible = false;

        playerAS = playerGO.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonUp("Pause"))
        {
            isInGameMainMenu = true;
            isInGameOptions = false;
            isInGameOptionsAudio = false;
       
            if (pauseEnabled == true)
            {
                pauseEnabled = false;
                Time.timeScale = 1;
                AudioListener.volume = GUIMainMenu.currentVolume;
                AudioListener.pause = false;
                Cursor.visible = false;
            }


            else if (pauseEnabled == false)
            {
                pauseEnabled = true;
                AudioListener.volume = GUIMainMenu.currentVolume;
                AudioListener.pause = true;
                Time.timeScale = 0;
                Cursor.visible = true;
            }
        }
    }

    void OnGUI()
    {

        //GUI.skin.box.font = pauseMenuFont;
        //GUI.skin.button.font = pauseMenuFont;
        //GUI.backgroundColor = Color.white;
        //GUI.skin.box.border.left = 1;
        //GUI.skin.box.border.right = 1;
        //GUI.skin.box.border.top = 1;
        GUI.skin = inGameMenuSkin;

        if (pauseEnabled == true)
        {
            if (isInGameMainMenu == true)
            {                        
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 130, 250, 40), inGameMenu_Resume))
                {
                    pauseEnabled = false;
                    Time.timeScale = 1;
                    Cursor.visible = false;
                    AudioListener.pause = false;
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 90, 250, 40), inGameMenu_Load))
                {
                    GameSaveManager.m_Instance.StartLoading();
                    GameSaveManager.SceneState save = new GameSaveManager.SceneState();
                    save.Load();
                    Application.LoadLevel(save.m_SceneNo);

                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 250, 40), inGameMenu_Save))
                {
                    GameSaveManager.SceneState sceneSave = new GameSaveManager.SceneState();
                    sceneSave.m_SceneNo = Application.loadedLevel;
                    sceneSave.Save();
                    GameSaveManager.m_Instance.SaveGame();
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 250, 40), inGameMenu_Options))
                {
                    isInGameOptions = true;
                    isInGameMainMenu = false;
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 30, 250, 40), inGameMenu_ReturnToMenu))
                {
                    Application.LoadLevel(0);
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 80, 250, 40), inGameMenu_Quit))
                {
                    Application.Quit();
                }
            }
            if (isInGameOptions == true)
            {                    
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "OPTIONS");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 50), inGameMenu_Audio))
                {
                    isInGameOptionsAudio = true;
                    isInGameOptions = false;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 250, 50), inGameMenu_Display))
                {
                    isInGameOptionsDisplay = true;
                    isInGameOptions = false;
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), inGameMenu_Back))
                {
                    isInGameOptions = false;
                    isInGameMainMenu = true;
                }
            }

            if (isInGameOptionsAudio)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "AUDIO SETTINGS");

                AudioListener.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, 110, 150, 30), GUIMainMenu.currentVolume, 0.0F, 1.0F);
                GUIMainMenu.currentVolume = AudioListener.volume;
                GUI.Label(new Rect(Screen.width / 2, 90, 100, 20), "VOLUME");
                GUI.Label(new Rect((Screen.width / 2) - 60, 110, 100, 20), "0");
                GUI.Label(new Rect((Screen.width / 2) + 105, 110, 100, 20), "100");

                // Player Sound Stuff (NOT COMPLETE)
                playerAS.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, 200, 150, 30), playerAS.volume, 0.0F, 1.0F);
                //playerAS.volume = AudioListener.volume;
                GUI.Label(new Rect(Screen.width / 2, 180, 100, 20), "Background (Player Sound) Noise");
                GUI.Label(new Rect((Screen.width / 2) - 60, 200, 100, 20), "0");
                GUI.Label(new Rect((Screen.width / 2) + 105, 200, 100, 20), "100");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), inGameMenu_Back))
                {
                    isInGameOptionsAudio = false;
                    isInGameOptions = true;
                }
            }

            if (isInGameOptionsDisplay)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 250, 300), "DISPLAY SETTINGS");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 250, 50), "Resolution 640 x 480"))
                {
                    Screen.SetResolution(640, 480, true);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 250, 50), "Resolution 1920 x 1080"))
                {
                    Screen.SetResolution(1920, 1080, true);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), inGameMenu_Back))
                {
                    isInGameOptionsDisplay = false;
                    isInGameOptions = true;
                }
            }
        }
    }
}
