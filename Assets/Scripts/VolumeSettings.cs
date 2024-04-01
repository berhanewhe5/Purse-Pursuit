using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class VolumeSettings : MonoBehaviour
{

    [SerializeField] Slider soundEffectsVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] AudioMixer audioMixer;



    void Start()
    {
        if (PlayerPrefs.GetInt("GamePlayedBefore") == 0)
        {
            PlayerPrefs.SetFloat("SoundEffectsVolume", 0.5f);
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            PlayerPrefs.SetInt("SongNumber", 1);
        }

        GetComponent<GameManagerScript>().currentSong = PlayerPrefs.GetInt("SongNumber");
        if (PlayerPrefs.HasKey("SoundEffectsVolume"))
        {
            LoadSoundEffectsVolume();
        }
        else { 
            SetSoundEffectsVolume();
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    public void SetSoundEffectsVolume()
    {
        float volume = soundEffectsVolumeSlider.value;
        audioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(volume) *20);
        PlayerPrefs.SetFloat("SoundEffectsVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void LoadMusicVolume()
    { 
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume();
    }

    public void LoadSoundEffectsVolume()
    {
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");
        SetSoundEffectsVolume();
    }
}
