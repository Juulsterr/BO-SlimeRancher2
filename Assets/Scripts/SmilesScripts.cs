using System.Collections.Generic;
using UnityEngine;

public class SmilesScripts : MonoBehaviour
{
    [SerializeField] private bool isVacuumed = false;
    public bool IsVacuumed
    {
        get { return isVacuumed; }
        set { isVacuumed = value; }
    }
    [SerializeField] private float moveSpeed = 3.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}