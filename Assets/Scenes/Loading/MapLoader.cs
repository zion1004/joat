using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    string[] scenes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.backgroundLoadingPriority = ThreadPriority.High;
        scenes = new string[] { "map 1", "map 2", "map 3-1", "map 3-2",
                               "map 3-3", "map 3-4", "map 3-4-1", "map 3-5", "map 3-5-1",
                              "map 3-5-2", "map 3-6", "map 4", "map 4-1" };

        StartCoroutine(LoadScenes());
    }

    IEnumerator LoadScenes()
    {
        yield return null;
        foreach (var scene in scenes)
        {
            StartCoroutine(LoadScene(scene));
            yield return null;
        }
        yield return null;
    }
        IEnumerator LoadScene(string scene)
    {
        yield return null;
        var op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        yield return null;
    }
}
