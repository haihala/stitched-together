using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerSpawner : MonoBehaviour
{
    public GameObject hanger_template;
    public GameObject hanger;

    private SpringJoint2D connector;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        hanger = Instantiate(hanger_template);
        connector = GetComponent<SpringJoint2D>();
        hanger.transform.position = transform.position + (Vector3)(connector.anchor + connector.connectedAnchor);
        HangerMover hm = hanger.GetComponent<HangerMover>();
        hm.connector = connector;
        hm.creature = GetComponent<Creature>();
    }
}
