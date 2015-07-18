using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace CompiladorSIMPLE
{
    public class ControlSemantico
    {
        public string AnalizarSemantica(string lineaAnalisis, ArrayList tablaSimbolos, string codFuente, int numLinea) 
        {
            var erroresEncontrados = "";
            if (lineaAnalisis.StartsWith("INT ") || lineaAnalisis.StartsWith("DECIMAL ") || lineaAnalisis.StartsWith("TEXT ") || lineaAnalisis.StartsWith("CHAR ") || lineaAnalisis.StartsWith("FLAG "))
            {
                if (lineaAnalisis.StartsWith("INT "))
                {
                    const string expRegular = @"^(INT)(\s)[a-zA-Z][a-zA-Z0-9]*(\s)=(\s)(((([a-zA-Z][\w\s]*)|([0-9]{1,}[\s]*))(\+|\*|-|/)*(\s))+);$";
                    if (Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        var inicio = lineaAnalisis.IndexOf("=", StringComparison.Ordinal);
                        var fin = lineaAnalisis.IndexOf(";", StringComparison.Ordinal);
                        var valores = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                        valores = valores.Trim(' ');
                        var valor = valores.Split(new char[] { ' ' });
                        foreach (var t in valor.Where(t => !(t.Equals("+") || t.Equals("-") || t.Equals("/") || t.Equals("*"))))
                        {
                            if (Regex.IsMatch(t, @"^\d{1,}\.\d{1,}$") || Regex.IsMatch(t, "^(\")[\\w\\s\\W]*(\")$") || Regex.IsMatch(t, "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(t, "^(false|true)$"))
                            {
                                erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                            }
                            else
                            {
                                if (!Regex.IsMatch(t, @"^\d{1,}$"))
                                {
                                    erroresEncontrados += BuscarTS(t, "4", lineaAnalisis, tablaSimbolos, numLinea);
                                }
                            }
                        }
                    }
                    else 
                    {
                        if (Compilador.VerificarSintaxis == false)
                        {
                            erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                        }
                    }
                } 
                else if (lineaAnalisis.StartsWith("DECIMAL "))
                {
                    const string expRegular = @"^(DECIMAL)(\s)[a-zA-Z][a-zA-Z0-9]*(\s)=(\s)(((([a-zA-Z][\w\s]*)|(\d{1,}\.\d{1,}[\s]*))(\+|\*|-|/)*(\s))+);$";
                    if (Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        var inicio = lineaAnalisis.IndexOf("=", StringComparison.Ordinal);
                        var fin = lineaAnalisis.IndexOf(";", StringComparison.Ordinal);

                        var valores = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                        valores = valores.Trim(' ');

                        var valor = valores.Split(new char[] { ' ' });

                        foreach (var t in valor.Where(t => t.Equals("+") || t.Equals("-") || t.Equals("/") || t.Equals("*")))
                        {
                            if (Regex.IsMatch(t, @"^\d{1,}$") || Regex.IsMatch(t, "^(\")[\\w\\s\\W]*(\")$") || Regex.IsMatch(t, "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(t, "^(false|true)$"))
                            {
                                erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                            }
                            else
                            {

                                if (!Regex.IsMatch(t, @"^\d{1,}\.\d{1,}$"))
                                {
                                    erroresEncontrados += BuscarTS(t, "6", lineaAnalisis, tablaSimbolos, numLinea);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Compilador.VerificarSintaxis == false)
                        {
                            erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                        }
                    }
                }
                else if (lineaAnalisis.StartsWith("TEXT "))
                {
                    var inicio = lineaAnalisis.IndexOf("=", StringComparison.Ordinal);
                    var fin = lineaAnalisis.IndexOf(";", StringComparison.Ordinal);
                    var valores = "";
                    if (inicio > 0 && fin > 0)
                    {
                        valores = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                        valores = valores.Trim(' ');
                    }

                    var expRegular = "^(TEXT)(\\s)[a-zA-Z][a-zA-Z0-9]*(\\s)=(\\s)(((([a-zA-Z][\\w\\s]*)|((\")[\\w\\s]*(\")[\\s]*))(\\+)*(\\s))+);$";
                    if (Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        var valor = valores.Split(new char[] { '+' });
                        foreach (var t in valor)
                        {
                            if (!(t.Equals("+")))
                            {

                                if (Regex.IsMatch(t, @"^\d{1,}\.\d{1,}$") || Regex.IsMatch(t, @"^\d{1,}$") || Regex.IsMatch(t, "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(t, "^(false|true)$"))
                                {
                                    erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                                }
                                else
                                {

                                    if (!Regex.IsMatch(t, "(\")[\\w\\s\\W]*(\")"))
                                    {
                                        erroresEncontrados += BuscarTS(t, "5", lineaAnalisis, tablaSimbolos, numLinea);
                                    }
                                }
                            }

                            if (t.Equals("-") || t.Equals("*") || t.Equals("/"))
                            {
                                erroresEncontrados += "Error Semántico: Línea " + numLinea + ". El operador '" + t + "' no se puede aplicar a operandos del tipo 'cad' " + Environment.NewLine;
                            }
                        }
                    }
                    else
                    {
                        if (Compilador.VerificarSintaxis != false) return erroresEncontrados;
                        var valor = valores.Split(new char[] { ' ' });
                        erroresEncontrados = valor.Where(t => t.Equals("-") || t.Equals("*") || t.Equals("/")).Aggregate(erroresEncontrados, (current, t) => current + ("Error Semántico: Línea " + numLinea + ". El operador '" + t + "' no se puede aplicar a operandos del tipo 'cad' " + Environment.NewLine));
                    }
                }
                else if (lineaAnalisis.StartsWith("CHAR "))
                {
                    const string expRegular = "^(CHAR)(\\s)[a-zA-Z][a-zA-Z0-9]*(\\s)=(\\s)(')[\\w\\s\\W]?(')(\\s)(;)$";
                    if (Regex.IsMatch(lineaAnalisis, expRegular)) return erroresEncontrados;
                    if (Compilador.VerificarSintaxis != false) return erroresEncontrados;
                    var inicio = lineaAnalisis.IndexOf("=", StringComparison.Ordinal);
                    var fin = lineaAnalisis.IndexOf(";", StringComparison.Ordinal);

                    var valor = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');

                    if (Regex.IsMatch(valor, @"^\d{1,}\.\d{1,}$") || Regex.IsMatch(valor, "^(\")[\\w\\s\\W]*(\")$") || Regex.IsMatch(valor, @"^\d{1,}$") || Regex.IsMatch(valor, "^(false|true)$"))
                    {
                        erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                    }
                    else
                    {

                        var aux = valor.Split(' ');

                        if (aux.Length > 1)
                        {
                            erroresEncontrados = aux.Where(t => t.Equals("-") || t.Equals("*") || t.Equals("/") || t.Equals("+")).Aggregate(erroresEncontrados, (current, t) => current + ("Error Semántico: Línea " + numLinea + ". El operador '" + t + "' no se puede aplicar a operandos del tipo 'car' " + Environment.NewLine));
                        }
                        else 
                        {
                            erroresEncontrados += BuscarTS(valor, "8", lineaAnalisis, tablaSimbolos, numLinea);
                        }
                    }
                }
                else if (lineaAnalisis.StartsWith("FLAG "))
                {

                    var expRegular = "^(FLAG)(\\s)[a-zA-Z][a-zA-Z0-9]*(\\s)=(\\s)(false|true)?(\\s)(;)$";
                    if (Regex.IsMatch(lineaAnalisis, expRegular)) return erroresEncontrados;
                    if (Compilador.VerificarSintaxis != false) return erroresEncontrados;
                    var inicio = lineaAnalisis.IndexOf("=", StringComparison.Ordinal);
                    var fin = lineaAnalisis.IndexOf(";", StringComparison.Ordinal);

                    var valor = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');

                    if (Regex.IsMatch(valor, @"^\d{1,}\.\d{1,}$") || Regex.IsMatch(valor, "^(\")[\\w\\s\\W]*(\")$") || Regex.IsMatch(valor, "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(valor, @"^\d{1,}$"))
                    {
                        erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                    }
                    else
                    {

                        erroresEncontrados += BuscarTS(valor, "7", lineaAnalisis, tablaSimbolos, numLinea);
                    }
                }
            }
            else
            {
                const string expRegular = "^(([a-zA-Z][\\w\\s]*)(=(\\s)*)((([a-zA-Z][\\w\\s]*)|([0-9]{1,}[\\s]*)|(\\d{1,}\\.\\d{1,}[\\s]*)|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))(\\+|\\*|-|/)*(\\s))+);$";
                if (Regex.IsMatch(lineaAnalisis, expRegular))
                {
                    var inicio = lineaAnalisis.IndexOf("=", StringComparison.Ordinal);
                    var fin = lineaAnalisis.IndexOf(";", StringComparison.Ordinal);

                    var valor = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');

                    var aux = valor.Split(new char[] { ' ', '\t' });
                    fin = 0;
                    fin = lineaAnalisis.IndexOf("=", StringComparison.Ordinal);

                    var primerVariable = lineaAnalisis.Substring(0, fin - 1);

                    var refPricipal = "";

                    var refAux = "";
                    var contOperador = 0;
                    var contOperando = 0;

                    refPricipal = BuscarTS(primerVariable, tablaSimbolos);
                    foreach (var t in aux)
                    {
                        refAux = "";

                        if (!(t.Equals("+") || t.Equals("-") || t.Equals("/") || t.Equals("*")))
                        {
                            contOperando++;

                            if (Regex.IsMatch(t.ToString(), @"^\d{1,}$"))
                            {
                                refAux = "4";
                            }

                            else if (Regex.IsMatch(t.ToString(), @"^\d{1,}\.\d{1,}$"))
                            {
                                refAux = "6";
                            }

                            else if (Regex.IsMatch(t.ToString(), "^(\")[\\w\\s\\W]*(\")$"))
                            {
                                refAux = "5";
                            }

                            else if (Regex.IsMatch(t.ToString(), "^(')[\\w\\s\\W]?(')$"))
                            {
                                refAux = "8";
                            }
                            else
                            {

                                refAux = BuscarTS(t.ToString(), tablaSimbolos);
                            }

                            if (refPricipal.Equals("")) continue;
                            if (refPricipal == refAux) continue;
                            if (refAux.Equals(""))
                            {

                                if (t.Equals("INT") || t.Equals("DECIMAL") || t.Equals("TEXT") || t.Equals("CHAR") || t.Equals("FLAG") || t.Equals("START")
                                    || t.Equals("END") || t.Equals("EVAL") || t.Equals("THEN") || t.Equals("ALTERNATE") || t.Equals("DO")
                                    || t.Equals("LOOP") || t.Equals("PRINT") || t.Equals("READTOEND"))
                                {
                                    erroresEncontrados += "Error Léxico: Línea " + numLinea + ". Se esperaba un identificador, '" + t + "' es una palabra reservada." + Environment.NewLine;
                                }
                                else 
                                {
                                    erroresEncontrados += "Error Léxico: Línea " + numLinea + ". La palabra '" + t + "' no existe en el contexto actual." + Environment.NewLine;
                                }
                            }
                            else
                            {
                                erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                            }
                        }
                        else 
                        {
                            contOperador++;
                        }
                    }

                    if ((contOperador+1) != contOperando)
                    {
                        erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;    
                    }
                }
                else 
                {
                    if (lineaAnalisis.StartsWith("EVAL ") || lineaAnalisis.StartsWith("LOOP ") || lineaAnalisis.Equals("ALTERNATE"))
                    {
                        if (lineaAnalisis.StartsWith("EVAL "))
                        {
                            const string expRegular1 = "^(EVAL)(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)((y|o|<|>|!)|(<=|>=|==)){1}(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)(THEN)$";
                            if (Regex.IsMatch(lineaAnalisis, expRegular1))
                            {
                                var inicio = lineaAnalisis.IndexOf("EVAL", StringComparison.Ordinal);
                                var fin = lineaAnalisis.IndexOf("THEN", StringComparison.Ordinal);
                                var valor = lineaAnalisis.Substring(inicio + 2, (fin - 2) - inicio);
                                valor = valor.Trim(' ');
                                var valores = new ArrayList();
                                var aux = "";
                                for (var i = 0; i < valor.ToCharArray().Length; i++)
                                {
                                    
                                    if (!(valor[i] == '=' || valor[i] == '<' || valor[i] == '>' || valor[i] == '!'))
                                    {
                                        aux += valor[i].ToString();
                                    }
                                    else 
                                    {
                                        if (aux != "")
                                        {
                                            aux = aux.Trim(' ');
                                            valores.Add(aux);
                                            aux = "";    
                                        }
                                    }
                                    if (i == valor.ToCharArray().Length-1)
                                    {
                                        aux = aux.Trim(' ');
                                        valores.Add(aux);
                                        aux = "";
                                    }
                                }
                                string referencia1 = "";
                                string referencia2 = "";
                                referencia1 = BuscarTS(valores[0].ToString(), tablaSimbolos);
                                referencia2 = BuscarTS(valores[1].ToString(), tablaSimbolos);
                                if (referencia1 != "" && referencia2 != "")
                                {
                                    if (referencia1 != referencia2)
                                    {
                                        erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Incoincidencia de tipos en la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                                    }    
                                }
                            }
                        }
                        if (lineaAnalisis.StartsWith("LOOP "))
                        {
                            const string expRegular2 = "^(LOOP)(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)((y|o|<|>|!)|(<=|>=|==)){1}(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)(DO)$";
                            if (!Regex.IsMatch(lineaAnalisis, expRegular2)) return erroresEncontrados;
                            var inicio = lineaAnalisis.IndexOf("LOOP", StringComparison.Ordinal);
                            var fin = lineaAnalisis.IndexOf("DO", StringComparison.Ordinal);
                            var valor = lineaAnalisis.Substring(inicio + 8, (fin - 8) - inicio);
                            valor = valor.Trim(' ');
                            var valores = new ArrayList();
                            var aux = "";
                            for (var i = 0; i < valor.ToCharArray().Length; i++)
                            {
                                if (!(valor[i] == '=' || valor[i] == '<' || valor[i] == '>' || valor[i] == '!'))
                                {
                                    aux += valor[i].ToString();
                                }
                                else
                                {
                                    if (aux != "")
                                    {
                                        aux = aux.Trim(' ');
                                        valores.Add(aux);
                                        aux = "";
                                    }
                                }
                                if (i == valor.ToCharArray().Length - 1)
                                {
                                    aux = aux.Trim(' ');
                                    valores.Add(aux);
                                    aux = "";
                                }
                            }
                            string referencia1 = "";
                            string referencia2 = "";
                            referencia1 = BuscarTS(valores[0].ToString(), tablaSimbolos);
                            referencia2 = BuscarTS(valores[1].ToString(), tablaSimbolos);
                            if (referencia1 == "" || referencia2 == "") return erroresEncontrados;
                            if (referencia1 != referencia2)
                            {
                                erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Incoincidencia de tipos en la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                            }
                        }
                    }
                    else
                    {
                        const string expRegular1 = @"^([a-zA-Z][\w\s]*)(=(\s)*)(LeerInfo)(\s)(\()(\s)(\))(\s)(;)$";
                        if (Regex.IsMatch(lineaAnalisis, expRegular1)) { }
                        else if (lineaAnalisis.StartsWith("PRINT ") || lineaAnalisis.StartsWith("READTOEND "))
                        {
                            if (lineaAnalisis.StartsWith("PRINT "))
                            {

                            }
                        }
                        else 
                        {
                            if (!(lineaAnalisis.StartsWith("START") || lineaAnalisis.StartsWith("END")))
                            {
                                if (Compilador.VerificarSintaxis == false)
                                {
                                    erroresEncontrados += "Error Sintáctico aqui: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
            }
            return erroresEncontrados;
        }

        public string BuscarTS(string valor, string referencia, string lineaAnalisis, ArrayList tablaSimbolos, int numLinea) 
        {
            valor = valor.Trim(' ');
            var erroresEncontrados = "";
            var palabraEncontrada = false;
            var detenerBusquedaTs = false;
            var verifcarTipo = false;

            for (var j = 28; j < tablaSimbolos.Count; j++)
            {
                var auxLineasTs = tablaSimbolos[j].ToString();
                var auxPalabrasTs = auxLineasTs.Split(new char[] { ' ' });
                if (valor.Equals(auxPalabrasTs[1].ToString()))
                {
                    if (auxPalabrasTs[3].ToString().Equals(referencia))
                    {
                        verifcarTipo = true;
                    }
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
                erroresEncontrados += "Error Léxico: Línea " + numLinea + ". La palabra '" + valor + "' no existe en el contexto actual." + Environment.NewLine;
            }
            else
            {
                if (verifcarTipo == false)
                {
                    erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                }
            }
            return erroresEncontrados;
        }

        public string BuscarTS(string valor, ArrayList tablaSimbolos)
        {
            var referencia = "";
            var detenerBusquedaTs = false;

            for (int j = 28; j < tablaSimbolos.Count; j++)
            {
                var auxLineasTs = tablaSimbolos[j].ToString();
                var auxPalabrasTs = auxLineasTs.Split(new char[] { ' ' });
                if (valor.Equals(auxPalabrasTs[1].ToString()))
                {
                    referencia = auxPalabrasTs[3].ToString();
                    detenerBusquedaTs = true;
                }
                if (detenerBusquedaTs == true)
                {
                    j = tablaSimbolos.Count;
                }
            }
            if (referencia.Equals(""))
            {
                if (Regex.IsMatch(valor, @"^\d{1,}$"))
                {
                    referencia = "4";
                }
                else if (Regex.IsMatch(valor, @"^\d{1,}\.\d{1,}$"))
                {
                    referencia = "6";
                }
                else if (Regex.IsMatch(valor, "^(\")[\\w\\s\\W]*(\")$"))
                {
                    referencia = "5";
                }
                else if (Regex.IsMatch(valor, "^(')[\\w\\s\\W]?(')$"))
                {
                    referencia = "8";
                }
                else if (Regex.IsMatch(valor, "^(false|true)$"))
                {
                    referencia = "7";
                }
            }
            return referencia;
        }
    }
}
