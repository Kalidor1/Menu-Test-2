using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>(); // List of items in the inventory
    public TextMeshProUGUI inventoryText;

    void Update()
    {
        // Update the inventory text
        inventoryText.text = "Inventory: ";
        foreach (string item in items)
        {
            inventoryText.text += item + ", ";
        }
    }

    public void AddItem(string itemToAdd)
    {
        items.Add(itemToAdd);
    }

    public void RemoveItem(string itemToRemove)
    {
        items.Remove(itemToRemove);
    }
}