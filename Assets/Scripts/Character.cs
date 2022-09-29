using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField] List<TacoOrders> possibleTacoCombinations;
    [SerializeField] List<Sprite> characterOptions;
    SpriteRenderer spriteRenderer;

    [SerializeField] float tweenEndScale = 1f;
    [SerializeField] float tweenEndScaleTime = 3f;

    [SerializeField] float tweenEndLocalMoveY = 5f;
    [SerializeField] float tweenLocalMoveYTime = 3f;

    [SerializeField] float tweenEndFade = 1f;
    [SerializeField] float tweenEndFadeTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TweenAnimation();
    }

    private void TweenAnimation()
    {
        transform.DOScale(tweenEndScale, tweenEndScaleTime);
        transform.DOLocalMoveY(tweenEndLocalMoveY, tweenLocalMoveYTime);
        spriteRenderer.DOFade(tweenEndFade, tweenEndFadeTime);
    }
}

[System.Serializable]
public class TacoOrders
{
    public List<IngredientTypes> ingredientsNeeded;
    public Sprite finalTacoWanted;
}
