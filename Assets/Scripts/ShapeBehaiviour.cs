using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBehaiviour : MonoBehaviour
{

    public static void CreateShape(Vector3 position, ColorSO colorSO, Transform prefab, float speed)
    {
        var shape = Instantiate(prefab, position, Quaternion.identity);

        // Sets color of the shape
        ShapeBehaiviour shapeScript = shape.GetComponent<ShapeBehaiviour>();
        shapeScript.SetColor(colorSO);
        shapeScript.SetSpeed(speed);
    }

    private Transform targetPosition = null;
    private ColorSO colorSO;
    private float moveSpeed;

    private Vector3 refVel;
    private void Update()
    {
        // Move to a player if shape is in a radius of player's magnet
        if(targetPosition != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition.position, ref refVel, Time.deltaTime * 10f);
        }
        else
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);
        }

        // Destroys shape if it's out of bounds
        if (transform.position.y < -5.5f) Destroy(gameObject);
    }
    public void SetColor(ColorSO colorSO)
    {
        this.colorSO = colorSO;

        GetComponent<SpriteRenderer>().color = colorSO.color;
        if(colorSO.nameString == "Rainbow")
        {
            GetComponent<Animator>().SetBool("IsRainbow", true);
        }
    }
    public ColorSO GetColorSO()
    {
        return colorSO;
    }
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
    public void SetTarget(Transform newTarget)
    {
        targetPosition = newTarget;
    }
}
