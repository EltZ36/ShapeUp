using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRecipes : MonoBehaviour
{
    [SerializeField]
    private ShapeDatabase shapeDatabase;

    [SerializeField]
    private List<RecipeInfo> shapeRecipesList = new List<RecipeInfo>();

    public ShapeDatabase ShapeDatabase
    {
        get { return shapeDatabase; }
    }

    public Dictionary<(string, string), string> CombineRecipes { get; private set; } =
        new Dictionary<(string, string), string>(new ShapePairComparer());
    public Dictionary<string, (string, string)> SeparateRecipes { get; private set; } =
        new Dictionary<string, (string, string)>();

    void Awake()
    {
        CombineRecipes.Clear();
        SeparateRecipes.Clear();
        foreach (RecipeInfo recipeInfo in shapeRecipesList)
        {
            (string, string) pair = (recipeInfo.shapeA, recipeInfo.shapeB);
            string single = recipeInfo.shapeC;
            if (
                recipeInfo.recipeType == RecipeType.Combine
                || recipeInfo.recipeType == RecipeType.Both
            )
            {
                if (!CombineRecipes.ContainsKey(pair))
                {
                    CombineRecipes.Add(pair, single);
                }
            }
            if (
                recipeInfo.recipeType == RecipeType.Separate
                || recipeInfo.recipeType == RecipeType.Both
            )
            {
                if (!SeparateRecipes.ContainsKey(single))
                {
                    SeparateRecipes.Add(single, pair);
                }
            }
        }
    }

    void Start()
    {
        ShapeManager.Instance.shapeRecipes = this;
    }

    private class ShapePairComparer : IEqualityComparer<(string, string)>
    {
        public bool Equals((string, string) x, (string, string) y)
        {
            return (x.Item1 == y.Item1 && x.Item2 == y.Item2)
                || (x.Item1 == y.Item2 && x.Item2 == y.Item1);
        }

        public int GetHashCode((string, string) pair)
        {
            string[] names = new string[] { pair.Item1, pair.Item2 };
            Array.Sort(names);

            return HashCode.Combine(names[0], names[1]);
        }
    }
}
