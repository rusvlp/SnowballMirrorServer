using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasses
{
    public string id;
    public string name;
    public string status;
    public EmptyPlayer Player;
    
    public Glasses(string id, string name, string status, EmptyPlayer ep)
    {
        this.id = id;
        this.name = name;
        this.status = status;
        this.Player = ep;
    }
}
