using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    const string GROUND_TAG = "Ground";

    public BouncingMan BouncingMan;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100) && hit.collider.CompareTag(GROUND_TAG))
        {
            Debug.Log("MouseDownClick");
            BouncingMan.JumpToPosition(hit.point);
        }
    }
}
