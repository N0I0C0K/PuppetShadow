using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip dieAudio;
    public AudioClip effectAudio;
    public AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

}