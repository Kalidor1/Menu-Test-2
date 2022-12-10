using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchforkController : MonoBehaviour
{
    //cooldown in seconds
    public float coolDown = 0.5f;
    private float _canAttack = -1f;
    
    private bool _isExtracting = false;

    private float currentExtraction = 0f;
    private float maxExtraction = 0.5f;
    private float _slashSpeed = 0.1f;

    private Vector3 initialPosition ;

    void Start() {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > _canAttack)
        {
            _canAttack = Time.time + coolDown;
            _isExtracting = true;
            transform.localPosition += new Vector3(0f, 0.5f, 0f);
        }
    }

    void FixedUpdate() {
        if(_isExtracting && currentExtraction < maxExtraction)
        {
            currentExtraction += _slashSpeed;
            transform.localPosition += new Vector3(0f, _slashSpeed, 0f);
        }
        else if(_isExtracting)
        {
            _isExtracting = false;
            currentExtraction = 0f;
            transform.localPosition = initialPosition;
        }
    }
}
