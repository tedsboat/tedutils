using System;
using UnityEngine;

namespace Agricosmic.Utilities
{
    public class CameraPostProcessMaterial : MonoBehaviour
    {
        [SerializeField] private Material _material;
        
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, _material);
        }
    }
}