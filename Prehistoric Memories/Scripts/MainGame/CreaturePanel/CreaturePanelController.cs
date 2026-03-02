using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CreaturePanelPointer))]
[RequireComponent(typeof(CreaturePanelInitialization))]
public class CreaturePanelController : MonoBehaviour
{
    CreaturePanelPointer creaturePanelPointer;
    CreaturePanelInitialization creaturePanelInitialization;
    public int id;

    private void Awake()
    {
        creaturePanelPointer = GetComponent<CreaturePanelPointer>();
        creaturePanelInitialization = GetComponent<CreaturePanelInitialization>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateContent()
    {
        creaturePanelPointer.id = id;
        creaturePanelInitialization.id = id;

        creaturePanelInitialization.UpdateContent();
        creaturePanelPointer.UpdateContent();
    }
}
