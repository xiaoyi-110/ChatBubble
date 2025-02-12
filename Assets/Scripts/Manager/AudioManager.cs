
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    //public static AudioManager Instance;
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    [SerializeField] private AudioSource m_IsPlayingBGM;
    //[SerializeField] public AudioSource sceneChangeSound;


    // [SerializeField][Range(-80f, 20f)] private float m_MasterVolume = -10f;
    // [SerializeField][Range(-80f, 20f)] private float m_BGMVolume = -10f;
    // [SerializeField][Range(-80f, 20f)] private float m_EffectVolume = -10f;

    private void Awake() {
        //Instance = this;
    }
    private void Start() {
        m_IsPlayingBGM = GetComponent<AudioSource>();
        PlayBGM("BGM");
    }

    public AudioClip GetAudioClip(string name)
    {
        if (audioClips.ContainsKey(name))
        {
            return audioClips[name];
        }
        else
        {
            AudioClip clip = Resources.Load<AudioClip>(ResoucesUtility.GetAudioClipAsset(name));
            //Debug.Log(clip);
            return clip;
        }
        
    }

    public void PlayBGM(string name) {
        if(m_IsPlayingBGM!=null)
        {
            m_IsPlayingBGM.Stop();
        }
        m_IsPlayingBGM.clip = GetAudioClip(name);
        m_IsPlayingBGM.Play();
    }

    public void ResetBGM()
    {
        if(m_IsPlayingBGM != null)
        {
            m_IsPlayingBGM.time = 0;
        }
    }

    public void PlayEffect(string name)
    {
        AudioSource.PlayClipAtPoint(GetAudioClip(name), Camera.main.transform.position);
    }

    public void StopBGM()
    {
        if (m_IsPlayingBGM != null)
        {
            m_IsPlayingBGM.Stop();
        }
    }

    /*public void PlaySceneChangeSound()
    {
        if (sceneChangeSound != null)
        {
            sceneChangeSound.Play();
        }
        else
        {
            Debug.LogError("Scene change sound not assigned in AudioManager!");
        }
    }*/
}