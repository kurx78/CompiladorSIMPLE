using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace CompiladorSIMPLE
{
    public class ControlSintactico
    {
        //la siguiente variable sirve para validar que solo una vez se analice la estructura del codigo fuente y no se analice cada vez que entra
        //cada linea del codigo fuente
        private bool _verificarEstructuras = false;

        #region Métodos

        public string AnalizarSintaxis(string lineaAnalisis, ArrayList tablaSimbolos, string codFuente, int numLinea) 
        {
            string erroresEncontrados = "";
            //Pasamos por parametro el codigo fuente para obtener linea por linea del codigo fuente
            StringReader leer = new StringReader(codFuente);

            if (_verificarEstructuras == false)
            {
                //variable que sirve para verificar que la palabra inicio este dentro del codigo fuente
                bool verificarInicio = false;
                //se marca la varialbe en true para que solo analice la estructura del codigo fuente una sola vez
                _verificarEstructuras = true;
                //la siguiente variable almacena la cantidad de delimitadores 'fin' que haya en el codigo fuente
                int contadorFin = 0;
                //indica la cantidad de delimitadores 'fin' que tiene que tener el codigo fuente
                int contadorFinTotales = 0;
                //almacena la cantidad de 'sinos' que hayan en el codigo fuente
                int contSino = 0;
                //almacena la cantidad de 'si' que hayan en el codigo fuente
                int contSi = 0;
                //se almacena cada instruccion del codigo fuente en la variable instruccion
                var instruccion = "";
                while ((instruccion = leer.ReadLine()) != null)
                {
                    instruccion = instruccion.Trim(new char[] { '\t', ' ' });
                    //verifica que la palabra 'inicio' existe en el codigo fuente
                    if (instruccion.Equals("START"))
                    {
                        verificarInicio = true;
                    }
                    //se verifica cuantos delimitadore 'fin' hay en el codigo fuente
                    if (instruccion.Equals("END"))
                    {
                        contadorFinTotales++;
                    }
                    //verifica cuantas instrucciones del tipo 'si condicion entonces' hay en el codigo fuente
                    if (instruccion.StartsWith("EVAL "))
                    {
                        contSi++;
                        //para cada tipo de instruccion 'si condicion entonces' debe haber un delimitador 'fin'
                        contadorFin++;
                    }
                    //
                    if (instruccion.Equals("ALERNATE"))
                    {
                        contSino++;
                        contadorFin++;
                    }
                    //verifica si existen las instrucciones del tipo 'mientras condicion hacer' en el codigo fuente
                    if (instruccion.StartsWith("LOOP "))
                    {
                        //para cada tipo de instruccion 'mientras condicion hacer' debe haber un delimitador 'fin'
                        contadorFin++;
                    }
                }
                //si la palabra inicio no existe en el codigo fuente se muestra el mensaje
                if (verificarInicio == false)
                {
                    erroresEncontrados += "Error Sintáctico: Se espera la palabra 'START' en la estructura principal del código." + Environment.NewLine;
                }
                //verifica que cada estructura tenga su delimitador 'fin'
                if ((contadorFinTotales-1) != contadorFin)
                {
                    if ((contadorFinTotales - 1) < contadorFin)
                    {
                        erroresEncontrados += "Error Sintáctico: Se espera la palabra 'END' en la estructura principal del código." + Environment.NewLine;
                    }
                    else 
                    {
                        erroresEncontrados += "Error Sintáctico: La palabra 'END' no coincide con el número de estructuras." + Environment.NewLine;
                    }
                }
                //verifica que si hay una palabra 'sino' previamente deberia estar la instruccion 'si condicion entonces'
                if (contSi > 0 || contSino > 0)
                {
                    if (contSino != 0)
                    {
                        if (contSino > contSi)
                        {
                            erroresEncontrados += "Error Sintáctico: El término de la expresión 'ALTERNATE' no es válido." + Environment.NewLine;
                        }
                    }
                }
            }
            
            lineaAnalisis = lineaAnalisis.Trim(new char[] { '\t', ' ' });
            //si la linea de analisis termina con ;
            if (lineaAnalisis.EndsWith(";"))
            {
                //si la linea de analisis inicia con 'ent '
                if (lineaAnalisis.StartsWith("INT "))
                {
                    //la siguiente expresion regular contiene el formato que debe tener la definicion de usuario del tipo ent
                    string expRegular = @"^(INT)(\s)[a-zA-Z][a-zA-Z0-9]*(\s)=(\s)(((([a-zA-Z][\w\s]*)|(.{1,}[\s]*))(\+|\*|-|/)*(\s))+);$";
                    int inicio = lineaAnalisis.IndexOf("=");
                    int fin = lineaAnalisis.IndexOf(";");
                    //se obtiene el valor que esta entre el signo de '=' y el ';'
                    string valor = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');
                    //se ingresa el o los valores en el vector
                    string[] aux = valor.Split(new char[] { ' ', '\t' });
                    //almacena la cantidad de operadores que contiene la definicion de usuario
                    int contOperador = 0;
                    //almacena la cantidad de operandos que contiene la definicion de usuario
                    int contOperando = 0;
                    for (int i = 0; i < aux.Length; i++)
                    {
                        //si hay mas de un valor
                        if (aux.Length > 0)
                        {
                            //si es un operador
                            if (aux[i].Equals("+") || aux[i].Equals("-") || aux[i].Equals("/") || aux[i].Equals("*"))
                            {
                                contOperador++;
                            }
                            else//si es un operando
                            {
                                contOperando++;
                            }   
                        }
                    }
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    if (!Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                        Compilador.VerificarSintaxis = true;
                    }
                    //se verifica que la cantidad de operandos coincida con la cantidad de operadores
                    if ((contOperador + 1) != contOperando)
                    {
                        if (Compilador.VerificarSintaxis == false)
                        {
                            erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                        }
                    }
                }//si la linea de analisis inicia con 'dob '
                else if (lineaAnalisis.StartsWith("DECIMAL "))
                {
                    //la siguiente expresion regular contiene el formato que debe tener la definicion de usuario del tipo dob
                    string expRegular = @"^(DECIMAL)(\s)[a-zA-Z][a-zA-Z0-9]*(\s)=(\s).{1,}(\s)(;)$";
                    int inicio = lineaAnalisis.IndexOf("=");
                    int fin = lineaAnalisis.IndexOf(";");
                    //se obtiene el valor que esta entre el signo de '=' y el ';'
                    string valor = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');
                    //se ingresa el o los valores en el vector
                    string[] aux = valor.Split(new char[] { ' ', '\t' });
                    //almacena la cantidad de operadores que contiene la definicion de usuario
                    int contOperador = 0;
                    //almacena la cantidad de operandos que contiene la definicion de usuario
                    int contOperando = 0;
                    for (int i = 0; i < aux.Length; i++)
                    {
                        //si hay mas de un valor
                        if (aux.Length > 0)
                        {
                            //si es un operador
                            if (aux[i].Equals("+") || aux[i].Equals("-") || aux[i].Equals("/") || aux[i].Equals("*"))
                            {
                                contOperador++;
                            }
                            else//si es un operando
                            {
                                contOperando++;
                            }
                        }
                    }
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    if (!Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                        Compilador.VerificarSintaxis = true;
                    }
                    //se verifica que la cantidad de operandos coincida con la cantidad de operadores
                    if ((contOperador + 1) != contOperando)
                    {
                        if (Compilador.VerificarSintaxis == false)
                        {
                            erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                        }
                    }
                }//si la linea de analisis inicia con 'dob '
                else if (lineaAnalisis.StartsWith("TEXT "))
                {
                    //la siguiente expresion regular contiene el formato que debe tener la definicion de usuario del tipo dob
                    string expRegular = "^(TEXT)(\\s)[a-zA-Z][a-zA-Z0-9]*(\\s)=(\\s).{1,}(\\s)(;)$";
                    //almacena la cantidad de operadores que contiene la definicion de usuario
                    int contOperador = 0;
                    //almacena la cantidad de operandos que contiene la definicion de usuario
                    int contOperandos = 0;
                    //se utiliza para contar la cantidad de comillas. Por cada par de comillas hay un valor(operando)
                    int contComillas = 0;
                    int inicio = lineaAnalisis.IndexOf("=");
                    int fin = lineaAnalisis.IndexOf(";");
                    //se obtiene el valor que esta entre el signo de '=' y el ';'
                    string valor = lineaAnalisis.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');
                    for (int i = 0; i < valor.ToCharArray().Length; i++)
                    {
                        //si es un operador +
                        if (valor[i].ToString().Equals("+"))
                        {
                            contOperador++;
                        }
                        // si es una comilla
                        if (valor[i].ToString().Equals("\""))
                        {
                            contComillas++;
                        }
                    }
                    //se obtiene los valores(operandos) que se encuentre separados por el operador de suma
                    string[] aux = valor.Split(new char[] { '+' });
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    if (!Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                        Compilador.VerificarSintaxis = true;
                    }
                    //si el contador de comillas no contiene un valor par quiere decir que faltan comillas en los valores
                    if (!((contComillas % 2) == 0))
                    {
                        erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                    }
                    //se obtiene la cantidad de valores que hay en la definicion. Es decir en cada valor debe haber 2 comillas(una de apertura y otra de cierre)
                    //Por ejemplo si hay 4 comillas al dividirlo por 2 nos dara la cantidad de valores que hay en la definicion 
                    contOperandos = contComillas / 2;
                    if (contOperandos > 1)
                    {
                        //se suma uno al contOperador ya que el numero de operadores siempre sera un valor menos que el de los operandos
                        if ((contOperador + 1) != contOperandos)
                        {
                            if ((contOperador + 1) != aux.Length)
                            {
                                if (Compilador.VerificarSintaxis == false)
                                {
                                    erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                                }
                            }
                        }    
                    }
                }//si la linea de analisis inicia con 'car '
                else if (lineaAnalisis.StartsWith("CHAR "))
                {
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    string expRegular = "^(CHAR)(\\s)[a-zA-Z][a-zA-Z0-9]*(\\s)=(\\s).{1,}(\\s)(;)$";
                    if (!Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                        Compilador.VerificarSintaxis = true;
                    }
                }//si la linea de analisis inicia con 'bin '
                else if (lineaAnalisis.StartsWith("FLAG "))
                {
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    string expRegular = "^(FLAG)(\\s)[a-zA-Z][a-zA-Z0-9]*(\\s)=(\\s).{1,}(\\s)(;)$";
                    if (!Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las regla sintácticas del lenguaje." + Environment.NewLine;
                        Compilador.VerificarSintaxis = true;
                    }
                }//si la linea de analisis inicia con 'MostrarInfo '
                else if (lineaAnalisis.StartsWith("PRINT ")) 
                {
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    string expRegular = @"^(PRINT)(\s)(\()(\s).{1,}(\s)(\))(\s);$";
                    if (!Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las regla sintácticas del lenguaje." + Environment.NewLine;
                        Compilador.VerificarSintaxis = true;
                    }
                }//si la linea de analisis inicia con 'LeerInfo '
                else if (lineaAnalisis.StartsWith("READTOEND "))
                {
                    //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                    string expRegular = @"^(READTOEND)(\s)(\()(\s)(\))(\s);$";
                    if (!Regex.IsMatch(lineaAnalisis, expRegular))
                    {
                        erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las regla sintácticas del lenguaje." + Environment.NewLine;
                        Compilador.VerificarSintaxis = true;
                    }
                }
            }
            else //si la instruccion no inicia con ;
            {
                //si la instruccion inicia con ent, dob, cad, car y bin quiere decir que falta el ;
                if (lineaAnalisis.StartsWith("INT ") || lineaAnalisis.StartsWith("DECIMAL ") || lineaAnalisis.StartsWith("TEXT ") || lineaAnalisis.StartsWith("CHAR ") || lineaAnalisis.StartsWith("FLAG "))
                {
                    erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". Se esperaba ';' en la expresión '" + lineaAnalisis + "'." + Environment.NewLine;
                    Compilador.VerificarSintaxis = true;
                }
                else 
                {
                    // si la instruccion inicia con si condicion entonces o mientras condicion hacer o sino
                    if (lineaAnalisis.StartsWith("EVAL ") || lineaAnalisis.StartsWith("LOOP ") || lineaAnalisis.Equals("ALTERNATE"))
                    {
                        // si la instruccion inicia con si condicion entonces
                        if (lineaAnalisis.StartsWith("EVAL "))
                        {
                            //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                            string expRegular = "^(EVAL)(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)((<|>)|(!=|<=|>=|==)){1}(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)(THEN)$"; 
                            if (!Regex.IsMatch(lineaAnalisis,expRegular))
                            {
                                erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                            }
                        }// si la instruccion inicia con mientras condicion hacer
                        if (lineaAnalisis.StartsWith("LOOP "))
                        {
                            //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                            string expRegular = "^(LOOP)(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)((<|>)|(!=|<=|>=|==)){1}(\\s)(([a-zA-Z0-9])|(\\d{1,}\\.\\d{1,})|((\")[\\w\\s\\W]*(\"))|((')[\\w\\s\\W]?(')))+(\\s)(DO)$";
                            if (!Regex.IsMatch(lineaAnalisis, expRegular))
                            {
                                erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                            }
                        }
                        if (lineaAnalisis.Equals("ALTERNATE"))
                        {
                            //se verifica que la linea de analisis cumpla con el formato de la expresion regular
                            string expRegular = "^(ALTERNATE)$";
                            if (!Regex.IsMatch(lineaAnalisis, expRegular))
                            {
                                erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". La expresión '" + lineaAnalisis + "' no cumple las reglas sintácticas del lenguaje." + Environment.NewLine;
                            }
                        }
                    }
                    else
                    {
                        if (lineaAnalisis.StartsWith("INT ") || lineaAnalisis.StartsWith("DECIMAL ") || lineaAnalisis.StartsWith("TEXT ") || lineaAnalisis.StartsWith("CHAR ") || lineaAnalisis.StartsWith("FLAG "))
                        {
                            erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". Se espera un identificador en la expresión '" + lineaAnalisis + Environment.NewLine;
                        }
                        else 
                        {
                            if (!(lineaAnalisis.StartsWith("START") || lineaAnalisis.StartsWith("END")))
                            {
                                erroresEncontrados += "Error Sintáctico: Línea " + numLinea + ". Se esperaba ';' en la expresión '" + lineaAnalisis + "'." + Environment.NewLine;
                            }
                        }
                    }
                }
            }
            return erroresEncontrados;
        }

        #endregion
    }
}
