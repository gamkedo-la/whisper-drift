using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hopAround : MonoBehaviour
{
    public float jumpPower = 10f;
    public float jumpRandomness = 10f;
    public float jumpMinDelay = 1.0f;
    public float jumpMaxDelay = 4.0f;
    
    private Rigidbody2D rb;        

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RandomJump());
    }

 
    IEnumerator RandomJump(){
        while (true) {
            Debug.Log("Jump!");
            rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
            rb.AddForce(transform.right * (jumpRandomness*2*Random.value-jumpRandomness), ForceMode2D.Impulse);
            yield return new WaitForSeconds(Random.Range(jumpMinDelay,jumpMaxDelay));
        }
    }

}
