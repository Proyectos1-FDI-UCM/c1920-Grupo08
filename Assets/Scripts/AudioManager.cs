using UnityEngine;
using UnityEngine.Audio;

public enum Sound
{
    defaultSound,
    medKit,
    machineGun,
    sniper,    
    mine,
    bearTrap,    
    repairKit,
    playerBulletHit,
    shieldBulletHit,
    groundBulletHit,
    playerJump,    
    button,    
    shieldPickup,
    radio,
    tankShot,
    door,
    key,
}

// Este script maneja los efectos de audio de un solo uso utilizando la clase SoundFile y los enum de tipo Sound es capaz de reproducir cualquier sonido que este en su base de datos.
public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    private class SoundFile
    {
        public Sound sound;
        public AudioClip audioClip;
    }

    [SerializeField] AudioMixerGroup audioMixer;
    [SerializeField] private SoundFile[] soundFileArrray;

    #region Singleton
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    // Instancia un objeto vacio para reproducir un sonido, cuando este ha terminado de reproducirlo lo destruye.
    public void PlaySoundOnce(Sound sound, float volume = 1f)
    {
        AudioClip audioClip = GetAudioClip(sound);

        if (audioClip != null)
        {
            GameObject soundObj = new GameObject("soundObj");
            soundObj.transform.parent = transform;
            AudioSource audioSource = soundObj.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = audioMixer;
            audioSource.PlayOneShot(audioClip, volume);
            Destroy(soundObj, audioClip.length);
        }
    }

    // Busca el archivo de audio en la base de datos.
    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundFile soundFile in soundFileArrray)
        {
            if (soundFile.sound == sound)
            {
                return soundFile.audioClip;
            }
        }

        Debug.LogError("Audio Manager: el sonido " + sound + " no ha sido encontrado!");
        return null;
    }
}
