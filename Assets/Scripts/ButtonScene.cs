using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    [SerializeField] private string Scene;

    public void ChangeToScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Scene);
    }
}
