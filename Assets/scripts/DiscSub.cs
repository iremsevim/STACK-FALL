using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscSub : MonoBehaviour
{
    public static List<DiscSub> discs = new List<DiscSub>();
    private void Awake()
    {
        discs.Add(this);
    }
    public void OnDestroy()
    {
        discs.Remove(this);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
