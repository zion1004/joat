using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start()
    {
        Application.backgroundLoadingPriority = ThreadPriority.High;
        StartCoroutine(LoadScene("map 0"));
    }


    IEnumerator LoadScene(string scene)
    {
        yield return null;
        var op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        yield return null;
    }
}