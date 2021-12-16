using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private int _tailSize;
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSpringiness;
    [SerializeField] private SnakeHead _head;

    private SnakeInput _input;
    private TailGenerator _tailGenerator;
    private List<Segment> _tail;

    public event UnityAction<int> SizeUpdated;
    private void Start()
    {
        _tailGenerator = GetComponent<TailGenerator>();
        _input = GetComponent<SnakeInput>();

        _tail = _tailGenerator.Generate(_tailSize);

        SizeUpdated?.Invoke(_tailSize);
    }

    private void FixedUpdate()
    {
        _head.transform.up = _input.GetDirectionToClick(_head.transform.position);
        Move(_head.transform.position + _head.transform.up *  _speed * Time.fixedDeltaTime);  
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;
    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;
    }

    private void Move(Vector3 nextPosition)
    {
        Vector3 previousPosition = _head.transform.position;

        foreach (var segment in _tail)
        {
            Vector3 targetPosition = previousPosition;
            segment.transform.position = Vector3.Lerp(segment.transform.position, targetPosition, _tailSpringiness * Time.deltaTime);
            previousPosition = segment.transform.position;
        }

        _head.Move(nextPosition);
    }
    
    private void OnBlockCollided()
    {
        if(_tail.Count <= 0)
        {
            Destroy(gameObject);
            return;
        }
        Segment deletedSegment = _tail[_tail.Count - 1];
        _tail.Remove(deletedSegment);
        Destroy(deletedSegment.gameObject);
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnBonusCollected(int bonusSize)
    {
        _tail.AddRange(_tailGenerator.Generate(bonusSize));
        SizeUpdated?.Invoke(_tail.Count);
    }
}
