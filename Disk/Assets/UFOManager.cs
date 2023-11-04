using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOManager : MonoBehaviour
{
    public GameObject[] ufoPrefabs; // ��Ų�ͬUFOԤ���������
    public int poolSize = 10; // ����ش�С
    private List<GameObject>[] ufoPools; // UFO����ص�����

    void Start()
    {
        // ��ʼ�������
        ufoPools = new List<GameObject>[ufoPrefabs.Length];
        for (int i = 0; i < ufoPrefabs.Length; i++)
        {
            ufoPools[i] = new List<GameObject>();
            for (int j = 0; j < poolSize; j++)
            {
                GameObject ufo = Instantiate(ufoPrefabs[i]); // ����UFO����
                ufo.SetActive(false); // ��ʼʱ����Ϊ���ɼ�
                ufoPools[i].Add(ufo); // ��ӵ��������
            }
        }
    }

    void Update()
    {
        // ����Ϸ����Ҫʹ��ʱ���Ӷ�����л�ȡUFO����
        int ufoType = Random.Range(0, ufoPrefabs.Length); // ���ѡ��һ��UFO����
        GetUFOFromPool(ufoType);
    }

    void GetUFOFromPool(int type)
    {
        for (int i = 0; i < ufoPools[type].Count; i++)
        {
            if (!ufoPools[type][i].activeInHierarchy)
            {
                // �������δ������ɼ��������伤�����λ�õ�����
                GameObject ufo = ufoPools[type][i];
                ufo.SetActive(true);

                // ����UFO��λ��
                Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
                ufo.transform.position = spawnPosition;

                // ����UFO���ٶȺͷ��򣨼����ٶ��ǳ���������������ģ�
                Rigidbody ufoRigidbody = ufo.GetComponent<Rigidbody>();
                if (ufoRigidbody != null)
                {
                    Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
                    ufoRigidbody.velocity = randomDirection * ufo.GetComponent<UFO1Data>().speed;
                }

                // ��һ��ʱ��󣬽�UFO���յ������
                StartCoroutine(RecycleUFO(ufo, type));
                break;
            }
        }
    }

    IEnumerator RecycleUFO(GameObject ufo, int type)
    {
        yield return new WaitForSeconds(3f); // ����UFO����3��󱻻���
        ufo.SetActive(false); // ����UFOΪ���ɼ�
    }
}