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
    Vector3 Ppos;
    float Particle_VELOSITY;
    float MagnitudeOfParicle;
    float Particle_v;
    public static UnityEngine.Object[] InstObject;
   public static  Particle P;
   public static Vector3 Position_Of_Ins;
   static float Particle_count=-1;
    // Use this for initialization
    void Start()
    {
        InstObject = GameObject.FindObjectsOfTypeIncludingAssets(typeof(Canvas));
        P = new Particle();
    }
    
    // Update is called once per frame
    void Update()
    {
        
       
            
        {


                if ( Input.GetButtonDown("Fire1"))
                {
                    P = new Particle();
                    //int EmitParticle = Particles.LastIndexOf( P, i);//10
                    
                    float ParticleLifeTime = Recalculate(P).GetEnumerator().Current;
                MainEmit.Emit(System.Convert.ToInt32(Recalculate(P).GetEnumerator().Current));
                //float Particle_POS = Recalculate(P).GetEnumerator().Current;
                if (Particle_count==0)
                {
                    MirrorS.DeltaX = 1;
                }
                else
                {
                    MirrorS.PersonMoves();
                }
                if (!MirrorS.PersonMoves())
                {
                    MirrorS.DeltaX = Particle_count;
                }
                else
                {
                    MirrorS.PersonMoves();
                }
                Ppos = new Vector3((MirrorS.LookFor_Camera_T.x -MirrorS.DeltaX),
                      (P.position.y ),
                       (P.position.z));
                data = new ParticleData(Ppos,

                        P.rotation = P.rotation );

                    //X=X0+VT





                }
            

            }


    
        
    }


   public IEnumerable<float> Recalculate(Particle p)

    {
        //Particle_VELOSITY = (p.energy - p.startEnergy) / Time.fixedDeltaTime;
        // MagnitudeOfParicle = p.position.magnitude;
        // Particle_v = p.startEnergy + (Particle_VELOSITY * MainEmit.time) / 2;
        //float TIme = Particle_v * MainEmit.time;
         Ppos = p.position;
        yield return Particle_count++;
        //yield return Ppos;
        
    }

    

    IEnumerator<float> IEnumerable<float>.GetEnumerator()
    {
        
        yield return AllTime;
        
        //yield return Ppos;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        
        yield return AllTime;
      
    }

   public class ParticleData
    {
      
        public  ParticleData(Vector3 position, float rotation)
        {

            Instantiate(InstObject[0], position, new Quaternion(rotation, 0, 0, 0));
            Position_Of_Ins = position;


        }

    }
}


interface IRecalculate : IEnumerable<float>
{
     IEnumerable<float> Recalculate(Particle p);
    
}






