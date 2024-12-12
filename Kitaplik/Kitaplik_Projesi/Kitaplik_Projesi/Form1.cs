using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kitaplik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=MUSTAFA\\MUSTAFASQL;Initial Catalog=Kitaplik;Integrated Security=True;");

        void Listele()
        {
            //SqlCommand cmd = new SqlCommand("Select * from Tbl_Kitaplar", baglanti);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Kitaplar", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridKitapListesi.DataSource = dt;
        }
        void TurListele()
        {
            SqlCommand cmdTur = new SqlCommand("select * from Tbl_Turler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmdTur);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbTur.ValueMember = "TurId";
            cmbTur.DisplayMember = "TurAd";
            cmbTur.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
            TurListele();
            txtKitapAd.Focus();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmdekle = new SqlCommand("insert into Tbl_Kitaplar (KitapAdi,Yazar,Sayfa,Fiyat,YayinEvi,Tur) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            cmdekle.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            cmdekle.Parameters.AddWithValue("@p2", txtYazar.Text);
            cmdekle.Parameters.AddWithValue("@p3", txtSayfa.Text);
            cmdekle.Parameters.AddWithValue("@p4", Convert.ToDouble(txtFiyat.Text));
            cmdekle.Parameters.AddWithValue("@p5", txtYayinEvi.Text);
            cmdekle.Parameters.AddWithValue("@p6", cmbTur.SelectedIndex + 1);
            cmdekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Veri Tabanına Eklendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }

        private void gridKitapListesi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = gridKitapListesi.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKitapAd.Text = gridKitapListesi.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtYazar.Text = gridKitapListesi.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSayfa.Text = gridKitapListesi.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtFiyat.Text = gridKitapListesi.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtYayinEvi.Text = gridKitapListesi.Rows[e.RowIndex].Cells[5].Value.ToString();
            cmbTur.Text = gridKitapListesi.Rows[e.RowIndex].Cells[6].Value.ToString();

        }
        void Temizle()
        {
            txtId.Clear();
            txtKitapAd.Clear();
            txtYazar.Clear();
            txtSayfa.Clear();
            txtFiyat.Clear();
            txtYayinEvi.Clear();
            cmbTur.Text = "";
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmdsil = new SqlCommand("Delete From Tbl_Kitaplar where KitapId=@p1", baglanti);
            cmdsil.Parameters.AddWithValue("@p1", txtId.Text);
            cmdsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Veri Tabanından Silindi!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmdekle = new SqlCommand("Update Tbl_Kitaplar set KitapAdi=@p1,Yazar=@p2, Sayfa=@p3,Fiyat=@p4,YayinEvi=@p5,Tur=@p6 where KitapId=@p7", baglanti);
            cmdekle.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            cmdekle.Parameters.AddWithValue("@p2", txtYazar.Text);
            cmdekle.Parameters.AddWithValue("@p3", txtSayfa.Text);
            cmdekle.Parameters.AddWithValue("@p4", Convert.ToDouble(txtFiyat.Text));
            cmdekle.Parameters.AddWithValue("@p5", txtYayinEvi.Text);
            cmdekle.Parameters.AddWithValue("@p6", cmbTur.SelectedIndex + 1);
            cmdekle.Parameters.AddWithValue("@p7", txtId.Text);
            cmdekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Bilgisi Veri Tabanında Güncellendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }
    }
}
