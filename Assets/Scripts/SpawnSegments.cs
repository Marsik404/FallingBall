using System.Collections.Generic;
using UnityEngine;

public class SpawnSegments : MonoBehaviour
{
    private int _sectorsPerCylinder = 6;

    public GameObject segmentPrefab;
    public int numberOfSegment = 5;
    public float distanceBetweenSegments = 3f;

    private void Start()
    {
        SpawnSegmentsOnRod();
    }

    void SpawnSegmentsOnRod()
    {
        float cylinderHeight = GetComponent<Renderer>().bounds.size.y;
        float cylinderRadius = GetComponent<Renderer>().bounds.size.x / 2;

        Vector3 basePosition = transform.position - new Vector3(0, cylinderHeight / 2, 0);
        float angleStepForSegment = 360f / _sectorsPerCylinder;

        for (int i = 0; i < numberOfSegment; i++)
        {
            Vector3 spawnPositionVertical = basePosition + new Vector3(0, i * distanceBetweenSegments, 0);

            if (spawnPositionVertical.y <= transform.position.y + cylinderHeight / 2)
            {
                int segmentsToSpawn = (i == 0) ? _sectorsPerCylinder : Random.Range(1, 6);                        // 1,6 - магічні числа?

                HashSet<int> listSectors = new HashSet<int>();

                for (int j = 0; j < segmentsToSpawn; j++)
                {
                    // Вибираємо випадковий сектор для сегмента, поки не знайдемо вільний. Логіка ГІВНО!!!
                    int randomSector;
                    do
                    {
                        randomSector = Random.Range(0, 6);                                                        // 0,6 - магічні числа? Логіка гівно!!!
                    } while (listSectors.Contains(randomSector));

                    listSectors.Add(randomSector);



                    // Обчислюємо кут для поточного сегмента з урахуванням рандомного сектора
                    float angle = randomSector * angleStepForSegment * Mathf.Deg2Rad;                             // Перетворюємо градуси в радіани

                    // Обчислюємо позицію сегмента на основі кута
                    Vector3 sectorOffsetHorizontal = new Vector3(Mathf.Cos(angle) * (cylinderRadius - 0.5f), 0, Mathf.Sin(angle) * (cylinderRadius - 0.5f)); // Зменшено на 0.5 для уникнення накладання
                    Vector3 finalSpawnPosition = spawnPositionVertical + sectorOffsetHorizontal;

                    GameObject segment = Instantiate(segmentPrefab, finalSpawnPosition, Quaternion.identity);

                    // Обчислюємо поворот сегмента, щоб носик вказував у центр
                    Quaternion rotation = Quaternion.LookRotation(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)), Vector3.up);



                    segment.transform.rotation = rotation;

                    segment.transform.parent = transform;
                }
            }
            else
            {
                break;
            }
        }
    }
}
