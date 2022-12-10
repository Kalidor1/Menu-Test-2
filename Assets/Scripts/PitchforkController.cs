using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchforkController : MonoBehaviour
{
    //cooldown in seconds
    private float _canAttack = -1f;

    public bool isExtracting = false;

    private float currentExtraction = 0f;
    private float _slashSpeed = 0.1f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton5)) && Time.time > _canAttack)
        {
            _canAttack = Time.time + GameController.Instance.attackSpeed;
            isExtracting = true;
            transform.localPosition += new Vector3(0f, 0.5f, 0f);
        }
    }

    void FixedUpdate()
    {
        if (isExtracting && currentExtraction < GameController.Instance.attackRange)
        {
            currentExtraction += _slashSpeed;
            transform.localPosition += new Vector3(0f, _slashSpeed, 0f);
        }
        else if (isExtracting)
        {
            isExtracting = false;
            currentExtraction = 0f;
            transform.localPosition = initialPosition;
        }
    }
}
