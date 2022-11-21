using UnityEngine;
using UnityEngine.SceneManagement;

// Read the full commented version of this here: https://gist.github.com/theshaneobrien/5116d8fa0919ffcbcd3db8e7d298154f
public class MenuManager : MonoBehaviour
{
    [SerializeField] private string LevelToLoad = "Level1";

    public void ButtonClick()
    {
        SceneManager.LoadScene(LevelToLoad, LoadSceneMode.Single);
    }
}
