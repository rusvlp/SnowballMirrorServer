using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Mirror;
using UnityEngine;

public struct DiscoveryResponse : NetworkMessage
{
    public IPEndPoint EndPoint { get; set; }

    public Uri uri;
    
    public string serverId;
    public string operatingSystem;


    public override int GetHashCode()
    {
        int hash = 0;
        foreach (char ch in serverId)
        {
            hash += ch;
        }

        return hash;
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != this.GetType())
        {
            return false;
        }
        
        
        DiscoveryResponse toEquals = (DiscoveryResponse)obj;

        if (GetHashCode() != toEquals.GetHashCode())
        {
            return false;
        }
        
        if (uri != toEquals.uri)
        {
            return false;
        }

        if (serverId != toEquals.serverId)
        {
            return false;
        }

        if (operatingSystem != toEquals.operatingSystem)
        {
            return false;
        }

        return true;
    }
}
