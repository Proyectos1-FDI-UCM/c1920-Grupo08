using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Sound
{
    defaultSound,
    medKit,
    machineGun,
    sniper,
    bomb,
    mine,
    trap,
    log,
    repairKit,
    playerBulletHit,
    shieldBulletHit,
    groundBulletHit,
    playerJump,
    playerLand,
    playerRun,
    respawn,
    death,
    button,
    shieldOn,
    shieldOff,
    shieldBroken,
    shieldPickup,
}

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    private class SoundFile
    {
        public Sound sound;
        public AudioClip audioClip;        
    }

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

        DontDestroyOnLoad(gameObject);
    }
	#endregion

  public void PlaySoundOnce(Sound sound) 
    {       
        AudioClip audioClip = GetAudioClip(sound);

        if (audioClip != null)
        {
            GameObject soundObj = new GameObject("soundObj");
            soundObj.transform.parent = transform;

            AudioSource audioSource = soundObj.AddComponent<AudioSource>();
            audioSource.PlayOneShot(audioClip);

            Destroy(soundObj, audioClip.length);
        }         
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundFile soundFile in soundFileArrray)
        {
            if(soundFile.sound == sound) 
            {
                return soundFile.audioClip;
            }            
        }
        
        Debug.LogError("Audio Manager: el sonido " + sound + " no ha sido encontrado!");
        return null;
    }
}
