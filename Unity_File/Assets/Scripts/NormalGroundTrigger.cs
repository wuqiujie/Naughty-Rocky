using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGroundTrigger : MonoBehaviour
{
    //Range for raise ground
    public Vector2 xRange;

    //Range for raise amount
    public float raiseAmount;

    float playerHeight;

    private void Start()
    {
        playerHeight = GameManager.Instance.Player.transform.position.y;    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Transform rigTransform = other.transform.parent.parent;

            float raiseAmount = GetRaiseAmount(rigTransform.position) + playerHeight;
            
            rigTransform.position = new Vector3(rigTransform.position.x, raiseAmount, rigTransform.position.z);
        }
    }

    float GetRaiseAmount(Vector3 PlayerPosition)
    {
        float center = transform.position.x;
        if (PlayerPosition.x < center + xRange.x && PlayerPosition.x > center - xRange.y)
        {
            float current = PlayerPosition.x - (center + xRange.x);
            float total = (center + xRange.x) - (center - xRange.y);
            float percentage = current / total;
            return Mathf.SmoothStep(0, raiseAmount, Mathf.Abs(percentage));
        }

        if(PlayerPosition.x < center - xRange.y)
        {
            return raiseAmount;
        }
        return 0;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, 0.3f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center + new Vector3(xRange.x, 0, 0), 0.3f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center - new Vector3(xRange.y, 0, 0), 0.3f);
    }
}
