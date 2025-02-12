using UnityEngine;
using System.Collections.Generic;

public class FoodDetector : MonoBehaviour
{
    // ��HashSet����¼�Ѿ������������壬ȷ��ÿ������ֻ����һ��
    private HashSet<GameObject> detectedFood = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        // ������������Ƿ����"Food"��ǩ����֮ǰû�б�����
        if (other.CompareTag("Food") && !detectedFood.Contains(other.gameObject))
        {
            // ��������ӵ��Ѽ�⼯����
            detectedFood.Add(other.gameObject);
            ScoreSystem.Instance.AddScore(1);
            // �����־
            Debug.Log("trash point added");
        }
    }
}