using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class _Test_SpawnSpheres : MonoBehaviour
{
    [SerializeField] private GameObject _spherePrefab;                      // GameObject - because there is no component on the segment
    [SerializeField] private int _numberOfSpheres = 5;
    [SerializeField] private float _distanceBetweenSpheres = 3f;

    private Renderer _renderer;

    private void Awake()
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }
    }

    private void Start()
    {
        SpawnSpheresOnRod();
    }

    void SpawnSpheresOnRod()
    {
        float cylinderHeight = _renderer.bounds.size.y;

        Vector3 startPosition = transform.position - new Vector3(0, cylinderHeight / 2, 0);



        // Fixed distance
        for (int i = 0; i < _numberOfSpheres; i++)
        {
            Vector3 spawnPosition = startPosition + new Vector3(0, i * _distanceBetweenSpheres, 0);

            if (spawnPosition.y <= transform.position.y + cylinderHeight / 2)
            {
                Instantiate(_spherePrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                break;
            }
        }



        //// Uniform distance
        //float distanceBetweenSpheres = cylinderHeight / (numberOfSpheres - 1);
        //for (int i = 0; i < numberOfSpheres; i++)
        //{
        //    Vector3 spawnPosition = startPosition + new Vector3(0, i * distanceBetweenSpheres, 0);
        //    Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
        //}

    }
}
