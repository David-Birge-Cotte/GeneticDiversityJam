using UnityEngine;
using System.Collections;

public enum ForceType
{
    R_F,
    L_F,
    F_F,
    R_R,
    L_R,
    F_R,
    R_L,
    L_L,
    F_L
} 


[RequireComponent(typeof(Rigidbody2D))]
public class Limb : MonoBehaviour
{
    public Agent _myAgent;
    public ForceType forceType;
    public int forcePower;

    float sTime;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        switch (forceType)
        {
            case ForceType.R_F:
                GetComponent<SpriteRenderer>().color = Color.blue - new Color(0.25f, 0, 0, 0);
                break;
            case ForceType.L_F:
                GetComponent<SpriteRenderer>().color = Color.blue - new Color(0, 0.25f, 0, 0);
                break;
            case ForceType.F_F:
                GetComponent<SpriteRenderer>().color = Color.blue - new Color(0, 0, 0.25f, 0);
                break;
            case ForceType.R_R:
                GetComponent<SpriteRenderer>().color = Color.red - new Color(0.25f, 0, 0, 0); 
                break;
            case ForceType.L_R:
                GetComponent<SpriteRenderer>().color = Color.red - new Color(0, 0.25f, 0, 0); 
                break;
            case ForceType.F_R:
                GetComponent<SpriteRenderer>().color = Color.red - new Color(0, 0, 0.25f, 0);
                break;
            case ForceType.R_L:
                GetComponent<SpriteRenderer>().color = Color.green - new Color(0.25f, 0, 0, 0);
                break;
            case ForceType.L_L:
                GetComponent<SpriteRenderer>().color = Color.green - new Color(0, 0.25f, 0, 0);
                break;
            case ForceType.F_L:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        sTime += Time.fixedDeltaTime;
        float power = Mathf.Abs(Mathf.Sin(sTime)) * forcePower * 30;
        float angle = -1;

        if (_myAgent.closeFood != null)
        {
            angle = Vector2.Angle(this.transform.position, _myAgent.closeFood.transform.position);
        }

        //Vector(x2 - x1, y2 - y1)
        Vector2 dir = new Vector2(_myAgent.closeFood.transform.position.x - transform.position.x, _myAgent.closeFood.transform.position.y - transform.position.y);
        dir.Normalize();

        switch (forceType)
        {
            case ForceType.R_F:
                rb.AddForce(Vector2.right * power * Mathf.Cos(angle * Mathf.Deg2Rad), ForceMode2D.Force);
                break;
            case ForceType.L_F:
                rb.AddForce(Vector2.left * power * Mathf.Cos(angle * Mathf.Deg2Rad), ForceMode2D.Force);
                break;
            case ForceType.F_F:
                rb.AddForce(Vector2.up * power * Mathf.Cos(angle * Mathf.Deg2Rad), ForceMode2D.Force);
                break;
            case ForceType.R_R:
                rb.AddForce(Vector2.down * power * Mathf.Cos(angle * Mathf.Deg2Rad), ForceMode2D.Force);
                break;
            case ForceType.L_R:
                rb.AddForce(Vector2.right * power * Mathf.Cos(angle * Mathf.Deg2Rad), ForceMode2D.Force);
                break;
            case ForceType.F_R:
                rb.AddForce(Vector2.left * power * Mathf.Cos(angle * Mathf.Deg2Rad), ForceMode2D.Force);
                break;
            case ForceType.R_L:
                rb.AddForce(Vector2.up * power * Mathf.Cos(angle * Mathf.Deg2Rad), ForceMode2D.Force);
                break;
            case ForceType.L_L:
                rb.AddForce(Vector2.down * power * Mathf.Cos(angle * Mathf.Deg2Rad), ForceMode2D.Force);
                break;
            case ForceType.F_L:
                rb.AddForce(dir * power, ForceMode2D.Force); // cheataay
                break;
            default:
                break;
        }
    }
}
