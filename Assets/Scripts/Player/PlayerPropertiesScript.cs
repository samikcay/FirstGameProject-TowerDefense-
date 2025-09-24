using UnityEngine;

public class PlayerPropertiesScript : MonoBehaviour
{
    private int gold = 0;
    public int Gold 
    { 
        get { return gold; } 
        set { gold = value; } 
    }
}
