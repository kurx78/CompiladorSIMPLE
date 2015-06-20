using System.Collections;
using System.IO;

namespace CompiladorSIMPLE
{
    public class Compilador
    {
        static ArrayList _listaTs;
        public static bool VerificarSintaxis = false;

        #region Métodos

        
        public string CompilarCodigoFuente(string codFuente, ArrayList tablaSimbolos) 
        {
            _listaTs = tablaSimbolos;
            var leer = new StringReader(codFuente);
            var lexico = new ControlLexico();
            var sintactico = new ControlSintactico();
            var semantico = new ControlSemantico();
            var numLinea = 1;
            var instruccion = "";
            var erroresEncontrados = "";
            var erroresTotales = "";
            while ((instruccion = leer.ReadLine()) != null)
            {

                instruccion = instruccion.Trim(' ', '\t');
                VerificarSintaxis = false;
                if (!(instruccion.Equals("") || instruccion.Equals("\t")))
                {

                    if (!instruccion.StartsWith("--") && !instruccion.EndsWith("--"))
                    {

                        instruccion = OptimizarCodigoIntermedio(instruccion);
                        erroresEncontrados += lexico.AnalisisLexico(instruccion, _listaTs, numLinea);
                        erroresEncontrados += sintactico.AnalizarSintaxis(instruccion, _listaTs, codFuente, numLinea);
                        //AnalisisSemantico DEsactivado temporalmente
                        //erroresEncontrados += semantico.AnalizarSemantica(instruccion, _listaTs, codFuente, numLinea);

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

            if (erroresTotales.Equals(""))
            {
                //La Parte que compila se encuentra desactivada temporalmente
                //ControlLenguajeIntermedio traductor = new ControlLenguajeIntermedio();
                //erroresTotales = traductor.TraducirCF(codFuente, tablaSimbolos);
            }
            return erroresTotales;
        }

        public string OptimizarCodigoIntermedio(string instruccion)
        {
            instruccion = instruccion.Trim(new char[] { '\t', ' ' });

            string[] auxInstruccion = instruccion.Split(new char[] { '\t', ' ', '\n' });
            instruccion = "";
            for (int i = 0; i < auxInstruccion.Length; i++)
            {

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

            string[] palabras = linea.Split(new char[] { ' ', '\n', '\t' });
            int numLineaTS = _listaTs.Count;

            switch (palabras[0].ToString())
            {
                case "INT": 
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");

                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');

                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "4" + " " + valor);
                }break;
                case "TEXT":
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");

                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');

                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "5" + " " + valor);
                } break;
                case "DECIMAL": 
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");

                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');

                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "6" + " " + valor); 
                } break;
                case "FLAG": 
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");

                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');

                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "7" + " " + valor); 
                }break;
                case "CHAR":
                {
                    int inicio = linea.IndexOf("=");
                    int fin = linea.IndexOf(";");

                    string valor = linea.Substring(inicio + 1, (fin - 1) - inicio);
                    valor = valor.Trim(' ');

                    _listaTs.Add((numLineaTS + 1) + " " + palabras[1] + " " + "Identificador" + " " + "8" + " " + valor);
                } break;
            }
            
        }

        public static ArrayList RefTablaSimbolos() 
        {
            return _listaTs;
        }

        #endregion
    }
}
