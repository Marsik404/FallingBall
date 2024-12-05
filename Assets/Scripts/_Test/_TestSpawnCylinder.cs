using UnityEngine;

public class _TestSpawnCylinder : MonoBehaviour
{
    [SerializeField] private GameObject _cylinderBlockPrefab;
    [SerializeField] private int _heightBlocks = 5;



    private void Start()
    {
        BuildCylinder();
    }

    private void BuildCylinder()
    {
        float cylinderHeight = _cylinderBlockPrefab.GetComponent<Renderer>().bounds.size.y;
        Vector3 spawnStartPosition = transform.position;
        spawnStartPosition.y += transform.position.y + cylinderHeight / 2;

        for (int i = 0; i < _heightBlocks; i++)
        {
            Instantiate(_cylinderBlockPrefab, spawnStartPosition, Quaternion.identity, transform);
            spawnStartPosition += new Vector3(0, cylinderHeight, 0);
        }
    }
}