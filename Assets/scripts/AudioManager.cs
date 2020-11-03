using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<AudioProfil> audios;
    public AudioSource auidiosource;

    private void Awake()
    {
        instance = this;
    }
    public void PLayAudio(string ID,Vector3 pos)
    {
      AudioProfil findedauido= audios.Find(x => x.ID == ID);
        auidiosource.PlayOneShot(findedauido.clip);

    }
   



    [System.Serializable]
    public class AudioProfil
    {
        public string ID;
        public AudioClip clip;
    }
}
