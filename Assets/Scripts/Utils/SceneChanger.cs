using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string target;

    public void Load()
    {
        SceneManager.LoadScene(target);
    }
}
