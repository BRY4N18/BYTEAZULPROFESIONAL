using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYTEAZULPROFESIONAL
{
    public partial class fmSuscripciones : Form
    {
        CsSuscripciones suscripcion;
        CsMovimientos movimientos;
        CsCaja caja;
        public fmSuscripciones()
        {
            InitializeComponent();
            suscripcion = new CsSuscripciones();
            movimientos = new CsMovimientos();
            caja = new CsCaja();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            fmGestionarClientes clientes = new fmGestionarClientes(2);
            this.AddOwnedForm(clientes);
            clientes.ShowDialog();
        }

        private void fmSuscripciones_Load(object sender, EventArgs e)
        {
            txtIDEmpleado.Text = movimientos.ID();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool validar()
        {
            if (string.IsNullOrWhiteSpace(txtMontoPagar.Text) || string.IsNullOrWhiteSpace(txtIDCliente.Text) || string.IsNullOrWhiteSpace(txtIDEmpleado.Text))
            {
                MessageBox.Show("No puede haber campos vacios", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnConfirmarSus_Click(object sender, EventArgs e)
        {
            if (validar() && (txtMontoSus.Text == txtMontoPagar.Text || txtMontoSus.Text + ".00" == txtMontoPagar.Text))
            {
                if (!suscripcion.VerificarSuscriptor(txtIDCliente.Text))
                {
                    suscripcion.InsertarSuscripcion(txtIDCliente.Text, txtIDEmpleado.Text);
                    caja.InsertarIngresoCaja(txtMontoPagar.Text.Replace('.', ','));
                    this.Close();
                }
                else { MessageBox.Show("Ya cuenta con una suscripcion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
                MessageBox.Show("El monto de la suscripcion no es el adecuado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtMontoPagar_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Contains('.'))
            {
                string[] partes = textBox.Text.Split('.');
                if (partes.Length > 1 && partes[1].Length > 2)
                {
                    textBox.Text = partes[0] + "." + partes[1].Substring(0, 2);
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void txtMontoPagar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
                e.Handled = true;
        }
    }
}
