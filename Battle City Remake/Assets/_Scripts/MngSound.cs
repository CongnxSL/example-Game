using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngSound : MonoBehaviour
{
    public static MngSound Instance;
    [SerializeField] private AudioSource effectsSource, effectsSourceOnly, enemyMoving, playerMoving;
    [SerializeField] public List<AudioClip> effectClip;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        OffEnemyMoveSound();
        OffPlayerMoveSound();
    }
    public void PlaySoundOnly(AudioClip clip)
    {
        if (effectsSourceOnly.isPlaying)
        {
            effectsSourceOnly.Stop();
        }
        effectsSourceOnly.PlayOneShot(clip);
    }
    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    public void OnEnemyMoveSound()
    {
        enemyMoving.mute = false;
    }
    public void OffEnemyMoveSound()
    {
        enemyMoving.mute = true;
    }
    public void OnPlayerMoveSound()
    {
        playerMoving.mute = false;
    }
    public void OffPlayerMoveSound()
    {
        playerMoving.mute = true;
    }
}
