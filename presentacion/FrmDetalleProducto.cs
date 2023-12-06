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
using static System.Net.Mime.MediaTypeNames;

namespace presentacion
{
    public partial class FrmDetalleProducto : Form
    {

        private Articulo articulo = null;

        public FrmDetalleProducto()
        {
            InitializeComponent();
            Text = "Agregar Articulo";
        }
        public FrmDetalleProducto(Articulo editable)
        {
            InitializeComponent();
            this.articulo = editable;
            Text = "Modificar Articulo";
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


            if (articulo != null)
            {
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                cbxMarca.SelectedValue = articulo.Marca.Id;
                cbxCategoria.SelectedValue = articulo.Categoria.Id;
                txtImagen.Text = articulo.ImagenUrl;
                cargarImagen(txtImagen.Text);
                txtPrecio.Text = articulo.Precio.ToString();
            }




        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            if (articulo == null)
                articulo = new Articulo();


                try
                {
                    articulo.Codigo = txtCodigo.Text;
                    articulo.Nombre = txtNombre.Text;
                    articulo.Descripcion = txtDescripcion.Text;
                    articulo.ImagenUrl = txtImagen.Text;
                    articulo.Marca = (Marca)cbxMarca.SelectedItem;
                    articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                    articulo.Precio = decimal.Parse(txtPrecio.Text);



                if (articulo.Id >= 0)
                {
                    negocio.modificar(articulo);

                    MessageBox.Show("Articulo modificado correctamente!");

                    Close();
                }
                else
                {
                    negocio.agregar(articulo);

                    MessageBox.Show("Articulo agregado correctamente!");

                    Close();
                }
                    
                }

                catch (Exception ex)
                {
                MessageBox.Show(ex.ToString());
                    
                }




        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {

            cargarImagen(txtImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {

                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pbxArticulo.Load("https://i0.wp.com/casagres.com.ar/wp-content/uploads/2022/09/placeholder.png?ssl=1");

            }
        }
    }
}
