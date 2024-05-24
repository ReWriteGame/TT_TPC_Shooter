using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] private CharacterAnimationEvents heroAnimationEvents;
    [SerializeField] private AudioClip[] footstepAudioClips;
    [Range(0, 1)][SerializeField] private float footstepAudioVolume = 0.5f;

    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    private void Subscribe()
    {
        heroAnimationEvents.OnFootstepEvent += FootstepAudio;
    }

    private void Unsubscribe()
    {
        heroAnimationEvents.OnFootstepEvent -= FootstepAudio;
    }

    private void FootstepAudio(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (footstepAudioClips.Length > 0)
            {
                int index = Random.Range(0, footstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(footstepAudioClips[index], transform.position, footstepAudioVolume);
            }
        }
    }
}
