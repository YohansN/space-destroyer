using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptionsManager : MonoBehaviour
{
    [SerializeField] private Animator transition;
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
    public void ExitTutorial() 
    {
        transition.SetTrigger("Start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
