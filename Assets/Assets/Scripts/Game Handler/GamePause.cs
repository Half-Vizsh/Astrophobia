using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GamePause : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    public static bool isPause;
    void Start()
    {
        Time.timeScale = 1f;  
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPause)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;  
        isPause = true;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;   
        isPause = false;
    }
    public void Reload()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void MenuButton()
    {
        SceneManager.LoadSceneAsync("Game_Scene_Main_Menu");
    }
}
