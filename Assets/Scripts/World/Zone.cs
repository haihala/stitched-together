using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private List<GameObject> content;

    private void Start()
    {
        content = new List<GameObject>();
    }
    public bool Contains(GameObject go)
    {
        return content.Contains(go);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!content.Contains(other.gameObject))
            content.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (content.Contains(other.gameObject))
            content.Remove(other.gameObject);
    }
}
