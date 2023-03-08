using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparateFootAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource footAudio;
    [SerializeField] private List<AudioClip> woodWalkEffects = new List<AudioClip>();
    [SerializeField] private List<AudioClip> metalWalkEffects = new List<AudioClip>();
    
    private string floorType;
    private void OnCollisionEnter(Collision thingWeHit)
    {
        floorType = thingWeHit.collider.tag;
        if (floorType == "WoodSound")
        {
            footAudio.PlayOneShot(woodWalkEffects[Random.Range(0, woodWalkEffects.Count)]);
        }

        if (floorType == "MetalSound")
        {
            footAudio.PlayOneShot(metalWalkEffects[Random.Range(0, metalWalkEffects.Count)]);
        }
    }
}
