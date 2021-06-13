using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerSpawner : MonoBehaviour
{
    public GameObject hanger_template;

    private SpringJoint2D connector;
    private GameObject template_object;

    void Start()
    {
        template_object = Instantiate(hanger_template);
        connector = GetComponent<SpringJoint2D>();
        template_object.transform.position = transform.position + (Vector3)(connector.anchor + connector.connectedAnchor);
        HangerMover hm = template_object.GetComponent<HangerMover>();
        hm.connector = connector;
        hm.creature = GetComponent<Creature>();
    }
}
