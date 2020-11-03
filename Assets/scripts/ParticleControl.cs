using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    public static ParticleControl liveparticle;
    
    public  List<ParticleProfil> particles;

    private void Awake()
    {
        liveparticle = this;
    }
    public static  void CreateParticle(string ID,Vector3 pos)
    {
       ParticleProfil findedparticles =liveparticle.particles.Find(x => x.particleID == ID);
        GameObject createdeparticle = Instantiate(findedparticles.particleobject, pos, Quaternion.identity);
        Destroy(createdeparticle.gameObject, 2f);
    }

   
   [System.Serializable]
    public class ParticleProfil
    {
        public string particleID;
        public GameObject particleobject;
    }
}
