using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour, IMarkerUPNP
{
    [Header("Угол поворота башни:")]
    public float RotationAngleY = 30f;
    [Header("Угол поворота ракетгицы:")]
    public float RotationAngleX = 30f;

    [Header("Вес полезной нагрузки:")]
    [SerializeField]
    private float weight = 0f;
    [Header("Физика маркера:")]
    [SerializeField]
    private Rigidbody rb;

    float IMarkerUPNP.Weight
    {
        get { return weight; }
        set
        {
            weight = value;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
        rb.mass += weight;
    }

    public void EnableUPNP()
    {

    }

    public void DisavleUPNP()
    {

    }

    private void RotatePlayloadY()
    {

    }


}
