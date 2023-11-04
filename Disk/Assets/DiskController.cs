using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DiskController : MonoBehaviour
{
    public float minForce;
    public float maxForce;


    public DiskConfig diskConfig; // ����DiskConfig��Դ

    public float randomTorque;

    public float speed;
    public int diskscore;

    private Rigidbody rb;
    private MeshRenderer[] meshRenderers;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // ��ȡ�����Ӷ����ϵ�MeshRenderer���
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        if (meshRenderers == null || meshRenderers.Length == 0)
        {
            Debug.LogError("MeshRenderer components not found in children!");
        }

        speed = Random.Range(diskConfig.MinSpeed, diskConfig.MaxSpeed);
        diskscore = Random.Range(diskConfig.MinScore, diskConfig.MaxScore);
    }

    private void OnEnable()
    {
        rb.AddForce(Random.Range(minForce, maxForce) * transform.forward);
        rb.AddTorque(Random.onUnitSphere * randomTorque);

        Debug.Log("Disk enabled and added force/torque.");
    }

    /*public void HandleClickEvent()
    {
        ScoreManager.Instance.IncreaseScore();
        DiskPool.Instance.ReturnDiskToPool(gameObject);

        Debug.Log("Disk clicked. Score increased and returned to the pool.");
    }*/
    private void OnMouseDown()
    {
        Debug.Log("Disk clicked!");
        ScoreManager.Instance.IncreaseScore(diskscore);
        DiskPool.Instance.ReturnDiskToPool(gameObject);
    }

    public void SetColor()
    {
        Debug.Log("Setting color " );
        // Ϊÿ�� Mesh Renderer ������������ɫ
        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (renderer != null)
            {
                renderer.material.color = Random.ColorHSV(); // ���������ɫ
            }
            else
            {
                Debug.LogError("MeshRenderer component not found on the cylinder!");
            }
        }
    }

    public void SetMovementParameters(Vector3 direction)
    {
        // ���÷ɵ����˶������߼�
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * speed;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on the disk!");
        }
    }
}


