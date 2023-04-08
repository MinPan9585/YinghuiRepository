using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public GameObject cubePrefab;
    public int spawnAmount = 10;
    public float spawnInterval = 0.1f;
    private float timer = 0f;

    private ObjectPool<GameObject> cubePool;
    private void Start()
    {
        cubePool = new ObjectPool<GameObject>(CreateCube, OnGetCube, OnReleaseCube, OnDestroyCube,false, 10, 10000);
    }

    private GameObject CreateCube()
    {
        GameObject cube = Instantiate(cubePrefab);
        cube.GetComponent<CubeBehaviour>().SetPool(cubePool);
        return cube;
    }

    private void OnGetCube(GameObject cube)
    {
        cube.SetActive(true);
        cube.transform.position = transform.position + Random.onUnitSphere * 10f;
    }

    private void OnReleaseCube(GameObject cube)
    {
        cube.SetActive(false);
    }

    private void OnDestroyCube(GameObject cube)
    {
        Destroy(cube);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnInterval)
        {
            timer = 0f;
            for (int i = 0; i < spawnAmount; i++)
            {
                cubePool.Get();
            }
        }
    }
}
