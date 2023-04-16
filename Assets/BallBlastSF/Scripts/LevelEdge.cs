using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EdgeType
{
    Bottom,
    Left,
    Right
}

public class LevelEdge : MonoBehaviour
{
    [SerializeField] private EdgeType _edgeType;

    public EdgeType Type => _edgeType;

}
