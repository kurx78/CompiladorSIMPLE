using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace CompiladorSIMPLE
{
    public class ControlLexico
    {
        #region Métodos

        public string AnalisisLexico(string lineaAnalisis, ArrayList tablaSimbolos, int numLinea)  
        {
            

            string erroresEncontrados = "";

            lineaAnalisis = lineaAnalisis.Trim(new char[] { '\t', ' ' });

            var palabrasCf = lineaAnalisis.Split(new char[] {' ', '\n', '\t'});

            const string expRegular = @"[^a-zA-Z0-9;""'' <>\+-/\[\]\*=!¡_]";

            for (var i = 0; i < palabrasCf.Length; i++)
            {
                if (palabrasCf[i] == "") continue;
                var detenerBusquedaTs = false;
                var palabraEncontrada = false;
                string[] auxPalabrasTs;
                var auxLineasTs = "";
                for (var j = 0; j < tablaSimbolos.Count; j++)
                {

                    auxLineasTs = tablaSimbolos[j].ToString();

                    auxPalabrasTs = auxLineasTs.Split(new char[] { ' ' });

                    if (palabrasCf[i].Equals(auxPalabrasTs[1].ToString()))
                    {
                        palabraEncontrada = true;
                        detenerBusquedaTs = true;
                    }
                    if (detenerBusquedaTs == true)
                    {

                        j = tablaSimbolos.Count;
                    }
                }

                if (palabraEncontrada == false)
                {

                    if (lineaAnalisis.StartsWith("INT ") || lineaAnalisis.StartsWith("DECIMAL ") || lineaAnalisis.StartsWith("CHAR ") || 
                        lineaAnalisis.StartsWith("TEXT ") || lineaAnalisis.StartsWith("FLAG ") || lineaAnalisis.StartsWith("PRINT "))
                    {
                        if (!lineaAnalisis.StartsWith("PRINT "))
                        {

                            if (Regex.IsMatch(palabrasCf[i], expRegular))
                            {
                                erroresEncontrados += "Analisis Léxico - línea " + numLinea + ". Caracter inválido en palabra '" + palabrasCf[i] + "'" + Environment.NewLine;
                            }    
                        }
                    }
                    else
                    {

                        if (!(Regex.IsMatch(palabrasCf[i], @"^\d{1,}$")||Regex.IsMatch(palabrasCf[i], @"^\d{1,}\.\d{1,}$")||Regex.IsMatch(palabrasCf[i], "^(\")[\\w\\s\\W]*(\")$")
                              || Regex.IsMatch(palabrasCf[i], "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(palabrasCf[i], "^(false|true)$")))
                        {
                            erroresEncontrados += "Analisis Léxico - línea " + numLinea + ". La palabra '" + palabrasCf[i] + "' no existe en el contexto actual." + Environment.NewLine;
                        }
                    }
                }
                else 
                {

                    if (lineaAnalisis.StartsWith("INT ") || lineaAnalisis.StartsWith("DECIMAL ") || lineaAnalisis.StartsWith("CHAR ") 
                        || lineaAnalisis.StartsWith("TEXT ") || lineaAnalisis.StartsWith("FLAG "))
                    {
                        if (i==1)
                        {
                            detenerBusquedaTs = false;
                            for (var j = 0; j < tablaSimbolos.Count; j++)
                            {
                                auxLineasTs = tablaSimbolos[j].ToString();
                                auxPalabrasTs = auxLineasTs.Split(new char[] { ' ' });
                                if (palabrasCf[1].Equals(auxPalabrasTs[1].ToString()))
                                {

                                    if (j > 29)
                                    {
                                        erroresEncontrados += "Analisis Léxico - línea " + numLinea + ". Ya se ha definido un identificador denominado '" + palabrasCf[1] + 
                                            "' en el contexto actual." + Environment.NewLine;
                                        detenerBusquedaTs = true;
                                    }
                                    else
                                    {

                                        erroresEncontrados += "Analisis Léxico - línea " + numLinea + ". Se esperaba un identificador, '" + palabrasCf[1] + "' " +
                                                              "es una palabra reservada." + Environment.NewLine;
                                        detenerBusquedaTs = true;
                                    }
                                }
                                if (detenerBusquedaTs == true)
                                {
                                    j = tablaSimbolos.Count;
                                }
                            }
                        }
                    }
                }

                if (palabrasCf[i]=="=")
                {
                    i = palabrasCf.Length;
                }
            }
         
            
            return erroresEncontrados;
        }
        #endregion
    }
}
