using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.ProBuilder;
using System.Collections.Generic;

public class RodSpawner : MonoBehaviour
{
    [SerializeField] private Cylinder _cylinderBlockPrefab;
    [SerializeField] private int _heightBlocks = 5;
    [SerializeField, Range(1, 5)] private int _heightSpawnBall = 5;

    public Vector3 _highestPointRod { get; private set; }

    private List<Cylinder> _cylinderBloks = new List<Cylinder>();



    public void BuildRod()
    {
        float cylinderHeight = _cylinderBlockPrefab.GetComponent<Renderer>().bounds.size.y;

        Vector3 faceCylinder = new Vector3(0, cylinderHeight / 2, 0);                           // "cylinderHeight / 2" - the face of the cylinder

        Vector3 spawnPosition = transform.position + new Vector3(0, cylinderHeight / 2, 0);     // "cylinderHeight / 2" - the 1-st cylinder is on the 0 plane

        for (int i = 0; i < _heightBlocks; i++)
        {
            _cylinderBloks.Add(Instantiate(_cylinderBlockPrefab, spawnPosition, Quaternion.identity, transform));

            // Pass the command to the new block to add the platform (the next element in the "chain")
            _cylinderBloks[i].AddPlatform(i == 0);

            spawnPosition += new Vector3(0, cylinderHeight, 0);

            if (i == _heightSpawnBall - 1)
            {
                _highestPointRod = _cylinderBloks[i].transform.position + faceCylinder;            // the upper face of the last cylinder
            }
        }
    }
}
