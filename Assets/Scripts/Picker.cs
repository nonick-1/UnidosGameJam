using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Picker : MonoBehaviour
{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] GameObject ingredientLocation;

    public Vector3 MousePosition { get; set; }
    CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot = Vector2.zero;

    public GameObject Handler { get; set; }

    public static Picker Instance { get { return _instance; } }
    private static Picker _instance;

    Item currentHeldItem;

    [SerializeField] LayerMask equipmentLayers;
    [SerializeField] LayerMask ingredientsLayer;
    [SerializeField] LayerMask servingPlateLayer;

    bool canStart;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
        Handler = Instantiate(ingredientLocation, Vector3.zero, Quaternion.identity, null);
    }  

    private void Update()
    {
        if (!canStart) { return; }

        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Handler.transform.position = new Vector3(MousePosition.x, MousePosition.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            if(currentHeldItem == null)
                Interact();
            else
                currentHeldItem.ItemHoverInteraction();
        }
    }

    private void OnEnable()
    {
        UIManager.onStart += () => canStart = true;
    }

    private void OnDisable()
    {
        UIManager.onStart -= () => canStart = false;
    }

    public void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, ~equipmentLayers);

        if(hit)
        {
            Item cachedItem = hit.collider.GetComponent<Item>();
            Spawner cachedSpawner = hit.collider.GetComponent<Spawner>();

            if (cachedItem)
                cachedItem.PickupInteraction();
            else if (cachedSpawner)
                cachedSpawner.CreateItem();
        }
    }

    public Item GetCurrentHeldItem()
    {
        return currentHeldItem;
    }

    public void SetCurrentHeldItem(Item item, bool removedChild = false)
    {
        currentHeldItem = item;

        //Used to remove the combined cooking item
        if (removedChild)
        {
            GameObject foodIngredientOnPlate = Handler.gameObject.transform.GetChild(0).gameObject;
            Destroy(foodIngredientOnPlate);
        }
    }
}
