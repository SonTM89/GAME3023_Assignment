using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWorldTraveller : MonoBehaviour
{
    string spawnLocation = null;

    [SerializeField]
    public SpringJoint2D cameraSpring;

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

    private void Update()
    {
        if(cameraSpring.distance > 0.005)
        {
            cameraSpring.distance = 0.005f;
        }
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
