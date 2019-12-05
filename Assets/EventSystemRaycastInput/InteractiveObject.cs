using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] static List<InteractiveObject> instances = new List<InteractiveObject>();

    [SerializeField] Canvas targetCanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        instances.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(bool enable) {
        targetCanvas.sortingOrder += (enable ? 1 : -1);
    }
}
