using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitScript : MonoBehaviour {

    public float fHitpoints = 100;
    private float fMaxHitpoints = 100;

    public bool boolInvulnerable = false;

    public bool EnemyInvulnerable = false;

    public Image currentHealthbar;
    public Text ratioText;

    public int iScore = 0;

    public AudioSource ausSwordHit;

    public AudioSource ausGrunt;

    public AudioSource ausShieldBlock;

    public Text textScore;

    public GameObject Sword;

    // Use this for initialization
    private void Start () {
        UpdateHealthbar();
	}
	
	private void UpdateHealthbar()
    {
        float ratio = fHitpoints / fMaxHitpoints;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + "%";
        try
        {
            textScore.text = "Score = " + GameObject.FindGameObjectWithTag("Player").GetComponent<HitScript>().iScore;
        }
        catch
        {

        }
        
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        boolInvulnerable = false;      
    }

    IEnumerator EnemyFlag()
    {
        yield return new WaitForSeconds(1);
        EnemyInvulnerable = false;
        boolInvulnerable = false;
    }

    void OnTriggerEnter (Collider col)
    {

        //If My Sword Collided With The Enemy
        if (this.gameObject.name == "TestSword" && col.gameObject.tag == "Head" && (!EnemyInvulnerable || !boolInvulnerable))
        {
            EnemyInvulnerable = true;
            Debug.Log("MY SWORD COLLIDED WITH HIS HEAD!");
            ausSwordHit.Play();
            Sword.GetComponent<HitScript>().iScore += 8;
            fHitpoints -= 8;
            if (fHitpoints < 0)
            {
                fHitpoints = 0;
            }
            StartCoroutine( EnemyFlag());
            UpdateHealthbar();
        }
        else if (this.gameObject.name == "TestSword" && col.gameObject.tag == "UpperBody" && (!EnemyInvulnerable || !boolInvulnerable))
        {
            EnemyInvulnerable = true;
            Debug.Log("MY SWORD COLLIDED WITH HIS UpperBody");
            ausSwordHit.Play();
            Sword.GetComponent<HitScript>().iScore += 4;
            fHitpoints -= 4;
            if (fHitpoints < 0)
            {
                fHitpoints = 0;
            }

            StartCoroutine(EnemyFlag());
            UpdateHealthbar();
        }
        else if (this.gameObject.name == "TestSword" && col.gameObject.tag == "LowerBody" && (!EnemyInvulnerable || !boolInvulnerable))
        {
            EnemyInvulnerable = true;
            Debug.Log("MY SWORD COLLIDED WITH HIS LowerBody");
            ausSwordHit.Play();
            Sword.GetComponent<HitScript>().iScore += 2;
            fHitpoints -= 2;
            if (fHitpoints < 0)
            {
                fHitpoints = 0;
            }
            StartCoroutine(EnemyFlag());
            UpdateHealthbar();
        }

        if(this.gameObject.name == "Cylinder" && col.gameObject.name == "Shield" && !boolInvulnerable)
        {
            boolInvulnerable = true;
            Debug.Log("Blocked!");
            ausShieldBlock.Play();
            StartCoroutine(Cooldown());
            UpdateHealthbar();
            return;
        }

        //If the enemy Hits me
        if(this.gameObject.name == "Cylinder" && col.gameObject.name == "00_Hip_Center" && !boolInvulnerable)
        {
            boolInvulnerable = true;
            Debug.Log("Enemy hitted in UpperBody");
            ausGrunt.Play();
            Sword.GetComponent<HitScript>().iScore -= 8;
            fHitpoints -= 10;
            if (fHitpoints < 0)
            {
                fHitpoints = 0;
            }
            StartCoroutine(Cooldown());
            UpdateHealthbar();
        }
        else if (this.gameObject.name == "Cylinder" && col.gameObject.name == "03_Head" && !boolInvulnerable)
        {
            boolInvulnerable = true;
            Debug.Log("Enemy hitted in Head");
            ausGrunt.Play();
            Sword.GetComponent<HitScript>().iScore -= 4;
            fHitpoints -= 15;
            if (fHitpoints < 0)
            {
                fHitpoints = 0;
            }
            StartCoroutine(Cooldown());
            UpdateHealthbar();
        }
        else if (this.gameObject.name == "Cylinder" && col.gameObject.name == "17_Knee_Right" && !boolInvulnerable)
        {
            boolInvulnerable = true;
            Debug.Log("Enemy hitted in LowerBody");
            ausGrunt.Play();
            Sword.GetComponent<HitScript>().iScore -= 2;
            fHitpoints -= 5;
            if (fHitpoints < 0)
            {
                fHitpoints = 0;
            }
            StartCoroutine(Cooldown());
            UpdateHealthbar();
        }

    }
}
