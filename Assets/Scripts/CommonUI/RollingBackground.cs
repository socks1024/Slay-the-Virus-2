using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UIElements;

public class RollingBackground : MonoBehaviour
{
    [Serializable]
    public struct RollingLayer
    {
        public GameObject Layer;
        [HideInInspector] public SpriteRenderer LayerSpriteRenderer;
        [HideInInspector] public Transform LayerTransform;
        public float Speed;
        [HideInInspector] public float currMoveLength;

        [HideInInspector] public Transform subLayerTransform;
    }

    public List<RollingLayer> layers = new();

    public float TotalSpeed;

    public float Length;

    void Start()
    {
        for (int i = 0; i < layers.Count; i++) 
        {
            RollingLayer currLayer = new();

            currLayer.Layer = layers[i].Layer;
            currLayer.Speed = layers[i].Speed;
            currLayer.LayerSpriteRenderer = currLayer.Layer.GetComponent<SpriteRenderer>();
            currLayer.LayerTransform = currLayer.Layer.transform;

            currLayer.subLayerTransform = Instantiate(currLayer.Layer, currLayer.LayerTransform.parent).transform;
            currLayer.subLayerTransform.position += Vector3.down * Length;

            layers[i] = currLayer;
        }
    }

    void Update()
    {
        for(int i = 0; i < layers.Count; i++)
        {
            layers[i].LayerTransform.position += Vector3.down * layers[i].Speed * TotalSpeed;

            layers[i].subLayerTransform.position += Vector3.down * layers[i].Speed * TotalSpeed;

            RollingLayer newLayer = layers[i];
            newLayer.currMoveLength += newLayer.Speed * TotalSpeed;
            layers[i] = newLayer;

            if (layers[i].currMoveLength >= Length)
            {
                layers[i].LayerTransform.position += Vector3.up * Length;

                layers[i].subLayerTransform.position += Vector3.up * Length;

                RollingLayer newLayer2 = layers[i];
                newLayer2.currMoveLength = 0;
                layers[i] = newLayer2;
            }
        }
    }
}

