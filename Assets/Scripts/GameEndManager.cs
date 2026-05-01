using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
