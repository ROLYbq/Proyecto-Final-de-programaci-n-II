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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.IO;


namespace EmapaProyect
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private readonly string rutaYnombreArch = "c:\\datosUsuario\\datosUsr.txt";


        private void btnLimpiar_Click(object sender, RoutedEventArgs e)

        {

            txtEmail.Clear();

            pwdContra.Password = "";

        }


        private void txtEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)

        {

            Regex regexEmail = new Regex("^[a-zA-Z0-9@.]$");

            e.Handled = !regexEmail.IsMatch(e.Text);

        }


        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text == "" || pwdContra.Password == "")
            {
                lblMensajes.Content = "Debe completar TODOS los datos";
                lblMensajes.Foreground = Brushes.Red;
            }
            else
            {
                try
                {
                    if (!File.Exists(rutaYnombreArch))
                    {
                        lblMensajes.Content = "No hay usuarios registrados. Por favor, regístrese primero.";
                        lblMensajes.Foreground = Brushes.Red;
                        return;
                    }

                    string[] lineas = File.ReadAllLines(rutaYnombreArch);

                    // Buscar si el email existe en el archivo
                    bool usuarioExiste = lineas.Any(line =>
                    {
                        var partes = line.Split(',');
                        return partes.Length > 1 && partes[1].Equals(txtEmail.Text.Trim(), StringComparison.OrdinalIgnoreCase);
                    });

                    if (!usuarioExiste)
                    {
                        lblMensajes.Content = "Usuario no registrado. Por favor, regístrese primero.";
                        lblMensajes.Foreground = Brushes.Red;
                        return;
                    }

                    // Si el usuario existe, continúa con el proceso normal:
                    lblMensajes.Content = "Bienvenido al sistema  " + "...";
                    lblMensajes.Foreground = Brushes.Black;

                    string datos = lblEmail.Content.ToString() + txtEmail.Text + "\n" +
                                   lblPassword.Content.ToString() + pwdContra.Password + "\n";

                    File.AppendAllText(rutaYnombreArch, datos);

                    Principal winP = new Principal();
                    winP.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo" + ex.Message);
                }
            }
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)

        {

            SIGNUP winSignup = new SIGNUP();

            winSignup.Show();

            this.Close();

        }
    }


}


