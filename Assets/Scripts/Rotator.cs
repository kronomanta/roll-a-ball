using UnityEngine;

public class Rotator : MonoBehaviour
{

    private Transform _transform;
    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        _transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
