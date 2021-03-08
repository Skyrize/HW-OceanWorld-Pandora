using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected float rotationSpeed = 5f;
    [SerializeField] protected LayerMask placableMask = 1;
    [SerializeField] protected LayerMask ignoreGhostMask = 1;
    [SerializeField] protected Color invalidGhostColor = new Color(1, 0, 0, 0.5f);
    [SerializeField] protected Color validGhostColor = new Color(0, 1, 0, 0.5f);
    [Header("References")]
    [SerializeField] protected TMP_InventoryItem currentItem = null;
    [SerializeField] public TMP_InventoryItem CurrentItem {
        get {
            return currentItem;
        }
        set {
            if (ghostPlacer) {
                ClearGhost();
            }
            currentItem = value;
            if (currentItem != null) {
                GenerateGhost();
            }
        }
    }
    [SerializeField] protected GameObject ghostPlacer = null;
    [SerializeField] protected Camera cam = null;

    [Header("Runtime")]
    [SerializeField] protected bool onBoat = false;
    [SerializeField] protected bool canPlace = false;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    public void ClearGhost()
    {
        Destroy(ghostPlacer);
        InputManager.Instance.RemoveKeyEvent(KeyCode.R, PressType.HOLD, RotateGhost);
        InputManager.Instance.RemoveMouseButtonEvent(MouseButtonType.LEFT_BUTTON, PressType.DOWN, PlaceFromGhost);
    }

    public void GenerateGhost()
    {
        ghostPlacer = Instantiate(currentItem.item, Vector3.zero, Quaternion.identity);
        ghostPlacer.layer = LayerMask.NameToLayer("GhostPlacer");
        InputManager.Instance.AddKeyEvent(KeyCode.R, PressType.HOLD, RotateGhost);
    }

    public void PlaceFromGhost()
    {
        if (!onBoat) { // try to place outside boat
            //TODO : Add event for UI display error ?
            return;
        }
        if (!canPlace) { // try to place on something else
            //TODO : Add event for UI display error ?
            return;
        }
            
        GameObject placedItem = GameObject.Instantiate(currentItem.item, ghostPlacer.transform.position, ghostPlacer.transform.rotation);
        placedItem.transform.parent = hit.collider.transform;
        //TODO : activate cannon ?
        //TODO : bind to Input ?
        //TODO : Add UI for task management
        //TODO : Recalculate nashmesh after place ?
    }

    public void RotateGhost()
    {
        ghostPlacer.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    public void ValidateGhostPlacing()
    {
        MeshRenderer[] meshes = ghostPlacer.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material = mesh.material;
            mesh.material.color = validGhostColor;
        }
        InputManager.Instance.AddMouseButtonEvent(MouseButtonType.LEFT_BUTTON, PressType.DOWN, PlaceFromGhost);
        canPlace = true;
        
    }

    public void InvalidateGhostPlacing()
    {
        MeshRenderer[] meshes = ghostPlacer.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.material = mesh.material;
            mesh.material.color = invalidGhostColor;
        }
        InputManager.Instance.RemoveMouseButtonEvent(MouseButtonType.LEFT_BUTTON, PressType.DOWN, PlaceFromGhost);
        canPlace = false;
    }

    void TestGhostPlacing()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit placingHit;

        if (Physics.Raycast(ray, out placingHit, Mathf.Infinity, ignoreGhostMask)
        && placingHit.collider.gameObject.layer != LayerMask.NameToLayer("PlaceZone")) {
            Debug.Log(placingHit.collider.gameObject.name);
            InvalidateGhostPlacing();
        } else {
            //TODO : need to add box collider projection here
            ValidateGhostPlacing();
        }
    }

    void PlaceGhostAtMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, Time.deltaTime);
        onBoat = Physics.Raycast(ray, out hit, Mathf.Infinity, placableMask);
        if (onBoat) {
            ghostPlacer.transform.position = hit.point;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItem != null && currentItem.item != null) {
            PlaceGhostAtMouse();
            if (onBoat)
                TestGhostPlacing();
                
        }
    }
}
