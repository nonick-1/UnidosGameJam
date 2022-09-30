using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public delegate void OnSuccessfulOrder();
    public static event OnSuccessfulOrder onSuccessfulOrder;

    public delegate void OnFailedOrder();
    public static event OnFailedOrder onFailedOrder;

    [SerializeField] List<TacoOrders> possibleTacoCombinations;
    [SerializeField] List<Sprite> characterOptions;

    [SerializeField] SpriteRenderer chatBubble;
    [SerializeField] SpriteRenderer foodOrder;
    SpriteRenderer spriteRenderer;

    [SerializeField] float tweenEndScale = 1f;
    [SerializeField] float tweenEndScaleTime = 3f;

    [SerializeField] float tweenStartLocalMoveY = .8f;
    [SerializeField] float tweenEndLocalMoveY = 5f;
    [SerializeField] float tweenLocalMoveYTime = 3f;

    [SerializeField] float tweenEndFade = 1f;
    [SerializeField] float tweenEndFadeTime = 3f;

    TacoOrders currentOrder;

    bool canStart;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!canStart) { return; }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OrderStartTween();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            OrderCompleteTween();
        }
    }

    private void OnEnable()
    {
        UIManager.onStart += OrderStartTween;
    }

    private void OnDisable()
    {
        UIManager.onStart -= OrderStartTween;
    }

    public void TurnInFood(Plate currentPlateOrder)
    {
        Debug.Log("Turned in food!");
        var equal = (currentOrder.ingredientsNeeded.Count == currentPlateOrder.GetCurrentIngredientsOnPlate().Count);

        if (equal)
        {
            currentOrder.ingredientsNeeded.Sort();
            currentPlateOrder.GetCurrentIngredientsOnPlate().Sort();
            equal = currentOrder.ingredientsNeeded.SequenceEqual(currentPlateOrder.GetCurrentIngredientsOnPlate());

            //Reusing Equal to verify contents
            if (equal)
            {
                Debug.Log("Combination Correct!");
                onSuccessfulOrder?.Invoke();
            }
            else
                onFailedOrder?.Invoke();
        }
        else
            onFailedOrder?.Invoke();

        Picker.Instance.SetCurrentHeldItem(null, true);
    }

    private void OrderStartTween()
    {
        transform.DOScale(tweenEndScale, tweenEndScaleTime);
        transform.DOLocalMoveY(tweenEndLocalMoveY, tweenLocalMoveYTime);
        spriteRenderer.DOFade(tweenEndFade, tweenEndFadeTime);

        chatBubble.DOFade(tweenEndFade, tweenEndFadeTime).OnComplete(() => SetTargetOrder());
    }

    private void OrderCompleteTween()
    {
        transform.DOScale(0, tweenEndScaleTime);
        transform.DOLocalMoveY(tweenStartLocalMoveY, tweenLocalMoveYTime);
        spriteRenderer.DOFade(0, tweenEndFadeTime);

        chatBubble.DOFade(0, tweenEndFadeTime);
    }

    private void SetTargetOrder()
    {
        var randomIndex = Random.Range(0, possibleTacoCombinations.Count);
        currentOrder = possibleTacoCombinations[randomIndex];
        foodOrder.sprite = currentOrder.finalTacoWanted;
    }
}

[System.Serializable]
public class TacoOrders
{
    public List<ItemType> ingredientsNeeded;
    public Sprite finalTacoWanted;
}
