using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip damageSound;
    public AudioClip bonusSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayShoot()
    {
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void PlayHit()
    {
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    public void PlayDamage()
    {
        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    public void PlayBonus()
    {
        if (bonusSound != null)
        {
            audioSource.PlayOneShot(bonusSound);
        }
    }
}