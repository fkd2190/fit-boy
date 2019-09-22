using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Utils;

public class QuestObject : MonoBehaviour
{
    public Quest quest;

    public void DrawQuest(GameObject questButton)
    {
        GameObject.Find("InitClasses").GetComponent<FitBoyGUI>().StartQuest(quest);
        Destroy(questButton);
    }
}
