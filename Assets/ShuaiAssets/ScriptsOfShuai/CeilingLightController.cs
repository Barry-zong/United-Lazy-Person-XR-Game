using UnityEngine;

public class CeilingLightController : MonoBehaviour
{
    // ������Ϸ��������
    public GameObject ceilingLight;

    // ��¼��ʼ��תֵ
    private Quaternion initialRotation;

    // ��ȴʱ����ر���
    private float cooldownTime = 1f;
    private float cooldownTimer = 0f;

    // ��¼��һ�μ��ʱ����תֵ
    private float lastCheckedRotation;

    // ׷���Ƿ��ǵ�һ���л�״̬
    private bool isFirstSwitch = true;

    void Start()
    {
        // ȷ���Ѿ�ָ����������
        if (ceilingLight == null)
        {
            Debug.LogError("����Inspector��ָ���������壡");
            enabled = false;
            return;
        }

        // ��¼��ʼ��תֵ
        initialRotation = transform.rotation;
        lastCheckedRotation = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        // ������ȴ��ʱ��
        cooldownTimer -= Time.deltaTime;

        // ���������ȴ�У�ֱ�ӷ���
        if (cooldownTimer > 0)
        {
            return;
        }

        // ��ȡ��ǰZ����תֵ
        float currentRotationZ = transform.rotation.eulerAngles.z;

        // �������ϴμ��ʱ����ת��ֵ
        float rotationDelta = Mathf.Abs(currentRotationZ - lastCheckedRotation);

        // �����ת��ֵ����50��
        if (rotationDelta > 50f)
        {
            // �л�����״̬
            ceilingLight.SetActive(!ceilingLight.activeSelf);

            // ����Ƿ��ǵ�һ���л�
            if (isFirstSwitch)
            {
                Debug.Log("light off add point");
               
                    ScoreSystem.Instance.AddScore(1);
                   
                isFirstSwitch = false;
            }
            else
            {
                Debug.Log($"��⵽��ת�仯{rotationDelta}�ȣ��л�����״̬Ϊ: {ceilingLight.activeSelf}");
            }

            // ������ȴʱ��
            cooldownTimer = cooldownTime;

            // �����ϴμ�����תֵ
            lastCheckedRotation = currentRotationZ;
        }
    }
}