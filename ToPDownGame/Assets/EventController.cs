using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    public delegate void VideoRewarded(bool Isrewarded);
    public static VideoRewarded videoRewarded;

    public delegate void ChnageButtonRewardRequest(bool b);
    public static ChnageButtonRewardRequest chnageButtonRewardRequest;
}
