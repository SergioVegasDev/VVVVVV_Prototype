using System.Collections.Generic;
using UnityEngine;

public enum AudioClips
{
    ChangeGravity,
    DeathSound,
    FinalEncounter
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    public Dictionary<AudioClips, AudioClip> clipList = new Dictionary<AudioClips, AudioClip>
    {

    };
    private void Awake()
    {
        Instance = this;
        clipList.Add(AudioClips.ChangeGravity, audioClips[0]);
        clipList.Add(AudioClips.DeathSound, audioClips[1]);
        clipList.Add(AudioClips.FinalEncounter, audioClips[2]);
    }
}
