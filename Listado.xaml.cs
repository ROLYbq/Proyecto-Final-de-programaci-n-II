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

namespace EmapaProyect
{
    /// <summary>
    /// Lógica de interacción para Listado.xaml
    /// </summary>
    public partial class Listado : Window
    {
        public Listado()
        {
            InitializeComponent();
        }

        public class ItemSupermercado
        {
            public string Nombre { get; set; }
            public int Cantidad { get; set; }
            public double Precio { get; set; }
            public double Subtotal => Cantidad * Precio;
        }

        // Diccionario de precios en Bs
        Dictionary<string, double> precios = new Dictionary<string, double>
        {
            { "Leche", 7.5 },
            { "Huevos (12)", 12.0 },
            { "Arroz (kg)", 6.0 },
            { "Azúcar (kg)", 5.0 },
            { "Aceite (L)", 12.5 },
            { "Pan (unidad)", 0.80 },
            { "Queso (kg)", 28.0 },
            { "Yogurt (1L)", 9.0 }
        };

      //  public Listado()
      //  {
       //     InitializeComponent();
      //      CargarProductos();
       // }

        private void CargarProductos()
        {
            cbProductos.ItemsSource = precios.Keys;
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (cbProductos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un producto.");
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingresa una cantidad válida.");
                return;
            }

            string nombre = cbProductos.SelectedItem.ToString();
            double precio = precios[nombre];

            ItemSupermercado item = new ItemSupermercado
            {
                Nombre = nombre,
                Cantidad = cantidad,
                Precio = precio
            };

            lvLista.Items.Add(item);
            txtCantidad.Clear();
            ActualizarTotal();
        }

        private void Quitar_Click(object sender, RoutedEventArgs e)
        {
            if (lvLista.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un item para quitar.");
                return;
            }

            lvLista.Items.Remove(lvLista.SelectedItem);
            ActualizarTotal();
        }

        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            if (lvLista.Items.Count == 0)
            {
                MessageBox.Show("No hay productos en la lista.");
                return;
            }

            MessageBox.Show($"Compra finalizada.\nTotal a pagar: {lblTotal.Content}",
                            "Supermercado", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ActualizarTotal()
        {
            double total = 0;

            foreach (ItemSupermercado item in lvLista.Items)
            {
                total += item.Subtotal;
            }

            lblTotal.Content = $"{total} Bs";
        }
    }
}

