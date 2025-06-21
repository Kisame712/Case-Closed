using UnityEngine;

public class ClickSoundManager : MonoBehaviour
{
    AudioSource clickSource;
    private void Awake()
    {
        clickSource = GetComponent<AudioSource>();    
    }
    public void PlayClickSound()
    {
        clickSource.Play();
    }

}
