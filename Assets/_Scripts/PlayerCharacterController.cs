using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private static PlayerCharacterController instance;
    public static PlayerCharacterController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float speed = 5;

    private string saveKey;

    // Start is called before the first frame update
    void Start()
    {
        saveKey = "PlayerLocation";

        SaveLocation.LoadPlayerLocation();

        Saver.OnSave.AddListener(SaveLocation.SavePlayerLocation);
        Saver.OnSave.AddListener(SaveLocation.SaveScene);
    }

    // Update is called once per frame
    void Update()
    {
        // Set Player velocity
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementVector *= speed;

        //transform.Translate(movementVector);
        rb.velocity = movementVector;
    }

    public void TurnOff()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
    }

    public void TurnOn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        this.enabled = true;
    }
}
