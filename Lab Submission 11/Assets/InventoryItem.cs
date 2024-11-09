using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    // Inventory Attributes
    private int id;
    private string name;
    private int value;

    // Constructor
    public InventoryItem(int id, string name, int value)
    {
        this.id = id;
        this.name = name;
        this.value = value;
    }

    // Getters
    public int GetId()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public int GetValue()
    {
        return value;
    }
}
