using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using EntornoGrafico.Properties;

namespace Logica
{
    public class AccionesIDE
    {

        #region Métodos

        public string GuardarComo(string texto) 
        {
            var guardar = new SaveFileDialog();
            guardar.Filter = Resources.AccionesIDE_GuardarComo_Archivos_simple___simple;
            guardar.FilterIndex = 2;
            guardar.RestoreDirectory = true;

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = guardar.OpenFile()) != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(texto);
                    foreach (byte b in bytes)
                    {
                        myStream.WriteByte(b);
                    }
                    myStream.Flush();
                    myStream.Close();
                }
            }
            return guardar.FileName;
        }

        public void Guardar(string texto, string ruta) 
        {
            var fs = File.Create(ruta);
            var bytes = Encoding.UTF8.GetBytes(texto);
            foreach (byte b in bytes)
            {
                fs.WriteByte(b);
            }
            fs.Flush();
            fs.Close();
        }

        public string AbrirArchivo(RichTextBox rtxtEditor) 
        {
            var abrir = new OpenFileDialog
            {
                Title = Resources.AccionesIDE_AbrirArchivo_Abrir_Proyecto,
                Filter = Resources.AccionesIDE_AbrirArchivo_Archivos_simple___simple
            };
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                StreamReader leer = new StreamReader(abrir.FileName);
                rtxtEditor.Text = leer.ReadToEnd();
                leer.Close();
            }
            return abrir.FileName;
        }

        #endregion
    }
}
