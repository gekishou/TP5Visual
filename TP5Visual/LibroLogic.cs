using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5Visual
{
    class LibroLogic
    {
        private LibroDatos _Librodatos; //variable global de tipo capa de datos

        public LibroLogic()
        {
            _Librodatos = new LibroDatos(); //crear una nueva capa de datos al inicializar
        }

        public Libro GuardarLibro(Libro l)
        {
            if (l.Id == 0) //si el libro es nuevo, su ID es 0. Ejecutar un INSERT


                _Librodatos.InsertarLibro(l);


            else
            {
                _Librodatos.UpdateLibro(l); //caso contrario, tiene un ID y se ejecuta un UPDATE

            }
            return l;
        }
   
        public List<Libro> GetLibros(string TextSearch = null) //obtener lista de libros de la base con SELECT

        {

            return _Librodatos.GetLibros(TextSearch);

        }

        public void BorrarLibro(int id) //borrar libro por id con DELETE
        {

            _Librodatos.BorrarLibro(id);

        }
    }
}

