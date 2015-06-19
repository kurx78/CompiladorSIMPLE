using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Logica
{
    public class ControlLexico
    {
        #region Métodos

        public string AnalisisLexico(string lineaAnalisis, ArrayList tablaSimbolos, int numLinea)  
        {
            

            string erroresEncontrados = "";
            //se quita el espacio en blanco del principio y final de la linea en analisis
            lineaAnalisis = lineaAnalisis.Trim(new char[] { '\t', ' ' });
            //se ingresa cada palabra que contiene la instruccion en el vector auxInstruccion
            string[] palabrasCF = lineaAnalisis.Split(new char[] {' ', '\n', '\t'});
            //la siguiente expresion regular es utilizada para hallar caracteres con no esten especificados dentro de ella (caracteres ilegales en el lenguage)
            string expRegular = @"[^a-zA-Z0-9;""'' <>\+-/\[\]\*=!¡_]";
            bool palabraEncontrada = false;
            bool detenerBusquedaTS = false;
            string auxLineasTS = "";
            string[] auxPalabrasTS; 

            for (int i = 0; i < palabrasCF.Length; i++)
            {
                //si el valor que se encuentra dentro de la posicion del vector es diferente de nulo
                if (!(palabrasCF[i] == ""))
                {
                    detenerBusquedaTS = false;
                    palabraEncontrada = false;
                    for (int j = 0; j < tablaSimbolos.Count; j++)
                    {
                        //se asigna cada linea de palabras que contiene la tabla de simbolos en la posicion 'J' a la variable auxLineasTS
                        auxLineasTS = tablaSimbolos[j].ToString();
                        //se asigna cada palabra que contiene la variable auxLineaTS el vector auxPalabrasTS
                        auxPalabrasTS = auxLineasTS.Split(new char[] { ' ' });
                        //se compara cada palabra del codigo fuente contenida en el vector palabrasCF con los tokens de la tabla de simbolos en la posicion especificada
                        if (palabrasCF[i].Equals(auxPalabrasTS[1].ToString()))
                        {
                            palabraEncontrada = true;
                            detenerBusquedaTS = true;
                        }
                        if (detenerBusquedaTS == true)
                        {
                            //si la palabra buscada en la tabla de simbolos es encontrada se detiene la busqueda
                            j = tablaSimbolos.Count;
                        }
                    }
                    //si la palabra buscada no es encontrada
                    if (palabraEncontrada == false)
                    {
                        //si la linea en analisis inicia con ent, dob, car, cad, bin, MostrarInfo (osea una definición de usuario)
                        if (lineaAnalisis.StartsWith("INT ") || lineaAnalisis.StartsWith("DECIMAL ") || lineaAnalisis.StartsWith("CHAR ") || 
                            lineaAnalisis.StartsWith("TEXT ") || lineaAnalisis.StartsWith("FLAG ") || lineaAnalisis.StartsWith("PRINT "))
                        {
                            if (!lineaAnalisis.StartsWith("PRINT "))
                            {
                                //se verifica que no hayan caracteres ilegales en la palabra
                                if (Regex.IsMatch(palabrasCF[i], expRegular))
                                {
                                    erroresEncontrados += "Error Léxico: Línea " + numLinea + ". Caracter ilegal usado en la palabra '" + palabrasCF[i] + "'" + Environment.NewLine;
                                }    
                            }
                        }
                        else
                        {
                            //si el token en analisis no existe dentro de los patrones especificado por las siguientes expresiones se muestra el mensaje de error
                            if (!(Regex.IsMatch(palabrasCF[i], @"^\d{1,}$")||Regex.IsMatch(palabrasCF[i], @"^\d{1,}\.\d{1,}$")||Regex.IsMatch(palabrasCF[i], "^(\")[\\w\\s\\W]*(\")$")
                                || Regex.IsMatch(palabrasCF[i], "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(palabrasCF[i], "^(false|true)$")))
                            {
                                erroresEncontrados += "Error Léxico: Línea " + numLinea + ". La palabra '" + palabrasCF[i] + "' no existe en el contexto actual." + Environment.NewLine;
                            }
                        }
                    }
                    else 
                    {
                        //si no se encuentra la palabra quiere decir que puede ser un definicion de usuario
                        if (lineaAnalisis.StartsWith("INT ") || lineaAnalisis.StartsWith("DECIMAL ") || lineaAnalisis.StartsWith("CHAR ") || lineaAnalisis.StartsWith("TEXT ") || lineaAnalisis.StartsWith("FLAG "))
                        {
                            if (i==1)//solo se analizara la variable declarada
                            {
                                detenerBusquedaTS = false;
                                for (int j = 0; j < tablaSimbolos.Count; j++)
                                {
                                    auxLineasTS = tablaSimbolos[j].ToString();
                                    auxPalabrasTS = auxLineasTS.Split(new char[] { ' ' });
                                    if (palabrasCF[1].Equals(auxPalabrasTS[1].ToString()))
                                    {
                                        //se verifica en la tabla de simbolos si ya existe una variable con ese nombre
                                        if (j > 29)
                                        {
                                            erroresEncontrados += "Error Léxico: Línea " + numLinea + ". Ya se ha definido un identificador denominado '" + palabrasCF[1] + "' en el contexto actual." + Environment.NewLine;
                                            detenerBusquedaTS = true;
                                        }
                                        else
                                        {
                                            //se verifica que no se utilice palabras reservadas en una definicion de varable
                                            erroresEncontrados += "Error Léxico: Línea " + numLinea + ". Se esperaba un identificador, '" + palabrasCF[1] + "' es una palabra reservada." + Environment.NewLine;
                                            detenerBusquedaTS = true;
                                        }
                                    }
                                    if (detenerBusquedaTS == true)
                                    {
                                        j = tablaSimbolos.Count;
                                    }
                                }
                            }
                        }
                    }

                    if (palabrasCF[i]=="=")
                    {
                        i = palabrasCF.Length;
                    }
                }
            }
         
            
            return erroresEncontrados;
        }
        #endregion
    }
}
