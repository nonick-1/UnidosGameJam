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
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Handler.transform.position = new Vector3(MousePosition.x, MousePosition.y, 0);

        if(Input.GetMouseButtonDown(0))
            Interact();
        
    }

    private void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, ~LayerMask.NameToLayer("Equipment"));

        Item cachedItem = hit.collider.GetComponent<Item>();
        Spawner cachedSpawner = hit.collider.GetComponent<Spawner>();

        if (cachedItem)
            cachedItem.PickupInteraction();
        else if(cachedSpawner)
            cachedSpawner.CreateItem();
    }

    //private void GrabItem()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, ~LayerMask.NameToLayer("Equipment"));

    //    //TODO: Implement Interface ISpawn
    //    Item cachedItem = hit.collider.GetComponent<Item>();
    //    Spawner cachedSpawner = hit.collider.GetComponent<Spawner>();
    //    PlateSpawner cachedPlateSpawner = hit.collider.GetComponent<PlateSpawner>();

    //    if (cachedPlateSpawner)
    //    {
    //        cachedPlateSpawner.SpawnIngredient();
    //    }
    //    else if(cachedItem)
    //    {
    //        if(cachedItem.GetCurrentItemType() == ItemType.Plates)
    //        {
    //            cachedItem.Pickup(false);
    //        }
    //        else
    //            cachedItem.Pickup(true);
    //    }
    //    else if(cachedSpawner)
    //    {
    //        Debug.Log("Target: " + hit.collider.gameObject.name);
    //        cachedSpawner.SpawnIngredient();
    //    }
    //}

    public Item GetCurrentHeldItem()
    {
        return currentHeldItem;
    }

    public void SetCurrentHeldItem(Item item, bool removedChild = false)
    {
        Debug.Log("Item Set!");
        currentHeldItem = item;

        //Used to remove the combined cooking item
        if (removedChild)
        {
            GameObject foodIngredientOnPlate = Handler.gameObject.transform.GetChild(0).gameObject;
            Destroy(foodIngredientOnPlate);
        }
    }
}
