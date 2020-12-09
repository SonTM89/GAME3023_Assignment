using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SaveLocation
{

    public static void SavePlayerLocation()
    {
        GameObject rigidBody = GameObject.FindGameObjectWithTag("Player");
        string savedData = (rigidBody.transform.position).ToString();
        savedData = savedData.Trim(new char[] { '(', ')' });
        PlayerPrefs.SetString("PlayerLocation", savedData);
        Debug.Log("Player location saved!");
    }

    public static void LoadPlayerLocation()
    {
        string saveKey = "PlayerLocation";
        GameObject rigidBody = GameObject.FindGameObjectWithTag("Player");

        if (PlayerPrefs.HasKey(saveKey))
        {
            string savedData = PlayerPrefs.GetString(saveKey, "");
            Debug.Log(savedData);

            char[] delimiters = new char[] { ',' };
            string[] splitData = savedData.Split(delimiters);

            float posX = float.Parse(splitData[0]);
            float posY = float.Parse(splitData[1]);
            float posZ = float.Parse(splitData[2]);

            rigidBody.transform.position = new Vector3(posX, posY, posZ);

        }
    }

    public static void SaveScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch(currentScene)
        {
            case "OverWorld":
                PlayerPrefs.SetInt("CurrentScene", (int)SceneEnum.OVERWORLD);
                break;
            case "City":
                PlayerPrefs.SetInt("CurrentScene", (int)SceneEnum.CITY);
                break;
            case "Level1":
                PlayerPrefs.SetInt("CurrentScene", (int)SceneEnum.LEVEL1);
                break;
        }

        Debug.Log("Current Scene saved!");
    }

    public static void LoadCurrentScene()
    {
        string saveKey = "CurrentScene";
        int savedData = PlayerPrefs.GetInt(saveKey);

        switch(savedData)
        {
            case (int)SceneEnum.OVERWORLD:
                SceneManager.LoadScene("Overworld");
                break;
            case (int)SceneEnum.CITY:
                SceneManager.LoadScene("City");
                break;
            case (int)SceneEnum.LEVEL1:
                SceneManager.LoadScene("Level1");
                break;
        }

        Debug.Log("Scene Loaded!");
    }
}
