using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader m_Instance;
    private bool m_IsLoading = false;
    public GUISkin m_LoadingBackground;
    private int m_Tick = 0;
    private bool m_SaveOnLoad = false;

    void Start()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(int level = -1, string name = "", bool saveOnLoad = false)
    {
        StartCoroutine(Loading(level, name, saveOnLoad));
    }

    private IEnumerator Loading(int level, string name, bool saveOnLoad)
    {
        AsyncOperation async;
        if (level != -1)
            async = Application.LoadLevelAsync(level);
        else
            async = Application.LoadLevelAsync(name);

        m_Tick = 0;
        m_SaveOnLoad = saveOnLoad;

        // do initial loading screen stuff
        m_IsLoading = true;
        GUI.skin = m_LoadingBackground;
        Debug.Log("Loading Started!");


        while (!async.isDone)
        {
            // do loading screen loop

            Debug.Log("Loading!");
            m_Tick++;

            yield return (0);
        }

        // do end loading screen stuff
        EndOfLoading();

        
    }

    void OnLevelWasLoaded(int level)
    {
        EndOfLoading();
    }
    
    void OnGUI()
    {
        if (m_IsLoading)
        {
            GUI.skin = m_LoadingBackground;
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "" /*"LOADING " + m_Tick*/);
        }
    }

    void EndOfLoading()
    {
        m_IsLoading = false;

        if (m_SaveOnLoad)
        {
            GameSaveManager.SceneState sceneSave = new GameSaveManager.SceneState();
            sceneSave.m_SceneNo = Application.loadedLevel;

            sceneSave.Save();
            GameSaveManager.m_Instance.SaveGame();
            Debug.Log("Saved level " + Application.loadedLevel);
        }
        m_SaveOnLoad = false;

        Debug.Log("Loading Complete!");
    }
    
}
