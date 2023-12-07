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
using System.IO;
using System.Configuration;

namespace presentacion
{
    public partial class FrmAlta : Form
    {

        private Articulo articulo = null;
        private OpenFileDialog archivo = null;

        public FrmAlta()
        {
            InitializeComponent();
            Text = "Agregar Articulo";
        }
        public FrmAlta(Articulo editable)
        {
            InitializeComponent();
            this.articulo = editable;
            Text = "Modificar Articulo";
        }

        private void FrmDetalleProducto_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcaNegocio marca = new MarcaNegocio();

            try
            {
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            



        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            


                try
                {
                    if (articulo == null)
                        articulo = new Articulo();

                    articulo.Codigo = txtCodigo.Text;
                    articulo.Nombre = txtNombre.Text;
                    articulo.Descripcion = txtDescripcion.Text;
                    articulo.ImagenUrl = txtImagen.Text;
                    articulo.Marca = (Marca)cbxMarca.SelectedItem;
                    articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                    articulo.Precio = decimal.Parse(txtPrecio.Text);



                    if (articulo.Id != 0)
                    {
                        negocio.modificar(articulo);
                        MessageBox.Show("Articulo modificado correctamente!");

                   
                    }
                    else 
                    {
                        negocio.agregar(articulo);
                        MessageBox.Show("Articulo agregado correctamente!");
                    
                    }
                Close();

                   /*/ if (articulo != null && !(txtImagen.Text.Contains("http")))
                        File.Copy(archivo.FileName, ConfigurationManager.AppSettings["articulo-imagen"] + archivo.SafeFileName);

                        Close();
                   /*/
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

          private void btnAgregarImagen_Click(object sender, EventArgs e)
        {

            /*/
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;|png|*.png";


            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);

            }
            
            /*/

        }
        
    }
}
