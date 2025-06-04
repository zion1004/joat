using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public List<GameObject> levels;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject level in levels) { 
            Instantiate(level);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
