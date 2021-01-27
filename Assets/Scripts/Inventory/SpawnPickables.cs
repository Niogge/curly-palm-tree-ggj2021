using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickables : MonoBehaviour
{
    public GameObject[] PickablesPrefabs;

    public void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Spawn Pickable"))
            SpawnPickable();
    }

    public void SpawnPickable()
    {
        int randomPickable = Random.Range(0, PickablesPrefabs.Length);
        GameObject pickable = Instantiate(PickablesPrefabs[randomPickable]);
        pickable.transform.position = transform.position;
        pickable.GetComponent<Pickable>().CreateItem();
    }
}
