using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GamePause : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    public static bool isPause;
    void Start()
    {
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
}
