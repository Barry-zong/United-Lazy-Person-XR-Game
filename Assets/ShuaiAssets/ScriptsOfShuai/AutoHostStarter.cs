using UnityEngine;
using Unity.Netcode;

public class AutoHostStarter : MonoBehaviour
{
    void Start()
    {
        // ������������ɺ��Զ����� host
        NetworkManager.Singleton.StartHost();

        // ��ѡ�������־��ȷ������״̬
        Debug.Log("�������� Host...");
    }
}