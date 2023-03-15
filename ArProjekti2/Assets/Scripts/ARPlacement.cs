using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject joyStickCanvas;
    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    //private ARPlaneManager aRPlaneManager;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    public Camera camera;
  //  public GameObject shoot;
    //private float initialDistance;
    //private Vector3 initialScale;

    void Start()
    {
        //  aRPlaneManager = GetComponent<ARPlaneManager>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        joyStickCanvas.SetActive(false);
     //   shoot.SetActive(false);
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {
        if (spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
            //shoot.SetActive(true);
            joyStickCanvas.SetActive(true);
        }
        // scale using pinch
        //if(Input.touchCount == 2)
        //{
        //    var touchZero = Input.GetTouch(0);
        //    var touchOne = Input.GetTouch(1);

        //    if(touchZero.phase == TouchPhase.Ended || touchZero.phase  == TouchPhase.Canceled ||
        //        touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled )
        //    {
        //        return;
        //    }
        //    if(touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
        //    {
        //        initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
        //        initialScale = spawnedObject.transform.localScale; 

        //    }
        //    else
        //    {
        //        var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

        //        if (Mathf.Approximately(initialDistance, 0))
        //        {
        //            return;
        //        }
        //       var factor = currentDistance / initialDistance;
        //        spawnedObject.transform.localScale = initialScale *factor;
        //    }
        //}

        UpdatePlacementPose();
        UpdatePlacementIndicator();


    }
    void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    void ARPlaceObject()
    {
        spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);
        spawnedObject.transform.Rotate(0.0f, 180f, 0.0f, Space.Self);
    }


}