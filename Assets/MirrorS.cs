using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorS : MonoBehaviour
{
    public Camera MCamera;// текущая камера(камера зеркала)
    RenderTexture textureForMirror;//текстура для отображения 
    float[] tempArray; //для хранения возвращаемых значения 
   static float PersonHeight;// высота персонажа
    float heigth_Texture;// искомая высота
  public static  Vector3 LookFor_Camera_T;// длина направления взглада (направление взгляда из камеры на основане)(точка взгляда и длина до нее)
    Vector3 Gipotinuza_Of_Heighth_T1;
  public static  float DeltaX; //изминение передвижения персонажа
   public static float sinCamera;//синус взгляда камеры
   public static float cosCamera;//косинус взглада камеры
    static float Sin_For_Plane;//искомый синус при основании
    float Cos_For_Plane;// тскомый косинус  при основании
    bool IsCos;
  static  float PersonCoordsBefore; //для проверки DeltaX
    //float PersonCoordsAfter = 0;
    //float DeltaP;
    //float DeltaX1;
     static float Sin_For_Plane_Delata;
   static float d1;// переменная для запоминаний предыдущих значений  Sin_For_Plane;
    // Use this for initialization
    void Start()
    {
        textureForMirror = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);// задаем текстуру


    }

    // Update is called once per frame
    void Awake()
    {
        PersonHeight = Move.herTransorm.transform.lossyScale.y;// высота персонажа

     
        Gipotinuza_Of_Heighth_T1 = new Vector3(0, 0, 0);

    }

    private void Update()
    {
        LookFor_Camera_T = new Vector3(Move.LOOK, 0, 0);// длина направления взглада (направление взгляда из камеры на основане)(точка взгляда и длина до нее)
        tempArray = RecalculateRenderTexture(Move.herTransorm.position);//обновление изображения в зеркале
        textureForMirror = 
            new RenderTexture(
            System.Convert.ToInt32(tempArray[0]),
           System.Convert.ToInt32(tempArray[1]), 16, RenderTextureFormat.ARGB32);// обновляем текстуру зеркала

        textureForMirror.Create();// обновляем текстуру зеркала

        MCamera.targetTexture = textureForMirror;


    }



    public float[] RecalculateRenderTexture(Vector3 pos)

    {
        //float Gipotinuza_Of_Person;
        

        try
        {
            



            //else if ((PersonCoordsBefore == PersonCoordsAfter) && !Move.KeyMove())
            //{
            //    DeltaX = particleSystemEmiter.Position_Of_Ins.x - PersonCoordsBefore;
            //}





        }
        finally
        {
            sinCamera = DeltaX / PersonHeight;//синус взгляда камеры (из прямоугольного треугольника)
            cosCamera = PersonHeight / DeltaX;//косинус взглада камеры(из прямоугольного треугольника)
            Sin_For_Plane = Mathf.Sin(cosCamera);//искомый синус при основании (зеркала)(из прямоугольного треугольника для зеркала - по накрест лежащим углам)
            Cos_For_Plane = Mathf.Cos(sinCamera);// искомый косинус  при основании(зеркала)(из прямоугольного треугольника для зеркала - по накрест лежащим углам)
            IsCos = true;
        }
        

        Gipotinuza_Of_Heighth_T1.x = (LookFor_Camera_T.x * Sin_For_Plane) / cosCamera;//гипотинуза взгляда
        //DeltaP = DeltaX - (DeltaX - LookFor_Camera_T.x);

        //DeltaX1 = Sin_For_Plane_Delata / DeltaP;
        heigth_Texture = Mathf.Pow(PersonHeight, 2) + (DeltaX - LookFor_Camera_T.x) - (Sin_For_Plane_Delata / DeltaX);//высота искомой текстуры для зеркала


        float whidth_Texture = heigth_Texture;// квадарат


        //particleSystemEmiter.P.position
        float[] return_Array = new float[3];

        return_Array[0] = heigth_Texture;//высота искомой текстуры для зеркала
        return_Array[1] = 256;
      
        return return_Array;

    }

   public static bool PersonMoves()// см ParticleSystemEmiter
    {
        if (Move.KeyMove())//если сработал скрипт Move - персонаж двигался
        {
            DeltaX = Move.herTransorm.position.x * Time.deltaTime - Move.herTransorm.position.x; //расчет изминения передвижения персонажа

            for (int i = 0; i < 1; i++)
            {
                //PersonCoordsAfter = Move.herTransorm.position.x * Time.deltaTime;
                //найдем гипотинузу если персонаж двигается
                //Gipotinuza_Of_Person = Mathf.Sqrt(Mathf.Pow(PersonHeight, 2) + Mathf.Pow((DeltaX - LookFor_Camera_T.x), 2));

                d1 = Sin_For_Plane;// после каждого перемещения меняем d1 -   запоминанинаем предыдущих значений  Sin_For_Plane;

            }
            PersonCoordsBefore = Move.herTransorm.position.x;//нужно если персонаж не двигался
            return true;
        }
        else
        {
            return false;
        }

        if (!Move.KeyMove())// если персонаж не двигался
        {
            for (int i = 0; i < 1; i++)
            {

                //найдем гипотинузу если  персонаж НЕ двигается
                //Gipotinuza_Of_Person = Mathf.Sqrt(Mathf.Pow(PersonHeight, 2) + Mathf.Pow(DeltaX, 2));

                Sin_For_Plane_Delata = Sin_For_Plane - d1; // Sin_For_Plane_Delata = искомый синус при основании - предыдущее значение  Sin_For_Plane;
            }
            PersonCoordsBefore = Move.herTransorm.position.x;//нужно если персонаж не двигался
            DeltaX = particleSystemEmiter.Position_Of_Ins.x - PersonCoordsBefore;// тогда персонаж только повернулся и место создания Particle - осталось неизменным
            return false;
        }
        else
        {
            return true;
        }
    }
}
