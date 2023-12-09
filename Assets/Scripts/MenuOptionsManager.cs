using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptionsManager : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;
    #region Main Menu Screen options
    public void PlayButton() 
    {
        clickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
    public void ExitButton() { Application.Quit(); }
    #endregion

    //Tutorial screen button
    public void ExitTutorial() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }

    //EndGame screen button
    //public void BackToMainMenu() { SceneManager.LoadScene(0); }

}
