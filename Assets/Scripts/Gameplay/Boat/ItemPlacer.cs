using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlacerToolMode
{
    GHOST,
    REMOVE,
    NONE
}

public class ItemPlacer : MonoBehaviour
{
    //TODO : maybe implement to replace static ref
    [Header("Events")]
    [SerializeField] public UnityEvent onPlace = new UnityEvent();
    [SerializeField] public UnityEvent onRemove = new UnityEvent();
    [Header("Settings")]
    [SerializeField] protected float rotationSpeed = 5f;
    [SerializeField] protected LayerMask placableMask = 1;
    [SerializeField] protected LayerMask itemMask = 1;
    [SerializeField] protected LayerMask removableItemMask = 1;
    [SerializeField] protected Color invalidGhostColor = new Color(1, 0, 0, 0.5f);
    [SerializeField] protected Color validGhostColor = new Color(0, 1, 0, 0.5f);
    [Header("References")]
    [SerializeField] protected Sprite removeSprite = null;
    [SerializeField] protected PlacableInventoryUI placableInventoryUI = null;
    [SerializeField] protected PostManagerUI postManagerUI = null;
    [SerializeField] protected Player player = null; //TODO : move to anotherScript
    [SerializeField] protected InventoryStorage currentStored = null;
    [SerializeField] protected Transform postStorage = null;
    [SerializeField] protected GameObject buttonRotate = null;
    [SerializeField] public InventoryStorage CurrentItem {
        get {
            return currentStored;
        }
        set {
            if (ghostPlacer) {
                ClearGhost();
            }
            currentStored = value;
            if (currentStored != null) {
                GenerateGhost();
            }
        }
    }
    [SerializeField] protected GameObject ghostPlacer = null;
    [SerializeField] protected Camera cam = null;

    [Header("Runtime")]
    [SerializeField] protected bool onBoat = false;
    [SerializeField] protected bool activated = false;
    [SerializeField] protected bool canPlace = false;
    [SerializeField] protected PlacerToolMode mode = PlacerToolMode.REMOVE;
    [SerializeField] public PlacerToolMode Mode {
        set {
            this.mode = value;
            if (mode == PlacerToolMode.REMOVE) {
                SetRemove();
            } else {
                UnsetRemove();
            }
        }
    }
    [SerializeField] protected Vector3 worldOrigin = Vector3.zero;
    [SerializeField] protected Vector3 worldHalfExtents = Vector3.zero;
    RaycastHit hit;


    void SetRemove()
    {
        InputManager.Instance.AddMouseButtonEvent(MouseButtonType.RIGHT_BUTTON, PressType.DOWN, RemoveAtMouse);
        InputManager.Instance.AddMouseButtonEvent(MouseButtonType.LEFT_BUTTON, PressType.DOWN, RemoveAtMouse);
    }

    void UnsetRemove()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        InputManager.Instance.RemoveMouseButtonEvent(MouseButtonType.RIGHT_BUTTON, PressType.DOWN, RemoveAtMouse);
        InputManager.Instance.RemoveMouseButtonEvent(MouseButtonType.LEFT_BUTTON, PressType.DOWN, RemoveAtMouse);
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        placableInventoryUI.onSelect.AddListener(SelectPlacableItem);

        //TODO : move to anotherScript
        if (!player)
        this.player = GameObject.FindObjectOfType<Player>();

        if (!player) {
            throw new MissingReferenceException("No Player in scene to hold an inventory. It is need to ensure you don't modify an asset while using inventory.");
        }
        buttonRotate.SetActive(false);
    }

    public void SelectPlacableItem(InventoryStorage item)
    {
        if (postManagerUI.crewUI.CurrentCrewMember) {
            postManagerUI.crewUI.UnselectCrewMember();
        }
        CurrentItem = item;
    }

    private void OnEnable() {
    }
    
    private void OnDisable() {
    }

    public void Activate()
    {
        activated = true;
        Mode = PlacerToolMode.REMOVE;
    }

    public void Desactivate()
    {
        activated = false;
        CurrentItem = null;
        UnfocusRemovableItem();
        Mode = PlacerToolMode.NONE;
    }

    public void ClearGhost()
    {
        Mode = PlacerToolMode.REMOVE;
        Destroy(ghostPlacer);
        ghostPlacer = null;
        InputManager.Instance.RemoveKeyEvent(KeyCode.D, PressType.HOLD, RotateGhostClockwise);
        InputManager.Instance.RemoveKeyEvent(KeyCode.A, PressType.HOLD, RotateGhostCounterClockwise);
        buttonRotate.SetActive(false);
        InputManager.Instance.RemoveMouseButtonEvent(MouseButtonType.LEFT_BUTTON, PressType.DOWN, PlaceFromGhost);
        InputManager.Instance.RemoveMouseButtonEvent(MouseButtonType.RIGHT_BUTTON, PressType.DOWN, ClearGhost);
    }

    public void GenerateGhost()
    {
        Mode = PlacerToolMode.GHOST;
        ghostPlacer = Instantiate(currentStored.item.Prefab, Vector3.one * 1000, Quaternion.identity);
        ghostPlacer.SetCollisionActive(false);
        ghostPlacer.name = "GhostPlacer : " + ghostPlacer.name;
        buttonRotate.SetActive(true);
        InputManager.Instance.AddKeyEvent(KeyCode.D, PressType.HOLD, RotateGhostClockwise);
        InputManager.Instance.AddKeyEvent(KeyCode.A, PressType.HOLD, RotateGhostCounterClockwise);
        InputManager.Instance.AddMouseButtonEvent(MouseButtonType.RIGHT_BUTTON, PressType.DOWN, ClearGhost);
        InputManager.Instance.AddMouseButtonEvent(MouseButtonType.LEFT_BUTTON, PressType.DOWN, PlaceFromGhost);
    }

    //TODO : move to anotherScript
    public void PlaceWeapon(GameObject weapon)
    {
        if (placableInventoryUI.PlaceItem(currentStored)) {
            ClearGhost();
        }
        player.weaponry.weapons.Add(weapon.GetComponent<WeaponManager>());
        postManagerUI.AddPost(weapon.GetComponent<WeaponManager>());
        onPlace.Invoke();
    }

    //TODO : move to anotherScript
    public void RemoveWeapon(GameObject weapon)
    {
        placableInventoryUI.RemoveItem(weapon.GetComponent<ItemObject>().Item);
        player.weaponry.weapons.Remove(weapon.GetComponent<WeaponManager>());
        postManagerUI.RemovePost(weapon.GetComponent<WeaponManager>());
        onRemove.Invoke();
    }

    public void PlaceFromGhost()
    {
        if (!onBoat) {
            //TODO : add UI message ?
            return;
        }
        if (!canPlace) {
            //TODO : add UI message ?
            return;
        }
        GameObject placedItem = GameObject.Instantiate(currentStored.item.Prefab, ghostPlacer.transform.position, ghostPlacer.transform.rotation);
        placedItem.transform.parent = postStorage;
        PlaceWeapon(placedItem);
    }

    void RemoveAtMouse()
    {
        if (CurrentRemovable == null)
            return;
        RemoveWeapon(CurrentRemovable);
        var tmp = CurrentRemovable;
        tmp.SetActive(false);
        CurrentRemovable = null;
        Destroy(tmp);
    }

    public void RotateGhostClockwise()
    {
        ghostPlacer.transform.Rotate(new Vector3(0, rotationSpeed * Time.unscaledDeltaTime, 0));
    }

    public void RotateGhostCounterClockwise()
    {
        ghostPlacer.transform.Rotate(new Vector3(0, -rotationSpeed * Time.unscaledDeltaTime, 0));
    }

    public void ValidateGhostPlacing()
    {
        ghostPlacer.SetColor(validGhostColor);
        canPlace = true;
    }

    public void InvalidateGhostPlacing()
    {
        ghostPlacer.SetColor(invalidGhostColor);
        canPlace = false;
    }
    bool HasRoom()
    {
        BoxCollider collider = ghostPlacer.GetComponent<BoxCollider>();

        if (!collider)
            throw new System.Exception("Item need a box Collider to be placed.");
        worldOrigin = ghostPlacer.transform.TransformPoint(collider.center);
        worldHalfExtents = collider.size / 2f;
        return Physics.OverlapBox(worldOrigin, worldHalfExtents, collider.transform.rotation, itemMask).Length == 0;
    }

    void TestGhostPlacing()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit placingHit;

        if (Physics.Raycast(ray, out placingHit, Mathf.Infinity, itemMask) || !HasRoom()) {
            // // Debug.Log("Invalidate by " + placingHit.collider.gameObject.name);
            InvalidateGhostPlacing();
        } else {
            ValidateGhostPlacing();
        }
    }

    void PlaceGhostAtMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (DebugManager.instance.debug)
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue, Time.deltaTime);
        onBoat = Physics.Raycast(ray, out hit, Mathf.Infinity, placableMask);
        if (onBoat) {
            ghostPlacer.transform.position = hit.point;
            
        }
    }

    void UpdateGhostPlacer()
    {
        if (ghostPlacer != null) {
            
            PlaceGhostAtMouse();
            if (onBoat)
                TestGhostPlacing();
                
        }
    }

    public GameObject currentRemovable = null;
    GameObject CurrentRemovable {
        get { return currentRemovable; }
        set {
            if (value) {
                // Debug.Log("removeIcon");
                Cursor.SetCursor(removeSprite.texture, Vector2.zero, CursorMode.Auto);
            } else if (currentRemovable) {
                // Debug.Log("normalIcon");
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
            currentRemovable = value;
        }
    }
    public Color[] tmpColors;

    void FocusRemovableItem(GameObject item)
    {
        CurrentRemovable = item;
        tmpColors = CurrentRemovable.GetColors();
        CurrentRemovable.SetColor(invalidGhostColor);
    }
    void UnfocusRemovableItem()
    {
        if (!CurrentRemovable)
            return;
        CurrentRemovable.SetColors(tmpColors);
        CurrentRemovable = null;
    }

    void UpdateRemovePlacer()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit removeHit;

        if (Physics.Raycast(ray, out removeHit, Mathf.Infinity, removableItemMask)) {
            if (CurrentRemovable != removeHit.collider.gameObject) {
                UnfocusRemovableItem();
                FocusRemovableItem(removeHit.collider.gameObject);
            }
        } else {
            UnfocusRemovableItem();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!activated)
            return;
        switch (mode)
        {
            case PlacerToolMode.GHOST:
                UpdateGhostPlacer();
            break;
            case PlacerToolMode.REMOVE:
                UpdateRemovePlacer();
            break;
            default:
            break;
        }
    }

    private void OnDrawGizmos() {
        if (ghostPlacer) {

        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(worldOrigin, ghostPlacer.transform.rotation, ghostPlacer.transform.lossyScale);
        Gizmos.DrawWireCube(Vector3.zero, worldHalfExtents * 2);
        }
    }
}
