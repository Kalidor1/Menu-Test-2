using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>(); // List of items in the inventory

    public void AddItem(string itemToAdd)
    {
        items.Add(itemToAdd);
    }

    public void RemoveItem(string itemToRemove)
    {
        items.Remove(itemToRemove);
    }
}