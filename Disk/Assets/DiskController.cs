using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DiskController : MonoBehaviour
{
    public float minForce;
    public float maxForce;


    public DiskConfig diskConfig; // 引用DiskConfig资源

    public float randomTorque;

    public float speed;
    public int diskscore;

    private Rigidbody rb;
    private MeshRenderer[] meshRenderers;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // 获取所有子对象上的MeshRenderer组件
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
        // 为每个 Mesh Renderer 组件随机生成颜色
        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (renderer != null)
            {
                renderer.material.color = Random.ColorHSV(); // 随机生成颜色
            }
            else
            {
                Debug.LogError("MeshRenderer component not found on the cylinder!");
            }
        }
    }

    public void SetMovementParameters(Vector3 direction)
    {
        // 设置飞碟的运动参数逻辑
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


