using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceRange;

    private int _destroyPrice;
    private int _filling = 0;

    private int _leftToFill => _destroyPrice - _filling;

    public event UnityAction<int> FillingUpdated;

    private void Start()
    {
        _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y + 1);
        FillingUpdated?.Invoke(_leftToFill);
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
}
