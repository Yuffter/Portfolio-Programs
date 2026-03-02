using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreaturePanelInitialization : MonoBehaviour
{
    [HideInInspector]
    public int id;

    TextMeshProUGUI nameLabel;
    Image image;

    private void Awake()
    {
        nameLabel = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        image = transform.Find("Image").GetComponent<Image>();
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
        nameLabel.text = CreaturesData.Instance.dCreaturesData[id].name;
        image.sprite = CreaturesData.Instance.dCreaturesData[id].sprite;
    }
}
