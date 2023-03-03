using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    public PlayerMovementScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.customerCount == 1)
        {
            Destroy(gameObject);
        }
    }
}
