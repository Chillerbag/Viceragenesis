using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private AudioClip[] diggingAudioClips;
    [SerializeField] private AudioClip[] crawlAudioClips;
    [SerializeField] private AudioClip[] zapAudioClips;

    [SerializeField] private AudioClip lightningDamage;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip healthSound;
    [SerializeField] private AudioClip damageSound;

    [SerializeField] private ParticleSystem[] undergroundParticles;

    [SerializeField] private ParticleSystem[] attackParticles;

    public void PlayDiggingSound()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(diggingAudioClips, transform, 0.5f);
    }

    public void PlayCrawlSound()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(crawlAudioClips, transform, 0.5f);
    }

    public void PlayZapSound()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(zapAudioClips, transform, 0.2f);
    }

    public void PlayLightningSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(lightningDamage, transform, 0.5f);
    }

    public void PlayAttackSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(attackSound, transform, 1f);
    }

    public void PlayHealthSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(healthSound, transform, 0.8f);
    }

    public void PlayDamageSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(damageSound, transform, 5f);
    }

    public void PlayUndergroundParticles(bool play)
    {
        foreach (ParticleSystem particleSystem in undergroundParticles)
        {
            var undergroundEmission = particleSystem.emission;
            undergroundEmission.enabled = play;
        }

    }

    public void PlayAttackParticles(bool play)
    {
        foreach (ParticleSystem particleSystem in attackParticles)
        {
            var attackEmission = particleSystem.emission;
            attackEmission.enabled = play;
        }

    }
}