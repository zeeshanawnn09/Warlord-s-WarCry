using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInterface : MonoBehaviour
{
    public GameObject RocketPrefab;
    public GameObject CannonPrefab;
    public GameObject FlamerPrefab;


    public GameObject WeaponMenu;

    GameObject focusObject;
    GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RocketButton()
    {
        itemPrefab = RocketPrefab;
        CreateItemOnClick();
    }

    public void CannonButton()
    {
        itemPrefab = CannonPrefab;
        CreateItemOnClick();
    }

    public void FlamerButton()
    {
        itemPrefab = FlamerPrefab;
        CreateItemOnClick();
    }

    public void CloseButton()
    {
        WeaponMenu.SetActive(false);
    }
    void CreateItemOnClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }
        else
        {
            focusObject = Instantiate(itemPrefab, hit.point, itemPrefab.transform.rotation);
            focusObject.GetComponent<Collider>().enabled = false;
        }
    }

    void MouseBehavior()
    {
        //(if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)) --> for mobile

        //when mouse button is clicked, instantiate the turret
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Weapon"))
            {
                WeaponMenu.transform.position = Input.mousePosition;
                WeaponMenu.SetActive(true);
            }  
        }

        //(if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))

        //when mouse button is held down, move the turret to the mouse position
        else if (focusObject && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            else
            {
                
                focusObject.transform.position = hit.point;
            }
            }

        //(if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))

        //when mouse button is released make sure the turret is on the platform
        else if (focusObject && Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;

                                                   //(Input.getTouch(0).position)--> for mobile
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if the raycast hits the platform and the normal of the platform is up, place the turret
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Free") && hit.normal.Equals(new Vector3(0, 1, 0)))
            {
                hit.collider.gameObject.tag = "Occupied";
                
                //to place the turret on the centre of the platform
                focusObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x,
                                                             focusObject.transform.position.y,
                                                             hit.collider.gameObject.transform.position.z);

                focusObject.GetComponent<BoxCollider>().enabled = true;
                focusObject.GetComponent<SphereCollider>().enabled = true;
            }
            else
            {
                Destroy(focusObject);
                focusObject = null;
            }
            }
    }
    // Update is called once per frame
    void Update()
    {
        MouseBehavior();
    }


}
