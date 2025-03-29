using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _leftCube;
    [SerializeField] private GameObject _middleCube;
    [SerializeField] private GameObject _rightCube;

    [SerializeField] private float _triggerDistance;
    [SerializeField] private Transform _player;

    private float _offsetX = 0;
    private float _offsetZ = 0;

    private List<GameObject> _cubesInRaw;
    private Vector3 _startPoint;


    void Start()
    {
        _startPoint = transform.position;
        _offsetX = _startPoint.x;
        _offsetZ = _startPoint.z;
        
    }

    void Update()
    {
        if (_offsetZ - _triggerDistance < _player.position.z)
        {
            _offsetZ = SpawnRaw(_offsetX, _offsetZ);
        }
    }

    private float SpawnRaw(float offsetX, float offsetZ)
    {
        _cubesInRaw = new List<GameObject>();   

        for (int i = 0; i < 3; i++) 
        {
            GameObject newCube; 

            if(i == 0)
            {
                newCube = Instantiate(_leftCube);
            }
            else if (i == 1)
            {
                newCube = Instantiate(_middleCube);
            }
            else
            {
                newCube = Instantiate(_rightCube);
            }

            newCube.transform.position = new Vector3(offsetX, 0, offsetZ);

            offsetX += 1;

            _cubesInRaw.Add(newCube);
        }

        int randomIndex = Random.Range(0, _cubesInRaw.Count);
        int randomAction = Random.Range(0, 2);

        if (randomAction == 0)
        {
            _cubesInRaw[randomIndex].GetComponent<CubeController>().RiceCube();
        }
        else
        {
            _cubesInRaw[randomIndex].GetComponent<CubeController>().DropCube();
        }

        return offsetZ + 1;
    }
}
