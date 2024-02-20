using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject supportCreatorMenu;
    [SerializeField] GameObject storeMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void SupportCreator()
    {
        pauseMenu.SetActive(false);
        supportCreatorMenu.SetActive(true);
    }

    public void CloseSupportCreator()
    {
        pauseMenu.SetActive(true);
        supportCreatorMenu.SetActive(false);
    }

    public void Store()
    {
        pauseMenu.SetActive(false);
        storeMenu.SetActive(true);
    }

    public void CloseStore()
    {
        pauseMenu.SetActive(true);
        storeMenu.SetActive(false);
    }
}
