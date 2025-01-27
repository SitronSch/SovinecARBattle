using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public  bool animationsPaused = false;
    public  bool audioMuted = false;
    public  bool gridActive = true;
    public Animator audioAnimator;
    public Animator pauseAnimator;
    [SerializeField] private Button gridButton;
    public Animator gridAnimator;
    public Animator captionAnimator;
    [SerializeField] TextMeshProUGUI mainSubtitles;

    [SerializeField] private Animator subtitleAnimator;
    private bool subtitlesShowing = true;

    public void AudioStateChange()
    {
        try
        {
            if (!audioMuted)
            {
                audioMuted = true;
                audioAnimator.SetBool("AudioOff",true);
                AnimDay.lastAnimDay.subtitleToLaunch.currentAudioSource.volume = 0;

            }
            else
            {
                audioMuted = false;
                audioAnimator.SetBool("AudioOff",false);
                AnimDay.lastAnimDay.subtitleToLaunch.currentAudioSource.volume = 1;
            }
        }
        catch { }
        
    }
    public void AnimationStateChange()
    {
        try
        {
            if (!animationsPaused)
            {
                AnimationPause();                
            }
            else
            {
                AnimationRecover();
            }
        }
        catch { }
        
    }
    public void HidePlanes()
    {
        TapToPlace.planesEnabled = !TapToPlace.planesEnabled;
        TapToPlace.ChangePlanes(TapToPlace.planesEnabled,false);

        if(TapToPlace.planesEnabled)
        {
            gridAnimator.SetBool("GridOn",true);
        }
        else
        {
            gridAnimator.SetBool("GridOn",false);
        }
    }
    public void ChangeSubtitles()
    {
        try
        {
            if (subtitlesShowing)
            {
                mainSubtitles.enabled = false;
                subtitleAnimator.SetTrigger("SubtitleOFF");
                subtitlesShowing = false;
                captionAnimator.SetBool("CaptionOff",true);
            }
            else
            {
                mainSubtitles.enabled = true;
                subtitleAnimator.SetTrigger("SubtitleON");
                subtitlesShowing = true;
                captionAnimator.SetBool("CaptionOff",false);
            }
        }
        catch { }    
    }
    public void AnimationRecover()
    {
        animationsPaused = false;

        Time.timeScale = 1;
        pauseAnimator.SetBool("Paused",false);
        try
        {
            AnimDay.lastAnimDay.subtitleToLaunch.currentAudioSource.Play();
        }
        catch { }
    }
    public IEnumerator AnimationPause(float time)
    {
        animationsPaused = true;

        pauseAnimator.SetBool("Paused",true);
        yield return new WaitForSeconds(time);

        Time.timeScale = 0;
        try
        {
            AnimDay.lastAnimDay.subtitleToLaunch.currentAudioSource.Pause();
        }
        catch { }
        
    }
    public void AnimationPause()        //Especially for tooltip, so subtitles are not disabled
    {
        animationsPaused = true;

        pauseAnimator.SetBool("Paused",true);

        Time.timeScale = 0;
        try
        {
            AnimDay.lastAnimDay.subtitleToLaunch.currentAudioSource.Pause();
        }
        catch { }
    }
}
