using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class Route
{
    public string name;
    public List<Vector3> route = new List<Vector3>();
    public LineRenderer lR;

    public Route(LineRenderer _lR, Material _mat)
    {
        lR = _lR;
        lR.numCornerVertices = 2;
        lR.material = _mat;
        lR.startWidth = 0.5f;
        lR.endWidth = 0.5f;
        lR.sortingOrder = 10;
    }
}
