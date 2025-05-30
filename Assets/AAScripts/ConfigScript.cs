using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ConfigScript : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private bool isMuted = false;

    private void Start()
    {
        // Cargar valores guardados
        musicSlider.value = PlayerPrefs.GetFloat("Music", 5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 5f);

        // Aplicar valores iniciales
        UpdateMuteState();

        // Registrar eventos de los sliders
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    public void SetMusicVolume(float volume)
    {
        if (!isMuted)
        {
            audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        }

        PlayerPrefs.SetFloat("Music", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (!isMuted)
        {
            audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }

        PlayerPrefs.SetFloat("SFX", volume);
    }

    public void ToggleMute()
    {        
        UpdateMuteState();
    }

    private void UpdateMuteState()
    {
        if (isMuted)
        {
            audioMixer.SetFloat("Music", -80);
            audioMixer.SetFloat("SFX", -80);
        }
        else
        {
            SetMusicVolume(musicSlider.value);
            SetSFXVolume(sfxSlider.value);
        }
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }
}
