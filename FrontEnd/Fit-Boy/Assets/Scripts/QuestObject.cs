using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public GameObject questPanel;

    public void ToggleDetails()
    {
        questPanel.SetActive(!questPanel.active);
    }
}
