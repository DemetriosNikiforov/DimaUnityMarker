using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Payload : MonoBehaviour, IMarkerUPNP
{
    [Header("Угол поворота башни:")]
    public float RotationAngleY = 30f;
    [Header("Угол поворота ракетгицы:")]
    public float RotationAngleX = 30f;

    public float SpeedRotationY = 0.1f;

    public float SpeedRotationX = 0.1f;

    [Header("Обьект полезной нагрузки для поворота по сои Y:")]
    [SerializeField]
    private Transform objectYAxisRotation;

    [Header("Обьект полезной нагрузки для поворота по сои X:")]
    [SerializeField]
    private Transform objectXAxisRotation;



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
    void FixedUpdate()
    {


        float mouseX = Input.GetAxis("Mouse X");
        if (Input.GetMouseButton(0))
        {
            //float angleY = ClampAngle(objectYAxisRotation.eulerAngles.y + , -30, 30);
            //Debug.Log(angleY);

            //objectYAxisRotation.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * SpeedRotationY;

            objectYAxisRotation.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * SpeedRotationY;
            objectYAxisRotation.eulerAngles = Vector3.up * ClampAngle(objectYAxisRotation.eulerAngles.y, 330, 30);
            Debug.Log(objectYAxisRotation.eulerAngles.y);



            //RotatePlayloadY();
            //RotatePlayloadX();
        }

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

        Vector3 targetPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);


        targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);




        float mouseX = Input.GetAxis("Mouse X");
        if (mouseX > 0)
        {
            Quaternion rotation = Quaternion.Euler(0, RotationAngleY, 0);


            objectYAxisRotation.localRotation = Quaternion.Slerp(objectYAxisRotation.localRotation, rotation, SpeedRotationY);
        }
        else if (mouseX < 0)
        {
            Quaternion rotation = Quaternion.Euler(0, -RotationAngleY, 0);


            objectYAxisRotation.localRotation = Quaternion.Slerp(objectYAxisRotation.localRotation, rotation, SpeedRotationY);
        }



    }

    private void RotatePlayloadX()
    {
        Vector3 targetPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);


        targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);




        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseY > 0)
        {
            Quaternion rotation = Quaternion.Euler(-RotationAngleX, objectXAxisRotation.localRotation.y, objectXAxisRotation.localRotation.z);


            objectXAxisRotation.localRotation = Quaternion.Slerp(objectYAxisRotation.localRotation, rotation, SpeedRotationX);
        }
        else if (mouseY < 0)
        {
            Quaternion rotation = Quaternion.Euler(RotationAngleX, objectXAxisRotation.localRotation.y, objectXAxisRotation.localRotation.z);


            objectXAxisRotation.localRotation = Quaternion.Slerp(objectYAxisRotation.localRotation, rotation, SpeedRotationX);
        }
    }

    private float ClampAngle(float value, float min, float max)
    {

        if ((min <= value && value<=360) || (0 < value && value <= max))
        {
            return value;

        }
        else if(value<min)
        {
            return min;
        }else
        {
            return max;
        }



    }

}






