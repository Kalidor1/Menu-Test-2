using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class InventoryItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Property { get; set; }
    public float Value { get; set; }
    public Sprite PopUp { get; set; }
    public Sprite Icon { get; set; }

    public InventoryItem(Item item, Sprite icon)
    {
        Name = item.name;
        Description = item.description;
        Property = item.property;
        Value = item.value;
        PopUp = item.popUp;
        Icon = icon;
    }
}

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>(); // List of items in the inventory

    public void AddItem(InventoryItem itemToAdd)
    {
        items.Add(itemToAdd);
    }

    public void RemoveItem(InventoryItem itemToRemove)
    {
        items.Remove(itemToRemove);
    }
}