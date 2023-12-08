using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptionsManager : MonoBehaviour
{
    #region Main Menu Screen options
    public void PlayButton() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    public void ExitButton() { Application.Quit(); }
    #endregion

    //Tutorial screen button
    public void ExitTutorial() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }

}
