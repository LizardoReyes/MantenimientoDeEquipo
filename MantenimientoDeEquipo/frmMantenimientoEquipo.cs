using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MantenimientoDeEquipo
{
    public partial class frmMantenimientoEquipo : Form
    {
        LogicaNegocio objL = new LogicaNegocio();
        public frmMantenimientoEquipo()
        {
            InitializeComponent();
        }
        private void frmMantenimientoEquipo_Load(object sender, EventArgs e)
        {
            llenaTipo();
            llenaEstado();
            llenaEquipos();
            lblCodigo.Text = generaCodigo();
        }
        void llenaTipo()
        {
            cboTipo.DataSource = objL.listaTipoEquipos();
            cboTipo.DisplayMember = "TIPO";
            cboTipo.ValueMember = "CODIGO";
        }
        void llenaEstado()
        {
            cboEstado.DataSource = objL.listaEstado();
            cboEstado.DisplayMember = "TIPO";
            cboEstado.ValueMember = "ESTADO";
        }
        void llenaEquipos()
        {
            dgEquipos.DataSource = objL.listaEquipos();
        }
        string generaCodigo()
        {
            return objL.generaCodigo();
        }
        void limpiarControles()
        {
            txtDescripcion.Clear();
            cboEstado.SelectedIndex = -1;
            cboTipo.SelectedIndex = -1;
            txtPrecio.Clear();
            txtDescripcion.Focus();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void mnuNuevo_Click(object sender, EventArgs e)
        {
            lblCodigo.Text = generaCodigo();
            limpiarControles();
        }
        private void mnuGrabar_Click(object sender, EventArgs e)
        {
            if (valida() == "")
            {
                // Capturando los datos del formulario
                string codigo = lblCodigo.Text;
                string descripcion = txtDescripcion.Text;
                string tipo = cboTipo.SelectedValue.ToString();
                string estado = cboEstado.SelectedIndex.ToString();
                double precio = double.Parse(txtPrecio.Text);
                // Grabando el nuevo registro -equipo-
                string mensaje = objL.nuevoEquipo(codigo, tipo, descripcion, precio, estado);
                MessageBox.Show(mensaje);
                llenaEquipos();
            }
            else
            {
                MessageBox.Show("El error se encuentra " + valida(), "Error...");
            }
        }
        private void mnuModificar_Click(object sender, EventArgs e)
        {
            if (valida() == "")
            {
                // Capturando los datos de formulario
                string codigo = lblCodigo.Text;
                string descripcion = txtDescripcion.Text;
                string tipo = cboTipo.SelectedValue.ToString();
                string estado = cboEstado.SelectedIndex.ToString();
                double precio = double.Parse(txtPrecio.Text);
                // Guardando los cambios
                string mensaje = objL.actualizaEquipo(codigo, tipo, descripcion, precio, estado);
                MessageBox.Show(mensaje);
                llenaEquipos();
            }
            else
            {
                MessageBox.Show("El error se encuentra " + valida(), "Error...");
            }
        }

        private void mnuEliminar_Click(object sender, EventArgs e)
        {
            // Capturando los datos del formulario
            string codigo = lblCodigo.Text;
            // Eliminado
            string mensaje = objL.eliminaEquipo(codigo);
            MessageBox.Show(mensaje);
            llenaEquipos();
        }
        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgEquipos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblCodigo.Text = dgEquipos.CurrentRow.Cells[0].Value.ToString();
            txtDescripcion.Text = dgEquipos.CurrentRow.Cells[1].Value.ToString();
            cboTipo.Text = dgEquipos.CurrentRow.Cells[2].Value.ToString();
            cboEstado.Text = dgEquipos.CurrentRow.Cells[4].Value.ToString();
            txtPrecio.Text = dgEquipos.CurrentRow.Cells[3].Value.ToString();
        }
        string valida()
        {
            if (txtDescripcion.Text.Trim().Length == 0)
            {
                txtDescripcion.Clear();
                txtDescripcion.Focus();
                return " la descripcion del equipo";
            }
            else if (cboTipo.SelectedIndex == -1)
            {
                cboTipo.Focus();
                return " el tipo de equipo";
            }
            else if (cboEstado.SelectedIndex == -1)
            {
                cboEstado.Focus();
                return " el estado del equipo";
            }
            else if (txtPrecio.Text.Trim().Length == 0)
            {
                txtPrecio.Clear();
                txtPrecio.Focus();
                return " el precio del equipo";
            }
            else
                return "";
        }
    }
}
