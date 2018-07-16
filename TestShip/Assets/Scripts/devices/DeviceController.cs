using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

[RequireComponent(typeof(PlayerProperties))]
public class DeviceController : MonoBehaviour {
    public DeviceBase[] devices;

    private Dictionary<DeviceBase, DeviceShotController> references = new Dictionary<DeviceBase, DeviceShotController>();
    private PlayerProperties properties;

    void Start() {
        properties = GetComponent<PlayerProperties>();
        for (int i = 0; i < devices.Length; i++) {
            var device = devices[i];
            references.Add(device, new DeviceShotController(device));
        }
    }

    void Update() {
        if (!properties.isCrashed) {
            for (int i = 0; i < devices.Length; i++) {
                var device = devices[i];
                if (device && references.ContainsKey(device)) {
                    DeviceShotController controller = references[device];
                    controller.waitTime -= Time.deltaTime;
                    if (Input.GetButton("Fire" + (i + 1))) {
                        shoot(device, controller);
                    }
                }
            }
        }
    }

    private void shoot(DeviceBase device, DeviceShotController controller) {
        if (controller.ammunitionQty > 0 && controller.waitTime <= 0) {

            // Update controllers
            controller.waitTime = device.reloadTime;
            controller.ammunitionQty--;

            // Create new instance
            GameObject createdDevice = Instantiate(device.gameObject);

            // Configure
            createdDevice.SetActive(true);
            createdDevice.transform.position = this.transform.position + ( this.transform.up.normalized * device.distanceFromPlayer);
            createdDevice.transform.up = this.transform.up;
            DeviceBase aux = createdDevice.GetComponent<DeviceBase>();

            if (aux) {
                aux.owner = this.gameObject;
            }

            controller.lastShot = createdDevice;

            // Set to right parent
            createdDevice.transform.parent = this.transform.parent;
        }
    }

    private class DeviceShotController {
        public float waitTime = 0;
        public int ammunitionQty = 0;
        public GameObject lastShot;

        public DeviceShotController(DeviceBase device) {
            this.waitTime = 0;
            this.ammunitionQty = device.ammunitionQuantity;
        }

    }
}