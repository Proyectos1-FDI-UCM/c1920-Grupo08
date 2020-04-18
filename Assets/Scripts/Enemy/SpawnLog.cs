using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLog : MonoBehaviour
{
    public GameObject ShootingPoint;
    public LogSpeed Logs;
    public int SpawnTime;
    protected Transform spawnPool;

    //Invoca un tronco cada X tiempo
    private void Start()
    {
        InvokeRepeating("Spawn", 3f, SpawnTime);
    }

    public void Spawn()
    {
        LogSpeed newLog = Instantiate<LogSpeed>(Logs, ShootingPoint.transform.position, Quaternion.identity, spawnPool);
        newLog.Rotate(transform.right * transform.lossyScale.x);
    }
    
}
