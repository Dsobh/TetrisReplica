using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip line, piece;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayLineSound()
    {
        _audioSource.PlayOneShot(line);
    }

    public void PlayPieceSound()
    {
        _audioSource.PlayOneShot(piece);
    }
}
