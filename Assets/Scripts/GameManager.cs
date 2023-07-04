using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float leftBound = -10;
    [SerializeField] private float righBound = 10;
    [SerializeField] private float topBound = 5;
    [SerializeField] private float bottomBound = -5;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private int friendCount = 10;

    public GameObject flyPrefab;
    public GameObject ladybugPrefab;
    public int KillCount;

    private List<GameObject> targets;   

    void Start()
    {
        targets = new List<GameObject>();
        for (int i = 0; i < enemyCount; i++)
            targets.Add(flyPrefab);
        for (int i = 0; i < friendCount; i++)
            targets.Add(ladybugPrefab);
        targets = targets.OrderBy(_ => Random.value).ToList();
        KillCount = 0;
        Spawn(targets[0]);
    }

    void Update()
    {
        
    }

    public void Spawn(GameObject target)
    {
        Instantiate(target, new Vector3(Random.Range(leftBound, righBound), Random.Range(bottomBound, topBound), 0), target.transform.rotation);
    }
}
