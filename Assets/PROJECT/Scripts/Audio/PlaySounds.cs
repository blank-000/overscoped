
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySounds : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip SpecialClip;

    public float timeBetweenClips;

    [SerializeField]AudioSource source;
    float timer;

    void OnEnable()
    {
        if(source == null) source = GetComponent<AudioSource>();
        timer = timeBetweenClips;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0 )
        {
            if(!source.isPlaying)
            {
                PlayRandom();
                timer = timeBetweenClips;
            }
        }
    }

    void PlayRandom()
    {

        if(clips.Length > 0)
        {
            if(this.gameObject.CompareTag("Pryc")){
                return;
            }
            source.clip = clips[Random.Range(0,clips.Length-1)];
            source.Play();
        }
    }

    public void PlaySpecial()
    {

            source.clip = SpecialClip;
            source.Play();

    }

    public void PlayLightning()
    {
            if(!source.isPlaying)
            {
                source.pitch = Random.Range(.9f, 1.1f);
                source.clip = clips[0];
                source.Play();

            }
            
    }
}
