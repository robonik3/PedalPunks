using UnityEngine;

[CreateAssetMenu(fileName = "AllAudioClips", menuName = "Scriptable Objects/AllAudioClips")]
public class AllAudioClips : ScriptableObject
{
    public AudioClip[] listOfClips;
}
