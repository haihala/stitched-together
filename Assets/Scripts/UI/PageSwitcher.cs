using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    private int current_page;
    private List<GameObject> pages;

    void Start()
    {
        pages = new List<GameObject>();
        foreach (Transform child in transform)
        {
            pages.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        current_page = 0;
        Activate(current_page);
    }

    public void NextPage()
    {
        Activate(current_page + 1);
    }

    public void PreviousPage()
    {
        Activate(current_page - 1);
    }

    private void Activate(int page)
    {
        foreach (GameObject child in pages)
        {
            child.SetActive(false);
        }

        current_page = Mathf.Clamp(page, 0, pages.Count - 1);
        pages[current_page].SetActive(true);
    }
}
