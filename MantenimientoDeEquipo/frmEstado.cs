using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MantenimientoDeEquipo
{
    public partial class frmEstado : Form
    {
        LogicaNegocioEstado objLE = new LogicaNegocioEstado();
        public frmEstado()
        {
            InitializeComponent();
        }
        private void frmEstado_Load(object sender, EventArgs e)
        {
            llenaEstado();
            // lblCodigo.Text = generaCodigoEstado();
        }
        void llenaEstado()
        {
            dgEstado.DataSource = objLE.listaEstado();
        }

        void limpiarControles()
        {
            txtCodigo.Clear();
            txtCodigo.Focus();
        }
        string valida()
        {
            if (txtCodigo.Text.Trim().Length == 0)
            {
                txtCodigo.Clear();
                txtCodigo.Focus();
                return " la descripcion de tipo equipo";
            }
            else
                return "";
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //lblCodigo.Text = generaCodigoEstado();
            limpiarControles();
        }
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (valida() == "")
            {
                // Capturando los datos del formulario
                string codigo = txtCodigo.Text;
                string descripcion = txtDescripcion.Text;
                // Grabando el nuevo registro -equipo-
                string mensaje = objLE.nuevoEstadoEquipo(codigo, descripcion);
                MessageBox.Show(mensaje);
                llenaEstado();
            }
            else
            {
                MessageBox.Show("El error se encuentra " + valida(), "Error...");
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (valida() == "")
            {
                // Capturando los datos de formulario
                string codigo = txtCodigo.Text;
                string descripcion = txtDescripcion.Text;
                // Guardando los cambios: Modificando
                string mensaje = objLE.actualizaEstadoEquipo(codigo, descripcion);
                MessageBox.Show(mensaje);
                llenaEstado();
            }
            else
            {
                MessageBox.Show("El error se encuentra " + valida(), "Error...");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Capturando los datos del formulario
            string codigo = txtCodigo.Text;
            // Eliminado
            string mensaje = objLE.eliminaEstadoEquipo(codigo);
            MessageBox.Show(mensaje);
            llenaEstado();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgEstado_DoubleClick(object sender, EventArgs e)
        {
            txtCodigo.Text = dgEstado.CurrentRow.Cells[0].Value.ToString();
            txtDescripcion.Text = dgEstado.CurrentRow.Cells[1].Value.ToString();
        }

    }
}
