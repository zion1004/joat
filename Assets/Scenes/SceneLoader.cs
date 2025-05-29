using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SceneLoader : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Start()
    {
        Application.backgroundLoadingPriority = ThreadPriority.High;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        var op = SceneManager.LoadSceneAsync("mapjoat", LoadSceneMode.Single);
        op.allowSceneActivation = false;
        float time = -1f;
        int dots = 0;
        while (!op.isDone)
        {
            yield return null;
            if (op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(5);
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}