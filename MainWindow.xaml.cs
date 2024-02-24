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
using System.Data;
using System.Data.SqlClient;
using PekaraWPF.Forms;

namespace PekaraWPF
{ 
    public partial class MainWindow : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private string ucitanaTabela;
        private bool update;
        private DataRowView row;

        #region Select upiti

        private static string zaposleniSelect = @"SELECT zaposleniID as ID, ime + ' ' + prezime as 'Ime', pozicija as Pozicija, plata as Plata, kontakt as Kontakt,JMBG, adresa as Adresa,grad as Grad from Zaposleni ";
        private static string pecivoSelect = @"SELECT pecivoID as ID, ime as 'Naziv peciva', cena as Cena, kolicina as Kolicina, tipPeciva.nazivTipaPeciva as 'Tip peciva' FROM Pecivo
                                               JOIN tipPeciva ON Pecivo.tipPecivaID = tipPeciva.tipPecivaID";
        private static string piceSelect = @"SELECT piceID as ID, ime as 'Naziv pica', cena as Cena, kolicina as Kolicina, tipPica.nazivTipaPica as 'Tip pica' FROM Pice
                                             JOIN tipPica ON Pice.tipPicaID=tipPica.tipPicaID";
        private static string kupacSelect = @"SELECT kupacID as ID, ime as 'Ime kupca', kontakt as Kontakt, adresa as Adresa FROM Kupac";
        private static string tipPecivaSelect = @"SELECT tipPecivaID as ID, nazivTipaPeciva as 'Tip peciva' FROM tipPeciva";
        private static string tipPicaSelect = @"SELECT tipPicaID as ID, nazivTipaPica as 'Tip pica' FROM tipPica";
        private static string tipNarudzbineSelect = @"SELECT tipNarudzbineID as ID, vrstaNarudzbine as 'Vrsta narudzbine' FROM tipNarudzbine";
        private static string narudzbinaSelect = @"SELECT narudzbinaID as ID, vremeNarudzbine as 'Vreme narudzbine',tipNarudzbine.vrstaNarudzbine as 'Vrsta narudzbine',ukupnaCena as 'Ukupna cena',Kupac.ime as 'Ime kupca', Zaposleni.ime as 'Ime zaposlenog', Pecivo.ime as 'Naziv peciva', Pecivo.cena as 'Cena peciva', Pice.ime as 'Naziv pica', Pice.cena as 'Cena pica' FROM Narudzbina
                                                   JOIN tipNarudzbine ON Narudzbina.tipNarudzbineID=tipNarudzbine.tipNarudzbineID
                                                   JOIN Kupac ON Narudzbina.kupacID = Kupac.kupacID
                                                   JOIN Zaposleni ON Narudzbina.zaposleniID = Zaposleni.zaposleniID
                                                   JOIN Pecivo ON Narudzbina.pecivoID = Pecivo.pecivoID
                                                   JOIN Pice ON Narudzbina.piceID = Pice.piceID";
        #endregion
        #region Delete 

        private static string zaposleniDelete = @"DELETE FROM Zaposleni WHERE zaposleniID=";
        private static string pecivoDelete = @"DELETE FROM Pecivo WHERE pecivoID=";
        private static string piceDelete = @"DELETE FROM Pice WHERE piceID=";
        private static string narudzbinaDelete = @"DELETE FROM Narudzbina WHERE narudzbinaID=";
        private static string kupacDelete = @"DELETE FROM Kupac WHERE kupacID=";
        private static string tipPecivaDelete = @"DELETE FROM tipPeciva WHERE tipPecivaID=";
        private static string tipPicaDelete = @"DELETE FROM tipPica WHERE tipPicaID=";
        private static string tipNarudzbineDelete = @"DELETE FROM tipNarudzbine WHERE tipNarudzbineID=";
        #endregion
        #region Select sa uslovom
        private static string selectUslovZaposleni = @"SELECT * FROM Zaposleni WHERE zaposleniID=";
        private static string selectUslovKupac = @"SELECT * FROM Kupac WHERE kupacID=";
        private static string selectUslovPecivo = @"SELECT * FROM Pecivo WHERE pecivoID=";
        private static string selectUslovPice = @"SELECT * FROM Pice WHERE piceID=";
        private static string selectUslovNarudzbina = @"SELECT * FROM Narudzbina WHERE narudzbinaID=";
        private static string selectUslovTipPica = @"SELECT * FROM tipPica WHERE tipPicaID=";
        private static string selectUslovTipPeciva = @"SELECT * FROM tipPeciva WHERE tipPecivaID=";
        private static string selectUslovTipNarudzbine = @"SELECT * FROM tipNarudzbine WHERE tipNarudzbineID=";
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.CreateConnection();
            UcitajPodatke(pecivoSelect);
        }
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanaTabela.Equals(zaposleniSelect))
            {
                prozor = new FormZaposleni();
                prozor.ShowDialog();
                UcitajPodatke(zaposleniSelect);
            }
            else if (ucitanaTabela.Equals(kupacSelect))
            {
                prozor = new FormKupac();
                prozor.ShowDialog();
                UcitajPodatke(kupacSelect);
            }
            else if (ucitanaTabela.Equals(pecivoSelect))
            {
                prozor = new FormPecivo();
                prozor.ShowDialog();
                UcitajPodatke(pecivoSelect);
            }
            else if (ucitanaTabela.Equals(piceSelect))
            {
                prozor = new FormPice();
                prozor.ShowDialog();
                UcitajPodatke(piceSelect);
            }
            else if (ucitanaTabela.Equals(narudzbinaSelect))
            {
                prozor = new FormNarudzbina();
                prozor.ShowDialog();
                UcitajPodatke(narudzbinaSelect);
            }

        }
        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(zaposleniSelect))
            {
                PopuniFormu(selectUslovZaposleni);
                UcitajPodatke(zaposleniSelect);
            }
            else if (ucitanaTabela.Equals(kupacSelect))
            {
                PopuniFormu(selectUslovKupac);
                UcitajPodatke(kupacSelect);
            }
            else if (ucitanaTabela.Equals(pecivoSelect))
            {
                PopuniFormu(selectUslovPecivo);
                UcitajPodatke(pecivoSelect);
            }
            else if (ucitanaTabela.Equals(piceSelect))
            {
                PopuniFormu(selectUslovPice);
                UcitajPodatke(piceSelect);
            }
            else if (ucitanaTabela.Equals(narudzbinaSelect))
            {
                PopuniFormu(selectUslovNarudzbina);
                UcitajPodatke(narudzbinaSelect);
            }
        }
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(zaposleniSelect))
            {
                ObrisiZapis(zaposleniDelete);
                UcitajPodatke(zaposleniSelect);
            }
            else if (ucitanaTabela.Equals(kupacSelect))
            {
                ObrisiZapis(kupacDelete);
                UcitajPodatke(kupacSelect);
            }
            else if (ucitanaTabela.Equals(pecivoSelect))
            {
                ObrisiZapis(pecivoDelete);
                UcitajPodatke(pecivoSelect);
            }
            else if (ucitanaTabela.Equals(piceSelect))
            {
                ObrisiZapis(piceDelete);
                UcitajPodatke(piceSelect);
            }
            else if (ucitanaTabela.Equals(narudzbinaSelect))
            {
                ObrisiZapis(narudzbinaDelete);
                UcitajPodatke(narudzbinaSelect);
            }
        }
        private void btnZaposleni_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(zaposleniSelect);
        }
        private void btnKupac_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(kupacSelect);
        }
        private void btnPecivo_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(pecivoSelect);
        }
        private void btnPice_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(piceSelect);
        }
        private void btnNarudzbina_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(narudzbinaSelect);
        }
        private void UcitajPodatke(string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                if (dataGridCentralni != null)
                {
                    dataGridCentralni.ItemsSource = dataTable.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dataAdapter.Dispose();
                dataTable.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Greska pri ucitavanju tabele", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void PopuniFormu(object selectUslov)
        {
            try
            {
                konekcija.Open();
                update = true;
                row = (DataRowView)dataGridCentralni.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = row["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader reader = cmd.ExecuteReader();
                cmd.Dispose();
                if (reader.Read())
                {   
                    if (ucitanaTabela.Equals(zaposleniSelect))
                    {
                        FormZaposleni formZaposleni = new FormZaposleni(update, row);
                        formZaposleni.txtIme.Text = reader["ime"].ToString();
                        formZaposleni.txtPrezime.Text = reader["prezime"].ToString();
                        formZaposleni.txtJMBG.Text = reader["JMBG"].ToString();
                        formZaposleni.txtPozicija.Text = reader["pozicija"].ToString();
                        formZaposleni.txtPlata.Text = reader["plata"].ToString();
                        formZaposleni.txtKontakt.Text = reader["kontakt"].ToString();
                        formZaposleni.txtAdresa.Text = reader["adresa"].ToString();
                        formZaposleni.txtGrad.Text = reader["grad"].ToString();
                        formZaposleni.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(kupacSelect))
                    {
                        FormKupac formKupac = new FormKupac(update,row);
                        formKupac.txtIme.Text = reader["ime"].ToString();
                        formKupac.txtKontakt.Text = reader["kontakt"].ToString();
                        formKupac.txtAdresa.Text = reader["adresa"].ToString();
                        formKupac.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(pecivoSelect))
                    {
                        FormPecivo formPecivo = new FormPecivo(update, row);
                        formPecivo.txtIme.Text = reader["ime"].ToString();
                        formPecivo.txtCena.Text = reader["cena"].ToString();
                        formPecivo.txtKolicina.Text = reader["kolicina"].ToString();
                        formPecivo.cbTipPeciva.SelectedValue = reader["tipPecivaID"].ToString();
                        formPecivo.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(piceSelect))
                    {
                        FormPice formPice = new FormPice(update, row);
                        formPice.txtIme.Text = reader["ime"].ToString();
                        formPice.txtCena.Text = reader["cena"].ToString();
                        formPice.txtKolicina.Text = reader["kolicina"].ToString();
                        formPice.cbTipPica.SelectedValue = reader["tipPicaID"].ToString();
                        formPice.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(narudzbinaSelect))
                    {
                        FormNarudzbina formNarudzbina = new FormNarudzbina(update,row);
                        formNarudzbina.txtUkupnaCena.Text = reader["ukupnaCena"].ToString();
                        formNarudzbina.txtVremeNarudzbine.Text = reader["vremeNarudzbine"].ToString();
                        formNarudzbina.cbZaposleni.SelectedValue = reader["zaposleniID"].ToString();
                        formNarudzbina.cbKupac.SelectedValue = reader["kupacID"].ToString();
                        formNarudzbina.cbPice.SelectedValue = reader["piceID"].ToString();
                        formNarudzbina.cbPecivo.SelectedValue = reader["pecivoID"].ToString();
                        formNarudzbina.cbTipNarudzbine.SelectedValue = reader["tipNarudzbineID"].ToString();
                        formNarudzbina.ShowDialog();
                    }
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally 
            {
                if(konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void ObrisiZapis(string deleteUslov)
        {
            try
            {
                konekcija.Open();
                row = (DataRowView)dataGridCentralni.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni?", "WARNING", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (rezultat == MessageBoxResult.Yes)
                 {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = row["ID"];
                    cmd.CommandText = deleteUslov + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u drugim tabelama", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
    }
}
