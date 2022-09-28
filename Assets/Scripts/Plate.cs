using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Item
{
    [SerializeField] List<TacoCombinations> allTacoCombinations;
}

[System.Serializable]
public class TacoCombinations
{
    public List<IngredientTypes> recipeCombinations;
    public Sprite desiredPlateLook;
}
