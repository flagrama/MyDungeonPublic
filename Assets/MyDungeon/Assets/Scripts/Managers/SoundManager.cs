using UnityEngine;

namespace MyDungeon.Managers
{
    /// <summary>
    /// SoundManager is the class that handles audio
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        /// <summary>
        /// The current instance of SoundManager
        /// </summary>
        public static SoundManager Instance;
        /// <summary>
        /// High pitch shift range for sound pitch randomization
        /// </summary>
        public float HighPitchRange = 1.05f;
        /// <summary>
        /// Low pitch shift range for sound pitch randomization
        /// </summary>
        public float LowPtichRange = .95f;
        /// <summary>
        /// An AudioSource attached to the SoundManager used for music
        /// </summary>
        public AudioSource MusicSource;
        /// <summary>
        /// An AudioSource attached to the SoundManager used for sound effects
        /// </summary>
        public AudioSource SfxSource;

        /// <summary>
        /// Initializes the SoundManager instance
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Plays a provided audio clip once
        /// </summary>
        /// <param name="clip">AudioClip to play once</param>
        public void PlaySingle(AudioClip clip)
        {
            SfxSource.clip = clip;
            SfxSource.Play();
        }

        /// <summary>
        /// Randomizes the pitch of a sound effect
        /// </summary>
        /// <param name="clips">An array of AudioClips to randomly choose to play</param>
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