using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialChanger : MonoBehaviour
{
    public Material[] materials;    
    public Button[] buttons;        

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; 
            buttons[i].onClick.AddListener(() => ChangeMaterial(index));
        }
    }

    public void ChangeMaterial(int index)
    {
        if (index >= 0 && index < materials.Length)
        {
            meshRenderer.material = materials[index];
        }
    }
}
