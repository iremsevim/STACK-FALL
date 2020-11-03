using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using DG.Tweening;


public class BallController : MonoBehaviour
{
    public static BallController instance;
    public string CollisionID;
    public Rigidbody rb;
    public bool IsHole = false;
    public BallState ballState;
    public SphereCollider sphereCollider;
    public float explosionforce = 100f;
    public float explosionradius = 1f;
    public float endscale;
    public float startingscale;
    public GameObject fireparticle;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -3f, 3f), rb.velocity.z);

        IsHole = Input.GetMouseButton(0);
        if (ballState == BallState.Idel && IsHole)
        {
            ballState = BallState.Moving;
        }
        else if (ballState == BallState.Moving && !IsHole)
        {
            ballState = BallState.Idel;
        }

        if (IsHole)
        {
            sphereCollider.material.bounciness = 0;
            rb.velocity = new Vector3(rb.velocity.x, -12, rb.velocity.z);
        }
        else
        {
            sphereCollider.material.bounciness = 1;
           
        }
        if(Input.GetMouseButtonDown(0))
        {
            CameraController.instance.IsFollowing = true;
          
            if(UnityEngine.Random.Range(1,10)<5)
            {
                fireparticle.SetActive(true);
            }
            transform.DOScaleY(endscale, 0.2f);
        }
        else if(Input.GetMouseButtonUp(0))
        {

            CameraController.instance.IsFollowing = false;
            fireparticle.SetActive(false);
            rb.velocity = new Vector3(rb.velocity.x, 3, rb.velocity.z);
            transform.DOScaleY(startingscale, 0.2f);
        }




    }
       

    public void OnCollisionStay(Collision collision)
    {
        AudioManager.instance.PLayAudio("collision", transform.position);
        if (collision.transform.name=="dama")
        {

            //collision.transform.GetComponent<MeshCollider>().enabled = false;
            ParticleControl.CreateParticle(CollisionID,collision.contacts[0].point);
            if(IsHole)
            {
                foreach (Transform item in collision.transform.parent)
                {
                    if (!item.GetComponent<Rigidbody>())
                    {
                        Vector3 force = item.transform.up + (item.transform.forward * UnityEngine.Random.Range(-1, 1));
                        force *= explosionforce;

                        Rigidbody x = item.gameObject.AddComponent<Rigidbody>();
                        x.angularVelocity = new Vector3(UnityEngine.Random.Range(-500, 1000), 0, 0);
                        x.AddForce(force);

                        //  item.gameObject.AddComponent<Rigidbody>().AddExplosionForce(explosionforce,item.transform.position, explosionradius);
                        item.GetComponent<MeshCollider>().enabled = false;
                    }
                   
                }
                foreach (Transform item in collision.transform.parent)
                {
                    item.SetParent(null);
                    Destroy(item.gameObject,1.5f);
                }
               
            }
           
        }
        else if(collision.gameObject.tag=="finish")
        {
            Debug.Log("Bitti");
            AudioManager.instance.PLayAudio("confeti", transform.position);
            ParticleControl.CreateParticle("finish", transform.position);
            Destroy(gameObject);
        }
        else
        {

            if(IsHole)
            {
                AudioManager.instance.PLayAudio("finishh", transform.position);
                Camera.main.DOShakePosition(1f);
                Debug.Log("GAME OVER");
                GameManager.instance.GameOver();
                Destroy(gameObject);

            }
          
        }
        

    
    }
   
    public enum BallState
    {
        Idel=0,Moving=1
    }

   
  



}
