using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

// Responsible for playing music, modifying volume, transitioning music etc.
public class MusicManager : MonoBehaviour
{
    private MusicManager() { }

    private static MusicManager instance;

    public static MusicManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
            }

            return instance;
        }

        private set { }
    }

    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    AudioClip[] tracklist;

    [SerializeField]
    Slider musicSlider;

    [SerializeField]
    AudioMixer musicMixer;

    [SerializeField]
    float volumeMin_dB = -80.0f;

    [SerializeField]
    float volumeMax_dB = 0.0f;

    private Track currentTrack = Track.NONE;
    private int currentScene = -1;
    private string currentSceneName = "";

    public enum Track
    {
        StartScene,
        Overworld,
        City,
        Level1,
        Battle,
        NONE
    }

    // Start is called before the first frame update
    void Start()
    {
        //find others
        //if they are not the original ... DESTROY IT
        MusicManager[] musicManagers = FindObjectsOfType<MusicManager>();
        foreach(MusicManager mgr in musicManagers)
        {
            if(mgr != Instance)
            {
                Destroy(mgr.gameObject);
            }
        }

        // Dont Destroy on Load
        DontDestroyOnLoad(transform.root.gameObject);
    }

    private void Update()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        switch(currentSceneName)
        {
            case "StartScene":
                currentScene = (int)SceneEnum.STARTSCENE;
                break;
            case "OverWorld":
                currentScene = (int)SceneEnum.OVERWORLD;
                break;
            case "City":
                currentScene = (int)SceneEnum.CITY;
                break;
            case "Level1":
                currentScene = (int)SceneEnum.LEVEL1;
                break;
            case "EnemyEncounter":
                currentScene = (int)SceneEnum.ENEMYENCOUNTER;
                break;
        }

        if(currentScene != -1)
        {
            if ((int)currentTrack != currentScene)
            {
                switch (currentScene)
                {
                    case (int)SceneEnum.STARTSCENE:
                        currentTrack = Track.StartScene;
                        PlayTrack(currentTrack);
                        break;
                    case (int)SceneEnum.OVERWORLD:
                        currentTrack = Track.Overworld;
                        FadeInTrackOverSeconds(currentTrack, 3.0f);
                        break;
                    case (int)SceneEnum.CITY:
                        currentTrack = Track.City;
                        FadeInTrackOverSeconds(currentTrack, 3.0f);
                        break;
                    case (int)SceneEnum.LEVEL1:
                        currentTrack = Track.Level1;
                        FadeInTrackOverSeconds(currentTrack, 3.0f);
                        break;
                    case (int)SceneEnum.ENEMYENCOUNTER:
                        currentTrack = Track.Battle;
                        PlayTrack(currentTrack);
                        break;
                }
            }
        }

        if(musicSlider == null)
        {
            musicSlider = FindObjectOfType<Slider>();
            if(musicSlider != null)
            {
                musicSlider.onValueChanged.AddListener(SetMusicVolume);
            }           
        }
        
    }

    // Play Music belong to Track in MusicSource
    public void PlayTrack(MusicManager.Track trackID)
    {
        musicSource.clip = tracklist[(int)trackID];
        musicSource.Play();
    }

    // Fade In for a specific time to play a Track
    public void FadeInTrackOverSeconds(MusicManager.Track trackID, float duration)
    {
        musicSource.volume = 0.0f;
        PlayTrack(trackID);
        StartCoroutine(FadeInTrackOverSecondsCoroutine(duration));
    }

    IEnumerator FadeInTrackOverSecondsCoroutine(float duration)
    {
        musicSource.volume = 0.0f;
        float timer = 0.0f;

        // Fade volume
        while (timer < duration)
        {
            timer += Time.deltaTime;

            float normalizedTime = timer / duration;

            musicSource.volume = Mathf.SmoothStep(0, 1, normalizedTime);
            
            yield return new WaitForEndOfFrame();
        }
    }

    

    public void SetMusicVolume(float volumeNormalized)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Lerp(volumeMin_dB, volumeMax_dB, volumeNormalized));
    }
}
