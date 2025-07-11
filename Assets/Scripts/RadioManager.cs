using UnityEngine;

public class RadioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songs;

    private int currentIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (songs.Length > 0)
        {
            PlayCurrentSong();
        }
    }

    public void NextSong()
    {
        currentIndex = (currentIndex + 1) % songs.Length;
        PlayCurrentSong();
    }

    void PlayCurrentSong()
    {
        audioSource.clip = songs[currentIndex];
        audioSource.Play();
    }


    
}
