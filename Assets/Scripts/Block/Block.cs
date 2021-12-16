using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceRange;
    [Header("Colors by ascending block value")]
    [SerializeField] private Color[] _colors;

    private int _destroyPrice;
    private int _filling = 0;
    private SpriteRenderer _render;

    private int _leftToFill => _destroyPrice - _filling;

    public event UnityAction<int> FillingUpdated;

    private void Start()
    {
        _render = GetComponent<SpriteRenderer>();

        _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y + 1);
        FillingUpdated?.Invoke(_leftToFill);
        SetColor();
    }

    public void Fill()
    {
        _filling++;

        if(_leftToFill == 0)
        {
            Destroy(gameObject);
        }

        FillingUpdated?.Invoke(_leftToFill);
    }
    
    private void SetColor()
    {
        if(_destroyPrice <= 7)
        {
            _render.color = _colors[0];
        }
        else if(_destroyPrice <= 14){
            _render.color = _colors[1];
        }
        else if (_destroyPrice <= 21)
        {
            _render.color = _colors[2];
        }
        else if (_destroyPrice <= 28)
        {
            _render.color = _colors[3];
        }
        else if (_destroyPrice <= 35)
        {
            _render.color = _colors[4];
        }
        else if (_destroyPrice <= 42)
        {
            _render.color = _colors[5];
        }
        else if (_destroyPrice <= 50)
        {
            _render.color = _colors[6];
        }
    }
}
