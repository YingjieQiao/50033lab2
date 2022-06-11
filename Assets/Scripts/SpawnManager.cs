using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    void Awake() {
        // spawn two gombaEnemy
        for (int j = 0; j < 10; j++)
            spawnFromPooler(ObjectType.gombaEnemy);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnFromPooler(ObjectType i) {
        // static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null) {
            //set position, and other necessary states
            //item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
            item.transform.position = new Vector3(Random.Range(-4.5f, -3f), -0.45f, 0);
            item.SetActive(true);
            Debug.Log("aaa");
        }
        else {
            Debug.Log("not enough items in the pool.");
        }
    }
}
