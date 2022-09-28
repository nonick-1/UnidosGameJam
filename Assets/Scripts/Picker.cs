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
        //PlaceIngredient();

        if(Input.GetMouseButtonDown(0))
            MoveItem();
        
    }

    void MoveItem()
    {
        if (currentHeldItem == null)
            GrabItem();
        else if(currentHeldItem.GetCurrentDoneness() == Doneness.Cooked)
            PlaceItemOnPlate();
        else 
            PlaceItemOnEquipment();
    }

    private void PlaceItemOnPlate()
    {
        ContactFilter2D filter2D = new ContactFilter2D
        {
            layerMask = servingPlateLayer,
        };

        filter2D.useLayerMask = true;

        RaycastHit2D[] results = new RaycastHit2D[1];
        int objectsHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, filter2D, results, Mathf.Infinity);

        Plate cachedPlate = results[0].collider.GetComponent<Plate>();
        if (objectsHit > 0 && cachedPlate)
        {
            Debug.Log("Target: " + results[0].collider.gameObject.name);
            cachedPlate.ItemPlace(currentHeldItem);
        }
    }

    private void PlaceItemOnEquipment()
    {
        ContactFilter2D filter2D = new ContactFilter2D
        {
            layerMask = equipmentLayers,
        };

        filter2D.useLayerMask = true;

        RaycastHit2D[] results = new RaycastHit2D[1];
        int objectsHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, filter2D, results, Mathf.Infinity);

        Equipment cachedEquipment = results[0].collider.GetComponent<Equipment>();
        if (objectsHit > 0 && cachedEquipment)
        {
            Debug.Log("Target: " + results[0].collider.gameObject.name);
            cachedEquipment.IsAbleToPlaceItem(currentHeldItem);
        }
    }

    private void GrabItem()
    {
        ContactFilter2D filter2D = new ContactFilter2D
        {
            layerMask = ingredientsLayer,
        };

        filter2D.useLayerMask = true;

        RaycastHit2D[] results = new RaycastHit2D[1];
        int objectsHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, filter2D, results, Mathf.Infinity);

        if (objectsHit <= 0)
        {
            Debug.Log("No object detected!");
            return;
        }
        else
            Debug.Log("Item: " + results[0].collider.gameObject.name);


        //TODO: Implement Interface ISpawn
        Item cachedItem = results[0].collider.GetComponent<Item>();
        Spawner cachedSpawner = results[0].collider.GetComponent<Spawner>();
        PlateSpawner cachedPlateSpawner = results[0].collider.GetComponent<PlateSpawner>();

        if (cachedPlateSpawner)
        {
            Debug.Log("Target: " + results[0].collider.gameObject.name);
            cachedPlateSpawner.SpawnIngredient();
        }
        else if(cachedItem)
        {
            if(cachedItem.GetCurrentIngredientType() == IngredientTypes.Plates)
                cachedItem.Pickup(false);
            else
                cachedItem.Pickup(true);
        }
        else if(cachedSpawner)
        {
            Debug.Log("Target: " + results[0].collider.gameObject.name);
            cachedSpawner.SpawnIngredient();
        }
    }

    private void GrabCookedIngredient()
    {
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.layerMask = LayerMask.NameToLayer("Ingredients");

        RaycastHit2D[] results = new RaycastHit2D[1];
        int objectsHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, filter2D, results, Mathf.Infinity);

        if (objectsHit > 0)
        {
            Debug.Log("Target: " + results[0].collider.gameObject.name);
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
