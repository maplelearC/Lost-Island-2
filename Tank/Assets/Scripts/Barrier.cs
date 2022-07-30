using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
   public AudioClip hitAudio;


   public void PlayerAudio()
    {
        AudioSource.PlayClipAtPoint(hitAudio,transform.position);
    }
}
