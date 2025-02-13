using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material targetMaterial;
    private bool Firstadded = true;
    public Color triggerColor = Color.red;
    public Color triggerColorS = Color.red;

    void Start()
    {
        // ȷ������������
        if (targetMaterial == null)
        {
            Debug.LogWarning("û������Ŀ�����!");
        }
        targetMaterial.SetColor("_EmissionColor", triggerColorS);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetMaterial != null)
        {
            // �޸Ĳ��ʵ��Է�����ɫ
            targetMaterial.SetColor("_EmissionColor", triggerColor);
            // ȷ���Է��������õ�
            targetMaterial.EnableKeyword("_EMISSION");

            if (Firstadded)
            {
                ScoreSystem.Instance.AddScore(1);
                Firstadded = false;
            }
        }
    }
}