using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
   public static Transform herTransorm;
    Vector3 distDir;
    float cosL;
   public static float LOOK;
    // Use this for initialization

    private void Awake()
    {
        
    }

    void Start()
    {

        herTransorm = gameObject.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        distDir= Input.mousePosition - transform.position;
        distDir.Normalize();
        Vector3 rotation_y = new Vector3(0f, Mathf.Atan2(distDir.x/2, distDir.y/2) * Mathf.Rad2Deg,0);
        Vector3 rotation_z = new Vector3( 0f,0f,Mathf.Atan2(distDir.y, -distDir.x ) * Mathf.Rad2Deg);///
        Vector3 cosRot1;
        Vector3 cosRot2;
      
       
        try
        {
            cosRot1 = rotation_y;
            cosRot2 = rotation_z;
            if (distDir.x > 0 && distDir.x<=180)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotation_z.z);
                cosL = (cosRot1.y * cosRot2.z) / Vector3.Normalize(cosRot1).y * Vector3.Normalize(cosRot2).z;
                LOOK = rotation_z.z * rotation_y.y * Mathf.Sin(cosL);
            }

            else if (distDir.x > 180 && distDir.x <= 360)
            {
                float rotation_My = Mathf.Atan2(distDir.y * -1, distDir.x * -1) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, rotation_z.z*-1, rotation_My);
                cosL = (cosRot1.y * cosRot2.z) / Vector3.Normalize(cosRot1).y * Vector3.Normalize(cosRot2).z;
                LOOK = rotation_z.z * rotation_y.y * Mathf.Sin(cosL);
            }

            if (distDir.y >0 || distDir.y<45)
            {
                transform.rotation = Quaternion.Euler(0f, rotation_y.y, 0f);
                cosL = (cosRot1.y * cosRot2.z) / Vector3.Normalize(cosRot1).y * Vector3.Normalize(cosRot2).z;
                LOOK = rotation_z.z * rotation_y.y * Mathf.Sin(cosL);
            }
            
        }
        catch (System.Exception)
        {

            
    }

        finally
        {
            cosRot1 = rotation_y;
            cosRot2 = rotation_z;
            cosL = (cosRot1.y * cosRot2.z) / Vector3.Normalize(cosRot1).y * Vector3.Normalize(cosRot2).z;
            LOOK = rotation_z.z * rotation_y.y * Mathf.Sin(cosL);

            KeyMove();
        }

      
    }


   public static bool KeyMove()
    {
      
        if (Input.GetKey("w") )
        {

            herTransorm.Translate(new Vector3(herTransorm.position.x+1 , 0, 0) * 0.5f * Time.fixedDeltaTime);
            
        }
        

        //else if (Input.GetKey("w") && herTransorm.transform.rotation.z > 180 && herTransorm.transform.rotation.z  <= 360)
        //{
        //    herTransorm.Translate(new Vector3(LOOK, 0, 0) * 0.5f * Time.deltaTime);
        //}
        
        if (Input.GetKey("s")  )
        {
         
            herTransorm.Translate(new Vector3(herTransorm.position.x - 1, 0, 0) * -0.5f * Time.fixedDeltaTime);
            
           
           
        }
        if (Input.GetKey("a"))
        {

            herTransorm.Translate(new Vector3(0, 0, herTransorm.position.x) * 0.5f * Time.fixedDeltaTime);

        }

        if (Input.GetKey("d"))
        {
            herTransorm.Translate(new Vector3(0 , 0, herTransorm.transform.position.x) *- 0.5f * Time.fixedDeltaTime);
            
        }
        return true;
    }
}