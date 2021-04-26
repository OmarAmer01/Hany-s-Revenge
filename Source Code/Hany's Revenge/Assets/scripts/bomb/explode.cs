using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class explode : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip boomSnd;
  //  public GameObject explosionEffect;
    public float waitTimeforEffect = 1;
    public static bool hasExploded = false;
    bool isPlaying = false;
    float timeSincePlay = 0;
    float explosionTime = 0;
    private float fixedDeltaTime;
    public float blastRadius = 8;
    public float explosionForce = 1000;
    public Canvas loseUI;
    bool dontExplode = false;
    public Text loseTxt;
    //public GameObject camera;

    public void explodeBomb()
    {
        loseUI.enabled = true;
        loseTxt.text = "The Code Was " + defuse.bombCodeActual;
        //camera.GetComponent<AudioListener>().enabled = false;
        timeSincePlay = Time.time;
        isPlaying = true;
        audioSrc.PlayOneShot(boomSnd);
        //   Time.timeScale = 0.25f;
        if (Time.unscaledTime - timeSincePlay >= waitTimeforEffect && !hasExploded)
        {
            loseUI.enabled = true;
            loseTxt.text = "The Code Was " + defuse.bombCodeActual;
            if (Time.unscaledTime - timeSincePlay >= boomSnd.length && Time.time - explosionTime > 1)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Cursor.visible = true;
                timeSincePlay = 0;
                riddleMgr.paperNumber = 0;
                loseTxt.text = "The code was " + defuse.bombCodeActual;
                loseUI.enabled = true;
                return;
                //SceneManager.LoadScene(0);
            }

            //Time.timeScale = 0.25f;
            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
            foreach (Collider col in colliders)
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, rb.transform.position, blastRadius);
                }
            }
            if (!dontExplode)
                //Instantiate(explosionEffect, transform.position, transform.rotation);
            dontExplode = true;
            hasExploded = true;
            explosionTime = Time.time;
            //Destroy(gameObject);

        }


    }
    private void Start()
    {
        loseUI.enabled = false;
    }

    void Update()
    {

        if (bombTimer.currTime <= 0 && !isPlaying)
        {
            loseUI.enabled = true;
            loseTxt.text = "The Code Was " + defuse.bombCodeActual;
            timeSincePlay = Time.time;
            isPlaying = true;
            audioSrc.PlayOneShot(boomSnd);
            Time.timeScale = 0.25f;


        }
        else if (isPlaying)
        {
            if (Time.unscaledTime - timeSincePlay >= waitTimeforEffect && !hasExploded)
            {
                //Time.timeScale = 0.25f;
                Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
                foreach (Collider col in colliders)
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(explosionForce, rb.transform.position, blastRadius);

                    }
                }
                if (!dontExplode)
                  //  Instantiate(explosionEffect, transform.position, transform.rotation);
                dontExplode = true;
                hasExploded = true;
                explosionTime = Time.time;

            }
            if (Time.unscaledTime - timeSincePlay >= boomSnd.length && Time.time - explosionTime > 1)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                riddleMgr.paperNumber = 0;
                timeSincePlay = 0;
                // SceneManager.LoadScene(0);
                loseTxt.text = "The Code Was " + defuse.bombCodeActual;
                loseUI.enabled = true;
                return;

                //Destroy(gameObject);
            }
        }
    }
}
