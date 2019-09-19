using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Conversions.cs;

public class CoordsToPos : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var worldPosition = Conversions.GeoToWorldPosition(37.7749, 122.4194, new Vector2d(10, 10), (float)2.5);
        transform.position = worldPosition;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
