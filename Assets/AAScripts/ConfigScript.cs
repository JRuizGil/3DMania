using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ConfigScript : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;

    private bool isMuted = false;

    private void Start()
    {
        // Cargar valores guardados
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 75f);
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        // Aplicar valores iniciales
        UpdateMuteState();

        // Registrar eventos de los sliders
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Registrar evento del toggle (opcional si ya se conecta por el Inspector)
        muteToggle.isOn = isMuted;
        muteToggle.onValueChanged.AddListener(delegate { ToggleMute(); });
    }

    public void SetMusicVolume(float volume)
    {
        if (!isMuted)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (!isMuted)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }

        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void ToggleMute()
    {
        isMuted = muteToggle.isOn;
        UpdateMuteState();
    }

    private void UpdateMuteState()
    {
        if (isMuted)
        {
            audioMixer.SetFloat("MusicVolume", -80);
            audioMixer.SetFloat("SFXVolume", -80);
        }
        else
        {
            SetMusicVolume(musicSlider.value);
            SetSFXVolume(sfxSlider.value);
        }
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }
}
