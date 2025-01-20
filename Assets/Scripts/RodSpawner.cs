using UnityEngine;
using System.Collections.Generic;

public class RodSpawner : MonoBehaviour
{
    [SerializeField] private Cylinder _cylinderBlockPrefab;

    [SerializeField, Range(1, 30)] private int _heightBlocks = 5;
    public int HeightBlocks { get => _heightBlocks; }

    public Vector3 HightPointRod { get; private set; }

    private List<Cylinder> _cylinderBloks = new List<Cylinder>();



    public void BuildRod()
    {
        float cylinderHeight = GetHeightCylinder();

        Vector3 cylinder = new Vector3(0, cylinderHeight, 0);
        Vector3 faceCylinder = new Vector3(0, cylinderHeight / 2, 0);   // "cylinderHeight / 2" - the face of the cylinder
        Vector3 spawnPosition = transform.position + faceCylinder;      // "cylinderHeight / 2" - the 1-st cylinder is on the 0 plane

        for (int i = 0; i < _heightBlocks; i++)
        {
            Cylinder instanceCylinder = Instantiate(_cylinderBlockPrefab, spawnPosition, Quaternion.identity, transform);
            instanceCylinder.name = $"Cylinder_{i + 1}";
            _cylinderBloks.Add(instanceCylinder);

            // Pass the command to the new block to add the platform (the next element in the "chain")
            instanceCylinder.AddPlatform(i == 0);

            spawnPosition += cylinder;
        }
    }

    public void SetHighestSpawnBall(int cylinderIndex)
    {
        float cylinderHeight = GetHeightCylinder();

        Vector3 cylinder = _cylinderBloks[cylinderIndex - 1].transform.position;
        Vector3 faceCylinder = new Vector3(0, cylinderHeight / 2, 0);

        if (_heightBlocks == cylinderIndex)
        {
            HightPointRod = cylinder + faceCylinder;  // the upper face of the last cylinder + offset
        }
        else
        {
            HightPointRod = cylinder;                 // the upper face of the any cylinder
        }
    }

    private float GetHeightCylinder()
    {
        float cylinderHeight = _cylinderBlockPrefab.GetComponent<Renderer>().bounds.size.y;
        return cylinderHeight;
    }
}
