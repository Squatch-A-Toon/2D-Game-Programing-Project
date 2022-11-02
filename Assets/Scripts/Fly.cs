using UnityEngine;

public class Fly : MonoBehaviour
{
    Vector2 _startingPosition;
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _maxDistance = 2;
    [SerializeField] float _speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
        var distance = Vector2.Distance(_startingPosition, transform.position);
        if(distance >= _maxDistance)
        {
            transform.position = _startingPosition + (_direction.normalized * _maxDistance);
            _direction *= -1;
        }
    }

    
}
