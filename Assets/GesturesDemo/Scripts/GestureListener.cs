using UnityEngine;
using System.Collections;
using System;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;

    //Instance of this Script to Access GUITEXT
    public static GestureListener instance { get; protected set; }

    // private bool to track if progress message has been displayed
    private bool progressDisplayed;

    private bool swipeLeft;
	private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;

    //SoundSources

        //MenuAudio
    public AudioSource ausPs4;
    public AudioSource ausLoad;
    public AudioSource ausSong1;

        //Game Audio
    public AudioSource ausSong2;
    public AudioSource ausSlashLeft;
    public AudioSource ausSlashDown;
    public AudioSource ausShielding;

	void Start()
    {
        instance = this;
    }

	public bool IsSwipeLeft()
	{
		if(swipeLeft)
		{
			swipeLeft = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeRight()
	{
		if(swipeRight)
		{
			swipeRight = false;
			return true;
		}
		
		return false;
	}

    public void UserDetected(uint userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeDown);
        manager.DetectGesture(userId, KinectGestures.Gestures.Click);

		if(GestureInfo != null)
		{
            if(Application.loadedLevel == 1)
            {
                GestureInfo.GetComponent<GUIText>().text = "Swipe left or right to change the slides.";
            }
            else if (Application.loadedLevel == 2)
            {
                GestureInfo.GetComponent<GUIText>().text = "";
            }

			
		}
	}
	
	public void UserLost(uint userId, int userIndex)
	{
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = string.Empty;
		}
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{

        if (Application.loadedLevel == 1)
        {
            //Menu
            if (gesture == KinectGestures.Gestures.Click && progress > 0.3f)
            {
                string sGestureText = string.Format("Loading {0:F1}% complete", (progress * 100));
                GestureInfo.GetComponent<GUIText>().text = sGestureText;
            }
        }
        else if (Application.loadedLevel == 2)
        {
            //Game
            if (gesture == KinectGestures.Gestures.Click && progress > 0.3f && (int)joint == 7 && ShieldScript.instance.isShieldinCol && !ShieldScript.instance.isShielding)
            {
                string sGestureText = string.Format("Shielding {0:F1}% complete", (progress * 100));
                if (GestureInfo != null)
                    GestureInfo.GetComponent<GUIText>().text = sGestureText;

                progressDisplayed = true;
            }
        }
        else if (Application.loadedLevel == 3 || Application.loadedLevel == 4)
        {
            //Menu
            if (gesture == KinectGestures.Gestures.Click && progress > 0.3f)
            {
                string sGestureText = string.Format("Loading {0:F1}% complete", (progress * 100));
                GestureInfo.GetComponent<GUIText>().text = sGestureText;
            }
        }

    }

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
        if(Application.loadedLevel == 1)
        {
            string sGestureText = gesture + " detected";
            if (GestureInfo != null && gesture != KinectGestures.Gestures.Click && gesture != KinectGestures.Gestures.SwipeDown)
            {
                GestureInfo.GetComponent<GUIText>().text = sGestureText;
            }

            if (gesture == KinectGestures.Gestures.SwipeLeft)
            {
                swipeLeft = true;
                ausPs4.Play();
            }
            else if (gesture == KinectGestures.Gestures.SwipeRight)
            {
                swipeRight = true;
                ausPs4.Play();
            } 
            else if(gesture == KinectGestures.Gestures.Click)
            {
                ausSong1.Stop();
                ausSong2.Play();
                ausLoad.Play();
                GestureInfo.GetComponent<GUIText>().text = "LOADING LEVEL";
                Application.LoadLevel(2);
            }
        }
        else if (Application.loadedLevel == 2)
        {
            if (gesture == KinectGestures.Gestures.Click && (int)joint == 7 && ShieldScript.instance.isShieldinCol && !ShieldScript.instance.isShielding)
            {
                //sGestureText += string.Format(" at ({0:F1}, {1:F1})", screenPos.x, screenPos.y);
                GestureInfo.GetComponent<GUIText>().text = "Shielding!";
                ShieldScript.instance.isShielding = true;
                ausShielding.Play();
            }

            if (GestureInfo != null && gesture != KinectGestures.Gestures.Click)
            {
                GestureInfo.GetComponent<GUIText>().text = gesture + "";

                //Here is where i can instantiate the slashing particules, if
                // we ever add any...

                if(gesture == KinectGestures.Gestures.SwipeLeft && (int) joint == 11)
                {
                    ausSlashLeft.Play();
                }
                if(gesture == KinectGestures.Gestures.SwipeDown && (int) joint == 11)
                {
                    ausSlashDown.Play();
                }
            }
        }
        else if(Application.loadedLevel == 3 || Application.loadedLevel == 4)
        {
            if (gesture == KinectGestures.Gestures.Click)
            {
                ausSong1.Play();
                Application.LoadLevel(1);
            }
        }

        progressDisplayed = false;
		return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
        if(Application.loadedLevel == 1)
        {
            if (gesture == KinectGestures.Gestures.Click)
            {
                GestureInfo.GetComponent<GUIText>().text = "";
            }
        }
        else if(Application.loadedLevel == 2)
        {
            if (progressDisplayed)
            {
                // clear the progress info
                if (GestureInfo != null)
                    GestureInfo.GetComponent<GUIText>().text = String.Empty;

                progressDisplayed = false;
            }
        }
        else if (Application.loadedLevel == 3 || Application.loadedLevel == 4)
        {
            if (GestureInfo != null)
                GestureInfo.GetComponent<GUIText>().text = String.Empty;
        }


        return true;
	}
	
}
