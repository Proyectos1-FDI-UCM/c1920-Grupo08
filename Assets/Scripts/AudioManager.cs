using System.Collections;
using System.Collections.Generic;
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

  public void PlaySoundOnce(Sound sound) 
    {       
        AudioClip audioClip = GetAudioClip(sound);

        if (audioClip != null)
        {
            GameObject soundObj = new GameObject("soundObj");
            soundObj.transform.parent = transform;
            AudioSource audioSource = soundObj.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = audioMixer;
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
