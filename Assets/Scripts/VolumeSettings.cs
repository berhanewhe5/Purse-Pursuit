using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    [SerializeField] Slider soundEffectsVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
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
        audioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(volume)*20);
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
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume();
    }

    public void LoadSoundEffectsVolume()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");
        SetSoundEffectsVolume();
    }
}
