using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public GameObject pauseMenu_panel;
    public bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            StartCoroutine(FindPauseMenu()); // Ensure we find it after the scene loads
        }

        private System.Collections.IEnumerator FindPauseMenu()
        {
            yield return new WaitForSeconds(0f); // Give time for the scene to load

            GameObject panel = GameObject.Find("PauseMenu");
            if (panel != null)
            {
                pauseMenu_panel = panel;
                pauseMenu_panel.SetActive(false);
            }
            else
            {
                Debug.LogError("PauseMenu panel not found in loaded scene!");
            }
        }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseMenu_panel.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        // pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        
        pauseMenu_panel.SetActive(false);
        isPaused = false;
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

    public void Options()
    {
        isPaused = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player); // Remove the player from the scene
        }
        SceneManager.LoadSceneAsync("Options");
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        isPaused = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player); // Remove the player from the scene
        }
        SceneManager.LoadSceneAsync("MainMenu");
        Time.timeScale = 1f;
    }
    public void Quit_Game()
    {
        Application.Quit();
    }
}
