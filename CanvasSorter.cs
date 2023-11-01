using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
[ExecuteInEditMode]
public class CanvasSorter : MonoBehaviour
{
    [SerializeField] private int _canvasSortOffset = 1;
    private SortingGroup _sortingGroup;
    private Canvas[] _childCanvases;

    private void Awake()
    {
        if (_sortingGroup == null)
        {
            _sortingGroup = GetComponent<SortingGroup>();

            if (_sortingGroup == null)
            {
                Debug.LogError("No Sorting Group for Canvas Sorter to pull from.");
                Destroy(this);
                return;
            }
        }
        
        // TODO: update list on children updated
        _childCanvases = GetComponentsInChildren<Canvas>();
    }

    private void Update()
    {
        foreach (var canvas in _childCanvases)
        {
            canvas.sortingLayerID = _sortingGroup.sortingLayerID;
            canvas.sortingOrder = _sortingGroup.sortingOrder + _canvasSortOffset;
        }
    }
}
