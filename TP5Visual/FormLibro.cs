using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP5Visual
{
    public partial class FormLibro : Form
    {
        private LibroLogic _libroLogic; //variable global de tipo capa lógica
        private Libro _Libro; //variable global de tipo libro

        public FormLibro()
        {
            InitializeComponent();
            _libroLogic = new LibroLogic();
        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos()) //se llama a la función de validar campos (implementada abajo), si está todo OK se avanza.
            {
                SaveLibro(); //guardar libro (implementada abajo)

                if (_Libro != null) //si tiene un ID existente, se ejecutó un UPDATE.
                {
                    MessageBox.Show("Se modificó el libro OK."); //mostrar mensaje de UPDATE.
                    this.Close(); //cerrar formulario porque no se van a editar más libros.
                }
                else
                {//caso contrario, se ejecutó un INSERT.
                    MessageBox.Show("Se agregó el libro OK."); //mostrar mensaje de INSERT.
                    borrarCampos(); //limpiar campos para agregar más libros según consigna.
                    ((FormMain)this.Owner).CargarLibros(); //llamar a formulario padre y ejecutar el método CargarLibros() para actualizar la grid.
                }


            }
        }

        private void borrarCampos() //clear a todos los textbox.
        {
            boxTitulo.Clear();
            boxNombre.Clear();
            boxApellido.Clear();
            boxISBN.Clear();
            boxPaginas.Clear();
            boxEdicion.Clear();
            boxEditorial.Clear();
            boxCiudad.Clear();
            boxPais.Clear();
            boxFecha.Clear();

        }

        private bool ValidarCampos() //función que devuelve true si todos los campos están OK haciendo unión AND de valores bool por cada campo.
        {
            bool tituloOK = boxTitulo.Text.Trim() != ""; //no puede ser vacío
            bool nombreOK = boxNombre.Text.Trim() != ""; //no puede ser vacío
            bool apellidoOK = boxApellido.Text.Trim() != ""; //no puede ser vacío
            bool isbnOK = boxISBN.Text.Trim() != ""; //no puede ser vacío
            bool paginasOK;
            try
            {
                int paginas = Int32.Parse(boxPaginas.Text.Trim()); //no puede ser vacío, debe ser mayor a 0 y un int válido
                if (paginas <= 0)
                {
                    throw new ArithmeticException();
                }
                else
                {
                    paginasOK = true;
                }
            }
            catch (Exception e)
            {
                paginasOK = false;
            }
            bool edicionOK;
            try
            {
                int edicion = Int32.Parse(boxEdicion.Text.Trim()); //no puede ser vacío, debe ser mayor a 0 y un int válido
                if (edicion <= 0)
                {
                    throw new ArithmeticException();
                }
                else
                {
                    edicionOK = true;
                }
            }
            catch (Exception e)
            {
                edicionOK = false;
            }

            bool editorialOK = boxEditorial.Text.Trim() != ""; //no puede ser vacío
            bool ciudadOK = boxCiudad.Text.Trim() != ""; //no puede ser vacío
            bool paisOK = boxPais.Text.Trim() != ""; //no puede ser vacío
            bool fechaOK;
            try
            {
                DateTime fecha = DateTime.Parse(boxFecha.Text.Trim()); //no puede ser vacío, debe ser menor a fecha actual  y fecha válida
                if (fecha >= DateTime.Now)
                {
                    throw new ArithmeticException();
                }
                else
                {
                    fechaOK = true;
                }
            }
            catch (Exception e)
            {
                fechaOK = false;
            }

            bool datosOK = tituloOK &&
                            nombreOK &&
                            apellidoOK &&
                            isbnOK &&
                            paginasOK &&
                            edicionOK &&
                            editorialOK &&
                            ciudadOK &&
                            paisOK &&
                            fechaOK; //si todos los valores están OK, la función devuelve true.

            if (!datosOK) //si algún dato no está OK, se devuelve un único message box con todos los errores que haya.
            {
                MessageBox.Show("Se encontraron los siguientes errores:\n "
                    + (tituloOK ? "" : "El título está vacío.\n")
                    + (nombreOK ? "" : "El nombre está vacío.\n")
                    + (isbnOK ? "" : "El ISBN está vacío.\n")
                    + (paginasOK ? "" : "El número de páginas está vacío o es inválido.\n")
                    + (edicionOK ? "" : "El número de edición está vacío o es inválido.\n")
                    + (editorialOK ? "" : "La editorial está vacía.\n")
                    + (ciudadOK ? "" : "La ciudad está vacía.\n")
                    + (paisOK ? "" : "El pais está vacío.\n")
                    + (fechaOK ? "" : "La fecha está vacía o es inválida.\n")
                    );
            }
            return (datosOK);




        }

        void SaveLibro() //crear un nuevo Libro con los parámetros de los textbox
        {

            Libro l = new Libro();
            l.Titulo = boxTitulo.Text;
            l.Autor = new Autor(boxNombre.Text, boxApellido.Text);
            l.Isbn = boxISBN.Text;
            l.Paginas = Int32.Parse(boxPaginas.Text);
            l.Edicion = Int32.Parse(boxEdicion.Text);
            l.Editorial = boxEditorial.Text;
            l.Lugar = new Lugar(boxCiudad.Text, boxPais.Text);
            l.Fecha = DateTime.Parse(boxFecha.Text);

            l.Id = _Libro != null ? _Libro.Id : 0; //Si el libro es nuevo, su ID es 0, caso contrario se usa su ID existente.
            _libroLogic.GuardarLibro(l); //llamar a capa lógica para ejecutar INSERT o UPDATE según corresponda
        }
        public void CargarLibro(Libro l) //se llama al actualizar un libro

        {
            _Libro = l; //cargar libro actual a la variable global
            if (l != null)
            {
                borrarCampos(); //borrar campos y pre-cargar los campos con los datos del libro a actualizar
                boxTitulo.Text = l.Titulo;
                boxNombre.Text = l.Autor.nombre;
                boxApellido.Text = l.Autor.apellido;
                boxISBN.Text = l.Isbn;
                boxPaginas.Text = l.Paginas.ToString();
                boxEdicion.Text = l.Edicion.ToString();
                boxEditorial.Text = l.Editorial;
                boxCiudad.Text = l.Lugar.ciudad;
                boxPais.Text = l.Lugar.pais;
                boxFecha.Text = l.Fecha.ToString();
            }

        }
        private void botonBorrar_Click(object sender, EventArgs e) //botón de borrar para limpiar campos
        {
            borrarCampos();
        }
    }
}
