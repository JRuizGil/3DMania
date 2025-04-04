using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ConfigScript : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer; // Asigna el AudioMixer desde el Inspector
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;

    private bool isMuted = false;

    private void Start()
    {
        // Cargar valores guardados
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        // Aplicar valores iniciales
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
        UpdateMuteState();
    }

    public void SetMusicVolume(float volume)
    {
        if (!isMuted)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Convierte lineal a logarítmico
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (!isMuted)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        UpdateMuteState();
    }

    private void UpdateMuteState()
    {
        if (isMuted)
        {
            audioMixer.SetFloat("MusicVolume", -80); // Silencia la música
            audioMixer.SetFloat("SFXVolume", -80);   // Silencia los efectos
        }
        else
        {
            SetMusicVolume(musicSlider.value);
            SetSFXVolume(sfxSlider.value);
        }

        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }
}

