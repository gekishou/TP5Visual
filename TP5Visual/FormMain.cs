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
    public partial class FormMain : Form
    {
        private LibroLogic _libroLogic; //variable privada de tipo capa lógica
        public FormMain()
        {
            InitializeComponent();
            _libroLogic = new LibroLogic(); //crear nueva instancia de capa lógica
        }

        private void botonAgregar_Click(object sender, EventArgs e)
        {
            //llamar al formulario FormLibro
            FormLibro fl = new FormLibro();
            fl.ShowDialog(this);

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            CargarLibros();
        }

 
        public void CargarLibros(String SearchText = null) //ejecuta un SELECT y actualiza la grid 
        {
            List<Libro> listaLibros = _libroLogic.GetLibros(SearchText);
            GridLibro.DataSource = listaLibros;

        }

        private void GridLibro_CellContentClick(object sender, DataGridViewCellEventArgs e) //al clickear una celda
        {
            try //si no se puede castear la celda a DataGridViewLinkCell, se clickeó una celda que no es link y no se toma acción
            {
                DataGridViewLinkCell cell = (DataGridViewLinkCell)GridLibro.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value.ToString() == "Actualizar") //si clickeó actualizar
                {
                    FormLibro fl = new FormLibro(); //abrir formulario para editar valores
                     fl.CargarLibro(new Libro //cargar un libro con los valores que se muestran en las columnas de la fila seleccionada
                    {
                        Id = int.Parse((GridLibro.Rows[e.RowIndex].Cells[0]).Value.ToString()),
                        Titulo = GridLibro.Rows[e.RowIndex].Cells[1].Value.ToString(),
                        Autor = new Autor(GridLibro.Rows[e.RowIndex].Cells[2].Value.ToString().Split(' ')),
                        Isbn = GridLibro.Rows[e.RowIndex].Cells[3].Value.ToString(),
                        Paginas = int.Parse(GridLibro.Rows[e.RowIndex].Cells[4].Value.ToString()),
                        Edicion = int.Parse(GridLibro.Rows[e.RowIndex].Cells[5].Value.ToString()),
                        Editorial = GridLibro.Rows[e.RowIndex].Cells[6].Value.ToString(),
                        Lugar = new Lugar(GridLibro.Rows[e.RowIndex].Cells[7].Value.ToString().Split(',')),
                        Fecha = DateTime.Parse(GridLibro.Rows[e.RowIndex].Cells[8].Value.ToString()),
                    });
                    fl.ShowDialog(this);
                }
                else if (cell.Value.ToString() == "Eliminar") //si clickeó eliminar
                {
                    BorrarLibro(int.Parse((GridLibro.Rows[e.RowIndex].Cells[0]).Value.ToString())); //borrar libro con el id en la columna 0 de la fila seleccionada


                }
                CargarLibros();
            }
            catch
            {

            }
            

        }

        private void BorrarLibro(int id)
        {
            _libroLogic.BorrarLibro(id); //DELETE con el ID seleccionado
        }

        private void botonBuscar_Click_1(object sender, EventArgs e)
        {
            CargarLibros(boxBusqueda.Text); //realizar búsqueda con el contenido del box de búsqueda
        }
    }
}
