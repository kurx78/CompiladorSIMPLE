using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompiladorSIMPLE;
using Logica;

namespace EntornoGrafico
{
    public partial class Editor : Form
    {
        #region Variables
        //private ControlIDE controlIDE;
        private ControlLexico analizadorLex;
        private ArrayList tablaSimbolos;
        bool archGuardado = false;
        string rutaTS = "";
        string rutaArchivo = "";

        #endregion
        public Editor()
        {
            InitializeComponent();
            rutaTS = "TablaSimbolos.txt";
            CargarTs();
        }

        private void rtxtEditor_VScroll(object sender, EventArgs e)
        {

        }

        private void rtxtEditor_FontChanged(object sender, EventArgs e)
        {

        }

        private void rtxtEditor_SizeChanged(object sender, EventArgs e)
        {

        }

        private void rtxtEditor_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void CargarTs()
        {
             
            StreamReader leer = new StreamReader(rutaTS, true);
            tablaSimbolos = new ArrayList();
            string linea = "";
            while (linea != null)
            {
                linea = leer.ReadLine();
                if (linea != null)
                {
                    tablaSimbolos.Add(linea);
                }
            }
            leer.Close();

            grdSimbolos.Rows.Clear();
            grdSimbolos.Columns.Clear();
            
            char[] delimitador = { ' ', '\n', '\r' };
            string aux = "";
            for (int i = 0; i < tablaSimbolos.Count; i++)
            {
                aux += " " + tablaSimbolos[i].ToString();
            }
            string[] palabras = aux.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
            grdSimbolos.Columns.Add("numero", "Número");
            grdSimbolos.Columns.Add("token", "Token");
            grdSimbolos.Columns.Add("tipo", "Tipo");
            grdSimbolos.Columns.Add("referencia", "Referencia");
            grdSimbolos.Columns.Add("valor", "Valor");
            for (int i = 0; i < palabras.Length; i = i + 3)
            {
                grdSimbolos.Rows.Add(palabras[i], palabras[i + 1], palabras[i + 2], "", "");
            }
        }

        private void ActualizarTS()
        {
            ArrayList listaTS = Compilador.RefTablaSimbolos();
            string valor = "";
            string auxLineasTS = "";
            string[] auxPalabrasTS;
            for (int i = 30; i < listaTS.Count; i++)
            {
                valor = "";
                auxLineasTS = tablaSimbolos[i].ToString();
                auxPalabrasTS = auxLineasTS.Split(new char[] { ' ' });
                for (int j = 4; j < auxPalabrasTS.Length; j++)
                {
                    valor += " " + auxPalabrasTS[j];
                }
                valor = valor.Trim(' ');
                grdSimbolos.Rows.Add(auxPalabrasTS[0], auxPalabrasTS[1], auxPalabrasTS[2], auxPalabrasTS[3], valor);
            }
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CargarTs();
            var objCompilador = new Compilador();
            var resultado = objCompilador.CompilarCodigoFuente(txtEditorCodigo.Text, tablaSimbolos);
            if (resultado != string.Empty)
            {
                txtErrores.ForeColor = Color.Red;
                txtErrores.Text = resultado;
            }
            else
            {
                txtErrores.ForeColor = Color.Blue;
                txtErrores.Text = "Compilado sin errores " + DateTime.Now;
            }
            ActualizarTS();
        }
    }
}
