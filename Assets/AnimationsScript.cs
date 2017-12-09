using UnityEngine;
using System.Collections;

public class AnimationsScript : MonoBehaviour {

    public Animator o;
    private int intNumber = 5;

	// Use this for initialization
	void Start () {
        StartCoroutine(MyCoroutine());
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator MyCoroutine()
    {
        while (true)
        {
            //This is a coroutine
            int intNumber = Random.Range((int)1, (int)4);
            if (
                intNumber == 1
                )
            {
                o.Play("Attack1");
                yield return new WaitForSeconds(2);
            }
            else if (
                intNumber == 3
                )
            {
                o.Play("Attack2");
                yield return new WaitForSeconds(2);
            }
            else if (
                intNumber == 2
                )
            {
                o.Play("Attack3");
                yield return new WaitForSeconds(4);
            }
            else
            {
                yield return new WaitForSeconds(3);
            }
            intNumber = Random.Range((int)1, (int)4);
        }
    }
}
