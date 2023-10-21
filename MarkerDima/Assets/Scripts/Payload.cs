using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Payload : MonoBehaviour, IMarkerUPNP
{
    [Header("���� �������� �����:")]
    [SerializeField]
    private float rotationAngleY = 30f;
    [Header("����������� ���� �������� ���������:")]
    [SerializeField]
    private float minRotationAngleX = -10f;
    [Header("������������ ���� �������� ���������:")]
    [SerializeField]
    private float maxRotationAngleX = 30f;

    [Space]

    [Header("�������� �������� ����� �� ��� Y:")]
    [SerializeField]
    private float speedRotationY = 0.1f;
    [Header("�������� �������� ��������� �� ��� X:")]
    [SerializeField]
    private float speedRotationX = 0.1f;

    [Space]

    [Header("������ �������� �������� ��� �������� �� ��� Y:")]
    [SerializeField]
    private Transform playloadRotationY;

    [Header("������ �������� �������� ��� �������� �� ��� X:")]
    [SerializeField]
    private Transform playloadRotationX;

    [Space]

    [Header("��� �������� ��������:")]
    [SerializeField]
    private float weight = 0f;
    [Header("������ �������:")]
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

    void Update()
    {
        if (Input.GetMouseButton(0))
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

    }

    public void DisavleUPNP()
    {

    }
    #endregion

    #region Additional methods

    /// <summary>
    /// ������������ ������� �������� �������� �� ��� Y
    /// </summary>
    private void RotatePlayloadY()
    {
        playloadRotationY.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X") * speedRotationY * Time.deltaTime;
        playloadRotationY.localEulerAngles = Vector3.up * ClampAngle(playloadRotationY.localEulerAngles.y, -rotationAngleY, rotationAngleY);
    }

    /// <summary>
    /// ������������ ������� �������� �������� �� ��� X
    /// </summary>
    private void RotatePlayloadX()
    {
        playloadRotationX.localEulerAngles += -1 * Vector3.right * Input.GetAxis("Mouse Y") * speedRotationX * Time.deltaTime;
        playloadRotationX.localEulerAngles = Vector3.right * ClampAngle(playloadRotationX.localEulerAngles.x, minRotationAngleX, maxRotationAngleX);

    }

    /// <summary>
    /// ������� ����������� ��������� ����
    /// </summary>
    /// <param name="value">������� �������� ����</param>
    /// <param name="min">����������� �������� ����</param>
    /// <param name="max">������������ �������� ����</param>
    /// <returns>��������� ���� �� ���������</returns>
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






