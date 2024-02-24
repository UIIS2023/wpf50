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
    public partial class FormPecivo : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool update;
        private DataRowView row;
        public FormPecivo()
        {
            InitializeComponent();
            konekcija = kon.CreateConnection();
            FillComboBox();
        }

        public FormPecivo(bool update, DataRowView row)
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
                string tipovi = @"SELECT tipPecivaID,nazivTipaPeciva FROM tipPeciva";
                SqlDataAdapter adapter = new SqlDataAdapter(tipovi, konekcija);
                DataTable dataTableTipovi = new DataTable();
                adapter.Fill(dataTableTipovi);
                cbTipPeciva.ItemsSource = dataTableTipovi.DefaultView;
                adapter.Dispose();
                dataTableTipovi.Dispose();
            }
            catch(SqlException)
            {
                MessageBox.Show("Neuspesno upisivanje vrednosti u ComboBox", "ERROR",MessageBoxButton.OK, MessageBoxImage.Error);
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
                cmd.Parameters.Add("@tipPecivaID", SqlDbType.Int).Value = cbTipPeciva.SelectedValue;

                if (update)
                {
                    cmd.Parameters.Add("@pecivoID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"UPDATE Pecivo SET ime=@ime,cena=@cena, kolicina=@kolicina, tipPecivaID=@tipPecivaID WHERE pecivoID=@pecivoID";
                    row = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Pecivo(ime,cena,kolicina,tipPecivaID) VALUES (@ime,@cena,@kolicina,@tipPecivaID)";
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
