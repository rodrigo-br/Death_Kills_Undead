using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    int activeSlotIndex = 0;
    PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        playerControls.Inventory.KeyboardNumber.performed += context => ToggleActiveSlot((int)context.ReadValue<float>());

        ToggleActiveSlot(1);
    }

    void ToggleActiveSlot(int numberValue)
    {
        if (activeSlotIndex == numberValue) { return ; }
        activeSlotIndex = numberValue;
        int index = 0;
        foreach(Transform child in this.transform)
        {
            child.GetChild(0).gameObject.SetActive(index == (numberValue - 1));
            index++;
        }

        ChangeActiveWeapon();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Disable()
    {
        playerControls.Disable();
    }

    void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.transform.parent.gameObject);
        }

        InventorySlot inventorySlot = transform.GetChild(activeSlotIndex - 1).GetComponentInChildren<InventorySlot>();
        if (!inventorySlot)
        {
            ActiveWeapon.Instance.WeaponNull();
            return ;
        }

        GameObject weaponToSpawn = inventorySlot.GetWeaponInfo()?.weaponPrefab;
        if (!weaponToSpawn) { return; }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        MonoBehaviour newWeaponMonoBehavior = newWeapon.transform.GetComponentInChildren<MonoBehaviour>();
        if (!newWeaponMonoBehavior)
        {
            ActiveWeapon.Instance.WeaponNull();
            return ;
        }
        ActiveWeapon.Instance.NewWeapon(newWeaponMonoBehavior);
    }
}
