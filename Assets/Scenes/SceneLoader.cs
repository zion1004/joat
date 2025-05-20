using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("map 1", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 2", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 3-1", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 3-2", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 3-3", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 3-4", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 3-4-1", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 3-5", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 3-5-1", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 3-5-2", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 3-6", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 4", LoadSceneMode.Additive);
        SceneManager.LoadScene("map 4-1", LoadSceneMode.Additive);
    }

}
