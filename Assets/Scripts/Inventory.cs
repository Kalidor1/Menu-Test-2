using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class InventoryItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Property { get; set; }
    public float Value { get; set; }

    public InventoryItem(Item item)
    {
        Name = item.name;
        Description = item.description;
        Property = item.property;
        Value = item.value;
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