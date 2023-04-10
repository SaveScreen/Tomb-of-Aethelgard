using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastScript : MonoBehaviour {

    [Header("Line Renderer")]
    public LineRenderer lineRenderer; //drag the lineRendered component in here

/* -- This seems impossible. Not deleting for now in case I go back to this idea.
    [Header("Particle System")]
    //private ParticleSystem _particles;
    //public int swag;
    public ParticleSystem particles;
    {
        get{return _particles;}
        set{_particles = value;}
    }*/


    [Header("Ray Data")]
    public int rayBounces = 2; //the amount of times the ray can bounce
    public float rayLength = 5f; //maximum length of a ray
    public enum LightColor{
        any,
        red,
        blue,
        green
    }
    public LightColor rayColor = 0;

    [Header("Projector or Filter")]
    public bool isProjector = true;


    //private variables
    private int bouncesRemaining;
    private int pointsRendered = 0; //used for the lineRendered component


    void Start()
    {
        bouncesRemaining = rayBounces + 1;
        lineRenderer.positionCount = rayBounces + 2; //if the ray can bounce once, 3 points are needed (start, bounce-point, endpoint)

        //Reset lineRenderer positions to 0 to prevent visual errors. This for-loop might not be necessary because of later code.
        for(int i = 0; i < rayBounces + 1; i++){
            lineRenderer.SetPosition(i, transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(isProjector){
            pointsRendered = 0;        
            bouncesRemaining = rayBounces;
            launchRay(transform.position, transform.forward);
        }
    }

    void launchRay(Vector3 pos, Vector3 dir)
    {

        lineRenderer.SetPosition(pointsRendered, pos);
        pointsRendered++;
        RaycastHit hit; //store hit data
        Ray ray = new Ray(pos, dir);
        Debug.DrawRay(pos, dir*rayLength, Color.green);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            lineRenderer.SetPosition(pointsRendered, hit.point);
            if(hit.collider.tag == "reflector"){
                //Debug.Log("Ray reflected");
                if (bouncesRemaining > 0){
                    bouncesRemaining--;
                    //Debug.Log("Ray bounced");


                    /*Recursion. If hits reflector and can bounce again, 
                    launch new ray from the hit.point position in the hit.normal direction*/
                    launchRay(hit.point, hit.normal);
                }
            }else if(hit.collider.tag == "signalCatcher"){
                FinishRenderPoints(hit.point);

                //FindObjectOfType<reflectionManager>().SetAllCatchersIsActivatedExcept(false, );
                signalCatcherScript catcherScript = hit.collider.gameObject.GetComponent<signalCatcherScript>();

                if(catcherScript.GetColor() == (int)rayColor || catcherScript.GetColor()==0){
                    hit.collider.gameObject.GetComponent<signalCatcherScript>().SetHasBeenActivated(true);
                    hit.collider.gameObject.GetComponent<signalCatcherScript>().SetIsActivated(true);
                }
            }else if(hit.collider.tag == "filter"){
                FinishRenderPoints(hit.point);
                raycastScript filterScript = hit.collider.gameObject.GetComponent<raycastScript>();
                filterScript.SetIsProjector(true);

                //filterScript.launchRay(hit.point, dir);
            }
            else {
                /*if the line collides with an object not tagged "reflector" or "signalCatcher" then the line dies.
                Set all remaining points of the lineRenderer to the end point to prevent visual errors.
                May need to change this to: 
                    lineRenderer.SetPosition(pointsRendered, hit.point);
                to prevent drawing lines through walls. For now it is fine. 
                 */

                //hit.collider.gameObject.GetComponent<signalCatcherScript>().SetIsActivated(false);

                FindObjectOfType<reflectionManager>().SetAllCatchersIsActivated(false);

                //lineRenderer.SetPosition(pointsRendered, ray.GetPoint(rayLength));
                
                lineRenderer.SetPosition(pointsRendered, hit.point);
                FinishRenderPoints(ray.GetPoint(rayLength));
            }
        }
        else{
            FindObjectOfType<reflectionManager>().SetAllCatchersIsActivated(false);
            
            /*if the line does not collide with an object within rayLength units, the line dies. 
                Set all remaining points of the lineRenderer to the end point to prevent visual errors.*/

            Debug.DrawRay(pos, dir *rayLength, Color.blue);
            lineRenderer.SetPosition(pointsRendered, ray.GetPoint(rayLength));
            FinishRenderPoints(ray.GetPoint(rayLength));
        }
    }

    void FinishRenderPoints(Vector3 endPoint){
        /* This helper function sets all remaining points of the line renderer
            to the last meaningful point to prevent visual errors.
            
            ex: if the ray hits a non-reflective object, kill the line
            by setting all remaining points after to this same position.
            */
        
        for(int i = pointsRendered; i < rayBounces+2; i++){
            lineRenderer.SetPosition(i, endPoint);
        }
    }

    public void SetIsProjector(bool b){
        isProjector = b;
    }

/*    void MakeParticles(LineRenderer lineRenderer){
        Mesh m = new Mesh();
        lineRenderer.BakeMesh(m);
        var shape = particles.shape;
        shape.shapeType = ParticleSystemShapeType.Mesh;
        shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
        shape.mesh = m;
        //particles.shape = shape;

    }*/
}
