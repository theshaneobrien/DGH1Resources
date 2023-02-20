using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// You can view the fully commented code here: https://gist.github.com/theshaneobrien/741aed79faae7d7d22b9cc29f39bee8d
public class GamePlayUI : MonoBehaviour
{
    
    [SerializeField] private Button readyUpButton;
    
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI playerWonText;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Update()
    {
        scoreText.text = "Score: " + GameStateManager.Instance.playerScore.ToString();
    }

    public void ReadyUp()
    {
        GameStateManager.Instance.SetPlayerIsReady(true);
        readyUpButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisplayPlayerWonScreen()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        playerWonText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
