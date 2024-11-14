using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    [SerializeField] protected Segment _segmentPrefab;

    public abstract void AddSegments();

}
