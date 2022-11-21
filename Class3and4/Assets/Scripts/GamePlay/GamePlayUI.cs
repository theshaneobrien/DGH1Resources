using UnityEngine;
using UnityEngine.UI;
using TMPro;

// You can view the fully commented code here: https://gist.github.com/theshaneobrien/741aed79faae7d7d22b9cc29f39bee8d
public class GamePlayUI : MonoBehaviour
{
    private GameStateManager gameState;
    
    [SerializeField] private Button readyUpButton;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private void Start()
    {
        gameState = this.GetComponent<GameStateManager>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + gameState.playerScore.ToString();
    }

    public void ReadyUp()
    {
        gameState.playerIsReady = true;
        readyUpButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
