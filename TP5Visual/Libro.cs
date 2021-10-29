using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5Visual
{
    public class Autor
    {
        public String nombre { get; set; }
        public String apellido { get; set; }

        public Autor(String n, String a)
        {
            this.nombre = n;
            this.apellido = a;
        }

        public Autor(String[] NombreYApellido) //constructor con array de String para armar a partir de celdas de grid
        {
            this.nombre = NombreYApellido[0];
            this.apellido = NombreYApellido[1].TrimStart();
        }

        public override string ToString() //método ToString para mostrar por grid
        {
            return this.nombre + " " + this.apellido;
        }
    }

    public class Lugar
    {
        public String ciudad { get; set; }
        public String pais { get; set; }

        public Lugar(String c, String p)
        {
            this.ciudad = c;
            this.pais = p;
        }
        public Lugar(String[] CiudadYPais)  //constructor con array de String para armar a partir de celdas de grid
        {
            this.ciudad = CiudadYPais[0];
            this.pais = CiudadYPais[1].TrimStart();
        }

        public override string ToString()  //método ToString para mostrar por grid
        {
            return this.ciudad + ", " + this.pais;
        }
    }
    public class Libro
    {
        public int Id { get; set; }
        public String Titulo { get; set; }
        public Autor Autor { get; set; }
        public String Isbn { get; set; }
        public int Paginas { get; set; }
        public int Edicion { get; set; }
        public String Editorial { get; set; }
        public Lugar Lugar { get; set; }
        public DateTime Fecha { get; set; }

    }
}
