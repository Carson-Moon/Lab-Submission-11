using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Inventory list
    private List<InventoryItem> inventory = new List<InventoryItem>();


    private void Start()
    {
        // Hold one id to find later as an example.
        int targetID = 0;

        // Populate our inventory with items.
        inventory.Add(new InventoryItem(targetID = RandomInt(), "Apple", RandomInt()));
        inventory.Add(new InventoryItem(RandomInt(), "Potion", RandomInt()));
        inventory.Add(new InventoryItem(RandomInt(), "Bow", RandomInt()));
        inventory.Add(new InventoryItem(RandomInt(), "Bread", RandomInt()));
        inventory.Add(new InventoryItem(RandomInt(), "Soda", RandomInt()));
        inventory.Add(new InventoryItem(RandomInt(), "Water", RandomInt()));
        inventory.Add(new InventoryItem(RandomInt(), "Orange", RandomInt()));
        inventory.Add(new InventoryItem(RandomInt(), "Shield", RandomInt()));
        inventory.Add(new InventoryItem(RandomInt(), "Arrow", RandomInt()));
        inventory.Add(new InventoryItem(RandomInt(), "Sword", RandomInt()));
        PrintInventory();

        // Perform our linear search.
        string target = "Orange";
        InventoryItem item = LinearSearchByName(target);
        if(item != null)
        {
            print($"We found {item.GetName()} in our current inventory!");
        }
        else
        {
            print($"We did not find {target} in our current inventory.");
        }

        target = "Pizza";
        item = LinearSearchByName(target);
        if(item != null)
        {
            print($"We found {item.GetName()} in our current inventory!");
        }
        else
        {
            print($"We did not find {target} in our current inventory.");
        }

        // Perform our quicksort by id.
        QuickSortID(inventory, 0, inventory.Count - 1);
        PrintInventory();

        // Perform our binary search by id.
        item = BinarySearchID(inventory, targetID);
        if(item != null)
        {
            print($"We found {targetID} ({item.GetName()}) in our current inventory!");
        }
        else
        {
            print($"We did not find {targetID} in our current inventory.");
        }

        int randomID = RandomInt();
        item = BinarySearchID(inventory, randomID);
        if(item != null)
        {
            print($"We found {targetID} ({item.GetName()}) in our current inventory!");
        }
        else
        {
            print($"We did not find {randomID} in our current inventory.");
        }

        // Sort our inventory by value.
        QuickSortValue(inventory, 0, inventory.Count - 1);
        PrintInventory();
    }

    // Get a random integer.
    private int RandomInt()
    {
        return Random.Range(0, 500);
    }

    // Print out our current inventory.
    private void PrintInventory()
    {
        // Loop through our entire inventory.
        for(int i=0; i<inventory.Count; i++)
        {
            // Print this item out!
            print(inventory[i].GetName() + "[" + inventory[i].GetId().ToString() + "] " + "[" + inventory[i].GetValue().ToString() + "]");
        }
    }

    // Search linearly by name.
    private InventoryItem LinearSearchByName(string itemName)
    {
        // Iterate through our entire inventory.
        for(int i=0; i<inventory.Count; i++)
        {
            // If the name matches, we have found our item!
            if(inventory[i].GetName() == itemName)
            {
                return inventory[i];
            }
        }

        // If we get here, we did not find our item.
        return null;
    }

#region Quicksort by Value
    // Sort our inventory by their values. (low -> high)
    private int PartitionValue(List<InventoryItem> array, int first, int last)
    {
        int pivot = array[last].GetValue();
        int smaller = first - 1;

        for(int i=first; i<last; i++)
        {
            // If our i value is smaller than our last value...
            if(array[i].GetValue() < pivot)
            {
                smaller++;

                // Swap our smaller value with our i value.
                InventoryItem temp = array[smaller];
                array[smaller] = array[i];
                array[i] = temp; 
            }
        }

        InventoryItem temp2 = array[smaller + 1];
        array[smaller + 1] = array[last];
        array[last] = temp2;

        return smaller + 1;
    }

    private void QuickSortValue(List<InventoryItem> array, int first, int last)
    {
        if(first < last)
        {
            int pivot = PartitionValue(array, first, last);

            QuickSortValue(array, first, pivot - 1);
            QuickSortValue(array, pivot + 1, last);
        }
    }

#endregion

#region Quicksort by ID
    // Sort our inventory by their ids. (low -> high)
    private int PartitionID(List<InventoryItem> array, int first, int last)
    {
        int pivot = array[last].GetId();
        int smaller = first - 1;

        for(int i=first; i<last; i++)
        {
            // If our i value is smaller than our last value...
            if(array[i].GetId() < pivot)
            {
                smaller++;

                // Swap our smaller value with our i value.
                InventoryItem temp = array[smaller];
                array[smaller] = array[i];
                array[i] = temp; 
            }
        }

        InventoryItem temp2 = array[smaller + 1];
        array[smaller + 1] = array[last];
        array[last] = temp2;

        return smaller + 1;
    }

    private void QuickSortID(List<InventoryItem> array, int first, int last)
    {
        if(first < last)
        {
            int pivot = PartitionID(array, first, last);

            QuickSortID(array, first, pivot - 1);
            QuickSortID(array, pivot + 1, last);
        }
    }

#endregion

#region Binary Search by ID
    // Find an item by its id.
    private InventoryItem BinarySearchID(List<InventoryItem> array, int targetID)
    {
        int left = 0;
        int right = array.Count - 1;

        // While we have not searched through the entire list...
        while(left <= right)
        {
            // Grab our midpoint.
            int mid = left + (right - left) / 2;

            // Determine if our midpoint is our id, less than our id, or greater than our id.
            // Shift our midpoint to cut out the appropriate part of the list.
            if(array[mid].GetId() == targetID)
            {
                return array[mid];
            }
            else if(array[mid].GetId() < targetID)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        // If we get here, we did not find our items id.
        return null;
    }

#endregion
}
