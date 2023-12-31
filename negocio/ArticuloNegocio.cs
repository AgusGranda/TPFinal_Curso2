﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
				datos.setearConsulta("select A.Id, Codigo, Nombre,A.Descripcion,M.Descripcion Marca,IdMarca, C.Descripcion Categoria,IdCategoria ,ImagenUrl,Precio from ARTICULOS A inner join MARCAS M on IdMarca = M.Id inner join CATEGORIAS C  on IdCategoria = C.Id");
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
					aux.Marca.Id = (int)datos.Lector["IdMarca"];
					aux.Categoria = new Categoria();
					aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
					aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
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

		public void agregar(Articulo nuevo)
		{
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("insert into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) values(@codigo,@nombre,@descripcion,@idMarca,@idCategoria,@img,@precio)");
				
				datos.setearParametros("@codigo",nuevo.Codigo );
				datos.setearParametros("@nombre",nuevo.Nombre);
				datos.setearParametros("@descripcion",nuevo.Descripcion);
				datos.setearParametros("@idMarca",nuevo.Marca.Id);
				datos.setearParametros("@idCategoria",nuevo.Categoria.Id);
				datos.setearParametros("@img",nuevo.ImagenUrl);
				datos.setearParametros("@precio",nuevo.Precio);

				datos.ejecutarAccion();
				

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

		public void modificar(Articulo editable)
		{

			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("update ARTICULOS set Codigo=@codigo,Nombre=@nombre ,Descripcion=@descripcion,IdMarca=@marca,IdCategoria=@categoria,ImagenUrl=@imagen,Precio=@precio  where Id=@id");
				
				datos.setearParametros("@codigo",editable.Codigo);
				datos.setearParametros("@nombre",editable.Nombre);
				datos.setearParametros("@descripcion",editable.Descripcion);
				datos.setearParametros("@marca",editable.Marca.Id);
				datos.setearParametros("@categoria",editable.Marca.Id);
				datos.setearParametros("@imagen",editable.ImagenUrl);
				datos.setearParametros("@precio",editable.Precio);
				datos.setearParametros("@id",editable.Id);
			
				datos.ejecutarAccion();
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

		public List<Articulo> filtrar(string campo , string subcampo, string texto)
		{
			AccesoDatos datos = new AccesoDatos();
			List<Articulo> listaFiltrada = new List<Articulo>();

			try
			{
				string consulta = "select A.Id, Codigo, Nombre,A.Descripcion,M.Descripcion Marca,IdMarca, C.Descripcion Categoria,IdCategoria ,ImagenUrl,Precio from ARTICULOS A inner join MARCAS M on IdMarca = M.Id inner join CATEGORIAS C  on IdCategoria = C.Id where ";



				if (campo == "Precio")
				{
					switch (subcampo)
					{
						case "Mayor A":
							consulta += "Precio > " + $"'{texto}'";
							break;
						case "Menor A":
							consulta += "Precio < " + $"'{texto}'";
							break;
						default:
							consulta += "Precio = " + $"'{texto}'";
							break;

					}
				}
				else if (campo == "Marca")
				{
					consulta += "M.Descripcion= " + $"'{subcampo}'";
				}
				else
				{
                    consulta += "C.Descripcion= " + $"'{subcampo}'";
                }

				
                datos.setearConsulta(consulta);
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
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];


                    listaFiltrada.Add(aux);
                }





                return listaFiltrada;
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

		public void eliminar(Articulo eliminar)
		{

			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("delete from ARTICULOS where Id=@id");
				datos.setearParametros("@id", eliminar.Id);
				datos.ejecutarAccion();

			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				datos.cerrarConexion();
			}
		}


    }
}
