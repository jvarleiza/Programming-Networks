using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionServidor
{
    public partial class MantenimientoClientes : Form
    {
        private bool nuevo;
        private string idViejo;

        public MantenimientoClientes(bool esNuevo, string id)
        {
            InitializeComponent();
            nuevo = esNuevo;
            if (nuevo)
            {
                lblTitulo.Text = "Ingrese la identificacion del nuevo cliente";
            }
            else
            {
                lblTitulo.Text = "Modifique la identificacion del cliente";
                idViejo = id;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!txtId.Text.Equals(""))
            {
                if (nuevo)
                {
                    bool existia = Sistema.Instancia().AgregarCliente(txtId.Text);
                    if (existia)
                    {
                        MessageBox.Show("No se puede agregar el usuario porque ya existe uno con el mismo ID.");
                    }
                }
                else
                {
                    if (!Sistema.Instancia().ModificarCliente(idViejo, txtId.Text))
                    {
                        MessageBox.Show("ID repetida. Pruebe de nuevo con otra identificación");
                    }
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("La identificacion no puede ser vacía");
            }
        }
    }
}
