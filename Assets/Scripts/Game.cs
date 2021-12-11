﻿using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Game : MonoBehaviour
{

    private static Game _gameInstance;
    public static Game GameInstance { get { return _gameInstance; } }

    public MergableItem DraggableObjectPrefab;
    public GridHandler MainGrid;

    [Range(0f, 1f)] public float itemDensity;


    private List<string> ActiveRecipes = new List<string>();

    private void Awake()
    {
        if (_gameInstance && _gameInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _gameInstance = this;
        }

        Screen.fullScreen = false; // https://issuetracker.unity3d.com/issues/game-is-not-built-in-windowed-mode-when-changing-the-build-settings-from-exclusive-fullscreen

        // load all item definitions
        ItemUtils.InitializeMap();

        //Ricardo Valdes, Dec. 7th: Added this log to print a line to console each time the Game.cs class is initialized. 
        Debug.Log("Game Instance Created at ", gameObject);
    }

    private void Start()
    {
        ReloadLevel(1);
    }

    public void ReloadLevel(int difficulty = 1)
    {
        // clear the board
        var fullCells = MainGrid.GetFullCells.ToArray();
        for (int i = fullCells.Length - 1; i >= 0; i--)
            MainGrid.ClearCell(fullCells[i]);

        // choose new recipes
        ActiveRecipes.Clear();
        difficulty = Mathf.Max(difficulty, 1);
        for (int i = 0; i < difficulty; i++)
        {
            // a 'recipe' has more than 1 ingredient, else it is just a raw ingredient.
            var recipe = ItemUtils.RecipeMap.FirstOrDefault(kvp => kvp.Value.Count > 1).Key;
            ActiveRecipes.Add(recipe);
        }

        // populate the board
        GridCell[] emptyCells = MainGrid.GetEmptyCells.ToArray();
        int itemsToSpawn = (int)(emptyCells.ToArray().Length * itemDensity);

        Debug.Log("Number of items that will be spawned: " + itemsToSpawn);

            for (int i = 0; i < emptyCells.ToArray().Length; i++)
            {
                GridCell cell = emptyCells[i];
                if (itemsToSpawn > 0 && Random.Range(0.0f,1.0f) <= itemDensity)
                {
                    var chosenRecipe = ActiveRecipes[Random.Range(0, ActiveRecipes.ToArray().Length)];
                    var ingredients = ItemUtils.RecipeMap[chosenRecipe].ToArray();
                    var ingredient = ingredients[Random.Range(0, ingredients.Length)];
                    var item = ItemUtils.ItemsMap[ingredient.NodeGUID];
                    cell.SpawnItem(item);
                    Debug.Log("spawned");
                    itemsToSpawn--;
                }
            }
        }
    }

