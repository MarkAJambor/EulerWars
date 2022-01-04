using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomPauseMenu : MonoBehaviour {

    public bool gameIsPaused = false;
    public GameObject pauseMenu;
    public AudioListener[] audioListeners;

    public void Start()
    {
        gameIsPaused = false;
        audioListeners = FindObjectsOfType<AudioListener>();
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<GameManager>().controlsPopup.SetActive(false);
            if (gameIsPaused)
            {
                Cursor.visible = false;
                this.resume();
            }
            else
            {
                this.pause();
                foreach (AIController enemy in FindObjectOfType<PlayerManagement>().enemies)
                {
                    if (enemy != null)
                    {
                        foreach (ModuleActivater module in enemy.modules)
                        {
                            if (module != null)
                            {
                                if (module.laserSoundEffect != null)
                                {
                                    module.laserSoundEffect.Pause();
                                }
                            }
                        }
                    }
                }
                foreach (ModuleActivater module in FindObjectOfType<PlayerManagement>().modules)
                {
                    if (module != null)
                    {
                        if (module.laserSoundEffect != null)
                        {
                            module.laserSoundEffect.Pause();
                        }
                    }
                }
            }
        }
	}

    public void resume()
    {
        AudioListener.volume = 1;
        foreach (AIController enemy in FindObjectOfType<PlayerManagement>().enemies)
        {
            if (enemy != null)
            {
                foreach (ModuleActivater module in enemy.modules)
                {
                    if (module != null)
                    {
                        if(module.laserSoundEffect != null)
                        {
                            module.laserSoundEffect.Play();
                        }
                        
                    }
                }
            }
        }
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void pause()
    {
        AudioListener.volume = 0;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void loadMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        Debug.Log("Load main menu");
    }

    public void quitGame()
    {
        Debug.Log("quite game");
        Application.Quit();
    }
}
