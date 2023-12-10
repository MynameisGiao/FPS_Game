using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellControl : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;
    public ParticleSystem particleSystem_;
    private bool isFire;
    public void Awake()
    {
        audioSource.volume = 0.5f;
    }
    public void Fire()
    {
        isFire = true;
        particleSystem_.Play();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(isFire)
        {
            audioSource.PlayOneShot(audioClip[UnityEngine.Random.Range(0, audioClip.Length)]);
            isFire = false;
        }
    }

    //IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(0.4f);
    //    if (audioSource)
    //        if (!audioSource.isPlaying)
    //            audioSource.PlayOneShot(audioClip[UnityEngine.Random.Range(0, audioClip.Length)]);
    //}
}
