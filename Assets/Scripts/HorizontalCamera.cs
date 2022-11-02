using UnityEngine;

public class HorizontalCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_target.position.x, transform.position.y, transform.position.z); 
    }
}
