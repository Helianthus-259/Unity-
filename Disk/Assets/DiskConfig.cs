using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Disk Config", menuName = "ScriptableObjects/DiskConfig")]
public class DiskConfig : ScriptableObject
{
    public float MinSpeed;
    public float MaxSpeed;
    public int MinScore;
    public int MaxScore;
}
