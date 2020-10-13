using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWorldTraveller : MonoBehaviour
{
    string spawnLocation = null;

    //Property
    public string SpawnLocation
    {
        get;
        set;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Keep player exists in new scene
        DontDestroyOnLoad(gameObject);
    }

    // Set position for player at new scene
    private void OnLevelWasLoaded(int level)
    {
        if (spawnLocation != null)
        {
            SpawnPoint p = FindObjectOfType<SpawnPoint>();

            transform.position = p.transform.position;
        }
    }
}
