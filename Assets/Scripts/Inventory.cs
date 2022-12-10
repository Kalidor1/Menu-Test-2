using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // List of items in the inventory

    public void AddItem(Item itemToAdd)
    {
        items.Add(itemToAdd);
    }

    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
    }
}