using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioClip[] Btn;
    public AudioClip[] Btnquit;

    private AudioSource audioSource;

    private void Awake()
    {
        // Asegúrate de que este GameObject tenga un AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void ReproductBtnSound()
    {
        if (Btn.Length > 0)
        {
            AudioClip clip = Btn[Random.Range(0, Btn.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public void ReproductBtnQuitSound()
    {
        if (Btnquit.Length > 0)
        {
            AudioClip clip = Btnquit[Random.Range(0, Btnquit.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
