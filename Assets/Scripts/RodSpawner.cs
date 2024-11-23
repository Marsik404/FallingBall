using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.ProBuilder;

public class RodSpawner : MonoBehaviour
{
    [SerializeField] private Cylinder _cylinderBlockPrefab;
    [SerializeField] private int _heightBlocks = 5;

    private Vector3 _highestPointRod;
    public Vector3 HighestPointRod { get => _highestPointRod; }


    // TODO x Debug
    //private void Start()
    //{
    //    BuildRod();
    //    Debug.Log($"Total height of Rod: {_highestPointRod}");      // TODO x Debug.Log
    //}

    public void BuildRod()
    {
        float cylinderHeight = _cylinderBlockPrefab.GetComponent<Renderer>().bounds.size.y;

        Vector3 spawnPosition = transform.position + new Vector3(0, cylinderHeight / 2, 0);

        for (int i = 0; i < _heightBlocks; i++)
        {
            Cylinder newBlock = Instantiate(_cylinderBlockPrefab, spawnPosition, Quaternion.identity, transform);

            // Pass the command to the new block to add the platform (the next element in the chain)
            newBlock.AddPlatform(i == 0);
            spawnPosition += new Vector3(0, cylinderHeight, 0);
        }

        //_highestPointRod = _heightBlocks * cylinderHeight;                // TODO ? _highestPointRod int ?     
        _highestPointRod = spawnPosition - new Vector3(0, 1f, 0);            // the upper face of the last cylinder
    }
}
