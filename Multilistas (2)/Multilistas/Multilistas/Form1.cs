using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multilistas
{
    public partial class Form1 : Form
    {
        Multilista multilista = new Multilista();
        private DataTable dtMaterias;
        private DataTable dtEstudiantes;
        private DataTable dtEstudiante;
        private DataTable dtListaEstudiantes;
        private DataTable dtMateria;
        private DataTable dtListaMaterias;

        public void MostrarMaterias( DataTable dtMaterias)
        {
            NodoMateria p;
            dtMaterias.Rows.Clear();
            p = multilista.carrera.EnlaceMateria;
            while ( p != null)
            {
                DataRow row = dtMaterias.NewRow();
                row[ "Sigla"] = p.Sigla;
                row["Materia"] = p.NombreMateria;

                dtMaterias.Rows.Add(row);
                p = p.EnlaceMateria;
            } 
        }
        public void MostrarEstudiantes(DataTable dtEstudiantes)
        {
            NodoMateria p;
            NodoEstudiante q;
            dtEstudiantes.Rows.Clear();

            p = multilista.carrera.EnlaceMateria;
           
            while (p != null)
            {
                DataRow rowMateria = dtEstudiantes.NewRow();
                rowMateria["Sigla"] = p.Sigla;
                
                dtEstudiantes.Rows.Add(rowMateria);
               
                q = p.EnlaceEstudiante;

                while (q != null)
                {
                    DataRow rowEstudiante = dtEstudiantes.NewRow();
                    rowEstudiante["CU"] = q.Cu;
                    rowEstudiante["Ci"] = q.Ci;
                    rowEstudiante["Nombre"] = q.NombreCompleto;
                    rowEstudiante["Direccion"] = q.Direccion;
                    rowEstudiante["Celular"] = q.Celular;

                    dtEstudiantes.Rows.Add(rowEstudiante);
                    q = q.EnlaceEstudiante;

                }
                p = p.EnlaceMateria;
            }
        }

        public Form1()
        {
            InitializeComponent();
            dtMaterias = new DataTable();
            dtMaterias.Columns.Add("Sigla");
            dtMaterias.Columns.Add("Materia");
            dGMaterias.DataSource = dtMaterias;

            dtEstudiantes = new DataTable();
            dtEstudiantes.Columns.Add("Sigla");
            dtEstudiantes.Columns.Add("CU");
            dtEstudiantes.Columns.Add("CI");
            dtEstudiantes.Columns.Add("Nombre");
            dtEstudiantes.Columns.Add("Direccion");
            dtEstudiantes.Columns.Add("Celular");
            dGEstudiantes.DataSource = dtEstudiantes;

            // busqueda de estudiante
            dtEstudiante = new DataTable();
            dtEstudiante.Columns.Add("Sigla");
            dtEstudiante.Columns.Add("CU");
            dtEstudiante.Columns.Add("CI");
            dtEstudiante.Columns.Add("Nombre");
            dtEstudiante.Columns.Add("Direccion");
            dtEstudiante.Columns.Add("Celular");
            dGDatosEstudiante.DataSource = dtEstudiante;

            dtListaEstudiantes = new DataTable();
            dtListaEstudiantes.Columns.Add("Sigla");
            dtListaEstudiantes.Columns.Add("CU");
            dtListaEstudiantes.Columns.Add("CI");
            dtListaEstudiantes.Columns.Add("Nombre");
            dtListaEstudiantes.Columns.Add("Direccion");
            dtListaEstudiantes.Columns.Add("Celular");
            dGListaEstudiantes.DataSource = dtListaEstudiantes;

            dtMateria = new DataTable();
            dtMateria.Columns.Add("Sigla");
            dtMateria.Columns.Add("Materia");
            dGDatosMateria.DataSource = dtMateria;

            dtListaMaterias = new DataTable();
            dtListaMaterias.Columns.Add("Sigla");
            dtListaMaterias.Columns.Add("Materia");
            dGListaMaterias.DataSource = dtListaMaterias;
        }

        

       
        private void btnCrearCarrera_Click(object sender, EventArgs e)
        {
            multilista.crearCarrera(txtCarrera.Text);
        }

        private void btnInsertarMateria_Click(object sender, EventArgs e)
        {
            multilista.insertarMateria(txtSigla.Text, txtMateria.Text);
            txtSigla.Text = "";
            txtMateria.Text = "";
            txtSigla.Focus();
            txtCantidadMaterias.Text = multilista.carrera.CantidadMaterias.ToString();
        }

        private void btnMostrarMaterias_Click(object sender, EventArgs e)
        {
            MostrarMaterias(dtMaterias);
        }

       

       
        private void bntInsertarEstudiantes_Click(object sender, EventArgs e)
        {
            NodoMateria materia;
            materia = multilista.buscarMateria(txtSiglaMateria.Text);
            if (materia != null)
            {
                multilista.insertarEstudiante(materia, txtCu.Text, txtCi.Text, txtNombre.Text, txtDireccion.Text, txtCelular.Text);
                txtCu.Text = "";
                txtCi.Text = "";
                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtCelular.Text = "";

                txtCu.Focus();

            }
            else
                MessageBox.Show(" La materia no existe");
        }

        private void btnMostrarEstudiantes_Click(object sender, EventArgs e)
        {
            MostrarEstudiantes(dtEstudiantes);
        }

        
        private void bntBuscarEstudiante_Click(object sender, EventArgs e)
        {
            NodoEstudiante estudiante;
            estudiante = multilista.buscarEstudiante(txtSiglaBusqueda.Text, txtCuBusqueda.Text);
            if (estudiante != null)
            {
                dtEstudiante.Rows.Clear();
                DataRow rowEstudiante = dtEstudiante.NewRow();
                rowEstudiante["CU"] = estudiante.Cu;
                rowEstudiante["Ci"] = estudiante.Ci;
                rowEstudiante["Nombre"] = estudiante.NombreCompleto;
                rowEstudiante["Direccion"] = estudiante.Direccion;
                rowEstudiante["Celular"] = estudiante.Celular;

                dtEstudiante.Rows.Add(rowEstudiante);
            }
            else
                MessageBox.Show(" El estudiante no se encuentra registrado");
        }

        private void bntEliminarEstudiante_Click(object sender, EventArgs e)
        {
            multilista.eliminarEstudiante(txtSiglaEliminar.Text, txtCuEliminar.Text);
        }

        private void bntMostrarListaEstudiantes_Click(object sender, EventArgs e)
        {
            MostrarEstudiantes( dtListaEstudiantes);
        }

        private void bntBuscarMateria_Click(object sender, EventArgs e)
        {
            NodoMateria materia ;
            materia = multilista.buscarMateria(txtSiglaBuscarMateria.Text);
            if (materia != null)
            {
                dtMateria.Rows.Clear();
                DataRow rowMateria = dtMateria.NewRow();
                rowMateria["Sigla"] = materia.Sigla;
                rowMateria["Materia"] = materia.NombreMateria;
                
                dtMateria.Rows.Add(rowMateria);
            }
            else
                MessageBox.Show(" La materia no se encuentra registrada");
        }

        private void bntEliminarMateria_Click(object sender, EventArgs e)
        {
            multilista.eliminarMateria(txtSiglaMateriaEliminar.Text);
            txtCantidadMateriasActualizada.Text = multilista.carrera.CantidadMaterias.ToString();
        }

        private void bntMostrarMateriaActualizada_Click(object sender, EventArgs e)
        {
            MostrarMaterias(dtListaMaterias) ;
        }
    }
}
