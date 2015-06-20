using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Logica
{
    public class ControlSemantico
    {
        public string AnalizarSemantica(string lineaAnalisis, ArrayList tablaSimbolos, string codFuente, int numLinea) 
        {
            string erroresEncontrados = "";
            // si la instruccion inicia con las siguientes palabras
            if (lineaAnalisis.StartsWith("INT ") || lineaAnalisis.StartsWith("DECIMAL ") || lineaAnalisis.StartsWith("TEXT ") || lineaAnalisis.StartsWith("CHAR ") || lineaAnalisis.StartsWith("FLAG "))
            {
                //si la instruccion incia con ent
                if (lineaAnalisis.StartsWith("INT "))
                {
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    string expRegular = @"^(INT)(\s)[a-zA-Z][a-zA-Z0-9]*(\s)=(\s)(((([a-zA-Z][\w\s]*)|([0-9]{1,}[\s]*))(\+|\*|-|/)*(\s))+);$";
                    if (Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        int inicio = lineaAnalisis.IndexOf("=");
                        int fin = lineaAnalisis.IndexOf(";");
                        //se obtiene el o los valor que esta entre el signo de '=' y el ';'
                        string valores = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                        valores = valores.Trim(' ');
                        //si hay mas de un valor se ingresan en el siguiente vector
                        string[] valor = valores.Split(new char[] { ' ' });
                        //se recorre el vector que contiene los valores
                        for (int i = 0; i < valor.Length; i++)
                        {
                            //si es diferente a un operador
                            if (!(valor[i].Equals("+") || valor[i].Equals("-") || valor[i].Equals("/") || valor[i].Equals("*")))
                            {
                                //si el valor tiene el formato que tienen las expresiones regulares se muestra el error
                                if (Regex.IsMatch(valor[i], @"^\d{1,}\.\d{1,}$") || Regex.IsMatch(valor[i], "^(\")[\\w\\s\\W]*(\")$") || Regex.IsMatch(valor[i], "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(valor[i], "^(false|true)$"))
                                {
                                    erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                                }
                                else
                                {
                                    //si el valor tiene el formato que tiene la expresion regular quiere decir que el valor coincide con el tipo de dato
                                    //sino quiere decir que es una variable, se envia al metodo BuscarTS para saber si existe y comprobar su tipo
                                    if (!Regex.IsMatch(valor[i], @"^\d{1,}$"))
                                    {
                                        erroresEncontrados += BuscarTS(valor[i], "4", lineaAnalisis, tablaSimbolos, numLinea);
                                    }
                                }
                            }
                        }
                    }
                    else 
                    {
                        //si la linea en analisis no cumple con el formato, se verifica que en el analizador sintactico no hubo errores para mostrar el mensaje
                        if (Compilador.VerificarSintaxis == false)
                        {
                            erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                        }
                    }
                }//si la instruccion incia con dob
                else if (lineaAnalisis.StartsWith("DECIMAL "))
                {
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    string expRegular = @"^(DECIMAL)(\s)[a-zA-Z][a-zA-Z0-9]*(\s)=(\s)(((([a-zA-Z][\w\s]*)|(\d{1,}\.\d{1,}[\s]*))(\+|\*|-|/)*(\s))+);$";
                    if (Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        int inicio = lineaAnalisis.IndexOf("=");
                        int fin = lineaAnalisis.IndexOf(";");
                        //se obtiene el o los valor que esta entre el signo de '=' y el ';'
                        string valores = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                        valores = valores.Trim(' ');
                        //si hay mas de un valor se ingresan en el siguiente vector
                        string[] valor = valores.Split(new char[] { ' ' });
                        //se recorre el vector que contiene los valores
                        for (int i = 0; i < valor.Length; i++)
                        {
                            //si es diferente a un operador
                            if (!(valor[i].Equals("+") || valor[i].Equals("-") || valor[i].Equals("/") || valor[i].Equals("*")))
                            {
                                //si el valor tiene el formato que tienen las expresiones regulares se muestra el error
                                if (Regex.IsMatch(valor[i], @"^\d{1,}$") || Regex.IsMatch(valor[i], "^(\")[\\w\\s\\W]*(\")$") || Regex.IsMatch(valor[i], "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(valor[i], "^(false|true)$"))
                                {
                                    erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                                }
                                else
                                {
                                    //si el valor tiene el formato que tiene la expresion regular quiere decir que el valor coincide con el tipo de dato
                                    //sino quiere decir que es una variable, se envia al metodo BuscarTS para saber si existe y comprobar su tipo
                                    if (!Regex.IsMatch(valor[i], @"^\d{1,}\.\d{1,}$"))
                                    {
                                        erroresEncontrados += BuscarTS(valor[i], "6", lineaAnalisis, tablaSimbolos, numLinea);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //si la linea en analisis no cumple con el formato, se verifica que en el analizador sintactico no hubo errores para mostrar el mensaje
                        if (Compilador.VerificarSintaxis == false)
                        {
                            erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                        }
                    }
                }//si la instruccion incia con dob
                else if (lineaAnalisis.StartsWith("TEXT "))
                {
                    int inicio = lineaAnalisis.IndexOf("=");
                    int fin = lineaAnalisis.IndexOf(";");
                    string valores = "";
                    if (inicio > 0 && fin > 0)
                    {
                        //se obtiene el o los valor que esta entre el signo de '=' y el ';'
                        valores = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                        valores = valores.Trim(' ');
                    }
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    string expRegular = "^(TEXT)(\\s)[a-zA-Z][a-zA-Z0-9]*(\\s)=(\\s)(((([a-zA-Z][\\w\\s]*)|((\")[\\w\\s]*(\")[\\s]*))(\\+)*(\\s))+);$";
                    if (Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        //si hay mas de un valor se ingresan en el siguiente vector
                        string[] valor = valores.Split(new char[] { '+' });//recordar que en los tipos 'cad' solo pueden ir operadores de suma
                        for (int i = 0; i < valor.Length; i++)
                        {
                            //si es diferente a un operador de suma
                            if (!(valor[i].Equals("+")))
                            {
                                //si el valor tiene el formato que tienen las expresiones regulares se muestra el error
                                if (Regex.IsMatch(valor[i], @"^\d{1,}\.\d{1,}$") || Regex.IsMatch(valor[i], @"^\d{1,}$") || Regex.IsMatch(valor[i], "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(valor[i], "^(false|true)$"))
                                {
                                    erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                                }
                                else
                                {
                                    //si el valor tiene el formato que tiene la expresion regular quiere decir que el valor coincide con el tipo de dato
                                    //sino quiere decir que es una variable, se envia al metodo BuscarTS para saber si existe y comprobar su tipo
                                    if (!Regex.IsMatch(valor[i], "(\")[\\w\\s\\W]*(\")"))
                                    {
                                        erroresEncontrados += BuscarTS(valor[i], "5", lineaAnalisis, tablaSimbolos, numLinea);
                                    }
                                }
                            }
                            //si encuentra uno de los siguientes operadores se muestra el error
                            if (valor[i].Equals("-") || valor[i].Equals("*") || valor[i].Equals("/"))
                            {
                                erroresEncontrados += "Error Semántico: Línea " + numLinea + ". El operador '" + valor[i] + "' no se puede aplicar a operandos del tipo 'cad' " + Environment.NewLine;
                            }
                        }
                    }
                    else
                    {
                        //si la linea en analisis no cumple con el formato, se verifica que en el analizador sintactico no hubo errores para mostrar el mensaje
                        if (Compilador.VerificarSintaxis == false)
                        {
                            //se obtiene el o los valor que esta entre el signo de '=' y el ';'
                            string[] valor = valores.Split(new char[] { ' ' });
                            for (int i = 0; i < valor.Length; i++)
                            {
                                //si encuentra uno de los siguientes operadores se muestra el error
                                if (valor[i].Equals("-") || valor[i].Equals("*") || valor[i].Equals("/"))
                                {
                                    erroresEncontrados += "Error Semántico: Línea " + numLinea + ". El operador '" + valor[i] + "' no se puede aplicar a operandos del tipo 'cad' " + Environment.NewLine;
                                }   
                            }
                            
                        }
                    }
                }//si la instruccion incia con dob
                else if (lineaAnalisis.StartsWith("CHAR "))
                {
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    string expRegular = "^(CHAR)(\\s)[a-zA-Z][a-zA-Z0-9]*(\\s)=(\\s)(')[\\w\\s\\W]?(')(\\s)(;)$";
                    if (!Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        //si la linea en analisis no cumple con el formato, se verifica que en el analizador sintactico no hubo errores para mostrar el mensaje
                        if (Compilador.VerificarSintaxis == false)
                        {
                            int inicio = lineaAnalisis.IndexOf("=");
                            int fin = lineaAnalisis.IndexOf(";");
                            //se obtiene el o los valor que esta entre el signo de '=' y el ';'
                            string valor = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                            valor = valor.Trim(' ');
                            //si el valor tiene el formato que tienen las expresiones regulares se muestra el error
                            if (Regex.IsMatch(valor, @"^\d{1,}\.\d{1,}$") || Regex.IsMatch(valor, "^(\")[\\w\\s\\W]*(\")$") || Regex.IsMatch(valor, @"^\d{1,}$") || Regex.IsMatch(valor, "^(false|true)$"))
                            {
                                erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                            }
                            else
                            {
                                //si hay mas de un valor se ingresan en el siguiente vector
                                string[] aux = valor.Split(' ');
                                //si hay mas de un valor en la definicion
                                if (aux.Length > 1)
                                {
                                    for (int i = 0; i < aux.Length; i++)
                                    {
                                        //si encuentra uno de los siguientes operadores se muestra el error
                                        if (aux[i].Equals("-") || aux[i].Equals("*") || aux[i].Equals("/") || aux[i].Equals("+"))
                                        {
                                            erroresEncontrados += "Error Semántico: Línea " + numLinea + ". El operador '" + aux[i] + "' no se puede aplicar a operandos del tipo 'car' " + Environment.NewLine;
                                        }
                                    }
                                }
                                else //si solo hay un valor se envia al metodo BuscarTS para comprobar si existe y su tipo
                                {
                                    erroresEncontrados += BuscarTS(valor, "8", lineaAnalisis, tablaSimbolos, numLinea);
                                }
                            }
                        }
                    }
                }//si la instruccion incia con bin
                else if (lineaAnalisis.StartsWith("FLAG "))
                {
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    string expRegular = "^(FLAG)(\\s)[a-zA-Z][a-zA-Z0-9]*(\\s)=(\\s)(false|true)?(\\s)(;)$";
                    if (!Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        //si la linea en analisis no cumple con el formato, se verifica que en el analizador sintactico no hubo errores para mostrar el mensaje
                        if (Compilador.VerificarSintaxis == false)
                        {
                            int inicio = lineaAnalisis.IndexOf("=");
                            int fin = lineaAnalisis.IndexOf(";");
                            //se obtiene el o los valor que esta entre el signo de '=' y el ';'
                            string valor = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                            valor = valor.Trim(' ');
                            //si el valor tiene el formato que tienen las expresiones regulares se muestra el error
                            if (Regex.IsMatch(valor, @"^\d{1,}\.\d{1,}$") || Regex.IsMatch(valor, "^(\")[\\w\\s\\W]*(\")$") || Regex.IsMatch(valor, "^(')[\\w\\s\\W]?(')$") || Regex.IsMatch(valor, @"^\d{1,}$"))
                            {
                                erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                            }
                            else
                            {
                                //si solo hay un valor se envia al metodo BuscarTS para comprobar si existe y su tipo
                                erroresEncontrados += BuscarTS(valor, "7", lineaAnalisis, tablaSimbolos, numLinea);
                            }
                        }
                    }
                }
            }
            else 
            {
                //si la linea en analisis tiene el formato que tiene la expresion regular, se analizara sus valores para comprobar sus tipos
                string expRegular = "^(([a-zA-Z][\\w\\s]*)(=(\\s)*)((([a-zA-Z][\\w\\s]*)|([0-9]{1,}[\\s]*)|(\\d{1,}\\.\\d{1,}[\\s]*)|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))(\\+|\\*|-|/)*(\\s))+);$";
                if (Regex.IsMatch(lineaAnalisis, expRegular))
                {
                    int inicio = lineaAnalisis.IndexOf("=");
                    int fin = lineaAnalisis.IndexOf(";");
                    //se obtiene el o los valor que esta entre el signo de '=' y el ';'
                    string valor = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');
                    //si hay mas de un valor se ingresan en el siguiente vector
                    string[] aux = valor.Split(new char[] { ' ', '\t' });
                    fin = 0;
                    fin = lineaAnalisis.IndexOf("=");
                    //se obtiene la primer varialbe a la cual se le esta asignando los valores
                    string primerVariable = lineaAnalisis.Substring(0, fin - 1);
                    //almacena la referencia hacia el tipo de dato de la primer variable
                    string refPricipal = "";
                    //almacena la referencia hacia el tipo de dato de cada valor
                    string refAux = "";
                    int contOperador = 0;
                    int contOperando = 0;
                    //se envia la primer variable para buscar su tipo de dato
                    refPricipal = BuscarTS(primerVariable, tablaSimbolos);
                    for (int i = 0; i < aux.Length; i++)
                    {
                        refAux = "";
                        // si es diferente a un operador
                        if (!(aux[i].Equals("+") || aux[i].Equals("-") || aux[i].Equals("/") || aux[i].Equals("*")))
                        {
                            contOperando++;
                            //si el valor es un numero entero
                            if (Regex.IsMatch(aux[i].ToString(), @"^\d{1,}$"))
                            {
                                refAux = "4";
                            }
                            //si el valor es un numero doble
                            else if (Regex.IsMatch(aux[i].ToString(), @"^\d{1,}\.\d{1,}$"))
                            {
                                refAux = "6";
                            }
                            //si el valor es una cadena de caracteres
                            else if (Regex.IsMatch(aux[i].ToString(), "^(\")[\\w\\s\\W]*(\")$"))
                            {
                                refAux = "5";
                            }
                            //si el valor es un caracter
                            else if (Regex.IsMatch(aux[i].ToString(), "^(')[\\w\\s\\W]?(')$"))
                            {
                                refAux = "8";
                            }
                            else
                            {
                                //si es una variable se envia al metodo BuscarTS para que busque una referencia hacia su tipo de dato
                                refAux = BuscarTS(aux[i].ToString(), tablaSimbolos);
                            }

                            if (!refPricipal.Equals(""))
                            {
                                // si son diferentes quiere decir que sus tipo no coinciden
                                if (refPricipal != refAux)
                                {
                                    if (refAux.Equals(""))
                                    {
                                        //se verifica que el valor no sea una palabra reservada
                                        if (aux[i].Equals("INT") || aux[i].Equals("DECIMAL") || aux[i].Equals("TEXT") || aux[i].Equals("CHAR") || aux[i].Equals("FLAG") || aux[i].Equals("START")
                                            || aux[i].Equals("END") || aux[i].Equals("EVAL") || aux[i].Equals("THEN") || aux[i].Equals("ALTERNATE") || aux[i].Equals("DO")
                                            || aux[i].Equals("LOOP") || aux[i].Equals("PRINT") || aux[i].Equals("READTOEND"))
                                        {
                                            erroresEncontrados += "Error Léxico: Línea " + numLinea + ". Se esperaba un identificador, '" + aux[i] + "' es una palabra reservada." + Environment.NewLine;
                                        }
                                        else 
                                        {
                                            erroresEncontrados += "Error Léxico: Línea " + numLinea + ". La palabra '" + aux[i] + "' no existe en el contexto actual." + Environment.NewLine;
                                        }
                                    }
                                    else
                                    {
                                        erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Valor inválido para la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                                    }
                                }
                            }
                        }
                        else 
                        {
                            contOperador++;
                        }
                    }
                    //se verifica que la cantidad de operadores coincida con los operandos
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
                            //si la linea en analisis tiene el formato que tiene la expresion regular, se analizara sus valores para comprobar sus tipos
                            string expRegular1 = "^(EVAL)(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)((y|o|<|>|!)|(<=|>=|==)){1}(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)(THEN)$";
                            if (Regex.IsMatch(lineaAnalisis, expRegular1))
                            {
                                int inicio = lineaAnalisis.IndexOf("EVAL");
                                int fin = lineaAnalisis.IndexOf("THEN");
                                //se obtiene los valores de la condicion, los que se encuentran en medio de la palabra si y entonces
                                string valor = lineaAnalisis.Substring(inicio + 2, (fin - 2) - inicio);
                                valor = valor.Trim(' ');
                                ArrayList valores = new ArrayList();
                                string aux = "";
                                for (int i = 0; i < valor.ToCharArray().Length; i++)
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
                            string expRegular2 = "^(LOOP)(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)((y|o|<|>|!)|(<=|>=|==)){1}(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)(DO)$";
                            if (Regex.IsMatch(lineaAnalisis, expRegular2))
                            {
                                int inicio = lineaAnalisis.IndexOf("LOOP");
                                int fin = lineaAnalisis.IndexOf("DO");
                                string valor = lineaAnalisis.Substring(inicio + 8, (fin - 8) - inicio);
                                valor = valor.Trim(' ');
                                ArrayList valores = new ArrayList();
                                string aux = "";
                                for (int i = 0; i < valor.ToCharArray().Length; i++)
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
                                if (referencia1 != "" && referencia2 != "")
                                {
                                    if (referencia1 != referencia2)
                                    {
                                        erroresEncontrados += "Error Semántico: Línea " + numLinea + ". Incoincidencia de tipos en la definición '" + lineaAnalisis + "'." + Environment.NewLine;
                                    }
                                }
                            }
                        }
                    }
                    else 
                    {
                        string expRegular1 = @"^([a-zA-Z][\w\s]*)(=(\s)*)(LeerInfo)(\s)(\()(\s)(\))(\s)(;)$";
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
            string erroresEncontrados = "";
            string auxLineasTS = "";
            string[] auxPalabrasTS;
            bool palabraEncontrada = false;
            bool detenerBusquedaTS = false;
            bool verifcarTipo = false;

            for (int j = 28; j < tablaSimbolos.Count; j++)
            {
                auxLineasTS = tablaSimbolos[j].ToString();
                auxPalabrasTS = auxLineasTS.Split(new char[] { ' ' });
                if (valor.Equals(auxPalabrasTS[1].ToString()))
                {
                    if (auxPalabrasTS[3].ToString().Equals(referencia))
                    {
                        verifcarTipo = true;
                    }
                    palabraEncontrada = true;
                    detenerBusquedaTS = true;
                }
                if (detenerBusquedaTS == true)
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
            string referencia = "";
            string auxLineasTS = "";
            string[] auxPalabrasTS;
            bool detenerBusquedaTS = false;

            for (int j = 28; j < tablaSimbolos.Count; j++)
            {
                auxLineasTS = tablaSimbolos[j].ToString();
                auxPalabrasTS = auxLineasTS.Split(new char[] { ' ' });
                if (valor.Equals(auxPalabrasTS[1].ToString()))
                {
                    referencia = auxPalabrasTS[3].ToString();
                    detenerBusquedaTS = true;
                }
                if (detenerBusquedaTS == true)
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
