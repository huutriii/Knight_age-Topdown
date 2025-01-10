using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChild : MonoBehaviour
{
    AudioSource m_AudioSource;
    List<AudioSource> sources = new();
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        StartCoroutine(DestroyAfterPlay());
    }

    IEnumerator DestroyAfterPlay()
    {
        yield return new WaitUntil(() => !m_AudioSource.isPlaying);
        gameObject.SetActive(false);
    }
}
