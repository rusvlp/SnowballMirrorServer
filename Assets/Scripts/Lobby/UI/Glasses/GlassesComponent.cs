using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UnityMirrorExample.Server
{
    public class GlassesComponent : MonoBehaviour
    {
        public string Id { get; private set; }
        public string Name { get; protected set; }
        public string Status { get; private set; }
        

        [SerializeField] protected TMP_Text text_id;
        [SerializeField] protected GameObject text_name;
        [SerializeField] protected TMP_Text text_status;

        public void SetFields(string id, string name, string status)
        {
            Id = id;
            Name = name;
            Status = status;

            text_id.text = id.ToString();
            text_name.GetComponent<TMP_Text>().text = name;
            text_status.text = status;
        }
        
    }
}

