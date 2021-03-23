using System;
using System.Collections.Generic;

[Serializable]
public class CheckpointData
{
    public static CheckpointData Current = new CheckpointData();
    public static CheckpointData LastSave;

    public List<int> DefeatedEnemies = new List<int>();
    
    public List<InventoryStorage> InventoryItems;
    public float Money;

    public static void RestoreLastSave()
    {
        Current = LastSave;
    }

    public static void SaveState()
    {
        LastSave = Current;
    }

    public static void Reset()
    {
        Current = null;
        LastSave = null;
    }
}