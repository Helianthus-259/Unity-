using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOManager : MonoBehaviour
{
    public GameObject[] ufoPrefabs; // 存放不同UFO预制体的数组
    public int poolSize = 10; // 对象池大小
    private List<GameObject>[] ufoPools; // UFO对象池的数组

    void Start()
    {
        // 初始化对象池
        ufoPools = new List<GameObject>[ufoPrefabs.Length];
        for (int i = 0; i < ufoPrefabs.Length; i++)
        {
            ufoPools[i] = new List<GameObject>();
            for (int j = 0; j < poolSize; j++)
            {
                GameObject ufo = Instantiate(ufoPrefabs[i]); // 创建UFO对象
                ufo.SetActive(false); // 初始时设置为不可见
                ufoPools[i].Add(ufo); // 添加到对象池中
            }
        }
    }

    void Update()
    {
        // 在游戏中需要使用时，从对象池中获取UFO对象
        int ufoType = Random.Range(0, ufoPrefabs.Length); // 随机选择一个UFO类型
        GetUFOFromPool(ufoType);
    }

    void GetUFOFromPool(int type)
    {
        for (int i = 0; i < ufoPools[type].Count; i++)
        {
            if (!ufoPools[type][i].activeInHierarchy)
            {
                // 如果对象未被激活（可见），则将其激活并设置位置等属性
                GameObject ufo = ufoPools[type][i];
                ufo.SetActive(true);

                // 设置UFO的位置
                Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
                ufo.transform.position = spawnPosition;

                // 设置UFO的速度和方向（假设速度是常数，方向是随机的）
                Rigidbody ufoRigidbody = ufo.GetComponent<Rigidbody>();
                if (ufoRigidbody != null)
                {
                    Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
                    ufoRigidbody.velocity = randomDirection * ufo.GetComponent<UFO1Data>().speed;
                }

                // 在一定时间后，将UFO回收到对象池
                StartCoroutine(RecycleUFO(ufo, type));
                break;
            }
        }
    }

    IEnumerator RecycleUFO(GameObject ufo, int type)
    {
        yield return new WaitForSeconds(3f); // 假设UFO存在3秒后被回收
        ufo.SetActive(false); // 设置UFO为不可见
    }
}