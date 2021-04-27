using UnityEngine;

[CreateAssetMenu(menuName = "Audio/SFXEvent")]
public class SFXEvent : ScriptableObject {

    public AudioClip[] sfx;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0, 256)]
    public int priority = 128;

    [Range(0f, 2f)]
    public float pitch = 1f;

    public void Play() {
        if (sfx.Length == 0) return;

        GameObject AudioObject = new GameObject();
        AudioObject.name = "SFX Event: " + name;

        AudioSource source = AudioObject.AddComponent<AudioSource>();
        source.clip = sfx[Random.Range(0, sfx.Length)];
        source.volume = volume;
        source.priority = priority;
        source.pitch = pitch;
        source.Play();

        DestroyAfterSeconds destruction = AudioObject.AddComponent<DestroyAfterSeconds>();
        destruction.Duration = (Mathf.Abs(pitch) < .01f) ? 0 : source.clip.length / Mathf.Abs(pitch);
    }

    public void Play(AudioSource source) {
        if (sfx.Length == 0) return;

        source.clip = sfx[Random.Range(0, sfx.Length)];
        source.volume = volume;
        source.priority = priority;
        source.pitch = pitch;
        source.Play();
    }
}