using System;
using UnityEngine;

public class EyeCreature : MonoBehaviour
{
    public GameObject Explosion;
    public AudioClip screechClip;
    KillSelf killer;
    bool hasExploded;
    bool playingScreech;
    AudioSource source;

    void Awake()
    {
        killer = GetComponent<KillSelf>();
        source =GetComponent<AudioSource>();
    }

    void Update()
    {
        if (killer.deathTimer < 1.5f && killer.deathTimer > .1f)
        {   
            if(source.isPlaying && !playingScreech)
            {
                PlayScreech();

            }
            
        } else if (killer.deathTimer <= .1f)
        {
            Explode();
        }
    }

    void PlayScreech()
    {
        playingScreech =true;
        source.clip = screechClip;
        source.Play();
    }

    private void Explode()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
