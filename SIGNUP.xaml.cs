using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;

namespace EmapaProyect
{
    /// <summary>
    /// Lógica de interacción para SIGNUP.xaml
    /// </summary>
    public partial class SIGNUP : Window
    {
        private readonly string rutaYnombreArch = "c:\\datosUsuario\\datosUsr.txt";
        public SIGNUP()
        {
            InitializeComponent();
           

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)

        {

            txtNombre.Clear();

            txtCelular.Clear();

            txtEmail.Clear();

            pwdContra.Password = "";

        }

        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)

        {

            Regex regexNombre = new Regex("^[a-zA-Z ]$");

            e.Handled = !regexNombre.IsMatch(e.Text);

        }
        private void txtEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex regexEmail = new Regex("^[a-zA-Z0-9@.]$");

            e.Handled = !regexEmail.IsMatch(e.Text);

        }
        private void txtCelular_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex regexCelular = new Regex("^[0-9-+]$");

            e.Handled = !regexCelular.IsMatch(e.Text);

        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == "" || txtEmail.Text == "" || txtCelular.Text == "" || pwdContra.Password == "")

            {

                lblMensajes.Content = "Debe completar TODOS los datos";

                lblMensajes.Foreground = Brushes.Red;


            }
            else
            {
                try
                {

                    lblMensajes.Content = "Bienvenido al sistema NN " + txtNombre.Text + "...";

                    lblMensajes.Foreground = Brushes.Black;

                    string datos = txtNombre.Text + "," + txtEmail.Text + "," + txtCelular.Text + "," + pwdContra.Password + "\n";

                    File.AppendAllText(rutaYnombreArch, datos);

                    MainWindow winP = new  MainWindow();

                    winP.Show();

                    this.Close();

                }

                catch (Exception ex)

                {

                    MessageBox.Show("Error al guardar el archivo" + ex.Message);

                }


            }

        }
    }
}
