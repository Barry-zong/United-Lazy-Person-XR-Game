using UnityEngine;

namespace CustomInteraction
{
    public class TemperatureColorChanger : MonoBehaviour
    {
        [Header("Color Settings")]
        public Material targetMaterial;      // Ŀ�����
        public Color coldColor = Color.blue; // ������ɫ (60��F)
        public Color hotColor = Color.red;   // ������ɫ (90��F)

        private void Start()
        {
            if (targetMaterial == null)
            {
                // ���û��ָ�����ʣ����Դӵ�ǰ�����ȡ
                var renderer = GetComponent<Renderer>();
                if (renderer != null)
                {
                    targetMaterial = renderer.material;
                }
                else
                {
                    Debug.LogError("No target material assigned and no renderer found!");
                    return;
                }
            }

            // ��ȡ�¶ȿ������������¼�
            var tempController = GetComponent<TemperatureControllerTransformer>();
            if (tempController != null)
            {
                tempController.OnTemperatureChanged.AddListener(UpdateColor);
            }
            else
            {
                Debug.LogError("TemperatureControllerTransformer not found on the same object!");
            }
        }

        private void UpdateColor(float temperature)
        {
            // ���¶�(90��F - 60��F)ӳ�䵽��ɫ��ֵ(0-1)
            float t = (temperature - 60f) / (90f - 60f);
            targetMaterial.color = Color.Lerp(coldColor, hotColor, t);
        }

        private void OnDestroy()
        {
            // �����¼�����
            var tempController = GetComponent<TemperatureControllerTransformer>();
            if (tempController != null)
            {
                tempController.OnTemperatureChanged.RemoveListener(UpdateColor);
            }
        }
    }
}