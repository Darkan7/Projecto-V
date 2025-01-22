using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilidades
{
    
    
    /// <summary>
    /// Mostrar Texto en Mundo
    /// </summary>
    public const int sortingOrderDefault = 5000;

    public static TextMesh CrearTextoEnMundo(string text, Transform parent = null,
        Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null,
        TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Left,
        int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CrearTextoEnMundo(parent, text, localPosition, fontSize, (Color) color, textAnchor, textAlignment,
            sortingOrder);
    }

    public static TextMesh CrearTextoEnMundo(Transform parent, string text, Vector3 localPosition, int fontSize,
        Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("Texto", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }


    /// <summary>
    /// Gestion clase Cuadricula
    /// </summary>

    private static LayerMask layerMaskDefault = ~0;
    
    public static Vector3 GetPosicionMundo(float maxDistance=999f,LayerMask layerMask = default)
    {
        Vector3 pos=Vector3.zero;
        layerMask = layerMaskDefault;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))
            {
                pos = hit.point;
                pos.y = 0;
            }

            return pos;
    }

    public static Vector3 GetCentroCelda(int posX, int posY, float tamCel)
    {
        Vector3 center = Vector3.zero;

        center.x = posX * tamCel + tamCel / 2;
        center.z = posY * tamCel + tamCel / 2;

        return center;
    }
}
