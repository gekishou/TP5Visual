using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TP5Visual
{
    //esta clase accede a la base y ejecuta queries
    class LibroDatos
    {
        //string de conexión
        SqlConnection cn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TP5Visual;Data Source=DESKTOP-NU89RCE\\SQLEXPRESS");

        public void InsertarLibro(Libro l) //INSERT para un nuevo libro en la base
        {
            try
            {
                cn.Open(); //abrir conexión
                String query = @"
                                INSERT INTO Libro (Titulo, Autor, ISBN, Paginas, Edicion, Editorial, Lugar, Fecha) 
                                VALUES (@Titulo, @Autor, @ISBN, @Paginas, @Edicion, @Editorial, @Lugar, @Fecha)"; 

                SqlParameter titulo = new SqlParameter("@Titulo", l.Titulo); //crear parámetros
                SqlParameter autor = new SqlParameter("@Autor", l.Autor.nombre + " " + l.Autor.apellido);
                SqlParameter isbn = new SqlParameter("@ISBN", l.Isbn);
                SqlParameter paginas = new SqlParameter("@Paginas", l.Paginas);
                SqlParameter edicion = new SqlParameter("@Edicion", l.Edicion);
                SqlParameter editorial = new SqlParameter("@Editorial", l.Editorial);
                SqlParameter lugar = new SqlParameter("@Lugar", l.Lugar.ciudad + ", " + l.Lugar.pais);
                SqlParameter fecha = new SqlParameter("@Fecha", l.Fecha.ToString("yyyy-MM-dd HH:mm:ss.fff")); //formato Date SQL

                SqlCommand comando = new SqlCommand(query, cn); //crear comando y agregar parámetros
                comando.Parameters.Add(titulo);
                comando.Parameters.Add(autor);
                comando.Parameters.Add(isbn);
                comando.Parameters.Add(paginas);
                comando.Parameters.Add(edicion);
                comando.Parameters.Add(editorial);
                comando.Parameters.Add(lugar);
                comando.Parameters.Add(fecha);

                comando.ExecuteNonQuery(); //ejecutar comando


            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                cn.Close(); //cerrar conexión
            }
        }

        public List<Libro> GetLibros(string search = null) //SELECT de la base para obtener lista de Libros
        {
            List<Libro> Libros = new List<Libro>(); //crear lista vacía praa devolver
            try
            {
                cn.Open(); //abrir conexión
                string query = @"SELECT * FROM Libro"; //traer todo desde la base libro
                SqlCommand comando = new SqlCommand();
                if (!string.IsNullOrEmpty(search)) //si se introdujo algo en la barra de búsqueda
                {

                    query += @" WHERE Titulo LIKE @Search 
                            OR Autor LIKE @Search 
                            OR ISBN LIKE @Search
                            OR Paginas LIKE @Search
                            OR Edicion LIKE @Search
                            OR Editorial LIKE @Search
                            OR Lugar LIKE @Search
                            OR Fecha LIKE @Search";


                    comando.Parameters.Add(new SqlParameter("@Search", $"%{search}%")); //buscar valores en todas las columnas que coincidan con lo buscado refinando el comando con WHERE
                }

                comando.CommandText = query;
                comando.Connection = cn;

                SqlDataReader reader = comando.ExecuteReader(); //obtener reader con lo obtenido de la query
                while (reader.Read()) //por cada registro obtenido
                {
                    Libros.Add(new Libro //agregar un libro nuevo a la lista a devolver
                    {
                        Id = int.Parse(reader["IdLibro"].ToString()),
                        Titulo = reader["Titulo"].ToString(),
                        Autor = new Autor(reader["Autor"].ToString().Split(' ')),
                        Isbn = reader["ISBN"].ToString(),
                        Paginas = int.Parse(reader["Paginas"].ToString()),
                        Edicion = int.Parse(reader["Edicion"].ToString()),
                        Editorial = reader["Editorial"].ToString(),
                        Lugar = new Lugar(reader["Lugar"].ToString().Split(',')),
                        Fecha = DateTime.Parse(reader["Fecha"].ToString()),
                    }) ;
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                cn.Close();
            }
            return Libros; //devolver lista armada
        }

        public void UpdateLibro(Libro l)
        {
            try
            {
                cn.Open(); //abrir conexión

                 string query = @"
                                 UPDATE  Libro
                                 SET Titulo = @Titulo, 
                                 Autor = @Autor, 
                                 ISBN = @ISBN, 
                                 Paginas = @Paginas, 
                                 Edicion = @Edicion, 
                                 Editorial = @Editorial, 
                                 Lugar = @Lugar, 
                                 Fecha = @Fecha
        
                                WHERE IdLibro=@Id"; //UPDATE al libro existente l
 
                SqlParameter id = new SqlParameter("@Id", l.Id);
                SqlParameter titulo = new SqlParameter("@Titulo", l.Titulo); 
                SqlParameter autor = new SqlParameter("@Autor", l.Autor.nombre + " " + l.Autor.apellido);
                SqlParameter isbn = new SqlParameter("@ISBN", l.Isbn);
                SqlParameter paginas = new SqlParameter("@Paginas", l.Paginas);
                SqlParameter edicion = new SqlParameter("@Edicion", l.Edicion);
                SqlParameter editorial = new SqlParameter("@Editorial", l.Editorial);
                SqlParameter lugar = new SqlParameter("@Lugar", l.Lugar.ciudad + ", " + l.Lugar.pais);
                SqlParameter fecha = new SqlParameter("@Fecha", l.Fecha.ToString("yyyy-MM-dd HH:mm:ss.fff")); //formato Date SQL

                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.Add(id);
                comando.Parameters.Add(titulo);
                comando.Parameters.Add(autor);
                comando.Parameters.Add(isbn);
                comando.Parameters.Add(paginas);
                comando.Parameters.Add(edicion);
                comando.Parameters.Add(editorial);
                comando.Parameters.Add(lugar);
                comando.Parameters.Add(fecha);

                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }
            finally
            {
                cn.Close();
            }
        }

        public void BorrarLibro(int id)
        {
            try
            {
                cn.Open(); //abrir conexión

                string query = @"
                                 DELETE FROM Libro WHERE IdLibro=@Id"; //DELETE para un libro existente cuya id es igual al parámetro
  
                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.Add(new SqlParameter("@id", id));

                comando.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }

        }
    }


}

