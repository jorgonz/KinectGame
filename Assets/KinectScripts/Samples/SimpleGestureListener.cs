using UnityEngine;
using System.Collections;
using System;

public class SimpleGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;

    //Instance of this Script to Access GUITEXT
    public static SimpleGestureListener instance { get; protected set; }
	
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;

    void Start()
    {
        instance = this;
    }

    public void UserDetected(uint userId, int userIndex)
	{
		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = "";
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
        if (gesture == KinectGestures.Gestures.Click && progress > 0.3f && (int)joint == 7 && ShieldScript.instance.isShieldinCol && !ShieldScript.instance.isShielding)
        {
            string sGestureText = string.Format("Shielding {0:F1}% complete", (progress * 100));
            if (GestureInfo != null)
                GestureInfo.GetComponent<GUIText>().text = sGestureText;

            progressDisplayed = true;
        }

    }

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		if(gesture == KinectGestures.Gestures.Click && (int) joint == 7 && ShieldScript.instance.isShieldinCol && !ShieldScript.instance.isShielding)
        {
            //sGestureText += string.Format(" at ({0:F1}, {1:F1})", screenPos.x, screenPos.y);
            GestureInfo.GetComponent<GUIText>().text = "Shielding!";
            ShieldScript.instance.isShielding = true;
        }
			
		if(GestureInfo != null && gesture != KinectGestures.Gestures.Click)
        {
            GestureInfo.GetComponent<GUIText>().text =  gesture + "";

            //Here is where i can instantiate the slashing particules, if
            // we ever add any...
        }
			
		
		progressDisplayed = false;
		
		return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		if(progressDisplayed)
		{
			// clear the progress info
			if(GestureInfo != null)
				GestureInfo.GetComponent<GUIText>().text = String.Empty;
			
			progressDisplayed = false;
		}
		
		return true;
	}
	
}
