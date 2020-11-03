using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> allprefabs;
    private Vector3 spawnpointposition;
    public Transform spawnpoint;
    public Vector3 ypositionspawnpoint;
    public int allcreaterprepabscount;
    public float rotatespeed = 5f;
    public GameObject ballprefab;
    public float explosionforce = 1000f;
   



    public void Awake()
    {
        instance = this;
        spawnpointposition = spawnpoint.transform.position;
        DiscCreate();

    }
    private void Update()
    {
        StartCoroutine(RotateDiscs());
    }

    public IEnumerator RotateDiscs()
    {
        yield return new WaitForSeconds(0.2f);
        spawnpoint.Rotate(0, 30 * Time.deltaTime * rotatespeed, 0);
    }

    public void DiscCreate()
    {


        for (int i = 0; i < allcreaterprepabscount ; i++)
        {
            spawnpointposition += ypositionspawnpoint;
            int randomprefab = Random.Range(0, allprefabs.Count);
            GameObject createprefab = Instantiate(allprefabs[randomprefab].gameObject, spawnpointposition, Quaternion.identity);
            createprefab.transform.SetParent(spawnpoint);


        }

        spawnpointposition += (ypositionspawnpoint*3);
        GameObject createdball = Instantiate(ballprefab,spawnpointposition, Quaternion.identity);
        createdball.transform.position = new Vector3(0.05f, createdball.transform.position.y, -1.042f);
       
    }
    public void GameOver()
    {

        foreach (var item in DiscSub.discs)
        {
            ParticleControl.CreateParticle("exp", item.transform.position);
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
    }
}
  

