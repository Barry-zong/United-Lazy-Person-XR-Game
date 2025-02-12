using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ScoreSystem : NetworkBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Score Settings")]
    [SerializeField] private string scorePrefix = "Score: ";

    // ʹ�� NetworkVariable ��ͬ������
    private NetworkVariable<int> currentScore = new NetworkVariable<int>(
        0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    // ����ģʽ�����������ű�����
    public static ScoreSystem Instance { get; private set; }

    private void Awake()
    {
        // ���õ���
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // ȷ�������õ�TextMeshPro���
        if (scoreText == null)
        {
            Debug.LogError("ScoreSystem: No TextMeshProUGUI reference set!");
            return;
        }

        // ���ķ����仯�¼�
        currentScore.OnValueChanged += OnScoreChanged;

        // ��ʼ����ʾ
        UpdateScoreDisplay(currentScore.Value);
    }

    // ���ⲿ���õļӷַ���
    public void AddScore(int amount)
    {
        if (!IsServer)
        {
            // ������Ƿ������������������ӷ���
            AddScoreServerRpc(amount);
            return;
        }

        // �ڷ�������ֱ���޸ķ���
        currentScore.Value += amount;
    }

    // ���ⲿ���õ����÷�������
    public void SetScore(int newScore)
    {
        if (!IsServer)
        {
            // ������Ƿ�������������������÷���
            SetScoreServerRpc(newScore);
            return;
        }

        // �ڷ�������ֱ�����÷���
        currentScore.Value = newScore;
    }

    // ��ȡ��ǰ����
    public int GetCurrentScore()
    {
        return currentScore.Value;
    }

    // �������仯ʱ������ʾ
    private void OnScoreChanged(int previousValue, int newValue)
    {
        UpdateScoreDisplay(newValue);
    }

    // ����UI��ʾ
    private void UpdateScoreDisplay(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"{scorePrefix}{score}";
        }
    }

    #region ServerRPCs

    [ServerRpc(RequireOwnership = false)]
    private void AddScoreServerRpc(int amount)
    {
        currentScore.Value += amount;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetScoreServerRpc(int newScore)
    {
        currentScore.Value = newScore;
    }

    #endregion
}