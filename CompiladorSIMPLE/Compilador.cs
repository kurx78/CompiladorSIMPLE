using System;
using System.Collections;
using System.IO;

namespace Logica
{
    public class Compilador
    {
        static ArrayList _listaTs;
        public static bool VerificarSintaxis = false;

        #region Métodos

        
        public string CompilarCodigoFuente(string codFuente, ArrayList tablaSimbolos) 
        {
            _listaTs = tablaSimbolos;
            //Pasamos por parametro el codigo fuente para obtener linea por linea del codigo fuente
            StringReader leer = new StringReader(codFuente);
            //Se crea una instancia de cada analizador
            ControlLexico lexico = new ControlLexico();
            ControlSintactico sintactico = new ControlSintactico();
            ControlSemantico semantico = new ControlSemantico();
            int numLinea = 1;//contador de lineas
            string instruccion = "";
            string erroresEncontrados = "";
            string erroresTotales = "";
            //se asigna la linea generada por el StringReader a la variable instruccion
            while ((instruccion = leer.ReadLine()) != null)
            {
                //se quita el espacio en blanco del principio y final de la linea en analisis
                instruccion = instruccion.Trim(' ', '\t');
                VerificarSintaxis = false;
                if (!(instruccion.Equals("") || instruccion.Equals("\t")))
                {
                    //Si la instruccion no es un comentario se envia a los analizadores
                    if (!instruccion.StartsWith("--") && !instruccion.EndsWith("--"))
                    {
                        //El siguiente metodo quita los espacios en blanco y las tabulaciones de mas que tiene el metodo
                        instruccion = OptimizarCodigoIntermedio(instruccion);
                        erroresEncontrados += lexico.AnalisisLexico(instruccion, _listaTs, numLinea);
                        erroresEncontrados += sintactico.AnalizarSintaxis(instruccion, _listaTs, codFuente, numLinea);
                        erroresEncontrados += semantico.AnalizarSemantica(instruccion, _listaTs, codFuente, numLinea);
                        //si no se encuentra ningun error en los analizadores se ingresa las palabras necesarias en tabla de simbolos
                        if (erroresEncontrados.Equals(""))
                        {
                            ActualizarSimbolos(instruccion, numLinea);
                        }
                        else
                        {
                            erroresTotales += erroresEncontrados;
                            erroresEncontrados = "";
                        }   
                    }
                }
                numLinea++;
            }
            //si no se encuentra ningun error en los analizadores se envia el codigo fuente al traductor
            if (erroresTotales.Equals(""))
            {
                ControlLenguajeIntermedio traductor = new ControlLenguajeIntermedio();
                erroresTotales = traductor.TraducirCF(codFuente, tablaSimbolos);
            }
            return erroresTotales;
        }

        public string OptimizarCodigoIntermedio(string instruccion)
        {
            instruccion = instruccion.Trim(new char[] { '\t', ' ' });
            //se ingresa cada palabra que contiene la instruccion en el vector auxInstruccion
            string[] auxInstruccion = instruccion.Split(new char[] { '\t', ' ', '\n' });
            instruccion = "";
            for (int i = 0; i < auxInstruccion.Length; i++)
            {
                //si es diferente a un espacio en blanco, una tabulacion o un salto de linea se acumula el valor que contiene el vector en el indice especificado
                if (!((auxInstruccion[i]=="") || (auxInstruccion[i]=="\t") || (auxInstruccion[i]=="\n")))
                {
                    instruccion += auxInstruccion[i] + " ";
                }
            }
            instruccion = instruccion.Trim(new char[] { ' ' });
            return instruccion;
        }

        public void ActualizarSimbolos(string linea, int numLinea)  
        {
            //se ingresa cada palabra que contiene la instruccion en el vector palabras 
            string[] palabras = linea.Split(new char[] { ' ', '\n', '\t' });
            int numLineaTS = _listaTs.Count;

            switch (palabras[0].ToString())
            {
                case "INT": 
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");
                    //se obtiene el valor que esta entre el signo '=' y el ';'
                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');
                    //se ingresan los valores en la tabla de simbolos(ArrayList)
                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "4" + " " + valor);
                }break;
                case "TEXT":
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");
                    //se obtiene el valor que esta entre el signo '=' y el ';'
                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');
                    //se ingresan los valores en la tabla de simbolos(ArrayList)
                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "5" + " " + valor);
                } break;
                case "DECIMAL": 
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");
                    //se obtiene el valor que esta entre el signo '=' y el ';'
                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');
                    //se ingresan los valores en la tabla de simbolos(ArrayList)
                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "6" + " " + valor); 
                } break;
                case "FLAG": 
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");
                    //se obtiene el valor que esta entre el signo '=' y el ';'
                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');
                    //se ingresan los valores en la tabla de simbolos(ArrayList)
                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "7" + " " + valor); 
                }break;
                case "CHAR":
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");
                    //se obtiene el valor que esta entre el signo '=' y el ';'
                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');
                    //se ingresan los valores en la tabla de simbolos(ArrayList)
                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "8" + " " + valor);
                } break;
            }
            
        }
        //el siguiente método se utiliza para poder acceder a la tabla de simbolos desde cualquier clase
        public static ArrayList RefTablaSimbolos() 
        {
            return _listaTs;
        }

        #endregion
    }
}
