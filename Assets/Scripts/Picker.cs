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

    public Ingredient CurrentHeldIngredient { get; set; }
    public GameObject Handler { get; set; }

    public static Picker Instance { get { return _instance; } }
    private static Picker _instance;

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
    }
}
