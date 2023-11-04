using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiskPool : MonoBehaviour
{
    public static DiskPool Instance;

    public DiskConfig diskConfigs;
    public GameObject diskPrefab;

    private Queue<GameObject> diskPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < 10; i++) // Initialize 10 disks for each configuration
        {
            GameObject disk = Instantiate(diskPrefab, transform);
            Renderer diskRenderer = disk.GetComponentInChildren<Renderer>();
            disk.SetActive(false);
            diskPool.Enqueue(disk);
        }


        Debug.Log("Disk pool initialized.");
    }

    public GameObject GetDiskFromPool()
    {
        if (diskPool.Count == 0)
        {
            InitializePool();
            Debug.Log("No disks in the pool. Initializing new disks.");
        }

        GameObject disk = diskPool.Dequeue();
        disk.SetActive(true);
        Debug.Log("Disk retrieved from the pool.");
        // 在一定时间后，将UFO回收到对象池
        StartCoroutine(RecycleDisk(disk));
        return disk;
    }

    public void ReturnDiskToPool(GameObject disk)
    {
        disk.SetActive(false);
        diskPool.Enqueue(disk);
        Debug.Log("Disk returned to the pool.");
    }
    IEnumerator RecycleDisk(GameObject disk)
    {
        yield return new WaitForSeconds(5f); // 假设UFO存在5秒后被回收
        ReturnDiskToPool(disk);
        Debug.Log("Time Excceed Disk returned to the pool.");
    }
}
