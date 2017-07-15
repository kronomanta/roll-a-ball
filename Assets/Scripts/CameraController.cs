using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player;
    private Vector3 _offset;

    private Transform _transform;
    private Transform _playerTransform;

	void Start () {
        _transform = transform;
        _playerTransform = Player.transform;
        _offset = _transform.position - _playerTransform.position;
	}
	
	void LateUpdate () {
        _transform.position = _playerTransform.position + _offset;
	}
}
