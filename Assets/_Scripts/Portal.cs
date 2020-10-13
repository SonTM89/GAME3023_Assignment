using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Change scene when player collides with portal
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerWorldTraveller player = collision.gameObject.GetComponent<PlayerWorldTraveller>();
            player.SpawnLocation = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(tag);
        }
    }
}
