using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

    public GameObject gbjPlayer;
    public GameObject gbjEnemy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        	    
        if(gbjPlayer.GetComponent<HitScript>().fHitpoints <= 0)
        {
            Debug.Log("PlayerLost");
            GestureListener.instance.ausSong2.Stop();
            Application.LoadLevel(3);
        }

        if (gbjEnemy.GetComponent<HitScript>().fHitpoints <= 0)
        {
            Debug.Log("EnemyLost");
            GestureListener.instance.ausSong2.Stop();
            Application.LoadLevel(4);

        }
    }
}
