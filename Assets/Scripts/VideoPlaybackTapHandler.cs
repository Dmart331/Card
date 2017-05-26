/*============================================================================== 
 * Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;
using Vuforia;


/// <summary>
/// Specialized tap handler class for video playback.
/// </summary>
/// 
public class VideoPlaybackTapHandler : TapHandler
{
    #region PRIVATE_MEMBERS
    private PlayVideo mPlayVideo = null;
    private bool paused = false;
    private float currentPosition; 
    #endregion //PRIVATE_MEMBERS


    #region PROTECTED_METHODS
    protected override void OnSingleTapConfirmed()
    {
        base.OnSingleTapConfirmed();

        if (mPlayVideo == null)
        {
            mPlayVideo = FindObjectOfType<PlayVideo>();
        }

        if (mPlayVideo)
        {
            mPlayVideo.TryPickingVideo();
        }
    }

    /// <summary>
    /// Overriding base class implementation for double tap handling
    /// </summary>
    protected override void OnDoubleTap()
    {
        base.OnDoubleTap();

        // Get currently playing video, if any,
        // and pause it before the UI menu is opened.
        // This is needed in Unity 5 in order to show the UI menu

        if (paused)
        {
            VideoPlaybackBehaviour video = GetPlayingVideo();

            video.VideoPlayer.Play(false, currentPosition);
            paused = false;
        }
        else
        {

        }

        
    }
    #endregion //PROTECTED_METHODS

    protected override void OnSingleTap()
    {
        base.OnSingleTap();

        VideoPlaybackBehaviour video = GetPlayingVideo();
        if (video != null && video.VideoPlayer.IsPlayableOnTexture())
        {
            currentPosition = video.VideoPlayer.GetCurrentPosition();
            video.VideoPlayer.Pause();
            paused = true;
        }


    }
    #region PRIVATE_METHODS
    /// <summary>
    /// Returns the currently active (playing) video, if any
    /// </summary>
    private VideoPlaybackBehaviour GetPlayingVideo()
    {
        VideoPlaybackBehaviour[] videos = (VideoPlaybackBehaviour[])
                FindObjectsOfType(typeof(VideoPlaybackBehaviour));
        
        foreach (VideoPlaybackBehaviour video in videos)
        {
            if (video.CurrentState == VideoPlayerHelper.MediaState.PLAYING || video.CurrentState == VideoPlayerHelper.MediaState.PAUSED)
            { 
                return video;
            }
        }
        return null;
    }
    #endregion //PRIVATE_METHODS
}
