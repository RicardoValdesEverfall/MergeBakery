using System.Collections.Generic;
using GramGames.CraftingSystem.DataContainers;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private GridCell _left;
    [SerializeField] private GridCell _right;
    [SerializeField] private GridCell _up;
    [SerializeField] private GridCell _down;
    [SerializeField] private GridHandler _handler;
    
    public MergableItem _item;
    public bool HasItem;

    public void SpawnItem(NodeContainer item)
    {
	    _handler.ClearCell(this);

        var game = Game.GameInstance; //Ricardo, Dec 9: Fixed to use Game.cs as a singleton instead of expensive object search.
		var obj = Instantiate(game.DraggableObjectPrefab);
	    obj.Configure(item, this);
        HasItem = true;
    }
    
    public void SetHandler(GridHandler handler)
    {
        _handler = handler;
    }

    public void SetItemAssigned(MergableItem item)
    {
	    if (_handler == null)
		    _handler = GetComponentInParent<GridHandler>();
       
        if (_item != null)
            _handler.SetCellState(_item.GetCurrentCell(), true);
      
        _item = item;
        _handler.SetCellState(this, _item != null);
    }

    public void SetNeighbor(GridCell neighbor, MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left:
                _left = neighbor;
                break;
            case MoveDirection.Right:
                _right = neighbor;
                break;
            case MoveDirection.Up:
                _up = neighbor;
                break;
            case MoveDirection.Down:
                _down = neighbor;
                break;
      
        }
    }

    public void ClearItem()
    {
        if (_item != null)
        {
            Destroy(_item.gameObject);
            _item = null;
        }
    }
}

public enum MoveDirection
{
    Left, Right, Up, Down
}
