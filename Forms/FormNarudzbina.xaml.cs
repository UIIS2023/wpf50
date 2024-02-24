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
    public partial class FormNarudzbina : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool update;
        private DataRowView row;
        public FormNarudzbina()
        {
            InitializeComponent();
            konekcija = kon.CreateConnection();
            FillComboBox();
        }
        public FormNarudzbina(bool update, DataRowView row)
        {
            InitializeComponent();
            konekcija = kon.CreateConnection();
            this.update = update;
            this.row = row;
            FillComboBox();
        }

        public void UpdateUkupnaCena(object sender, SelectionChangedEventArgs e)
        {
            int cenaPica, cenaPeciva, ukupnaCena;
            try
            {
                if (cbPice.SelectedValue != null)
                {
                    cenaPica = GetCena(Convert.ToInt32(cbPice.SelectedValue), "Pice", "piceID");
                }
                else
                {
                    cenaPica = 0;
                }
                if (cbPecivo.SelectedValue != null)
                {
                    cenaPeciva = GetCena(Convert.ToInt32(cbPecivo.SelectedValue), "Pecivo", "pecivoID");
                }
                else
                {
                    cenaPeciva = 0;
                }
                ukupnaCena = cenaPica + cenaPeciva;
                txtUkupnaCena.Text = ukupnaCena.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in UpdateUkupnaCena: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private int GetCena(int id, string tableName, string primaryKey)
        {
            if (id != 0)
            {
                try
                {
                    konekcija.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT cena FROM {tableName} WHERE {primaryKey} = @id", konekcija);
                    cmd.Parameters.AddWithValue("@id", id);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    return 0;
                }
                catch (SqlException)
                {
                    MessageBox.Show("Greška prilikom čitanja cene iz baze podataka:", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }
                finally
                {
                    if (konekcija != null)
                    {
                        konekcija.Close();
                    }
                }
            }
            return 0;
        }


        private void FillComboBox()
        {
            try
            {
                konekcija.Open();
                DateTime currentTime = DateTime.Now;
                txtVremeNarudzbine.Text = currentTime.ToString("yyyy-MM-dd HH:mm");
                string tipovi = @"SELECT tipNarudzbineID,vrstaNarudzbine FROM tipNarudzbine";
                SqlDataAdapter adapterTipNarudzbine = new SqlDataAdapter(tipovi, konekcija);
                DataTable dataTableTipovi = new DataTable();
                adapterTipNarudzbine.Fill(dataTableTipovi);
                cbTipNarudzbine.ItemsSource = dataTableTipovi.DefaultView;
                adapterTipNarudzbine.Dispose();
                dataTableTipovi.Dispose();

                string zaposleni = @"SELECT zaposleniID,ime FROM Zaposleni";
                SqlDataAdapter adapterZaposleni = new SqlDataAdapter(zaposleni, konekcija);
                DataTable dataTableZaposleni = new DataTable();
                adapterZaposleni.Fill(dataTableZaposleni);
                cbZaposleni.ItemsSource = dataTableZaposleni.DefaultView;
                adapterZaposleni.Dispose();
                dataTableZaposleni.Dispose();

                string kupac = @"SELECT kupacID,ime FROM Kupac";
                SqlDataAdapter adapterKupac = new SqlDataAdapter(kupac, konekcija);
                DataTable dataTableKupac = new DataTable();
                adapterKupac.Fill(dataTableKupac);
                cbKupac.ItemsSource = dataTableKupac.DefaultView;
                adapterKupac.Dispose();
                dataTableKupac.Dispose();

                string pice = @"SELECT piceID, ime FROM Pice";
                SqlDataAdapter adapterPice = new SqlDataAdapter(pice, konekcija);
                DataTable dataTablePice = new DataTable();
                adapterPice.Fill(dataTablePice);
                cbPice.ItemsSource = dataTablePice.DefaultView;
                adapterPice.Dispose();
                dataTablePice.Dispose();

                string pecivo = @"SELECT pecivoID, ime FROM Pecivo";
                SqlDataAdapter adapterPecivo = new SqlDataAdapter(pecivo,konekcija);
                DataTable dataTablePecivo = new DataTable();
                adapterPecivo.Fill(dataTablePecivo);
                cbPecivo.ItemsSource = dataTablePecivo.DefaultView;
                adapterPecivo.Dispose();
                dataTablePecivo.Dispose();

                cbPice.SelectionChanged += UpdateUkupnaCena;
                cbPecivo.SelectionChanged += UpdateUkupnaCena;
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
                cmd.Parameters.Add("@vremeNarudzbine", SqlDbType.DateTime).Value = txtVremeNarudzbine.Text;
                cmd.Parameters.Add("@zaposleniID", SqlDbType.Int).Value = cbZaposleni.SelectedValue;
                cmd.Parameters.Add("@kupacID", SqlDbType.Int).Value = cbKupac.SelectedValue;
                cmd.Parameters.Add("@piceID", SqlDbType.Int).Value = cbPice.SelectedValue;
                cmd.Parameters.Add("@pecivoID", SqlDbType.Int).Value = cbPecivo.SelectedValue;
                cmd.Parameters.Add("@tipNarudzbineID", SqlDbType.Int).Value = cbTipNarudzbine.SelectedValue;
                cmd.Parameters.Add("@ukupnaCena", SqlDbType.Int).Value = int.Parse(txtUkupnaCena.Text);

                if (update)
                {
                    cmd.Parameters.Add("@narudzbinaID", SqlDbType.Int).Value = Convert.ToInt32(row["ID"]);
                    cmd.CommandText = @"UPDATE Narudzbina SET ukupnaCena=@ukupnaCena,vremeNarudzbine=@vremeNarudzbine,zaposleniID=@zaposleniID,kupacID=@kupacID,piceID=@piceID,pecivoID=@pecivoID,tipNarudzbineID=@tipNarudzbineID WHERE narudzbinaID=@narudzbinaID";
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Narudzbina(zaposleniID,tipNarudzbineID,kupacID,pecivoID,piceID,vremeNarudzbine,ukupnaCena) VALUES (@zaposleniID,@tipNarudzbineID,@kupacID,@pecivoID,@piceID,@vremeNarudzbine,@ukupnaCena)";
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
