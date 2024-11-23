using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelPlatform : SegmentedPlatform
{
    [SerializeField, Range(0, 6)] private int _minSerments = 4;
    [SerializeField, Range(0, 6)] private int _maxSerments = 5;



    // TODO ? The project does not start (Random.Range(-1, 5))
    //[SerializeField] private int _minSerments;
    //public int MinSerments
    //{
    //    get { return _minSerments; }
    //    set
    //    {
    //        if (_minSerments >= 0)
    //        {
    //            _minSerments = value;
    //        }
    //        else
    //        {
    //            Debug.LogError("MinSerments має бути > 0");

    //            //throw new System.ArgumentOutOfRangeException("MinSerments має бути > 0");
    //        }
    //    }
    //}

    //[SerializeField] private int _maxSerments;
    //public int MaxSerments
    //{
    //    get { return _maxSerments; }
    //    set
    //    {
    //        if (_maxSerments >= 1 && _maxSerments <= 6)
    //        {
    //            _maxSerments = value;
    //        }
    //        else
    //        {
    //            Debug.LogError("MaxSerments повинно бути >=1 та <=6");

    //            //throw new System.ArgumentOutOfRangeException("MaxSerments повинно бути >=1 та <=6");
    //        }
    //    }
    //}



    public override void InitializeSegments()
    {
        int segmentsToSpawn = Random.Range(_minSerments, _maxSerments);

        InitializeSegments(segmentsToSpawn, _dividePlatformIntoSectors);
    }
}

