using Final_Prog2.Dominio;
using Final_Prog2.Servicios;
using Final_Prog2.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Prog2
{
    public partial class FrmAlta : Form
    {
        private IServicio service;
        private Equipo equipo;
        public FrmAlta()
        {
            InitializeComponent();
            service = new ServiceFactoryImp().crearServicio();
            equipo = new Equipo();
        }

        private void FrmAlta_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void CargarCombo()
        {
            cboPersona.DataSource = service.ObtenerPersona();
            cboPersona.ValueMember = "IdPersona";
            cboPersona.DisplayMember = "NombreCompleto";
            cboPersona.SelectedIndex = -1;
        }
        private void Limpiar()
        {
            txtPais.Text = "";
            txtDT.Text = "";
            cboPersona.SelectedIndex = -1;
            cboPosicion.SelectedIndex = -1;
            nudCamiseta.Value = 1;
            dgvDetalles.Rows.Clear();
        }
        private bool ValidarAgregar()
        {
            if(cboPersona.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una Persona correctamente","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
                cboPersona.Focus();
            }
            if(nudCamiseta.Value < 1 || nudCamiseta.Value > 23)
            {
                MessageBox.Show("El numero de camiseta debe estar comprendido entre 1 y 23", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                nudCamiseta.Focus();
            }
            if(cboPosicion.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una Posicion correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                cboPosicion.Focus();
            }
            foreach(DataGridViewRow row in dgvDetalles.Rows)
            {
                if(row.Cells["camiseta"].Value.ToString().Equals(nudCamiseta.Text))
                {
                    MessageBox.Show("La camiseta: " + nudCamiseta.Value + " ya se encuentra en uso", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                    nudCamiseta.Focus();
                }
            }
            foreach(DataGridViewRow row in dgvDetalles.Rows)
            {
                if(row.Cells["jugador"].Value.ToString().Equals(cboPersona.Text))
                {
                    MessageBox.Show("El jugador: " + cboPersona.SelectedItem + " ya se encuentra en otra posicion", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                    cboPersona.Focus();
                }
            }
            return true;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(ValidarAgregar())
            {
                Jugador j = new Jugador();
                j.Persona = (Persona)cboPersona.SelectedItem;
                j.Camiseta = (int)nudCamiseta.Value;
                j.Posicion = Convert.ToString(cboPosicion.SelectedItem);
                equipo.AgregarJugador(j);
                dgvDetalles.Rows.Add(new object[] {j.Persona.IdPersona, j.Persona.NombreCompleto, j.Camiseta, j.Posicion});
            }
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            lblTotal.Text = "Total de jugadores: " + dgvDetalles.Rows.Count;
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDetalles.CurrentCell.ColumnIndex == 4)
            {
                equipo.QuitarJugador(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
            }
        }
        private bool ValidarAceptar()
        {
            if(txtPais.Text == "")
            {
                MessageBox.Show("Escriba un Pais correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                txtPais.Focus();
            }
            if(txtDT.Text == "")
            {
                MessageBox.Show("Escriba un Director Tecnico correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                txtDT.Focus();
            }
            return true;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(ValidarAceptar())
            {
                equipo.Pais = txtPais.Text;
                equipo.DirectorTecnico = txtDT.Text;
                if(service.ConfirmarEquipo(equipo))
                {
                    MessageBox.Show("Equipo Agregado", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el Equipo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                CalcularTotal();
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Seguro de que desea cancelar y salir?","AVISO",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
