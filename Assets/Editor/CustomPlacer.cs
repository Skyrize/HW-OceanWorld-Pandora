using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
class CustomPlacer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    int currentIndex = 0;
    [SerializeField]
    LayerMask mask = 0;
    [SerializeField]
    bool randomizeRotation = true;
    [SerializeField]
    float yOffset = -1f;
    [SerializeField]
    bool random = true;
    [SerializeField]
    int randomIndexMin = 4, randomIndexMax = 7;
    
    [Header("References")]
    [SerializeField]
    GameObject[] prefabs;

    private static CustomPlacer instance = null;
    public static CustomPlacer Instance {
        get {
            return instance;
        }
        set {
            if (instance != null)
                Debug.LogException(new System.Exception("More than one CustomPlacer in the scene. Please remove the excess"));
            instance = value;
        }
    }
            
    private void Awake() {
        if (!Instance)
        Instance = this;
    }

    public void SpawnItem()
    {
        Debug.Log("Spawning..");
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, mask)) {
            Quaternion rot = randomizeRotation ? Quaternion.Euler(0, Random.Range(0, 360), 0) : Quaternion.identity;
            GameObject prefab = random ? prefabs[Random.Range(randomIndexMin, randomIndexMax + 1)] : prefabs[currentIndex];
            GameObject spawned = GameObject.Instantiate(prefab, hit.point + Vector3.up * yOffset, rot);
            
            Debug.Log("Spawned " + spawned.name + " !");
        } else {
            Debug.Log("Can't spawn, nothing touched..");

        }

    }

    [MenuItem("My Commands/Special Command %g")]
    static void SpecialCommand() {
        Instance.SpawnItem();
    }

    private void Update() {
        if (!Instance)
        Instance = this;
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * 1000f);
    }
}