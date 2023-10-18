using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour, IMarkerUPNP
{
    private float weight = 0f;
    //public float Weight { get; protected set; }
    float IMarkerUPNP.Weight
    {
        get { return weight; }
        set
        {
            weight = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {

    }

    public void EnableUPNP()
    {

    }

    public void DisavleUPNP()
    {

    }


}
