using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Purpose
    //      Load a scene

    public string target;

    public void Load()
    {
        SceneManager.LoadScene(target);
    }
}
