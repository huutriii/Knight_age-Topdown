using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> clips = new();
    List<AudioSource> pools = new();
    private static AudioController _instance;
    public static AudioController Instance => _instance;
    [SerializeField]
    bool isSound = true;
    //  isSFX = true;
    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        if (Instance.gameObject.GetInstanceID() != this.GetInstanceID())
            Destroy(this.gameObject);
    }

    private void Start()
    {
        isSound = PlayerPrefs.GetInt("SOUND", 1) == 1 ? true : false;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlaySound("Fire");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlaySound("Thunder");
        }
    }

    public void PlaySound(string clipName)
    {
        if (isSound == false)
            return;

        AudioClip clip = null;
        foreach (AudioClip c in clips)
        {
            if (c.name.Equals(clipName))
            {
                clip = c;
                break;
            }
        }

        if (clip == null)
        {
            Debug.LogError("SOUND is not exist !");
            return;
        }
        AudioSource source = GetSource();
        source.clip = clip;
        source.gameObject.SetActive(true);
    }


    AudioSource GetSource()
    {
        foreach (AudioSource c in pools)
        {
            if (!c.gameObject.activeSelf)
            {
                return c;
            }
        }
        AudioSource source = Instantiate(audioSource, transform.position, Quaternion.identity);
        pools.Add(source);
        source.gameObject.SetActive(false);
        return source;
    }
    public void SetSound(bool isSound)
    {
        this.isSound = isSound;
        PlayerPrefs.SetInt("SOUND", isSound ? 1 : 0);
        if (!this.isSound)
        {
            foreach (AudioSource c in pools)
            {
                c.Stop();
            }
        }
    }
}
