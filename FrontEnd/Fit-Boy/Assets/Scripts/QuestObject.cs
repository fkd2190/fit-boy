using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Utils;

public class QuestObject : MonoBehaviour
{
    public Quest quest;

    public void DrawQuest()
    {
        GameObject.Find("Directions").GetComponent<DirectionsFactory>().endPos = new Vector2d(quest.Stop_co.Lat, quest.Stop_co.Lon);
    }
}
