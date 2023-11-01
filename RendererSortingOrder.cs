using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererSortingOrder : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _sortingOrder;

    private void Start()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();
        _renderer.sortingOrder = 1;
    }
}
