using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject ExplosionPrefab;
    public Sprite BrokenSprite;
    public AudioClip dieAudio;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Die()
    {
        sr.sprite = BrokenSprite;
        Instantiate(ExplosionPrefab,transform.position,transform.rotation);
        PlayerMananger.Instance.isDefeat1 = true;
        PlayerMananger.Instance.isDefeat2 = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }

}
