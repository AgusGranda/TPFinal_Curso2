﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {


        public List<Articulo> listar()
        {
			
			List<Articulo> lista = new List<Articulo>();
			AccesoDatos datos = new AccesoDatos();


			try
			{
				datos.setearConsulta("select A.Id, Codigo, Nombre,A.Descripcion,M.Descripcion Marca, C.Descripcion Categoria ,ImagenUrl,Precio from ARTICULOS A inner join MARCAS M on IdMarca = M.Id inner join CATEGORIAS C  on IdCategoria = C.Id");
				datos.ejecutarLectura();

				while (datos.Lector.Read())
				{
					Articulo aux = new Articulo();
					aux.Id = (int)datos.Lector["Id"];
					aux.Codigo = (string)datos.Lector["Codigo"];
					aux.Nombre = (string)datos.Lector["Nombre"];
					aux.Descripcion = (string)datos.Lector["Descripcion"];
					aux.Marca = new Marca();
					aux.Marca.Descripcion = (string)datos.Lector["Marca"];
					aux.Categoria = new Categoria();
					aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
					aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
					aux.Precio = (decimal)datos.Lector["Precio"];


					lista.Add(aux);
				}


                return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.cerrarConexion();
			}
        }


    }
}