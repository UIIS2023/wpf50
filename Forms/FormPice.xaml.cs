using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace PekaraWPF.Forms
{
    public partial class FormPice : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool update;
        private DataRowView row;
        public FormPice()
        {
            InitializeComponent();
            konekcija = kon.CreateConnection();
            FillComboBox();
        }
        public FormPice(bool update, DataRowView row)
        {
            InitializeComponent();
            konekcija = kon.CreateConnection();
            this.update = update;
            this.row = row;
            FillComboBox();
        }

        private void FillComboBox()
        {
            try
            {
                konekcija.Open();
                string tipovi = @"SELECT tipPicaID,nazivTipaPica FROM tipPica";
                SqlDataAdapter adapter = new SqlDataAdapter(tipovi, konekcija);
                DataTable dataTableTipovi = new DataTable();
                adapter.Fill(dataTableTipovi);
                cbTipPica.ItemsSource = dataTableTipovi.DefaultView;
                adapter.Dispose();
                dataTableTipovi.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspesno upisivanje vrednosti u ComboBox", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@cena", SqlDbType.Int).Value = int.Parse(txtCena.Text);
                cmd.Parameters.Add("@kolicina", SqlDbType.Int).Value = int.Parse(txtKolicina.Text);
                cmd.Parameters.Add("@tipPicaID", SqlDbType.Int).Value = cbTipPica.SelectedValue;

                if (update)
                {
                    cmd.Parameters.Add("@piceID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"UPDATE Pice SET ime=@ime,cena=@cena, kolicina=@kolicina, tipPicaID=@tipPicaID WHERE piceID=@piceID";
                    row = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Pice(ime,cena,kolicina,tipPicaID) VALUES (@ime,@cena,@kolicina,@tipPicaID)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Invalid input", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
