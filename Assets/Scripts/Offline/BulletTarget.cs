using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    [SerializeField] int maxHp = 10;
    private int currentHp;
    private void Awake()
    {
        currentHp = maxHp;
    }
    private void Update()
    {
        if (currentHp <= 0) Destroy(gameObject);
    }
    public void GetHit(int dmg)
    {
        currentHp -= dmg;
    }
    
}
