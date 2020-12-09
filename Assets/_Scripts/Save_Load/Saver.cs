using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Saver : MonoBehaviour
{
    public static UnityEvent OnSave = new UnityEvent();
    public static UnityEvent OnLoad = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    Save();
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Load();
        //}
    }

    private void Load()
    {
        OnLoad.Invoke();
    }

    private void Save()
    {
        OnSave.Invoke();
    }

    public void OnClickSaveButton()
    {
        Save();
    }
}
