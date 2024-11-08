using UnityEngine;

public class SpawnCylinder : MonoBehaviour
{
    [SerializeField] private GameObject _cylinderBlockPrefab;   // GameObject - because there is no component on the segment
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
            GameObject newBlock = Instantiate(_cylinderBlockPrefab, spawnStartPosition, Quaternion.identity, transform);

            spawnStartPosition += new Vector3(0, cylinderHeight, 0);
        }
    }
    public int GetHeightBlocks()
    {
        return _heightBlocks;
    }
}
