using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDistortionScript : MonoBehaviour {

    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetColor("_Color", Color.green);
        Graphics.Blit(source, destination, mat);
    }
}
