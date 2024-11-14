using UnityEngine;

public class RodSpawner : MonoBehaviour
{
    [SerializeField] private Cylinder _cylinderBlockPrefab;
    [SerializeField] private int _heightBlocks = 5;

    private void Start()
    {
        BuildCylinder();
    }

    public void BuildCylinder()
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
    }
}
