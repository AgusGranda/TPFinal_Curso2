using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace presentacion
{
    public partial class FrmDetalleProducto : Form
    {
        public FrmDetalleProducto()
        {
            InitializeComponent();
        }

        private void FrmDetalleProducto_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcaNegocio marca = new MarcaNegocio();

            cbxCategoria.DataSource = categoria.listar();
            cbxCategoria.ValueMember = "Id";
            cbxCategoria.DisplayMember = "Descripcion";

            cbxMarca.DataSource = marca.listar();   
            cbxMarca.ValueMember = "Id";
            cbxMarca.DisplayMember = "Descripcion";

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();

            try
            {
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.ImagenUrl = txtImagen.Text;
                nuevo.Marca = (Marca)cbxMarca.SelectedItem;
                nuevo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                nuevo.Precio = int.Parse(txtPrecio.Text);
                negocio.agregar(nuevo);

                MessageBox.Show("Articulo agregado correctamente!");

                Close();
            }

            catch (Exception ex)
            {

                throw ex;
            }

            



        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {

            try
            {

                pbxArticulo.Load(txtImagen.Text);
            }
            catch (Exception )
            {

                pbxArticulo.Load("https://i0.wp.com/casagres.com.ar/wp-content/uploads/2022/09/placeholder.png?ssl=1");
                
            }
        }
    }
}
