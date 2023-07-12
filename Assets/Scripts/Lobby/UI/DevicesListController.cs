using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Mirror;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityMirrorExample.Server;

public class DevicesListController : MonoBehaviour
{
    public GameObject panelTitle;
    public GameObject panelPrefab;

    public GameObject k_panelPrefab;

    public GameObject parent;

    public float yDistance;

    public GameObject knownSignPrefab;
    public GameObject unknownSignPrefab;

    private int minSiblingIndex = 0;

    
    private GameObject knownSign;

    
    //singleton
   public static DevicesListController Instance;
    
    List<Glasses> unknownGlasses = new List<Glasses>()
    {
        

    };

    List<Glasses> knownGlasses = new List<Glasses>()
    {
        
    };

    //[SerializeField] TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
        //Vector2 position = new Vector2(panelTitle.transform.position.x, panelTitle.transform.position.y - yDistance);
        GameObject instance = panelTitle;
        Vector2 position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
        instance = Instantiate(unknownSignPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);

        int index = 0;
        #region Commented
        //foreach (Glasses g in unknownGlasses)
        //{
        //    position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
        //    instance = Instantiate(panelPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);
        //    var glassComponent = instance.GetComponent<UGlassesComponent>();

        //    instance.GetComponent<UGlassesComponent>().DevicesListController = this;
        //    instance.GetComponent<UGlassesComponent>().Instance = instance;
        //    glassComponent.SetFields(g.id, g.name, g.status);
        //    glassComponent.Button.GetComponent<Button>().onClick.AddListener(glassComponent.EditName);
        //    glassComponent.SaveButton.GetComponent<Button>().onClick.AddListener(glassComponent.FinishEdit);
        //    print(index);
        //    index++;

        //    elementsInList.Add(instance);
        //}
        #endregion



        position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
        instance = Instantiate(knownSignPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);
        knownSign = instance;

        #region Commented
        //foreach (Glasses g in knownGlasses)
        //{
        //    position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
        //    instance = Instantiate(k_panelPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);
        //    var glassComponent = instance.GetComponent<GlassesComponent>();
        //    glassComponent.SetFields(g.id, g.name, g.status);


        //    elementsInList.Add(instance);
        //}
        #endregion

        //NetworkManager = CustomNetworkManager.Instance;
       // CustomNetworkManager.Instance.devicesListController = this;

       /*
       EmptyPlayer ep1 = new EmptyPlayer()
       {
            PlayerIndex = 1,
            Fingerprint = "aabbc"
       };

       EmptyPlayer ep2 = new EmptyPlayer()
       {
           PlayerIndex = 2,
           Fingerprint = "asd"
       };
    
       AddPlayer(ep1);
       AddPlayer(ep2);
       */
    }

    private void getConnetctions()
    {
        
    }

    /*
    public void AddConnection(NetworkConnection conn)
    {
        Glasses g = new Glasses(conn.connectionId + "", "Новое устройство", "Новое");
        unknownGlasses.Add(g);
        GameObject instance = Instantiate(panelPrefab, parent.transform);

        var glassComponent = instance.GetComponent<UGlassesComponent>();


        instance.GetComponent<UGlassesComponent>().DevicesListController = this;
        instance.GetComponent<UGlassesComponent>().Instance = instance;
        glassComponent.SetFields(g.id, g.name, g.status);
        glassComponent.Button.GetComponent<Button>().onClick.AddListener(glassComponent.EditName);
        glassComponent.SaveButton.GetComponent<Button>().onClick.AddListener(glassComponent.FinishEdit);
        knownSign.transform.SetSiblingIndex(knownSign.transform.GetSiblingIndex() + 1);
    }
    */
    
    public void AddPlayer(EmptyPlayer player)
    {
       
        string name = PlayerPrefs.GetString(player.Fingerprint, null);

      //  print(player.Fingerprint + " is trying to connect");
      //  print(name + " it can be a name");

        if (name != "")
        {
            Glasses kGlasses = new Glasses(player.Fingerprint, name, "Подключено", player);
            PutToKnown(kGlasses);

        } else
        {
            Glasses g = new Glasses(player.Fingerprint, "Новое устройство", "Новое", player);
            PutToUnknown(g);
            
        }
        

    }

    private void editName(GameObject gameObject, int id)
    {
        print("Editing Glasses with index " + id);
        print("Instance is " + gameObject.GetHashCode());
        //throw new NotImplementedException();
    }


    public void SetKnown(GameObject instance, GameObject lastInstance)
    {

        print("Setting Known");
        /*int minSiblingIndex = 0;
        foreach (GameObject go in elementsInList)
        {
            go.transform.SetSiblingIndex(go.transform.GetSiblingIndex() - 1);

            print(go.transform.GetSiblingIndex());

            if (minSiblingIndex < go.transform.GetSiblingIndex())
            {
                minSiblingIndex = go.transform.GetSiblingIndex();
            }
        } */

        instance.transform.SetSiblingIndex(--minSiblingIndex);

        string name = instance.GetComponent<UGlassesComponent>().Name;
        string fingerprint = instance.GetComponent<UGlassesComponent>().Id;

        PlayerPrefs.SetString(fingerprint, name);
        print(PlayerPrefs.GetString(fingerprint).ToString());
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private void PutToKnown(Glasses kGlasses)
    {
        GameObject kInstance = Instantiate(k_panelPrefab, parent.transform);
        var kGlassComponent = kInstance.GetComponent<GlassesComponent>();
        kGlassComponent.SetFields(kGlasses.id, kGlasses.name, kGlasses.status);

        kInstance.transform.SetSiblingIndex(knownSign.transform.GetSiblingIndex() + 1);
    }

    private void PutToUnknown(Glasses g)
    {
        GameObject instance = Instantiate(panelPrefab, parent.transform);

        var glassComponent = instance.GetComponent<UGlassesComponent>();
        

        instance.GetComponent<UGlassesComponent>().DevicesListController = this;
        instance.GetComponent<UGlassesComponent>().Instance = instance;
        glassComponent.SetFields(g.id, g.name, g.status);
        glassComponent.Button.GetComponent<Button>().onClick.AddListener(glassComponent.EditName);
        glassComponent.SaveButton.GetComponent<Button>().onClick.AddListener(glassComponent.FinishEdit);


        instance.transform.SetSiblingIndex(1);
        
        g.Player.SetKnown();
    }

    void Update()
    {
       
    }
}
