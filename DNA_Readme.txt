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