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

    Ingredient currentHeldIngredient;
    Plate currentHeldPlate;

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
        PlaceIngredient();
    }

    public void SetCurrentHeldIngredient(Ingredient heldItem)
    {
        currentHeldIngredient = heldItem;
    }

    public void SetCurrentHeldPlate(Plate heldItem)
    {
        currentHeldPlate = heldItem;
    }

    public Ingredient GetCurrentHeldIngredient()
    {
        return currentHeldIngredient;
    }

    public Plate GetCurrentHeldPlate()
    {
        return currentHeldPlate;
    }

    private void PlaceIngredient()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Held: " + currentHeldIngredient);
            if (currentHeldIngredient && currentHeldIngredient.IsOverValidEquipment)
            {
                currentHeldIngredient.PlaceIngredientOnEquipment();
            }

            if (currentHeldPlate && currentHeldPlate.IsOverValidEquipment)
            {
                currentHeldPlate.PlacePlateOnEquipment();
            }
        }
    }
}
