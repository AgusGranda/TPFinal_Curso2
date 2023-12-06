﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            FrmDetalleProducto detalle = new FrmDetalleProducto();
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
            FrmDetalleProducto editar = new FrmDetalleProducto(seleccionado);
            editar.ShowDialog();
            cargarColumnas();
        }
    }
}
