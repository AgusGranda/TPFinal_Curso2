using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace presentacion
{
    public partial class FrmProductos : Form
    {

        private List<Articulo> articulos = new List<Articulo>();
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            
            cargarColumnas();

            cbxCriterio.Items.Add("Precio");
            cbxCriterio.Items.Add("Marca");
            cbxCriterio.Items.Add("Categoria");


        }

        private void cargarColumnas()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            articulos = negocio.listar();
            dgvArticulos.DataSource = articulos;
            suprimirColumnas();
        }

        private void suprimirColumnas()
        {
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Categoria"].Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAlta detalle = new FrmAlta();
            detalle.ShowDialog();
            cargarColumnas();


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

               DialogResult respuesta = MessageBox.Show("Esta seguro que desea eliminar este articulo?", "Eliminando articulo" ,MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ;

                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionado);
                    MessageBox.Show("Articulo eliminado correctamente!");
                    cargarColumnas();
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            FrmAlta editar = new FrmAlta(seleccionado);
            editar.ShowDialog();
            cargarColumnas();
        }

        private void cbxCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCriterio.SelectedItem != null && cbxCriterio.SelectedItem.ToString() == "Precio")
            {
                try
                {

                    cbxSubCriterio.DataSource = null;
                    cbxSubCriterio.Items.Clear();
                    cbxSubCriterio.Items.Add("Menor A");
                    cbxSubCriterio.Items.Add("Mayor A");
                    cbxSubCriterio.Items.Add("Igual A");
                    txtBuscar.Enabled = true;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else if (cbxCriterio.SelectedItem != null && cbxCriterio.SelectedItem.ToString() == "Marca")
            {
                  MarcaNegocio marca = new MarcaNegocio();
                try
                {
                    cbxSubCriterio.DataSource = null;
                    cbxSubCriterio.DataSource = marca.listar();
                    txtBuscar.Text = "";
                    txtBuscar.Enabled = false;

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            else if(cbxCriterio.SelectedItem != null && cbxCriterio.SelectedItem.ToString() == "Categoria")
            {
                CategoriaNegocio categoria = new CategoriaNegocio();
                try
                {
                    cbxSubCriterio.DataSource = null;
                    cbxSubCriterio.DataSource = categoria.listar();
                    txtBuscar.Text = "";
                    txtBuscar.Enabled = false;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }


        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            



            if (cbxCriterio.SelectedItem != null && cbxSubCriterio.SelectedItem != null)
            {
                try
                {
                    string criterio = cbxCriterio.SelectedItem.ToString();
                    string subCriterio = cbxSubCriterio.SelectedItem.ToString();
                    string filtro = txtBuscar.Text;
                    if (criterio == "Precio" && filtro == "")
                        txtBuscar.Text = 0.ToString();

                    else if (!(soloNumeros(filtro)))
                    {
                        MessageBox.Show("Ingrese solo numeros por favor!");
                        return;
                    }


                    dgvArticulos.DataSource = negocio.filtrar(criterio, subCriterio, filtro);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Por favor, complete los campos correspondientes");
            }





        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cbxSubCriterio.SelectedIndex = -1;
            cbxCriterio.SelectedIndex = -1;
            if (txtBuscar.Text != "")
                txtBuscar.Text = "";
            txtBuscar.Enabled = false;
            cargarColumnas();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;

            try
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                FrmDetalle detalle = new FrmDetalle(seleccionado);
                detalle.ShowDialog();

            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}
