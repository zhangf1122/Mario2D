using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public Transform CameraTransform;
    public List<value> values;
    [System.Serializable]
    public class value
    {
        public Transform root;
        public float minRotation;
        public float maxRotation;
    }
    private Dictionary<value, Renderer[]> renderers = new Dictionary<value, Renderer[]> ();
    private void Awake()
    {
        foreach (var value in values)
        {
            renderers[value] = value.root.GetComponentsInChildren<Renderer>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var value in values)
        {
            float a = value.maxRotation;
            float b = value.minRotation;
            float c = value.maxRotation + 360;
            float d = value.minRotation + 360;
            //jisuan 
            if(CameraTransform.rotation.eulerAngles.y>=0&&CameraTransform.rotation.eulerAngles.y<=180)
            {
                if (CameraTransform.rotation.eulerAngles.y >= b && CameraTransform.rotation.eulerAngles.y <= a)
                {
                    if (renderers.TryGetValue(value, out var renders))
                    {
                        foreach (var r in renders)
                        {
                            
                        }
                    }
                }
                else
                {
                    if (renderers.TryGetValue(value, out var renders))
                    {
                        foreach (var r in renders)
                        {
                            r.enabled = true;
                        }
                    }
                }
            }
            else if (CameraTransform.rotation.eulerAngles.y > 180 && CameraTransform.rotation.eulerAngles.y <360)
            {
                if (CameraTransform.rotation.eulerAngles.y >= d && CameraTransform.rotation.eulerAngles.y <= c)
                {
                    if (renderers.TryGetValue(value, out var renders))
                    {
                        foreach (var r in renders)
                        {
                            r.enabled = false;
                        }
                    }
                }
                else
                {
                    if (renderers.TryGetValue(value, out var renders))
                    {
                        foreach (var r in renders)
                        {
                            r.enabled = true;
                        }
                    }
                }
            }


        }
    }
}
