using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.CSharp;

namespace Logica
{
    public class ControlLenguajeIntermedio
    {
        public string TraducirCF(string codFuente, ArrayList tablaSimbolos)
        {
            string erroresEncontrados = "";
            string codigoCSharp = "using System; \n" +
                                  "using System.Collections.Generic; \n" +
                                  "using System.Text; \n" +
                                  "namespace ConsoleApplication { \n" +
                                  "class Program { \n" +
                                  "static void Main(string[] args) { \n";
            StringReader leer = new StringReader(codFuente);
            string instruccion = "";
            while ((instruccion = leer.ReadLine()) != null)
            {
                if (!instruccion.StartsWith("--") && !instruccion.EndsWith("--"))
                {
                    instruccion = OptimizarCodigo(instruccion);
                    string[] palabrasCF = instruccion.Split(new char[] { ' ' });
                    switch (palabrasCF[0].ToString())
                    {
                        case "INT":
                            {
                                palabrasCF[0] = "int";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "DECIMAL":
                            {
                                palabrasCF[0] = "double";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "TEXT":
                            {
                                palabrasCF[0] = "string";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "CHAR":
                            {
                                palabrasCF[0] = "char";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "FLAG":
                            {
                                palabrasCF[0] = "bool";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "EVAL":
                            {
                                palabrasCF[0] = "if (";
                                palabrasCF[4] = ") {";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "ALTERNATE":
                            {
                                palabrasCF[0] = "else {";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "LOOP":
                            {
                                palabrasCF[0] = "while (";
                                palabrasCF[4] = ") {";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "PRINT":
                            {
                                palabrasCF[0] = "Console.WriteLine";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "READTOEND":
                            {
                                palabrasCF[0] = "Console.ReadLine";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        case "END":
                            {
                                palabrasCF[0] = "}";
                                for (int i = 0; i < palabrasCF.Length; i++)
                                {
                                    codigoCSharp += palabrasCF[i] + " ";
                                    if (i == palabrasCF.Length - 1)
                                    {
                                        codigoCSharp += "\n";
                                    }
                                }
                            } break;
                        default:
                            {
                                if (!instruccion.Equals("START"))
                                {
                                    string expRegular1 = @"^([a-zA-Z][\w\s]*)(=(\s)*)(READ)(\s)(\()(\s)(\))(\s)(;)$";
                                    if (Regex.IsMatch(instruccion, expRegular1))
                                    {
                                        string referencia = "";
                                        referencia = BuscarTS(palabrasCF[0], tablaSimbolos);
                                        if (referencia.Equals("4"))
                                        {
                                            palabrasCF[2] = "Console.ReadLine";
                                            for (int i = 0; i < palabrasCF.Length; i++)
                                            {
                                                codigoCSharp += palabrasCF[i] + " ";
                                                if (i == 1)
                                                {
                                                    codigoCSharp += "int.Parse(";
                                                }
                                                if (i == palabrasCF.Length - 2)
                                                {
                                                    codigoCSharp += ")";
                                                }
                                                if (i == palabrasCF.Length - 1)
                                                {
                                                    codigoCSharp += "\n";
                                                }
                                            }
                                        }
                                        if (referencia.Equals("5"))
                                        {
                                            palabrasCF[2] = "Console.ReadLine";
                                            for (int i = 0; i < palabrasCF.Length; i++)
                                            {
                                                codigoCSharp += palabrasCF[i] + " ";
                                                if (i == palabrasCF.Length - 1)
                                                {
                                                    codigoCSharp += "\n";
                                                }
                                            }
                                        }
                                        if (referencia.Equals("6"))
                                        {
                                            palabrasCF[2] = "Console.ReadLine";
                                            for (int i = 0; i < palabrasCF.Length; i++)
                                            {
                                                codigoCSharp += palabrasCF[i] + " ";
                                                if (i == 1)
                                                {
                                                    codigoCSharp += "double.Parse(";
                                                }
                                                if (i == palabrasCF.Length - 2)
                                                {
                                                    codigoCSharp += ")";
                                                }
                                                if (i == palabrasCF.Length - 1)
                                                {
                                                    codigoCSharp += "\n";
                                                }
                                            }
                                        }
                                        if (referencia.Equals("8"))
                                        {
                                            palabrasCF[2] = "Console.ReadLine";
                                            for (int i = 0; i < palabrasCF.Length; i++)
                                            {
                                                codigoCSharp += palabrasCF[i] + " ";
                                                if (i == 1)
                                                {
                                                    codigoCSharp += "char.Parse(";
                                                }
                                                if (i == palabrasCF.Length - 2)
                                                {
                                                    codigoCSharp += ")";
                                                }
                                                if (i == palabrasCF.Length - 1)
                                                {
                                                    codigoCSharp += "\n";
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        codigoCSharp += instruccion + "\n";
                                    }
                                }
                            } break;
                    }
                }
            }
            codigoCSharp += "}\n}";
            erroresEncontrados = CompilarCodigoCSharp(codigoCSharp);
            return erroresEncontrados;
        }

        public string CompilarCodigoCSharp(string codigoCSharp)
        {
            string erroresEncontrados = "";
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            string output = "Programa.exe";

            CompilerParameters parametros = new CompilerParameters();
            parametros.GenerateExecutable = true;
            parametros.OutputAssembly = output;
            CompilerResults resultados = icc.CompileAssemblyFromSource(parametros, codigoCSharp);

            if (resultados.Errors.Count > 0)
            {
                foreach (CompilerError error in resultados.Errors)
                {
                    erroresEncontrados += "Línea " + (error.Line - 5) + ", '" + error.ErrorText + "." + Environment.NewLine;
                    erroresEncontrados = Regex.Replace(erroresEncontrados, "(string)", "TEXT");
                    erroresEncontrados = Regex.Replace(erroresEncontrados, "(int)", "INT");
                    erroresEncontrados = Regex.Replace(erroresEncontrados, "(char)", "CHAR");
                    erroresEncontrados = Regex.Replace(erroresEncontrados, "(double)", "DECIMAL");
                    erroresEncontrados = Regex.Replace(erroresEncontrados, "(bool)", "FLAG");
                    erroresEncontrados = Regex.Replace(erroresEncontrados, "(else)", "ALTERNATE");
                    erroresEncontrados = Regex.Replace(erroresEncontrados, "(\\{)", "END");
                }
            }
            else
            {
                Process.Start(output);
            }
            return erroresEncontrados;
        }

        public string OptimizarCodigo(string instruccion)
        {
            instruccion = instruccion.Trim(new char[] { '\t', ' ' });
            string[] auxInstruccion = instruccion.Split(new char[] { '\t', ' ', '\n' });
            instruccion = "";
            for (int i = 0; i < auxInstruccion.Length; i++)
            {
                if (!((auxInstruccion[i] == "") || (auxInstruccion[i] == "\t") || (auxInstruccion[i] == "\n")))
                {
                    instruccion += auxInstruccion[i] + " ";
                }
            }
            instruccion = instruccion.Trim(new char[] { ' ' });
            return instruccion;
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
            return referencia;
        }
    }
}
