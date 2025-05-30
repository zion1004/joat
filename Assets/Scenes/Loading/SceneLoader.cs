using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class SceneLoader : MonoBehaviour
{
    //    public Slider slider;
    string[] scenes;
    List<AsyncOperation> ops;

    private void Start()
    {
        Application.backgroundLoadingPriority = ThreadPriority.High;
        //scenes = new string[] { "map 0", "map 1", "map 2", "map 3-1", "map 3-2",
        //                        "map 3-3", "map 3-4", "map 3-4-1", "map 3-5", "map 3-5-1",
        //                        "map 3-5-2", "map 3-6", "map 4", "map 4-1" };
        scenes = new string[] { "map 0" };
        ops = new List<AsyncOperation>();
        StartCoroutine(LoadScenes());
//        slider.value = 0f;
    }
    IEnumerator LoadScenes()
    {
        yield return null;
        foreach (var scene in scenes) { 
            StartCoroutine(LoadScene(scene));
            yield return null;
        }
        while(!ops.All(o => o.progress >= 0.9f))
        {
            yield return null;
        }
        foreach (var op in ops)
        {
            op.allowSceneActivation = true;
            yield return null;
        }
        yield return null;
    }

    IEnumerator LoadScene(string scene)
    {
        yield return null;
        var op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        op.allowSceneActivation = false;
        ops.Add(op);
        while (!op.isDone)
        {
            yield return null;
        }
        yield return null;
    }

//    IEnumerator LoadScene()
//    {
//        var op = SceneManager.LoadSceneAsync("map 0", LoadSceneMode.Single);
//        op.allowSceneActivation = false;

//        while (!op.isDone)
//        {

//            if (op.progress >= 0.9f)
//            {
////                slider.value = op.progress;
//                op.allowSceneActivation = true;
//            }
////            slider.value = 1f;
//            yield return null;
//        }
//    }
}