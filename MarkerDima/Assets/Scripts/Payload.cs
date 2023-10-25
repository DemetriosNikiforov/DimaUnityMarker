using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Payload : MonoBehaviour, IMarkerUPNP
{
    [Header("Угол поворота башни:")]
    [SerializeField]
    private float rotationAngleY = 30f;
    [Header("Минимальный угол поворота ракетницы:")]
    [SerializeField]
    private float minRotationAngleX = -10f;
    [Header("Максимальный угол поворота ракетницы:")]
    [SerializeField]
    private float maxRotationAngleX = 30f;

    [Space]

    [Header("Скорость включения/выключения башни:")]
    [SerializeField]
    private float speedEnable = 0.5f;
    [Header("Скорость поворота башни по оси Y:")]
    [SerializeField]
    private float speedRotationY = 0.1f;
    [Header("скорость поворота ракетницы по оси X:")]
    [SerializeField]
    private float speedRotationX = 0.1f;

    [Space]

    [Header("Обьект полезной нагрузки:")]
    [SerializeField]
    private GameObject playload;

    [Header("Обьект полезной нагрузки для поворота по сои Y:")]
    [SerializeField]
    private Transform playloadRotationY;

    [Header("Обьект полезной нагрузки для поворота по сои X:")]
    [SerializeField]
    private Transform playloadRotationX;

    [Space]

    [Header("Вес полезной нагрузки:")]
    [SerializeField]
    private float weight = 0f;
    [Header("Физика маркера:")]
    [SerializeField]
    private Rigidbody rb;

    public float speed = 0.1f;
    private const float min = 0.322f;
    private const float max = 1.142f;
    public float time = 0.1f;
    [SerializeField]
    private bool _enable = false;
    private bool _disable = true;


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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _enable)
        {
            DisavleUPNP();
        }
        if (!_enable && Input.GetKeyDown(KeyCode.R))
        {
            EnableUPNP();
        }




        if (Input.GetMouseButton(0) && _enable && !_disable)
        {
            RotatePlayloadY();
            RotatePlayloadX();
        }
    }
    #region Interface methods
    public void Init()
    {
        rb.mass += weight;
    }

    public void EnableUPNP()
    {
        _disable = false;
        StartCoroutine(Up());


    }

    public void DisavleUPNP()
    {
        _disable = true;
        StartCoroutine(Down());


    }

    IEnumerator Down()
    {
        Vector3 minVector = new Vector3(playload.transform.localPosition.x, min, playload.transform.localPosition.z);
        while (playload.transform.localPosition.y > min)
        {
            playload.transform.localPosition = Vector3.MoveTowards(playload.transform.localPosition, minVector, speed);
            yield return new WaitForFixedUpdate();
        }
        playload.transform.localPosition = minVector;
        _enable = false;

    }

    IEnumerator Up()
    {
        Vector3 maxVector = new Vector3(playload.transform.localPosition.x, max, playload.transform.localPosition.z);
        while (playload.transform.localPosition.y < max)
        {
            playload.transform.localPosition = Vector3.MoveTowards(playload.transform.localPosition, maxVector, speed);
            yield return new WaitForFixedUpdate();
        }
        playload.transform.localPosition = maxVector;
        _enable = true;


    }

    #endregion

    #region Additional methods



    /// <summary>
    /// Поворачивает элемент полезной нагрузки по оси Y
    /// </summary>
    private void RotatePlayloadY()
    {
        playloadRotationY.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X") * speedRotationY * Time.deltaTime;
        playloadRotationY.localEulerAngles = Vector3.up * ClampAngle(playloadRotationY.localEulerAngles.y, -rotationAngleY, rotationAngleY);
    }

    /// <summary>
    /// Поворачивает элемент полезной нагрузки по оси X
    /// </summary>
    private void RotatePlayloadX()
    {
        playloadRotationX.localEulerAngles += -1 * Vector3.right * Input.GetAxis("Mouse Y") * speedRotationX * Time.deltaTime;
        playloadRotationX.localEulerAngles = Vector3.right * ClampAngle(playloadRotationX.localEulerAngles.x, minRotationAngleX, maxRotationAngleX);

    }

    /// <summary>
    /// Функция ограничения диапазона угла
    /// </summary>
    /// <param name="value">Тукущее значение угла</param>
    /// <param name="min">Минимальное значение угла</param>
    /// <param name="max">Максимальное значение угла</param>
    /// <returns>Возращает угол из диапазона</returns>
    private float ClampAngle(float value, float min, float max)
    {
        if (value > 180)
        {
            value -= 360;
        }

        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        return value;
    }

    #endregion


}






