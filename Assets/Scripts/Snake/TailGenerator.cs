using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
    [SerializeField] private Segment _segmentTemplate;

    private List<Segment> _tail;

    public List<Segment> Generate(int size)
    {
        _tail = new List<Segment>();

        for (int i = 0; i < size; i++)
        {
            _tail.Add(Instantiate(_segmentTemplate, transform));
        }

        return _tail;
    }
}
