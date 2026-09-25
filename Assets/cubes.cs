using UnityEngine;
using UnityEngine.UIElements;

public class cubes : MonoBehaviour
{
    [field: SerializeField] public GameObject PrefabCube { get; private set; }

    [field: SerializeField] public int CubeCount { get; private set; }

    [field: SerializeField] public float RotateRadius { get; private set; }

    [field: SerializeField] public uint RotateSpeed { get; private set; }

    [field: SerializeField] public bool IsÑlockwise { get; private set; }

    private float angle;
    private Vector3 spawnPoint;
    private int rotateSpeedWithDirection;



    private void SetProperties()
    {
        if (CubeCount != 0)
            angle = 360f / CubeCount;

        spawnPoint = transform.position + new Vector3(RotateRadius, 0, 0);
        rotateSpeedWithDirection = (int)RotateSpeed;
    }

    private void Spawn()
    {
        for (int i = CubeCount; i > 0; i--)
        {
            GameObject cube = Instantiate(PrefabCube, spawnPoint, Quaternion.identity);
            cube.transform.SetParent(transform);
            transform.Rotate(0, angle, 0);
        }
    }

    private void Rotate()
    {
        transform.Rotate(0, rotateSpeedWithDirection * Time.deltaTime, 0);
    }

    private void UpdateCubesPosition()
    {
        int count = transform.childCount;

        if (count == 0)
            return;

        float angle = 360f / count;

        for (int i = 0; i < count; i++)
        {
            Transform cube = transform.GetChild(i);

            float currentAngle = angle * i * Mathf.Deg2Rad;

            cube.localPosition = new Vector3(
                Mathf.Cos(currentAngle) * RotateRadius,
                0f,
                Mathf.Sin(currentAngle) * RotateRadius
            );
        }
    }

    private void UpdateDirection()
    {
        if (!IsÑlockwise)
        {
            rotateSpeedWithDirection *= -1;
        }
    }
    
    private void UpdateSpeed()
    {
        rotateSpeedWithDirection = (int)RotateSpeed;
    }

    private void Awake()
    {
        if (PrefabCube is null) Debug.LogError("Ïðåôàá âðàùàþùåãîñÿ îáúåêòà íå íàçíà÷åí");
    }

    private void Start()
    {
        SetProperties();
        Spawn();
    }

    private void Update()
    {
        Rotate();
    }

    private void OnValidate()
    {
        UpdateCubesPosition();
        UpdateSpeed();
        UpdateDirection();
    }
}
