using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject spawnPoint; // �ɵ����ɵ�
    public float spawnInterval = 1f; // �ɵ����ɼ��ʱ��

    public Button startButton;
    public Button restartButton;

    public Text winText;

    private bool gameStarted = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        winText.enabled = false;
        winText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        InvokeRepeating("SpawnDisk", 1f, spawnInterval);
    }

    public void StartGame()
    {
        // ��ʼ��Ϸ
        gameStarted = true;
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    private void SpawnDisk()
    {
        if (gameStarted)
        {
            Debug.Log("SpawnDisk ");
            GameObject disk = DiskPool.Instance.GetDiskFromPool();
            if (disk != null)
            {
                // ���÷ɵ�������λ�ú���ת�Ƕ�
                disk.transform.position = spawnPoint.transform.position;
                disk.transform.rotation = spawnPoint.transform.rotation;

                // ��ȡDiskController���
                DiskController diskController = disk.GetComponent<DiskController>();
                if (diskController != null)
                {
                    // ���÷ɵ�����ɫ
                    diskController.SetColor();

                    // ���÷ɵ����˶�����
                    Vector3 randomDirection = Random.onUnitSphere;
                    randomDirection.y = 0; // ������ˮƽ����
                    diskController.SetMovementParameters(randomDirection);
                }
                else
                {
                    Debug.LogError("DiskController component not found on the spawned disk!");
                }
            }
            else
            {
                Debug.LogError("Failed to get disk from the pool!");
            }
        }
    }
    /*
        public void OnDiskClicked(GameObject disk)
        {
            Debug.Log("Disk clicked!");
            // ��ȡDiskController���
            DiskController diskController = disk.GetComponent<DiskController>();
            if (diskController != null)
            {
                // ����DiskController��HandleClickEvent�����������¼�
                diskController.HandleClickEvent();
            }
        }*/

    public void EndGame(string message)
    {
        // ������Ϸ����ʾ������Ϣ
        gameStarted = false;
        //startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Debug.Log(message);
        winText.enabled = true;
        winText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        winText.enabled = false;
        winText.gameObject.SetActive(false);
        StartGame();
    }
}

