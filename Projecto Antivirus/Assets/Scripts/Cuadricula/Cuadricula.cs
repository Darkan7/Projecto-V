using System;
using UnityEngine;

public class Cuadricula<TCeldaTipo>
{
    public event EventHandler<CeldaCambiadaEventArgs> celdaCambiada;

    public class CeldaCambiadaEventArgs : EventArgs
    {
        public int coord1;
        public int coord2;
    }

    private int ancho;
    private int alto;
    private float tamCelda;
    private Vector3 origenCuadricula;
    private TCeldaTipo[,] celdaArray;
    private TextMesh[,] debugTextoArray;

    public Cuadricula(int ancho, int alto, float tamCelda, Vector3 origenCuadricula,
        Func<Cuadricula<TCeldaTipo>, int, int, TCeldaTipo> celdaDefault)
    {
        this.ancho = ancho;
        this.alto = alto;
        this.tamCelda = tamCelda;
        this.origenCuadricula = origenCuadricula;
        
        InicializarValores(celdaDefault);

        AnyadirDebugTexto();
    }

    private void AnyadirDebugTexto()
    {
        debugTextoArray = new TextMesh[ancho, alto];

        Transform parent = null;

        Vector3 centrar = 0.5f * new Vector3(tamCelda, 0, tamCelda);
        
        for (int coord1 = 0; coord1 < celdaArray.GetLength(0); coord1++)
        {
            for (int coord2 = 0; coord2 < celdaArray.GetLength(1); coord2++)
            {
                debugTextoArray[coord1, coord2] = Utilidades.CrearTextoEnMundo(celdaArray[coord1, coord2]?.ToString(), parent,
                    GetPosicionMundo(coord1, coord2)+centrar,  15, Color.white,
                    TextAnchor.MiddleCenter);
                Debug.DrawLine(GetPosicionMundo(coord1, coord2), GetPosicionMundo(coord1, coord2 + 1), Color.magenta, 100f);
                Debug.DrawLine(GetPosicionMundo(coord1, coord2), GetPosicionMundo(coord1 + 1, coord2), Color.magenta, 100f);
            }
        }

        Debug.DrawLine(GetPosicionMundo(0, alto), GetPosicionMundo(ancho, alto), Color.magenta, 100f);
        Debug.DrawLine(GetPosicionMundo(ancho, 0), GetPosicionMundo(ancho, alto), Color.magenta, 100f);

        
        celdaCambiada += ActualizarTCeldaDebugTexto;

    }

    private void ActualizarTCeldaDebugTexto(object sender, CeldaCambiadaEventArgs e)
    {
        debugTextoArray[e.coord1, e.coord2].text = celdaArray[e.coord1, e.coord2]?.ToString();
    }

    private void InicializarValores(Func<Cuadricula<TCeldaTipo>, int, int, TCeldaTipo> celdaDefault)
    {
        celdaArray = new TCeldaTipo[ancho, alto];

        for (int coord1 = 0; coord1 < celdaArray.GetLength(0); coord1++)
        {
            for (int coord2 = 0; coord2 < celdaArray.GetLength(1); coord2++)
            {
                celdaArray[coord1, coord2] = celdaDefault(this, coord1, coord2);
            }
        }
    }


    public void ActivarTCeldaCambiada(int coord1, int coord2)
    {
        if (celdaCambiada != null) celdaCambiada(this, new CeldaCambiadaEventArgs {coord1 = coord1, coord2 = coord2});
    }
    

    /*
    * 🄶🄴🅃🅃🄴🅁🅂
    */

    private Vector3 GetPosicionMundo(int coord1, int coord2)
    {
            return new Vector3(coord1, 0,coord2) * tamCelda + origenCuadricula;
        
    }

    private void Getxy(Vector3 posicionMundo, out int coord1, out int coord2)
    {
        coord1 = Mathf.FloorToInt((posicionMundo - origenCuadricula).x / tamCelda);
        coord2 = Mathf.FloorToInt((posicionMundo - origenCuadricula).z / tamCelda);
        
    }

    public int GetAncho()
    {
        return ancho;
    }

    public int GetAlto()
    {
        return alto;
    }

    public float GetTamCelda()
    {
        return tamCelda;
    }
    
    public TCeldaTipo GetCelda(int coord1, int coord2)
    {
        if (coord1 < 0 || coord2 < 0 || coord1 >= ancho || coord2 >= alto) return default(TCeldaTipo);

        return celdaArray[coord1, coord2];
    }


    public TCeldaTipo GetCelda(Vector3 posicionMundo)
    {
        Getxy(posicionMundo, out var coord1, out var coord2);
        return GetCelda(coord1, coord2);
    }


    /*
    *🅂🄴🅃🅃🄴🅁🅂
    */

    public void SetCelda(int coord1, int coord2, TCeldaTipo valor)
    {
        if (coord1 < 0 || coord2 < 0 || coord1 >= ancho || coord2 >= alto) return;

        celdaArray[coord1, coord2] = valor;

        if (celdaCambiada != null) celdaCambiada(this, new CeldaCambiadaEventArgs{coord1 = coord1, coord2 = coord2});
    }

    public void SetCelda(Vector3 posicionMundo, TCeldaTipo valor)
    {
        Getxy(posicionMundo, out var coord1, out var coord2);
        SetCelda(coord1, coord2, valor);
    }
    
}