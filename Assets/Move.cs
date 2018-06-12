using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
   public static Transform herTransorm;
    Vector3 distDir;//расстояние до зеркала
    float cosL;
   public static float LOOK;// направление взгляда камеры ( первый раз инициализируется ниже - в блоке finaly , а потом также используется в блоке try)
    // Use this for initialization

    private void Awake()
    {
        
    }

    void Start()
    {

        herTransorm = gameObject.GetComponent<Transform>();// получаем позицию игрока при старте

    }

    // Update is called once per frame
    void Update()
    {
        distDir= Input.mousePosition - transform.position;//длина поворота(длина вектора движения мыши)
        distDir.Normalize();//модуль
        Vector3 rotation_y = new Vector3(0f, Mathf.Atan2(distDir.x/2, distDir.y/2) * Mathf.Rad2Deg,0);//поворот взгляда по y от длины поворота
        Vector3 rotation_z = new Vector3( 0f,0f,Mathf.Atan2(distDir.y, -distDir.x ) * Mathf.Rad2Deg);//поворот взгляда по z от длины поворота
        Vector3 cosRot1;// первый раз инициализируется ниже - в блоке finaly , а потом также используется в блоке try
        Vector3 cosRot2;// первый раз инициализируется ниже - в блоке finaly , а потом также используется в блоке try


        try
        {
            cosRot1 = rotation_y;//после инициалаизации в блоке finaly
            cosRot2 = rotation_z;//после инициалаизации в блоке finaly
            if (distDir.x > 0 && distDir.x<=180)// на сколько повернулся персонаж по x
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotation_z.z);//поворачиваем персонажа
                cosL = (cosRot1.y * cosRot2.z) / Vector3.Normalize(cosRot1).y * Vector3.Normalize(cosRot2).z;//угол взгляда
                LOOK = rotation_z.z * rotation_y.y * Mathf.Sin(cosL);//окончательное направление взгляда камеры
            }

            else if (distDir.x > 180 && distDir.x <= 360)// на сколько повернулся персонаж по x
            {
                float rotation_My = Mathf.Atan2(distDir.y * -1, distDir.x * -1) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, rotation_z.z*-1, rotation_My);
                cosL = (cosRot1.y * cosRot2.z) / Vector3.Normalize(cosRot1).y * Vector3.Normalize(cosRot2).z;//угол взгляда
                LOOK = rotation_z.z * rotation_y.y * Mathf.Sin(cosL);//окончательное направление взгляда камеры
            }

            if (distDir.y >0 || distDir.y<45)// на сколько повернулся персонаж по y
            {
                transform.rotation = Quaternion.Euler(0f, rotation_y.y, 0f);
                cosL = (cosRot1.y * cosRot2.z) / Vector3.Normalize(cosRot1).y * Vector3.Normalize(cosRot2).z;//угол взгляда
                LOOK = rotation_z.z * rotation_y.y * Mathf.Sin(cosL);//окончательное направление взгляда камеры
            }
            
        }
        catch (System.Exception)
        {

            
    }

        finally
        {
            cosRot1 = rotation_y;// инициализируем(единажды) - первый поворот(псоледующие вовороты будут вычисляться  в try)
            cosRot2 = rotation_z;// инициализируем(единажды) - первый поворот(псоледующие вовороты будут вычисляться  в try)
            cosL = (cosRot1.y * cosRot2.z) / Vector3.Normalize(cosRot1).y * Vector3.Normalize(cosRot2).z;// инициализируем
            LOOK = rotation_z.z * rotation_y.y * Mathf.Sin(cosL);// инициализируем

            KeyMove();
        }

      
    }


   public static bool KeyMove()
    {
      
        if (Input.GetKey("w") )
        {

            herTransorm.Translate(new Vector3(herTransorm.position.x+1 , 0, 0) * 0.5f * Time.fixedDeltaTime);//двигаем персонажа
            
        }
        

        //else if (Input.GetKey("w") && herTransorm.transform.rotation.z > 180 && herTransorm.transform.rotation.z  <= 360)
        //{
        //    herTransorm.Translate(new Vector3(LOOK, 0, 0) * 0.5f * Time.deltaTime);
        //}
        
        if (Input.GetKey("s"))//двигаем персонажа
        {
         
            herTransorm.Translate(new Vector3(herTransorm.position.x - 1, 0, 0) * -0.5f * Time.fixedDeltaTime);
            
           
           
        }
        if (Input.GetKey("a"))//двигаем персонажа
        {

            herTransorm.Translate(new Vector3(0, 0, herTransorm.position.x) * 0.5f * Time.fixedDeltaTime);

        }

        if (Input.GetKey("d"))//двигаем персонажа
        {
            herTransorm.Translate(new Vector3(0 , 0, herTransorm.transform.position.x) *- 0.5f * Time.fixedDeltaTime);
            
        }
        return true;// да - персонаж двигался
    }
}