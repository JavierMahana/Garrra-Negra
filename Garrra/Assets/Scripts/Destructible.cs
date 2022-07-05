using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject[] DropType;


    public void Drop()
    {
        int num = Random.Range(0, DropType.Length);
        GameObject prefab = DropType[num];
        //GameObject drop = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        GameObject drop = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        drop.transform.parent = null;

        Debug.Log(drop.name);

        Destroy(gameObject);
        
    }
}
