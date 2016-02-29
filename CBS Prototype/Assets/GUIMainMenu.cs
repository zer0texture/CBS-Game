using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour, GameSaveManager.IGameSaver
{
    public bool isMainMenu;
    public bool isMainMenuOptions;
    public bool isMainMenuSliders;
    public bool isMainMenuOptionsAudio;

    public float sliderValue;
    public static float barMaxSliderValue = 100.0f;
    public static float snakeMaxSliderValue = 50.0f;
    public static float otherMaxSliderValue = 10.0f;
    public static int genderMaxSliderValue = 1;

    static public float currentVolume = 1.0F;

    public GUISkin mainMenuWelcomeSkin;
    public GUISkin mainMenuSkin;
    public static bool welcomeStarted = false;

    public Texture mainMenu_NewGame;
    public Texture mainMenu_LoadGame;
    public Texture mainMenu_Sliders;
    public Texture mainMenu_Options;
    //public Texture mainMenu_ReturnToDesktop;
    public Texture mainMenu_Quit;
    public Texture mainMenu_Back;
    public Texture mainMenu_Audio;
    public Texture mainMenu_Display;

    float timer = 1;
    bool timerStarted = false;
    //customTimer timer;

   // public AudioSource mouseNoises;
   // public AudioClip mouseClick;

    public enum MenuOption
    {
        MENU_CONTINUE,
        MENU_NEW_GAME,
        MENU_OPTION,
        MENU_SLIDERS,
        MENU_CREDITS,
        MENU_QUIT,
        MENU_BACK
    }

    public MenuOption menuOption;

    UISlider.SliderSave sliderSave;

    void Start()
    {
        isMainMenu = true;
        isMainMenuOptions = false;
        isMainMenuSliders = false;
        isMainMenuOptionsAudio = false;
        Time.timeScale = 1;
        AudioListener.volume = currentVolume;
        //timer = GetComponent<customTimer>();

        Load();
        Save();
        
    }
   
    void Update()
    { 
        if (Application.loadedLevel != 0)
        {
            this.enabled = false;
        }
    }

    void OnGUI()
    {
        if (welcomeStarted == false)
        {
            GUI.skin = mainMenuWelcomeSkin;
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), ""/*MainMenuTexture*/);
            GUI.TextField(new Rect(Screen.width/2 - 100, Screen.height/2 + 230, 170, 30), "Press Any Key to Continue");
            if ((Input.anyKeyDown)||Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                timerStarted = true;
            }
           
        }
        if (timerStarted)
        {
            timer -= Time.deltaTime;
           // GUI.color = new Color(1, 1, 1, timer);
            if (timer <= 0)
            {
                //GUI.color = new Color(1, 1, 1, 1);
                welcomeStarted = true;
            }
        }
        if(welcomeStarted)
        { 
           // GUI.color = new Color();
            GUI.skin = mainMenuSkin;
            
            if (isMainMenu)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 40), mainMenu_NewGame))
                {
                    // mouseNoises.PlayOneShot(mouseClick,1);
                    //Application.LoadLevel(1);
                    LevelLoader.m_Instance.LoadLevel(1, "", true);
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 250, 40), mainMenu_LoadGame))
                {
                    GameSaveManager.m_Instance.StartLoading();
                   // mouseNoises.PlayOneShot(mouseClick, 1);
                    GameSaveManager.SceneState save = new GameSaveManager.SceneState();
                    save.Load();
                    //Application.LoadLevel(save.m_SceneNo);
                    LevelLoader.m_Instance.LoadLevel(save.m_SceneNo);
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 250, 40), mainMenu_Sliders))
                {
                   // mouseNoises.PlayOneShot(mouseClick, 100);
                    isMainMenuSliders = true;
                    isMainMenu = false;
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 20, 250, 40), mainMenu_Options))
                {
                   // mouseNoises.PlayOneShot(mouseClick, 1);
                    isMainMenuOptions = true;
                    isMainMenu = false;
                }

                /*if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 70, 250, 40), mainMenu_ReturnToDesktop))
                {
                    Application.LoadLevel(0);
                }*/

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 110, 250, 40), mainMenu_Quit))
                {
                   // mouseNoises.PlayOneShot(mouseClick, 1);
                    Application.Quit();
                }
            }

            if (isMainMenuSliders == true)
            {
                

                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "SLIDERS");

                sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 190, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.GENDER), 0.0F, genderMaxSliderValue);
                UISlider.SetSliderValue(UISlider.SliderType.GENDER, sliderValue);

                GUI.Label(new Rect((Screen.width / 2) - 45, (Screen.height / 2) - 210, 300, 20), "Gender of Vic");
                GUI.Label(new Rect((Screen.width / 2) - 82, (Screen.height / 2) - 195, 100, 20), "Male");
                GUI.Label(new Rect((Screen.width / 2) + 105, (Screen.height / 2) - 195, 100, 20), "Female");

                sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 150, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.HEALTH), 0.0F, barMaxSliderValue);
                UISlider.SetSliderValue(UISlider.SliderType.HEALTH, sliderValue);

                GUI.Label(new Rect((Screen.width / 2) - 45, (Screen.height / 2) - 170, 300, 20), "Amount of Player Health");
                GUI.Label(new Rect((Screen.width / 2) - 60, (Screen.height / 2) - 150, 100, 20), "0");
                GUI.Label(new Rect((Screen.width / 2) + 105, (Screen.height / 2) - 150, 100, 20), "100");

                sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 110, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.OIL_MAX), 0.0F, barMaxSliderValue);
                UISlider.SetSliderValue(UISlider.SliderType.OIL_MAX, sliderValue);

                GUI.Label(new Rect((Screen.width / 2) - 35, (Screen.height / 2) - 130, 300, 20), "Amount of Player Oil");
                GUI.Label(new Rect((Screen.width / 2) - 60, (Screen.height / 2) - 110, 100, 20), "0");
                GUI.Label(new Rect((Screen.width / 2) + 105, (Screen.height / 2) - 110, 100, 20), "100");

                sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 70, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES), 0.0F, snakeMaxSliderValue);
                UISlider.SetSliderValue(UISlider.SliderType.NUM_OF_SNAKES, sliderValue);

                GUI.Label(new Rect((Screen.width / 2) - 30, (Screen.height / 2) - 90, 300, 20), "Number of Snakes");
                GUI.Label(new Rect((Screen.width / 2) - 60, (Screen.height / 2) - 70, 100, 20), "0");
                GUI.Label(new Rect((Screen.width / 2) + 105, (Screen.height / 2) - 70, 100, 20), "50");

                sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 30, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.LANTERN_SIZE), 0.0F, otherMaxSliderValue);
                UISlider.SetSliderValue(UISlider.SliderType.LANTERN_SIZE, sliderValue);

                GUI.Label(new Rect((Screen.width / 2) - 30, (Screen.height / 2) - 50, 300, 20), "Player Lantern Size");
                GUI.Label(new Rect((Screen.width / 2) - 60, (Screen.height / 2) - 30, 100, 20), "0");
                GUI.Label(new Rect((Screen.width / 2) + 105, (Screen.height / 2) - 30, 100, 20), "10");

                sliderValue = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, (Screen.height / 2) + 10, 150, 30), UISlider.GetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL), 0.0F, otherMaxSliderValue);
                UISlider.SetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL, sliderValue);

                GUI.Label(new Rect((Screen.width / 2) - 35, (Screen.height / 2) - 10, 300, 20), "Length of Player Trail");
                GUI.Label(new Rect((Screen.width / 2) - 60, (Screen.height / 2) + 10, 100, 20), "0");
                GUI.Label(new Rect((Screen.width / 2) + 105, (Screen.height / 2) + 10, 100, 20), "10");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, (Screen.height / 2) + 50, 250, 50), mainMenu_Back))
                {
                   // mouseNoises.PlayOneShot(mouseClick, 1);

                    isMainMenuSliders = false;
                    isMainMenu = true;
                }

            }

            if (isMainMenuOptions == true)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "OPTIONS");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 50), mainMenu_Audio))
                {
                   // mouseNoises.PlayOneShot(mouseClick, 1);
                    isMainMenuOptionsAudio = true;
                    isMainMenuOptions = false;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 250, 50), mainMenu_Display))
                {
                   // mouseNoises.PlayOneShot(mouseClick, 1);
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), mainMenu_Back))
                {
                  //  mouseNoises.PlayOneShot(mouseClick, 1);
                    isMainMenuOptions = false;
                    isMainMenu = true;
                }
            }

            if (isMainMenuOptionsAudio)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "AUDIO SETTINGS");

                AudioListener.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 150, 150, 30), AudioListener.volume, 0.0F, 1.0F);
                currentVolume = AudioListener.volume;
                GUI.Label(new Rect(Screen.width / 2, 90, 100, 20), "VOLUME");
                GUI.Label(new Rect((Screen.width / 2) - 60, 110, 100, 20), "0");
                GUI.Label(new Rect((Screen.width / 2) + 105, 110, 100, 20), "100");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 250, 50), mainMenu_Back))
                {
                   // mouseNoises.PlayOneShot(mouseClick, 1);
                    isMainMenuOptionsAudio = false;
                    isMainMenuOptions = true;
                }
            }
        }
    }


    public void Save()
    {
        if (sliderSave == null)
            sliderSave = new UISlider.SliderSave();

        sliderSave.snakesNum = (int)UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES);
        sliderSave.lanternSize = UISlider.GetSliderValue(UISlider.SliderType.LANTERN_SIZE);
        sliderSave.playerHealth = (int)UISlider.GetSliderValue(UISlider.SliderType.HEALTH);
        sliderSave.oilMax = UISlider.GetSliderValue(UISlider.SliderType.OIL_MAX);
        sliderSave.trailLength = UISlider.GetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL);

        sliderSave.Save();
    }

    public void Load()
    {
        if (sliderSave == null)
            sliderSave = new UISlider.SliderSave();
        sliderSave.Load();

        if (sliderSave.LoadedSuccessfully())
        {
            UISlider.SetSliderValue(UISlider.SliderType.NUM_OF_SNAKES, sliderSave.snakesNum);
            UISlider.SetSliderValue(UISlider.SliderType.LANTERN_SIZE, sliderSave.lanternSize);
            UISlider.SetSliderValue(UISlider.SliderType.HEALTH, sliderSave.playerHealth);
            UISlider.SetSliderValue(UISlider.SliderType.OIL_MAX, sliderSave.oilMax);
            UISlider.SetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL, sliderSave.trailLength);
        }
        else
        {
            UISlider.SetSliderValue(UISlider.SliderType.HEALTH, barMaxSliderValue);
            UISlider.SetSliderValue(UISlider.SliderType.OIL_MAX, barMaxSliderValue);
            UISlider.SetSliderValue(UISlider.SliderType.NUM_OF_SNAKES, snakeMaxSliderValue);
            UISlider.SetSliderValue(UISlider.SliderType.LANTERN_SIZE, otherMaxSliderValue);
            UISlider.SetSliderValue(UISlider.SliderType.LENGTH_OF_TRAIL, otherMaxSliderValue);
        }
    }

    public void AddAsListener()
    {
        GameSaveManager.m_Instance.AddSaveListener(this);
    }
}

