using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        Debug.Log("Switching to scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
