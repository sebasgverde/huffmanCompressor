﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace practicaFinalEstructuras
{
    /*clase en la que se basa el programa*/
    public class Arbol
    {
        internal string frec;
        internal string letra;
        internal Arbol hijoIzq;
        internal Arbol hijoDer;

        public Arbol()
        {
            hijoIzq = null;
            hijoDer = null;
        }


        public virtual void colocaIzq(Arbol V, Arbol t)
        {
            t.hijoIzq = V;
        }

        public virtual void colocaDer(Arbol V, Arbol t)
        {
            t.hijoDer = V;
        }

        public virtual void preOrden(Arbol A)
        {

            if (A != null)
            {
                Console.WriteLine(A.letra + "	" + A.frec + " ");
                preOrden(A.hijoIzq);
                preOrden(A.hijoDer);
            }
        }

    }

    public class Program
    {
        //ordena la lista de prioridades segun la frecuencia para armar el arbol
        public static List<Arbol> ordenar(List<Arbol> a)
        {
            for (int i = 0; i < a.Count - 1; i++)
            {
                for (int j = i; j < a.Count; j++)
                {
                    if (Convert.ToInt32(a[i].frec) > Convert.ToInt32(a[j].frec))
                    {
                        Arbol temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }
                }
            }
            return a;
        }

        //encuentra  en que indice de la lista de prioridades debe insertar el valor con su frecuencia
        public static int buscarPosicion(List<Arbol> a, Arbol b)
        {
            int i = 0;
            while (i < a.Count)
            {
                if (Convert.ToInt32(b.frec) >= Convert.ToInt32(a[i].frec))
                {
                    i++;
                }
                else
                {
                    return i;
                }
            }
            return a.Count;
        }

        //este recorrido lee el arbol armado con la frecuencia y devuelve cada caracter con su codigo huffman
        public static List<Arbol> recorridoArbol(Arbol A)
        {
            List<Arbol> pila = new List<Arbol>();
            List<Arbol> codificacion = new List<Arbol>();
            A.frec = "";
            Arbol temp;
            while (A != null || pila.Count > 0)
            {
                while (A != null)
                {
                    pila.Add(A);
                    temp = A;
                    A = A.hijoIzq;
                    if (A != null)
                    A.frec = temp.frec + "0";
                }
                if (pila.Count > 0)
                {

                    A = pila[pila.Count - 1];
                    pila.RemoveAt(pila.Count - 1);
                    if (A != null && A.letra != null)
                    {
                        Arbol a = new Arbol();
                        a.letra = A.letra;
                        a.frec = A.frec;
                        codificacion.Add(a);
                    }
                    temp = A;
                    A = A.hijoDer;
                    if (A != null)
                    A.frec = temp.frec + "1";

                }
            }
            return codificacion;
        }

     /* 
      * forma de sacar el codigo del arbol desechada despues de ver su complejidad
      * public static List<Arbol> codigoHuffman(Arbol A)
        {
            List<Arbol> pila = new List<Arbol>();
            List<Arbol> codificacion = new List<Arbol>();
            string binario = "";
            while (A != null || pila.Count > 0)
            {
                if (A.hijoIzq != null)
                {
                    binario += "0";
                    pila.Add(A);
                    A = A.hijoIzq;
                }
                else if (A.hijoDer != null)
                {
                    binario += "1";
                    pila.Add(A);
                    A = A.hijoDer;
                }

                else if (A.hijoIzq == null && A.hijoDer == null)
                {
                    if (A.letra != null)
                    {
                        Arbol a = new Arbol();
                        a.letra = A.letra;
                        a.frec = binario;
                        codificacion.Add(a);
                        //binario = binario.Remove(binario.Length - 1);
                    }

                    if (binario[binario.Length - 1] == '0')
                    {
                        binario = binario.Remove(binario.Length - 1);
                        A = pila[pila.Count - 1];
                        pila.RemoveAt(pila.Count - 1);
                        pila.Add(A.hijoDer);
                        A.hijoIzq = null;

                    }
                    else
                    {
                        binario = binario.Remove(binario.Length - 1);
                        A = pila[pila.Count - 1];
                        pila.RemoveAt(pila.Count - 1);
                        pila.Add(A.hijoDer);
                        A.hijoDer = null;
                    }
                }
            }
            return codificacion;
        }*/

        //este es el metodo encargado de armar el arbol recibiendo las letras con su frecuencia y devolver el codigo huffman de cada letra
        public static List<Arbol> codificacionHuffman(List<Arbol> codigoFrec)
        {
            //primero invoca ordenar para oordenar la lista
            List<Arbol> codigo = ordenar(codigoFrec);

            //for(int i = 0; i < codigo.size(); i++)System.out.println(codigo.get(i).letra + "	" + codigo.get(i).frec + " ");

            //aqui arma el arbol
            while (codigo.Count > 1)
            {
                Arbol a = new Arbol();
                a.colocaIzq(codigo[0], a);
                a.colocaDer(codigo[1], a);
                codigo.RemoveAt(0);
                codigo.RemoveAt(0);
                a.frec = Convert.ToString(Convert.ToInt32(a.hijoDer.frec) + Convert.ToInt32(a.hijoIzq.frec));
                codigo.Insert(buscarPosicion(codigo, a), a);

            }

            //finalmente invoca el metodo que recorre el arbol y arma el codigo huffman de cada caracter
            codigo = recorridoArbol(codigo[0]);
            return codigo;
        }

        //metodo encargado de reconstruir el arbol a partir del codigo de cada letra, con este arbol armado procede a coger el texto comprimido
        //y reemplazar el codigo con su letra, para finalmente devolver el texto descomprimido
        public static string decodificacionHuffman(List<Arbol> codificacion, string texto)
        {
            //aqui reconstruye el arbol
            Arbol decodif = new Arbol();

            for (int i = 0; i < codificacion.Count; i++)
            {

                string letra = codificacion[i].letra;

                string codigoBin = codificacion[i].frec;

                Arbol temp = decodif;

                for (int j = 0; j < codigoBin.Length; j++)
                {
                    if (codigoBin[j] == '0')
                    {
                        if (temp.hijoIzq == null)
                            temp.hijoIzq = new Arbol();
                        temp = temp.hijoIzq;
                    }
                    else
                    {
                        if (temp.hijoDer == null)
                            temp.hijoDer = new Arbol();
                        temp = temp.hijoDer;
                    }

                    if (j == codigoBin.Length - 1)
                        temp.letra = letra;

                }
                temp = decodif;
            }

            //aqui decodifica el texto
            string textDec = "";
            Arbol reco = decodif;
            int k = 0;
            while (k < texto.Length)
            {
                if (texto[k] == '0')
                    reco = reco.hijoIzq;
                else
                    reco = reco.hijoDer;
                if (reco.letra != null)
                {
                    textDec += reco.letra;
                    reco = decodif;
                }
                k++;
            }

            return textDec;
        }

        //este metodo recibe el texto a comprimir y el codigo huffman de cada caracter y devuelve el texto solo con 1 y 0
        public static string comprimir(string texto, List<Arbol> claves)
        {
            string comprimido = "";
            for (int i = 0; i < texto.Length; i++)
            {
                for (int j = 0; j < claves.Count ; j++)
                {
                    if (texto[i] == claves[j].letra[0])
                    {
                        comprimido += claves[j].frec;
                        break;
                    }
                }
            }
            return comprimido;
        }

        //recibe el texto sacado del txt y retorna cada letra con su frecuencia
        public static List<Arbol> contarFrec(string texto)
        {

            List<Arbol> frecuencias = new List<Arbol>();

            int cont = 0;
            char letra;
            while(texto.Length > 0 )
            {
                letra = texto[0];

                for (int j = 0; j <= texto.Length - 1; j++)
                {
                    if (letra == texto[j])
                    {
                        cont++;
                        texto = texto.Remove(j, 1);
                        j--;
                    }
                }

                Arbol temp = new Arbol();
                temp.letra = letra.ToString();
                temp.frec = cont.ToString();
                frecuencias.Add(temp);

                cont = 0;
            }
            return frecuencias;
        }

        public static void Main(string[] args)
        {
            List<Arbol> codific = new List<Arbol>();

            Console.WriteLine("Para usar codificacionHuffman ingrese 1 para descomprimir un archivo ingrese 2");
            if (Console.ReadLine() == "1")
            {
                Console.WriteLine("Ingrese el nombre del archivo");
                string nomTxt = Console.ReadLine();
                //leer txt original
                string textOriginal = "";
                try
                {
                    string texto;
                    StreamReader sr = new StreamReader(nomTxt);
                    texto = sr.ReadLine();
                    while (texto != null)
                    {
                        textOriginal += texto;
                        texto = sr.ReadLine();
                    }
                    sr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("excepcion : " + ex.Message);
                }

                codific = contarFrec(textOriginal);

                List<Arbol> a = codificacionHuffman(codific);

                //crear txt con las claves
                try
                {
                    File.Delete("clavesArchivo.txt");
                    string fileName = "clavesArchivo.txt";
                    StreamWriter writer = File.AppendText(fileName);

                    for (int i = 0; i < a.Count; i++)
                    {
                        writer.WriteLine(a[i].letra);
                        writer.WriteLine(a[i].frec);
                    }
                    writer.Close();
                }
                catch
                {
                    Console.WriteLine("Error al decodificar");
                }

                //crear txt comprimido
                try
                {
                    File.Delete("textoComprimido.txt");
                    string fileName = "textoComprimido.txt";
                    StreamWriter writer = File.AppendText(fileName);

                    string textoCom = comprimir(textOriginal, a);

                    writer.WriteLine(textoCom);


                    writer.Close();
                }
                catch
                {
                    Console.WriteLine("Error al decodificar");
                }

                Console.ReadKey();
            }
            else
            {
                //decodificar archivo
                try
                {
                    string texto;
                    StreamReader sr = new StreamReader("clavesArchivo.txt");
                    texto = sr.ReadLine();
                    while (texto != null)
                    {
                        Arbol temp = new Arbol();
                        temp.letra = texto;
                        texto = sr.ReadLine();
                        temp.frec = texto;
                        codific.Add(temp);
                        Console.WriteLine(temp.letra + " " + temp.frec);
                        texto = sr.ReadLine();
                    }
                    sr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("excepcion : " + ex.Message);
                }


                string textoBin = "";
                try
                {
                    string texto;
                    StreamReader sr = new StreamReader("textoComprimido.txt");
                    texto = sr.ReadLine();
                    while (texto != null)
                    {
                        textoBin += texto;
                        texto = sr.ReadLine();
                    }
                    sr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("excepcion : " + ex.Message);
                }
                try
                {
                    File.Delete("textoDescomprimido.txt");
                    string fileName = "textoDescomprimido.txt";
                    StreamWriter writer = File.AppendText(fileName);

                    writer.WriteLine(decodificacionHuffman(codific, textoBin));

                    writer.WriteLine("");

                    writer.Close();
                }
                catch
                {
                    Console.WriteLine("Error al decodificar");
                }

                Console.ReadKey();

            }
        }
    }
}