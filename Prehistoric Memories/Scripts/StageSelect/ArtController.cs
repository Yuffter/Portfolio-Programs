using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArtFocusController))]
[RequireComponent(typeof(ComeInArtController))]
[RequireComponent(typeof(ComeInArtAnimation))]
public class ArtController : MonoBehaviour
{
    public int artNumber { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetArtNumber(int num)
    {
        artNumber = num;
    }
}
