using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;

    private bool pickupActivated = false;

    private RaycastHit hitinfo;


    [SerializeField]
    private LayerMask layerMask;


    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theInventory;


    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if(hitinfo.transform != null)
            {
                Debug.Log("Got");
                theInventory.AcquireItem(hitinfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitinfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, range, layerMask))
        {
            if (hitinfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
            InfoDisappear();
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + "<color=yellow>" + " (Mouse L)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

}
