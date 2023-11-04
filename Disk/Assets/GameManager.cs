using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject spawnPoint; // 飞碟生成点
    public float spawnInterval = 1f; // 飞碟生成间隔时间

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
        // 开始游戏
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
                // 设置飞碟的生成位置和旋转角度
                disk.transform.position = spawnPoint.transform.position;
                disk.transform.rotation = spawnPoint.transform.rotation;

                // 获取DiskController组件
                DiskController diskController = disk.GetComponent<DiskController>();
                if (diskController != null)
                {
                    // 设置飞碟的颜色
                    diskController.SetColor();

                    // 设置飞碟的运动参数
                    Vector3 randomDirection = Random.onUnitSphere;
                    randomDirection.y = 0; // 保持在水平面上
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
            // 获取DiskController组件
            DiskController diskController = disk.GetComponent<DiskController>();
            if (diskController != null)
            {
                // 调用DiskController的HandleClickEvent方法处理点击事件
                diskController.HandleClickEvent();
            }
        }*/

    public void EndGame(string message)
    {
        // 结束游戏，显示结束消息
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

