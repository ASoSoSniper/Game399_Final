using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource spaceMusic;
    [SerializeField] AudioSource stationMusic;
    [SerializeField] float fadeTime = 1.0f;
    bool fading;
    bool inStation;

    private void Update()
    {
        Fade();
    }

    public void FadeBetweenSongs(bool isInStation)
    {
        fading = true;
        inStation = isInStation;
    }

    void Fade()
    {
        if (!fading) return;

        AudioSource source1 = inStation ? stationMusic : spaceMusic;
        AudioSource source2 = inStation ? spaceMusic : stationMusic;

        source2.volume = Mathf.Clamp01(source2.volume - Time.deltaTime * fadeTime);
        if (source2.volume > 0f) return;

        if (source2.isPlaying) source2.Stop();
        if (!source1.isPlaying) source1.Play();

        source1.volume = Mathf.Clamp01(source1.volume + Time.deltaTime * fadeTime);
        if (source1.volume >= 1f)
        {
            fading = false;
        }
    }
}
