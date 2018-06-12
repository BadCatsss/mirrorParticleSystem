using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class particleSystemEmiter : MonoBehaviour, IRecalculate
{
    public ParticleSystem MainEmit;
  public  ParticleData data;
    //List<Particle> Particles = new List<Particle>(10);
    float AllTime;
    Vector3 Ppos;//позиция создания (зависит от взгляда и скрипта MirrorS)
    float Particle_VELOSITY;
    float MagnitudeOfParicle;
    float Particle_v;
    public static UnityEngine.Object[] InstObject;
   public static  Particle P;//создоваемая частица
   public static Vector3 Position_Of_Ins;// позиция создания
   static float Particle_count=-1;// изначальное количество частиц
    // Use this for initialization
    void Start()
    {
        InstObject = GameObject.FindObjectsOfTypeIncludingAssets(typeof(Canvas));
        P = new Particle();//инициализация
    }
    
    // Update is called once per frame
    void Update()
    {
        
       
            
        {


                if ( Input.GetButtonDown("Fire1"))// если нажата клавиша
                {
                    P = new Particle();// создаем частицу
                    //int EmitParticle = Particles.LastIndexOf( P, i);//10
                    
                    float ParticleLifeTime = Recalculate(P).GetEnumerator().Current;// время жизни для данной частицы


                MainEmit.Emit(System.Convert.ToInt32(Recalculate(P).GetEnumerator().Current));// ИСПУСКАЕМ  ЧАСТИЦУ заново (перерасчитывая параметры) (Не создаем зеркало!)



                //float Particle_POS = Recalculate(P).GetEnumerator().Current;
                if (Particle_count==0)// если нет зеркал , то изначальное перемещение персонажа = 1
                {
                    MirrorS.DeltaX = 1;
                }
                else
                {
                    MirrorS.PersonMoves();//если игрок двигается
                }

                if (!MirrorS.PersonMoves())//если игрок НЕ двигается, НО создает зеркала
                {
                    MirrorS.DeltaX = Particle_count;// создаем зеркала на расстоянии равном их количеству(каждое новое зеркало на +1)
                }
                else//если игрок двигается
                {
                    MirrorS.PersonMoves();
                }

                //окончательное Позиция ТЕКУЩЕЙ частицы для СОЗДАНИЯ ЗЕРКАЛА
                Ppos = new Vector3((MirrorS.LookFor_Camera_T.x -MirrorS.DeltaX),// по x
                      (P.position.y ),//P.position.y - по y - испущеной частицы
                       (P.position.z)); //P.position.y - по y - испущеной частицы  

                data = new ParticleData(Ppos,

                        P.rotation = P.rotation );// окончательную Позицию ТЕКУЩЕЙ частицы  и  СОЗДАЕМ Зеркало 

                //X=X0+VT





            }
            

            }


    
        
    }


   public IEnumerable<float> Recalculate(Particle p)// передаем частицу для расчета(созданную  Input.GetButtonDown("Fire1"))

    {
        //Particle_VELOSITY = (p.energy - p.startEnergy) / Time.fixedDeltaTime;
        // MagnitudeOfParicle = p.position.magnitude;
        // Particle_v = p.startEnergy + (Particle_VELOSITY * MainEmit.time) / 2;
        //float TIme = Particle_v * MainEmit.time;
         Ppos = p.position;//ЗАПОМИНАЕМ позицию ДАННОЙ (НОВОСОЗДАННОЙ -ТЕКУЩЕЙ) переданной частицы
        yield return Particle_count++;
        //yield return Ppos;
        
    }

    

    IEnumerator<float> IEnumerable<float>.GetEnumerator()
    {
        
        yield return AllTime;//заглушка
        
        //yield return Ppos;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        
        yield return AllTime;//заглушка

    }

   public class ParticleData// определяет поворот и место создания частицы(зеркала)
    {
      
        public  ParticleData(Vector3 position, float rotation)
        {

            Instantiate(InstObject[0], position, new Quaternion(rotation, 0, 0, 0));
            Position_Of_Ins = position;// см MirrorS


        }

    }
}


interface IRecalculate : IEnumerable<float>
{
     IEnumerable<float> Recalculate(Particle p);
    
}






