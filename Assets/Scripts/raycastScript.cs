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

    [Header("Filter Values")]
    public LightColor rayColor = 0;
    public bool isProjector = true;
    public bool prismChild = false;
    private bool alwaysProjector;

    public int filterID;

    //private variables
    private int bouncesRemaining;
    private int pointsRendered = 0; //used for the lineRendered component

    private Vector3 startPosition;
    private Vector3 startDirection;

    private Color filterColor;

    private GameObject hitSpecialObject = null;

    private bool prismCh1Rot = false;
    private bool prismCh3Rot = false;

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
        alwaysProjector = isProjector;

        //for projectors, sets their direction
        if(alwaysProjector || prismChild){
            startPosition = transform.position;
            startDirection = transform.forward;
        }
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(alwaysProjector || prismChild){
            startPosition = transform.position;
            startDirection = transform.forward;
        }
        if(isProjector){
            pointsRendered = 0;        
            bouncesRemaining = rayBounces;
            launchRay(startPosition, startDirection);

        } else{
            //draw no line
            FinishNoLine(transform.position);
            //hit no object
            hitSpecialObject = null;
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
                signalCatcherScript catcherScript = hit.collider.gameObject.GetComponent<signalCatcherScript>();

                if(catcherScript.GetColor() == (int)rayColor || catcherScript.GetColor()==0){
                    hitSpecialObject = hit.collider.gameObject;
                }else{
                    hitSpecialObject = null;
                }
            }else if(hit.collider.tag == "filter"){
                FinishRenderPoints(hit.point);

                //temp variable for bypassing the width of the filter so it doesn't immediately collide with itself and die
                Vector3 throughPoint = new Vector3();
                throughPoint = ray.GetPoint(Vector3.Distance(pos, hit.point) + 0.1f);

                raycastScript filterScript = hit.collider.gameObject.GetComponent<raycastScript>();
                filterScript.CopyRayValues(bouncesRemaining, rayLength, throughPoint, dir);
                
                hitSpecialObject = hit.collider.gameObject;

            }else if(hit.collider.tag == "prism"){
                FinishRenderPoints(hit.point);

                //temp variable for bypassing the width of the filter so it doesn't immediately collide with itself and die
                Vector3 throughPoint = new Vector3();
                throughPoint = ray.GetPoint(Vector3.Distance(pos, hit.point) + 0.1f);

                Vector3 offAngle = new Vector3(0, 30f, 0);
                Vector3 negativeOffAngle = new Vector3(0, 330f, 0);
                
                /*
                when the prism is hit, it activates 
                its 3 children (which are actually filters with no collision) 
                to shoot each of the 3 rays individually. 

                - child 1 script is grabbed. 
                - child 1 script is activated.
                - rotate child 1.
                - child 1 gets new ray values.

                Repeat for all 3 children.
                */

                //child 1
                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                raycastScript child1 = hit.collider.gameObject.transform.GetChild(0).GetComponent<raycastScript>();
                Vector3 dir1 = new Vector3();
                //dir1 = (dir - Vector3.left)/2;
                dir1 = dir + offAngle;

                //child 2
                hit.collider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                raycastScript child2 = hit.collider.gameObject.transform.GetChild(1).GetComponent<raycastScript>();
                Vector3 dir2 = new Vector3();
                dir2 = dir;

                //child 3
                hit.collider.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                raycastScript child3 = hit.collider.gameObject.transform.GetChild(2).GetComponent<raycastScript>();
                Vector3 dir3 = new Vector3();
                //dir3 = (dir + Vector3.left)/2;
                dir3 = dir - offAngle;

                
                child1.SetIsProjector(true);
                child1.CopyRayValues(bouncesRemaining+1, rayLength, throughPoint);
                if(!child1.GetAlwaysProjector()){
                    if(!prismCh1Rot){
                        Debug.Log("rotated child 1");
                        hit.collider.gameObject.transform.GetChild(0).Rotate(offAngle);
                        prismCh1Rot = true;
                    }
                }

                child2.SetIsProjector(true);
                child2.CopyRayValues(bouncesRemaining+1, rayLength, throughPoint);


                child3.SetIsProjector(true);
                child3.CopyRayValues(bouncesRemaining+1, rayLength, throughPoint);
                if(!child3.GetAlwaysProjector()){
                    if(!prismCh3Rot){
                        hit.collider.gameObject.transform.GetChild(2).Rotate(negativeOffAngle);
                        prismCh3Rot = true;
                    }
                }

                hitSpecialObject = hit.collider.gameObject;

            }
            else {
                hitSpecialObject = null;
                
                lineRenderer.SetPosition(pointsRendered, hit.point);
                FinishRenderPoints(hit.point);
            }
        }
        else{
            hitSpecialObject = null;
            
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
        
        for(int i = pointsRendered; i < lineRenderer.positionCount; i++){
            lineRenderer.SetPosition(i, endPoint);
        }
    }

    void FinishNoLine(Vector3 endPoint){
        /* This helper function sets all remaining points of the line renderer
            to the last meaningful point to prevent visual errors.
            
            ex: if the ray hits a non-reflective object, kill the line
            by setting all remaining points after to this same position.
            */        
        for(int i = 0; i < lineRenderer.positionCount; i++){
            lineRenderer.SetPosition(i, endPoint);
        }
    }

    public void ActivateFilter(bool b){
        SetIsProjector(b);
    }

    public GameObject GetHitSpecialObject(){
        return hitSpecialObject;
    }

    public void SetIsProjector(bool b){
        isProjector = b;
    }

    public bool GetIsProjector(){
        return isProjector;
    }

    public bool GetAlwaysProjector(){
        return alwaysProjector;
    }

    public void CopyRayValues(int bounces, float length, Vector3 point, Vector3 startDir)
    {
        rayBounces = bounces;
        rayLength = length;
        startPosition = point;
        startDirection = startDir;
    }
    public void CopyRayValues(int bounces, float length, Vector3 point)
    {
        rayBounces = bounces;
        rayLength = length;
        startPosition = point;
        startDirection = transform.forward;
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
