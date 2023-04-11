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
        green,
        prism
    }
    public LightColor rayColor = 0;

    [Header("Projector or Filter")]
    public bool isProjector = true;


    //private variables
    private int bouncesRemaining;
    private int pointsRendered = 0; //used for the lineRendered component

    private Vector3 startPosition;
    private Vector3 startDirection;

    private Color filterColor;

    void Start()
    {
        bouncesRemaining = rayBounces + 1;
        lineRenderer.positionCount = rayBounces + 2; //if the ray can bounce once, 3 points are needed (start, bounce-point, endpoint)

        //Reset lineRenderer positions to 0 to prevent visual errors. This for-loop might not be necessary because of later code.
        for(int i = 0; i < rayBounces + 1; i++){
            lineRenderer.SetPosition(i, transform.position);
        }

        if((int)rayColor == 1)
        {
            filterColor = Color.red;
            lineRenderer.startColor = filterColor;
            lineRenderer.endColor = filterColor;
        } else if((int)rayColor == 2)
        {
            filterColor = Color.blue;
            lineRenderer.startColor = filterColor;
            lineRenderer.endColor = filterColor;
        } else if((int)rayColor == 3)
        {
            filterColor = Color.green;
            lineRenderer.startColor = filterColor;
            lineRenderer.endColor = filterColor;
        }

        

        startPosition = transform.position;
        startDirection = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {   
        if(isProjector){
            pointsRendered = 0;        
            bouncesRemaining = rayBounces;
            launchRay(startPosition, startDirection);
            
            //for prisms
            /*
            if((int)rayColor == 4){

                Vector3 rightDirection = new Vector3();
                Vector3 leftDirection = new Vector3();

                rightDirection = Vector3.right;
                leftDirection = Vector3.left;

                //forward ray
                launchRay(startPosition, startDirection);

                //side ray 1
                launchRay(startPosition, rightDirection);

                //side ray 2
                launchRay(startPosition, leftDirection);

            }*/
        } else{
            FinishRenderPoints(transform.position);
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
                FindObjectOfType<reflectionManager>().TurnOffFilters();

                if(catcherScript.GetColor() == (int)rayColor || catcherScript.GetColor()==0){
                    hit.collider.gameObject.GetComponent<signalCatcherScript>().SetHasBeenActivated(true);
                    hit.collider.gameObject.GetComponent<signalCatcherScript>().SetIsActivated(true);
                }
            }else if(hit.collider.tag == "filter"){
                FinishRenderPoints(hit.point);

                //temp variable for bypassing the width of the filter so it doesn't immediately collide with itself and die
                Vector3 throughPoint = new Vector3();
                throughPoint = ray.GetPoint(Vector3.Distance(pos, hit.point) + 0.1f);

                raycastScript filterScript = hit.collider.gameObject.GetComponent<raycastScript>();
                filterScript.SetIsProjector(true);
                filterScript.CopyRayValues(bouncesRemaining, rayLength, throughPoint, dir);

                //filterScript.launchRay(hit.point, dir);
            }else if(hit.collider.tag == "prism"){
                FinishRenderPoints(hit.point);

                //temp variable for bypassing the width of the filter so it doesn't immediately collide with itself and die
                Vector3 throughPoint = new Vector3();
                throughPoint = ray.GetPoint(Vector3.Distance(pos, hit.point) + 0.1f);
                
                /*
                when the prism is hit, it activates 
                its 3 children (which are actually filters with no collision) 
                to shoot each of the 3 rays individually. 

                - child 1 script is grabbed. 
                - child 1 script is turned on.
                - calculate the new angle for launch (dir + some value)
                - child 1 gets new ray values.

                Repeat for all 3 children.
                */

                //child 1
                raycastScript child1 = hit.collider.gameObject.transform.GetChild(0).GetComponent<raycastScript>();
                child1.SetIsProjector(true);
                Vector3 dir1 = new Vector3();
                dir1 = (dir - Vector3.left)/2;
                child1.CopyRayValues(bouncesRemaining, rayLength, throughPoint, dir1);

                //child 2
                raycastScript child2 = hit.collider.gameObject.transform.GetChild(1).GetComponent<raycastScript>();
                child2.SetIsProjector(true);
                child2.CopyRayValues(bouncesRemaining, rayLength, throughPoint, dir);

                //child 3
                raycastScript child3 = hit.collider.gameObject.transform.GetChild(2).GetComponent<raycastScript>();
                child3.SetIsProjector(true);
                Vector3 dir3 = new Vector3();
                dir3 = (dir + Vector3.left)/2;
                child3.CopyRayValues(bouncesRemaining, rayLength, throughPoint, dir3);

            }
            else {
                FindObjectOfType<reflectionManager>().SetAllCatchersIsActivated(false);
                FindObjectOfType<reflectionManager>().TurnOffFilters();

                //lineRenderer.SetPosition(pointsRendered, ray.GetPoint(rayLength));
                
                lineRenderer.SetPosition(pointsRendered, hit.point);
                FinishRenderPoints(hit.point);
            }
        }
        else{
            FindObjectOfType<reflectionManager>().SetAllCatchersIsActivated(false);
            FindObjectOfType<reflectionManager>().TurnOffFilters();
            
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

    public void CopyRayValues(int bounces, float length, Vector3 point, Vector3 startDir)
    {
        rayBounces = bounces;
        rayLength = length;
        startPosition = point;
        startDirection = startDir;
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
