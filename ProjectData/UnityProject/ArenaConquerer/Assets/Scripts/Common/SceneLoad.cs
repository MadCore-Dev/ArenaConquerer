using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;

    private void Start()
    {
        if (PauseMenu)
        {
            PauseMenu.SetActive(false);
        }
    }

    public void LoadScene(int number)
    {
        SceneManager.LoadSceneAsync(number);
    }

    private void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (PauseMenu)
            {
                PauseMenu.SetActive(true);
            }
        }
    }
}
