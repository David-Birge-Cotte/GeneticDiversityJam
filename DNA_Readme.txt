0000 : Nouveau membre [à la suite] (et fin de membre) 
0001 : Sin(angleEnRad) * power
0010 : Cos(angleEnRad) * power
0011 : 
0100 : 
0101 : 
0110 : 
0111 : 
1000 : 
1001 : 
1010 : 
1011 : 
1100 : 
1101 : 
1110 : 
1111 : Nouveau membre [sur le corps] (et fin de membre) 

















[OLD]
0000 : Nouveau membre / fin de membre
0001 : force vers la droite quand bouffe devant
0010 : force vers la gauche quand bouffe devant
0011 : force vers l'avant quand bouffe devant
0100 : force vers la droite quand bouffe à droite
0101 : force vers la gauche quand bouffe à droite
0110 : force vers l'avant quand bouffe à droite
0111 : force vers la droite quand bouffe à gauche
1000 : force vers la gauche quand bouffe à gauche
1001 : force vers l'avant quand bouffe à gauche
1010 : + de force
1011 : - de force
1100 : + de force
1101 : - de force
1110 : + de force
1111 : Fin d'un membre / début de membre


SAUVEGARDE
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




OLD SAUVEGARDE
switch (forceType)
        {
            case ForceType.R_F:
                if(angle > 90 && angle < 360)
                {
                    rb.AddForce(Vector2.right * power, ForceMode2D.Force);
                }
                break;
            case ForceType.L_F:
                if (angle > 90 && angle < 360)
                {
                    rb.AddForce(Vector2.left * power, ForceMode2D.Force);
                }
                break;
            case ForceType.F_F:
                if (angle > 90 && angle < 360)
                {
                    rb.AddForce(Vector2.up * power, ForceMode2D.Force);
                }
                break;
            case ForceType.R_R:
                if (angle > 180 || angle < 90)
                {
                    rb.AddForce(Vector2.right * power, ForceMode2D.Force);
                }
                break;
            case ForceType.L_R:
                if (angle > 180 || angle < 90)
                {
                    rb.AddForce(Vector2.left * power, ForceMode2D.Force);
                }
                break;
            case ForceType.F_R:
                if (angle > 180 || angle < 90)
                {
                    rb.AddForce(Vector2.up * power, ForceMode2D.Force);
                }
                break;
            case ForceType.R_L:
                if (angle > 0 || angle < 180)
                {
                    rb.AddForce(Vector2.right * power, ForceMode2D.Force);
                }
                break;
            case ForceType.L_L:
                if (angle > 0 || angle < 180)
                {
                    rb.AddForce(Vector2.left * power, ForceMode2D.Force);
                }
                break;
            case ForceType.F_L:
                if (angle > 0 || angle < 180)
                {
                    rb.AddForce(Vector2.up * power, ForceMode2D.Force);
                }
                break;
            default:
                break;
        }