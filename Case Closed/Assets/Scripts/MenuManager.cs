using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuManager : MonoBehaviour
{
   public void PlayGame()
    {
        StartCoroutine(WaitingForClick());
    }

   public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    } 

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator WaitingForClick()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
