using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void EnterTheGame()
    {
        SceneManager.LoadSceneAsync("Game_Main_Stage");
    }
    public void ExitTheGame()
    {
         #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
