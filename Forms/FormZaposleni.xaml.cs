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
    public partial class FormZaposleni : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool update;
        private DataRowView row;
        public FormZaposleni()
        {
            InitializeComponent();
            konekcija = kon.CreateConnection();
        }

        public FormZaposleni(bool update, DataRowView row)
        {
            InitializeComponent();
            konekcija = kon.CreateConnection();
            this.update = update;
            this.row = row;
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
                cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = txtJMBG.Text;
                cmd.Parameters.Add("@pozicija", SqlDbType.NVarChar).Value = txtPozicija.Text;
                cmd.Parameters.Add("@plata", SqlDbType.Money).Value = txtPlata.Text;
                cmd.Parameters.Add("@kontakt", SqlDbType.NVarChar).Value = txtKontakt.Text;
                cmd.Parameters.Add("@adresa", SqlDbType.NVarChar).Value = txtAdresa.Text;
                cmd.Parameters.Add("@grad", SqlDbType.NVarChar).Value = txtGrad.Text;
                if (update)
                {
                    cmd.Parameters.Add("@zaposleniID", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = @"UPDATE Zaposleni SET ime=@ime,prezime=@prezime, JMBG=@JMBG, pozicija=@pozicija,plata=@plata,kontakt=@kontakt,adresa=@adresa, grad=@grad WHERE zaposleniID=@zaposleniID";
                    row = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Zaposleni(ime,prezime,JMBG,pozicija,plata,kontakt,adresa,grad) VALUES (@ime,@prezime,@JMBG,@pozicija,@plata,@kontakt,@adresa,@grad)";
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
