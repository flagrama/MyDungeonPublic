using UnityEngine;

namespace MyDungeon.Demo
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        public float HighPitchRange = 1.05f;
        public float LowPtichRange = .95f;
        public AudioSource MusicSource;
        public AudioSource SfxSource;

        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void PlaySingle(AudioClip clip)
        {
            SfxSource.clip = clip;
            SfxSource.Play();
        }

        public void RandomizeSfx(params AudioClip[] clips)
        {
            int randomIndex = Random.Range(0, clips.Length);
            float randomPitch = Random.Range(LowPtichRange, HighPitchRange);

            SfxSource.pitch = randomPitch;
            SfxSource.clip = clips[randomIndex];
            SfxSource.Play();
        }
    }
}