using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private AudioClip[] diggingAudioClips;
    [SerializeField] private AudioClip[] crawlAudioClips;

    [SerializeField] private ParticleSystem[] undergroundParticles;

    [SerializeField] private ParticleSystem attackParticles;

    public void PlayDiggingSound()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(diggingAudioClips, transform, 0.2f);
    }

    public void PlayCrawlSound()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(crawlAudioClips, transform, 0.2f);
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
        var attackEmission = attackParticles.emission;
        attackEmission.enabled = play;
    }
}