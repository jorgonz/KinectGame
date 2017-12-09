using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {

    //This is the collider to test that shield is in the correct position
    public GameObject gbjShieldCollider;
    public bool isShielding;
    public bool isShieldinCol;
    Collider colShieldCollider;

    public static ShieldScript instance { get; protected set;}

    void Start()
    {
        instance = this;
        colShieldCollider = gbjShieldCollider.GetComponent<BoxCollider>();
        isShielding = false;
        isShieldinCol = false;
    }

    void OnTriggerStay(Collider colOther)
    {
        
        if (colOther == colShieldCollider)
        {
            
            isShieldinCol = true;
        }
    }

    void OnTriggerExit(Collider colOther)
    {
        
        if (colOther == colShieldCollider)
        {
            
            isShieldinCol = false;
            if (isShielding)
            {
                isShielding = false;
                SimpleGestureListener.instance.GestureInfo.GetComponent<GUIText>().text = "";
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
