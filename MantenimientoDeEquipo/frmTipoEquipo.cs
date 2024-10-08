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
    public partial class frmTipoEquipo : Form
    {
        LogicaNegocioTipoEquipo objLTE = new LogicaNegocioTipoEquipo();
        public frmTipoEquipo()
        {
            InitializeComponent();
        }
        private void frmTipoEquipo_Load(object sender, EventArgs e)
        {
            llenaTipoEquipos();
            lblCodigo.Text = generaCodigoTipoEquipo();
        }

        void llenaTipoEquipos()
        {
            dgTipoEquipo.DataSource = objLTE.listaTipoEquipos();
        }
        string generaCodigoTipoEquipo()
        {
            return objLTE.generaCodigoTipoEquipo();
        }
        void limpiarControles()
        {
            txtDescripcion.Clear();
            txtDescripcion.Focus();
        }
        string valida()
        {
            if (txtDescripcion.Text.Trim().Length == 0)
            {
                txtDescripcion.Clear();
                txtDescripcion.Focus();
                return " la descripcion de tipo equipo";
            }
            else
                return "";
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            lblCodigo.Text = generaCodigoTipoEquipo();
            limpiarControles();
        }
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (valida() == "")
            {
                // Capturando los datos del formulario
                string codigo = lblCodigo.Text;
                string descripcion = txtDescripcion.Text;
                // Grabando el nuevo registro -equipo-
                string mensaje = objLTE.nuevoTipoEquipo(codigo, descripcion);
                MessageBox.Show(mensaje);
                llenaTipoEquipos();
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
                string codigo = lblCodigo.Text;
                string descripcion = txtDescripcion.Text;
                // Guardando los cambios: Modificando
                string mensaje = objLTE.actualizaTipoEquipo(codigo, descripcion);
                MessageBox.Show(mensaje);
                llenaTipoEquipos();
            }
            else
            {
                MessageBox.Show("El error se encuentra " + valida(), "Error...");
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Capturando los datos del formulario
            string codigo = lblCodigo.Text;
            // Eliminado
            string mensaje = objLTE.eliminaTipoEquipo(codigo);
            MessageBox.Show(mensaje);
            llenaTipoEquipos();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgTipoEquipo_DoubleClick(object sender, EventArgs e)
        {
            lblCodigo.Text = dgTipoEquipo.CurrentRow.Cells[0].Value.ToString();
            txtDescripcion.Text = dgTipoEquipo.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
