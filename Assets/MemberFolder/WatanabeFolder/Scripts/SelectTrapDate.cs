using System.Collections;
using UnityEngine;

public class SelectTrapDate
{
    private Sprite trapImage;

    private string trapName;

    public SelectTrapDate(string name)
    {
        trapName = name;
    }

    public string GetTrapName() 
    {
        return trapName;
    }
}
