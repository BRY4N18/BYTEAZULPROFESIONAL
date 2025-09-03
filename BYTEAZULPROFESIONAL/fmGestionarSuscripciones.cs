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
    public partial class fmGestionarSuscripciones : Form
    {
        CsSuscripciones suscripciones = new CsSuscripciones();
        public fmGestionarSuscripciones()
        {
            InitializeComponent();
        }

        private void fmGestionarSuscripciones_Load(object sender, EventArgs e)
        {
            suscripciones.VerificarPago();
            Mostrar();
            CsAuxiliar auxiliar = new CsAuxiliar();
            fmAgregarLotes comprasimg = new fmAgregarLotes();
            this.AddOwnedForm(comprasimg);
            auxiliar.AgregarBoton(dgvGestionarSuscripciones, comprasimg.imlLogo.Images[2], "Actualizar");
            auxiliar.AgregarBoton(dgvGestionarSuscripciones, comprasimg.imlLogo.Images[3], "Cancelar");
        }
        private void Mostrar()
        {
            DataTable dt = suscripciones.TablaSuscriptores();
            dgvGestionarSuscripciones.DataSource = dt;

            dgvGestionarSuscripciones.AllowUserToAddRows = false;
            dgvGestionarSuscripciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvGestionarSuscripciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvGestionarSuscripciones.Columns["Actualizar"].Index && e.RowIndex >= 0)
                {
                    DataGridViewRow filaSeleccionada = dgvGestionarSuscripciones.Rows[e.RowIndex];
                    string estadosus = filaSeleccionada.Cells[9].Value.ToString();

                    if (estadosus != "Cancelada")
                    {
                        DialogResult res = MessageBox.Show("¿Esta seguro de que desea actualizar el pago de este cliente?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            string idsus = filaSeleccionada.Cells["ID Suscripción"].Value.ToString();
                            suscripciones.ActualizarSuscripcion(idsus);
                        }
                    }
                    else { MessageBox.Show("La suscripcion se encuentra cancelada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                else if (e.ColumnIndex == dgvGestionarSuscripciones.Columns["Cancelar"].Index && e.RowIndex >= 0)
                {
                    DataGridViewRow filaSeleccionada = dgvGestionarSuscripciones.Rows[e.RowIndex];
                    string estadopago = filaSeleccionada.Cells["Estado de Pago"].Value.ToString();
                    string estadosus = filaSeleccionada.Cells["Estado de la Suscripcion"].Value.ToString();

                    if (estadosus != "Cancelada")
                    {
                        DialogResult res = MessageBox.Show("¿Esta seguro de que desea cancelar la suscripcion?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            string idsus = filaSeleccionada.Cells["ID Suscripción"].Value.ToString();
                            suscripciones.CancelarSuscripcion(idsus);
                        }
                    }
                    else
                    {

                        DialogResult res = MessageBox.Show("¿Esta seguro de que desea renovar la suscripcion?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            string idsus = filaSeleccionada.Cells["ID Suscripción"].Value.ToString();
                            suscripciones.RenovarSuscripcion(idsus);
                        }
                    }
                }
                Mostrar();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
