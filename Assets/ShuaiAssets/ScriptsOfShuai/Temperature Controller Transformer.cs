using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace CustomInteraction
{
    public class TemperatureControllerTransformer : MonoBehaviour
    {
        [Header("Display Settings")]
        [SerializeField]
        private TextMeshProUGUI temperatureText;    // �¶���ʾ�ı�
       

        [Header("Temperature Settings")]
        [SerializeField]
        private float _maxTemperature = 90f;        // ����¶ȣ����϶ȣ�
        [SerializeField]
        private float _minTemperature = 60f;        // ����¶ȣ����϶ȣ�

        // �¶ȸı�ʱ���¼�
        public UnityEvent<float> OnTemperatureChanged;

        private float _initialZRotation;            // ��ʼZ����תֵ
        private float _currentTemperature;          // ��ǰ�¶�

        private void Start()
        {
            // ��ʼ���¼�
            if (OnTemperatureChanged == null)
            {
                OnTemperatureChanged = new UnityEvent<float>();
            }

            // ��¼��ʼ��תֵ
            _initialZRotation = transform.localEulerAngles.z;

            // ȷ�����¶���ʾ�ı����
            if (temperatureText == null)
            {
                Debug.LogError("Temperature Text component is not assigned!");
            }

            // ��ʼ���¶���ʾ
            UpdateTemperatureFromRotation();
        }

        private void Update()
        {
            UpdateTemperatureFromRotation();
        }

        // ������ת�����¶�
        private void UpdateTemperatureFromRotation()
        {
            // ��ȡ��ǰZ����ת
            float currentZRotation = transform.localEulerAngles.z;

            // ��������ڳ�ʼλ�õ���ת��ֵ��ȷ����0-90�ȷ�Χ�ڣ�
            float rotationDelta = Mathf.DeltaAngle(currentZRotation, _initialZRotation);
            float normalizedRotation = Mathf.Clamp(rotationDelta, 0f, 90f);

            // ��0-90�ȵ���תӳ�䵽90-60���϶ȣ�ע���Ƿ���ӳ�䣩
            float newTemperature = Mathf.Lerp(_maxTemperature, _minTemperature, normalizedRotation / 90f);
           

            // ����¶ȷ����仯��������ʾ�������¼�
            if (Mathf.Abs(newTemperature - _currentTemperature) > 0.01f)
            {
                _currentTemperature = newTemperature;
                UpdateTemperatureDisplay();
                OnTemperatureChanged.Invoke(_currentTemperature);
            }
        }

        // �����¶���ʾ
        private void UpdateTemperatureDisplay()
        {
            if (temperatureText != null)
            {
                temperatureText.text = $"{_currentTemperature:F1}��F";
            }
        }

        // ��ȡ��ǰ�¶�
        public float GetCurrentTemperature()
        {
            return _currentTemperature;
        }

        // ��ȡ���϶�ֵ
        public float GetTemperatureCelsius()
        {
            return (_currentTemperature - 32f) * 5f / 9f;
        }
    }
}