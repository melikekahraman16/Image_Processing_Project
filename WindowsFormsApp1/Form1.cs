using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Image CikisResmi { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void resimSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(open.FileName);

                }
            }
            catch
            {

            }

        }

        private void solaAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
        }

        private void sağaAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
        }

        private void griTonaDönüşümToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk, DonusenRenk;

            Bitmap GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı. Fonksiyonla gelmedi.
            int ResimYuksekligi = GirisResmi.Height;
            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi); //Cikis resmini

            int GriDeger = 0;
            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    double R = OkunanRenk.R;
                    double G = OkunanRenk.G;
                    double B = OkunanRenk.B;
                    //GriDeger = Convert.ToInt16((R + G + B) / 3);

                    GriDeger = Convert.ToInt16(R * 0.3 + G * 0.6 + B * 0.1);
                    DonusenRenk = Color.FromArgb(GriDeger, GriDeger, GriDeger);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void resminNegatifiniAlmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk, DonusenRenk;
            int R = 0, G = 0, B = 0;

            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int i = 0, j = 0;
            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    R = 255 - OkunanRenk.R;
                    G = 255 - OkunanRenk.G;
                    B = 255 - OkunanRenk.B;
                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            pictureBox2.Image = CikisResmi;

        }

        private void parlaklığıDeğiştirmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int R = 0, G = 0, B = 0;
            Color OkunanRenk, DonusenRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int i = 0, j = 0;
            for (int x = 0; x < ResimGenisligi; x++)
            {
                j = 0;
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);

                    R = OkunanRenk.R + 50;
                    G = OkunanRenk.G + 50;
                    B = OkunanRenk.B + 50;

                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(i, j, DonusenRenk);
                    j++;
                }
                i++;
            }
            pictureBox2.Image = CikisResmi;
        }

        private void histogramınEldeEdilmesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            ArrayList DiziPiksel = new ArrayList();
            int OrtalamaRenk = 0;
            Color OkunanRenk;
            int R = 0, G = 0, B = 0;
            Bitmap InputImage; //Histogram için giriş resmi gri-ton olmalıdır.
            InputImage = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = InputImage.Width; //GirisResmi global tanımlandı.
            int ResimYuksekligi = InputImage.Height;
            int i = 0; //piksel sayısı tutulacak.

            for (int x = 0; x < InputImage.Width; x++)
            {
                for (int y = 0; y < InputImage.Height; y++)
                {
                    OkunanRenk = InputImage.GetPixel(x, y);
                    OrtalamaRenk = (int)(OkunanRenk.R + OkunanRenk.G + OkunanRenk.B) / 3;
                    DiziPiksel.Add(OrtalamaRenk); //Resimdeki tüm noktaları diziye atıyor.
                }
            }
            int[] DiziPikselSayilari = new int[256];
            for (int r = 0; r <= 255; r++) //256 tane renk tonu için dönecek.
            {
                int PikselSayisi = 0;
                for (int s = 0; s < DiziPiksel.Count; s++) //resimdeki piksel sayısınca dönecek.
                {
                    if (r == Convert.ToInt16(DiziPiksel[s]))
                        PikselSayisi++;
                }
                DiziPikselSayilari[r] = PikselSayisi;
            }
            //Değerleri listbox'a ekliyor.
            int RenkMaksPikselSayisi = 0; //Grafikte y eksenini ölçeklerken kullanılacak.
            for (int k = 0; k <= 255; k++)
            {
                listBox1.Items.Add("Renk:" + k + "=" + DiziPikselSayilari[k]);

                if (DiziPikselSayilari[k] > RenkMaksPikselSayisi)
                {
                    RenkMaksPikselSayisi = DiziPikselSayilari[k];
                }
            }
            //Grafiği çiziyor.
            Graphics CizimAlani;
            Pen Kalem1 = new Pen(System.Drawing.Color.Yellow, 1);
            Pen Kalem2 = new Pen(System.Drawing.Color.Red, 1);
            CizimAlani = pictureBox2.CreateGraphics();
            pictureBox2.Refresh();
            int GrafikYuksekligi = 250;
            double OlcekY = RenkMaksPikselSayisi / GrafikYuksekligi;
            double OlcekX = 1.5;
            int X_kaydirma = 10;
            for (int x = 0; x <= 255; x++)
            {
                if (x % 50 == 0)
                    CizimAlani.DrawLine(Kalem2, (int)(X_kaydirma + x * OlcekX),
                   GrafikYuksekligi, (int)(X_kaydirma + x * OlcekX), 0);
                CizimAlani.DrawLine(Kalem1, (int)(X_kaydirma + x * OlcekX), GrafikYuksekligi,
               (int)(X_kaydirma + x * OlcekX), (GrafikYuksekligi - (int)(DiziPikselSayilari[x] / OlcekY)));
                //Dikey kırmızı çizgiler.

            }
            textBox1.Text = "Maks.Piks=" + RenkMaksPikselSayisi.ToString();
        }

        private void kontrastAyarlamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Lütfen X ve Y değerlerini giriniz!!");
            }
            else
            {
                Color OkunanRenk, DonusenRenk;
                int R = 0, G = 0, B = 0;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

                int X1 = Convert.ToInt16(textBox1.Text);
                int X2 = Convert.ToInt16(textBox2.Text);
                int Y1 = Convert.ToInt16(textBox3.Text);
                int Y2 = Convert.ToInt16(textBox4.Text);
                int i = 0, j = 0; //Çıkış resminin x ve y si olacak.
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x, y);
                        R = OkunanRenk.R;
                        G = OkunanRenk.G;
                        B = OkunanRenk.B;
                        int Gri = (R + G + B) / 3;
                        int X = Gri;
                        int Y = (((X - X1) * (Y2 - Y1)) / (X2 - X1)) + Y1;
                        if (Y > 255) Y = 255;
                        if (Y < 0) Y = 0;
                        DonusenRenk = Color.FromArgb(Y, Y, Y);
                        CikisResmi.SetPixel(x, y, DonusenRenk);
                    }
                }
                pictureBox2.Refresh();
                pictureBox2.Image = null;
                pictureBox2.Image = CikisResmi;


            }
        }


        private void medyanOrtancaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 0;
            try
            {
                SablonBoyutu = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                SablonBoyutu = 3;
            }
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int[] R = new int[ElemanSayisi];
            int[] G = new int[ElemanSayisi];
            int[] B = new int[ElemanSayisi];
            int[] Gri = new int[ElemanSayisi];
            int x, y, i, j;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            R[k] = OkunanRenk.R;
                            G[k] = OkunanRenk.G;
                            B[k] = OkunanRenk.B;
                            Gri[k] = Convert.ToInt16(R[k] * 0.299 + G[k] * 0.587 + B[k] * 0.114); //Gri ton formülü
                            k++;
                        }
                    }
                    //Gri tona göre sıralama yapıyor. Aynı anda üç rengide değiştiriyor.
                    int GeciciSayi = 0;
                    for (i = 0; i < ElemanSayisi; i++)
                    {
                        for (j = i + 1; j < ElemanSayisi; j++)
                        {
                            if (Gri[j] < Gri[i])
                            {
                                GeciciSayi = Gri[i];
                                Gri[i] = Gri[j];
                                Gri[j] = GeciciSayi;
                                GeciciSayi = R[i];
                                R[i] = R[j];
                                R[j] = GeciciSayi;
                                GeciciSayi = G[i];
                                G[i] = G[j];
                                G[j] = GeciciSayi;
                                GeciciSayi = B[i];
                                B[i] = B[j];
                                B[j] = GeciciSayi;
                            }
                        }
                    }
                    //Sıralama sonrası ortadaki değeri çıkış resminin piksel değeri olarak atıyor.
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R[(ElemanSayisi - 1) / 2], G[(ElemanSayisi - 1) /
                   2], B[(ElemanSayisi - 1) / 2]));
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void eşiklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen X1 değerini giriniz!!");
            }
            else
            {

                int R = 0, G = 0, B = 0;
                Color OkunanRenk, DonusenRenk;
                Bitmap InputImage, OutputImage;
                InputImage = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = InputImage.Width;
                int ResimYuksekligi = InputImage.Height;
                OutputImage = new Bitmap(ResimGenisligi, ResimYuksekligi);

                int EsiklemeDegeri = Convert.ToInt32(textBox1.Text);
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = InputImage.GetPixel(x, y);
                        if (OkunanRenk.R >= EsiklemeDegeri)
                            R = 255;
                        else
                            R = 0;
                        if (OkunanRenk.G >= EsiklemeDegeri)
                            G = 255;
                        else
                            G = 0;
                        if (OkunanRenk.B >= EsiklemeDegeri)
                            B = 255;
                        else
                            B = 0;
                        DonusenRenk = Color.FromArgb(R, G, B);
                        OutputImage.SetPixel(x, y, DonusenRenk);
                    }
                }
                pictureBox2.Image = OutputImage;

            }
        }

        private void aynalamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen X1 değerini giriniz!!");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                double Aci = Convert.ToDouble(textBox1.Text);
                double RadyanAci = Aci * 2 * Math.PI / 360;
                double x2 = 0, y2 = 0;
                //Resim merkezini buluyor. Resim merkezi etrafında döndürecek.
                int x0 = ResimGenisligi / 2;
                int y0 = ResimYuksekligi / 2;
                for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                {
                    for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        double Delta = (x1 - x0) * Math.Sin(RadyanAci) - (y1 - y0) * Math.Cos(RadyanAci);
                        x2 = Convert.ToInt16(x1 + 2 * Delta * (-Math.Sin(RadyanAci)));
                        y2 = Convert.ToInt16(y1 + 2 * Delta * (Math.Cos(RadyanAci)));
                        if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                            CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }


        }

        private void uzaklaştırmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int x2 = 0, y2 = 0; //Çıkış resminin x ve y si olacak.
            int KucultmeKatsayisi = 2;
            for (int x1 = 0; x1 < ResimGenisligi; x1 = x1 + KucultmeKatsayisi)
            {
                y2 = 0;
                for (int y1 = 0; y1 < ResimYuksekligi; y1 = y1 + KucultmeKatsayisi)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);

                    CikisResmi.SetPixel(x2, y2, OkunanRenk);
                    y2++;
                }
                x2++;
            }
            pictureBox2.Image = CikisResmi;
        }

        private void yakınlaştırmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen X1 değerini giriniz!!");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int BuyutmeKatsayisi = Convert.ToInt32(textBox1.Text);

                int x2 = 0, y2 = 0;
                for (int x1 = 0; x1 < ResimGenisligi; x1++)
                {

                    for (int y1 = 0; y1 < ResimYuksekligi; y1++)
                    {

                        OkunanRenk = GirisResmi.GetPixel(x1, y1);

                        for (int i = 0; i < BuyutmeKatsayisi; i++)
                        {
                            for (int j = 0; j < BuyutmeKatsayisi; j++)
                            {
                                x2 = x1 * BuyutmeKatsayisi + i;
                                y2 = y1 * BuyutmeKatsayisi + j;
                                if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                                    CikisResmi.SetPixel(x2, y2, OkunanRenk);
                            }
                        }

                    }

                }
                pictureBox2.Image = CikisResmi;
            }

        }

        private void açılıDöndürmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen X1 değerini giriniz!!");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int Aci = Convert.ToInt16(textBox1.Text);
                double RadyanAci = Aci * 2 * Math.PI / 360;
                double x2 = 0, y2 = 0;
                //Resim merkezini buluyor. Resim merkezi etrafında döndürecek.
                int x0 = ResimGenisligi / 2;
                int y0 = ResimYuksekligi / 2;
                for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                {
                    for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        //Döndürme Formülleri
                        x2 = Math.Cos(RadyanAci) * (x1 - x0) - Math.Sin(RadyanAci) * (y1 - y0) + x0;
                        y2 = Math.Sin(RadyanAci) * (x1 - x0) + Math.Cos(RadyanAci) * (y1 - y0) + y0;
                        if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                            CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }

        }

        private void konvolüsyonİleNetleştirmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j, toplamR, toplamG, toplamB;
            int R, G, B;
            int[] Matris = { 0, -2, 0, -2, 11, -2, 0, -2, 0 };
            int MatrisToplami = 0 + -2 + 0 + -2 + 11 + -2 + 0 + -2 + 0;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;

                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R * Matris[k];
                            toplamG = toplamG + OkunanRenk.G * Matris[k];
                            toplamB = toplamB + OkunanRenk.B * Matris[k];
                            k++;
                        }
                    }
                    R = toplamR / MatrisToplami;
                    G = toplamG / MatrisToplami;
                    B = toplamB / MatrisToplami;
                    //===========================================================
                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    //===========================================================
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void prewittFiltresiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y;
            Color Renk;
            int P1, P2, P3, P4, P5, P6, P7, P8, P9;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    Renk = GirisResmi.GetPixel(x - 1, y - 1);
                    P1 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y - 1);
                    P2 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y - 1);
                    P3 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y);
                    P4 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y);
                    P5 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y);
                    P6 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y + 1);
                    P7 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y + 1);
                    P8 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y + 1);
                    P9 = (Renk.R + Renk.G + Renk.B) / 3;
                    int Gx = Math.Abs(-P1 + P3 - P4 + P6 - P7 + P9); //Dikey çizgileri Bulur
                    int Gy = Math.Abs(P1 + P2 + P3 - P7 - P8 - P9); //Yatay Çizgileri Bulur.
                    int PrewittDegeri = 0;
                    PrewittDegeri = Gx;
                    PrewittDegeri = Gy;
                    PrewittDegeri = Gx + Gy;
                    if (PrewittDegeri > 255) PrewittDegeri = 255;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(PrewittDegeri, PrewittDegeri, PrewittDegeri));
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void meanFiltresiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen X1 değerini giriniz!!");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int SablonBoyutu = Convert.ToInt32(textBox1.Text);
                int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
                for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
                {
                    for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                    {
                        toplamR = 0;
                        toplamG = 0;
                        toplamB = 0;
                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                toplamR = toplamR + OkunanRenk.R;
                                toplamG = toplamG + OkunanRenk.G;
                                toplamB = toplamB + OkunanRenk.B;
                            }
                        }
                        ortalamaR = toplamR / (SablonBoyutu * SablonBoyutu);
                        ortalamaG = toplamG / (SablonBoyutu * SablonBoyutu);
                        ortalamaB = toplamB / (SablonBoyutu * SablonBoyutu);
                        CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void medyanFiltresiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 7;
            try
            {
                SablonBoyutu = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                SablonBoyutu = 7;
            }
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int[] R = new int[ElemanSayisi];
            int[] G = new int[ElemanSayisi];
            int[] B = new int[ElemanSayisi];
            int[] Gri = new int[ElemanSayisi];
            int x, y, i, j;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            R[k] = OkunanRenk.R;
                            G[k] = OkunanRenk.G;
                            B[k] = OkunanRenk.B;
                            Gri[k] = Convert.ToInt16(R[k] * 0.299 + G[k] * 0.587 + B[k] * 0.114); //Gri ton formülü
                            k++;
                        }
                    }
                    //Gri tona göre sıralama yapıyor. Aynı anda üç rengide değiştiriyor.
                    int GeciciSayi = 0;
                    for (i = 0; i < ElemanSayisi; i++)
                    {
                        for (j = i + 1; j < ElemanSayisi; j++)
                        {
                            if (Gri[j] < Gri[i])
                            {
                                GeciciSayi = Gri[i];
                                Gri[i] = Gri[j];
                                Gri[j] = GeciciSayi;
                                GeciciSayi = R[i];
                                R[i] = R[j];
                                R[j] = GeciciSayi;
                                GeciciSayi = G[i];
                                G[i] = G[j];
                                G[j] = GeciciSayi;
                                GeciciSayi = B[i];
                                B[i] = B[j];
                                B[j] = GeciciSayi;
                            }
                        }
                    }
                    //Sıralama sonrası ortadaki değeri çıkış resminin piksel değeri olarak atıyor.
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R[(ElemanSayisi - 1) / 2], G[(ElemanSayisi - 1) /
                   2], B[(ElemanSayisi - 1) / 2]));
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void gaussFiltresiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 7; //Çekirdek matrisin boyutu
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
            int[] Matris = { 0, 0, 1, 2, 1, 0, 0, 0, 3, 13, 22, 13, 3, 0, 1, 13, 59, 97, 59, 13, 1, 2, 22, 97, 160, 97, 22, 2, 1, 13, 59, 97, 59, 13, 1, 0, 3, 13, 22, 13, 3, 0, 0, 0, 1, 2, 1, 0, 0 };
            int MatrisToplami = 0 + 0 + 1 + 2 + 1 + 0 + 0 + 0 + 3 + 13 + 22 + 13 + 3 + 0 + 1 + 13 + 59 + 97 + 59 + 13 + 1 + 2 + 22 + 97 + 160 + 97 + 22 + 2 + 1 + 13 + 59 + 97 + 59 + 13 + 1 + 0 + 3 + 13 + 22 + 13 + 3 + 0 + 0 + 0 + 1 + 2 + 1 + 0 + 0;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0; //matris içindeki elemanları sırayla okurken kullanılacak.
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R * Matris[k];
                            toplamG = toplamG + OkunanRenk.G * Matris[k];
                            toplamB = toplamB + OkunanRenk.B * Matris[k];
                            k++;
                        }
                    }
                    ortalamaR = toplamR / MatrisToplami;
                    ortalamaG = toplamG / MatrisToplami;
                    ortalamaB = toplamB / MatrisToplami;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void sobelFiltresiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap GirisResmi, CikisResmiXY, CikisResmiX, CikisResmiY;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmiX = new Bitmap(ResimGenisligi, ResimYuksekligi);
            CikisResmiY = new Bitmap(ResimGenisligi, ResimYuksekligi);
            CikisResmiXY = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y;
            Color Renk;
            int P1, P2, P3, P4, P5, P6, P7, P8, P9;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    Renk = GirisResmi.GetPixel(x - 1, y - 1);
                    P1 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y - 1);
                    P2 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y - 1);
                    P3 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y);
                    P4 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y);
                    P5 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y);
                    P6 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y + 1);
                    P7 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y + 1);
                    P8 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y + 1);
                    P9 = (Renk.R + Renk.G + Renk.B) / 3;
                    //Hesaplamayı yapan Sobel Temsili matrisi ve formülü.
                    int Gx = Math.Abs(-P1 + P3 - 2 * P4 + 2 * P6 - P7 + P9); //Dikey çizgiler
                    int Gy = Math.Abs(P1 + 2 * P2 + P3 - P7 - 2 * P8 - P9);
                    int Gxy = Gx + Gy;
                    if (Gx > 255) Gx = 255;
                    if (Gy > 255) Gy = 255;
                    if (Gxy > 255) Gxy = 255;
                    CikisResmiX.SetPixel(x, y, Color.FromArgb(Gx, Gx, Gx));
                    CikisResmiY.SetPixel(x, y, Color.FromArgb(Gy, Gy, Gy));
                    CikisResmiXY.SetPixel(x, y, Color.FromArgb(Gxy, Gxy, Gxy));
                }
            }
            pictureBox2.Image = CikisResmiX;

        }

        private void kontrastArttırmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen X1 değerini giriniz!!");
            }
            else
            {
                int R = 0, G = 0, B = 0;
                Color OkunanRenk, DonusenRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width; //GirisResmi global tanımlandı.
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                double C_KontrastSeviyesi = Convert.ToInt32(textBox1.Text);
                double F_KontrastFaktoru = (259 * (C_KontrastSeviyesi + 255)) / (255 * (259 -
                C_KontrastSeviyesi));
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x, y);
                        R = OkunanRenk.R;
                        G = OkunanRenk.G;
                        B = OkunanRenk.B;
                        R = (int)((F_KontrastFaktoru * (R - 128)) + 128);
                        G = (int)((F_KontrastFaktoru * (G - 128)) + 128);
                        B = (int)((F_KontrastFaktoru * (B - 128)) + 128);
                        //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                        DonusenRenk = Color.FromArgb(R, G, B);
                        CikisResmi.SetPixel(x, y, DonusenRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }

        }

        private void eğmeKaydırmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            //Taşıma mesafelerini atıyor. 
            double EgmeKatsayisi = 0.2;
            double x2 = 0, y2 = 0;
            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);
                    // +X ekseni yönünde
                    //x2 = x1 + EgmeKatsayisi * y1;
                    //y2 = y1;
                    // -X ekseni yönünde
                    //x2 = x1 - EgmeKatsayisi * y1;
                    //y2 = y1;
                    // +Y ekseni yönünde
                    //x2 = x1;
                    //y2 = EgmeKatsayisi * x1 + y1;
                    // -Y ekseni yönünde
                    x2 = x1;
                    y2 = -EgmeKatsayisi * x1 + y1;

                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void histogramEşitlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            uint pixels = (uint)GirisResmi.Height * (uint)GirisResmi.Width;
            decimal Const = 255 / (decimal)pixels;

            int x, y, R, G, B;

            int[] HistogramRed2 = new int[256];
            int[] HistogramGreen2 = new int[256];
            int[] HistogramBlue2 = new int[256];


            for (var i = 0; i < GirisResmi.Width; i++)
            {
                for (var j = 0; j < GirisResmi.Height; j++)
                {
                    var piksel = GirisResmi.GetPixel(i, j);

                    HistogramRed2[(int)piksel.R]++;
                    HistogramGreen2[(int)piksel.G]++;
                    HistogramBlue2[(int)piksel.B]++;

                }
            }

            int[] cdfR = HistogramRed2;
            int[] cdfG = HistogramGreen2;
            int[] cdfB = HistogramBlue2;

            for (int r = 1; r <= 255; r++)
            {
                cdfR[r] = cdfR[r] + cdfR[r - 1];
                cdfG[r] = cdfG[r] + cdfG[r - 1];
                cdfB[r] = cdfB[r] + cdfB[r - 1];
            }

            for (y = 0; y < GirisResmi.Height; y++)
            {
                for (x = 0; x < GirisResmi.Width; x++)
                {
                    Color pixelColor = GirisResmi.GetPixel(x, y);

                    R = (int)((decimal)cdfR[pixelColor.R] * Const);
                    G = (int)((decimal)cdfG[pixelColor.G] * Const);
                    B = (int)((decimal)cdfB[pixelColor.B] * Const);

                    Color newColor = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, newColor);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void kontrastGermeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Lütfen X ve Y değerlerini giriniz!!");
            }
            else
            {
                Color OkunanRenk, DonusenRenk;
                int R = 0, G = 0, B = 0;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                int X1 = Convert.ToInt16(textBox1.Text);
                int X2 = Convert.ToInt16(textBox2.Text);
                int Y1 = Convert.ToInt16(textBox3.Text);
                int Y2 = Convert.ToInt16(textBox4.Text);
                //int i = 0, j = 0; //Çıkış resminin x ve y si olacak.
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    for (int y = 0; y < ResimYuksekligi; y++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x, y);
                        R = OkunanRenk.R;
                        G = OkunanRenk.G;
                        B = OkunanRenk.B;
                        int Gri = (R + G + B) / 3;
                        int X = Gri;
                        int Y = ((((X - X1) * Y2 - Y1)) / (X2 - X1)) + Y1;
                        if (Y > 255) Y = 255;
                        if (Y < 0) Y = 0;
                        DonusenRenk = Color.FromArgb(Y, Y, Y);
                        CikisResmi.SetPixel(x, y, DonusenRenk);
                    }
                }
                pictureBox2.Refresh();
                pictureBox2.Image = null;
                pictureBox2.Image = CikisResmi;
            }
        }

        private void laplacianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Image);
            Bitmap image = new Bitmap(img);
            for (int x = 1; x < image.Width - 1; x++)
            {
                for (int y = 1; y < image.Height - 1; y++)
                {
                    Color color2, color4, color5, color6, color8;
                    color2 = image.GetPixel(x, y - 1);
                    color4 = image.GetPixel(x - 1, y);
                    color5 = image.GetPixel(x, y);
                    color6 = image.GetPixel(x + 1, y);
                    color8 = image.GetPixel(x, y + 1);
                    int r = color2.R + color4.R + color5.R * (-4) + color6.R + color8.R;
                    int g = color2.G + color4.G + color5.G * (-4) + color6.G + color8.G;
                    int b = color2.B + color4.B + color5.B * (-4) + color6.B + color8.B;
                    int avg = (r + g + b) / 3;
                    if (avg > 255) avg = 255;
                    if (avg < 0) avg = 0;
                    image.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                }
            }
            pictureBox2.Image = image;
        }

        private void ötelemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen X1 ve Y1 değerlerini giriniz!!");
            }
            else
            {
                Color OkunanRenk;
                Bitmap GirisResmi, CikisResmi;
                GirisResmi = new Bitmap(pictureBox1.Image);
                int ResimGenisligi = GirisResmi.Width;
                int ResimYuksekligi = GirisResmi.Height;
                CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
                double x2 = 0, y2 = 0;

                //öteleme değeri gir

                int Tx = Convert.ToInt16(textBox1.Text);
                int Ty = Convert.ToInt16(textBox2.Text);

                for (int x1 = 0; x1 < (ResimGenisligi); x1++)
                {
                    for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                    {
                        OkunanRenk = GirisResmi.GetPixel(x1, y1);
                        x2 = x1 + Tx;
                        y2 = y1 + Ty;
                        if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                            CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                    }
                }
                pictureBox2.Image = CikisResmi;
            }
        }

        private void filtrelerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gradyentİleKenarKeskinleştirmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int x, y;
            Color Renk;
            int P1, P2, P3, P4, P5, P6, P7, P8, P9;
            int GM, G;
            int[,] Matris = { { -1, 0, 1, -1, 0, 1, -1, 0, 1 }, { 0, 1, 1, -1, 0, 1, -1, -1, 0 }, { 1,
            1, 1, 0, 0, 0, -1, -1, -1 }, { 1, 1, 0, 1, 0, -1, 0, -1, -1 }, { 1, 0, -1, 1, 0, -1, 1, 0, -1 },
            { 0, -1, -1, 1, 0, -1, 1, 1, 0 }, {-1, -1, -1, 0, 0, 0, 1, 1, 1 }, {-1, -1, 0, -1, 0, 1, 0, 1, 1
            } };
            for (x = 1; x < ResimGenisligi - 1; x++)
            {
                for (y = 1; y < ResimYuksekligi - 1; y++)
                {
                    Renk = GirisResmi.GetPixel(x - 1, y - 1);
                    P1 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y - 1);
                    P2 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y - 1);
                    P3 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y);
                    P4 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y);
                    P5 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y);
                    P6 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x - 1, y + 1);
                    P7 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x, y + 1);
                    P8 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = GirisResmi.GetPixel(x + 1, y + 1);
                    P9 = (Renk.R + Renk.G + Renk.B) / 3;
                    GM = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        G = Math.Abs(P1 * Matris[i, 0] + P2 * Matris[i, 1] + P3 * Matris[i, 2] + P4 *
                       Matris[i, 3] + P5 * Matris[i, 4] + P6 * Matris[i, 5] + P7 * Matris[i, 6] + P8 * Matris[i, 7] +
                       P9 * Matris[i, 8]);
                        if (G > GM) GM = G;
                    }
                    if (GM > 50)
                        CikisResmi.SetPixel(x, y, Color.White);
                    else
                        CikisResmi.SetPixel(x, y, Color.Black);
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void perspektifDüzeltmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (textBox16.Text == "")
            //{
                //MessageBox.Show("Lütfen tüm X ve Y değerlerini giriniz!!");
            //}
            //else
            //{

            double x1 = Convert.ToDouble(textBox1.Text);
            double y1 = Convert.ToDouble(textBox2.Text);
            double x2 = Convert.ToDouble(textBox3.Text);
            double y2 = Convert.ToDouble(textBox4.Text);
            double x3 = Convert.ToDouble(textBox5.Text);
            double y3 = Convert.ToDouble(textBox6.Text);
            double x4 = Convert.ToDouble(textBox7.Text);
            double y4 = Convert.ToDouble(textBox8.Text);
            double X1 = Convert.ToDouble(textBox9.Text);
            double Y1 = Convert.ToDouble(textBox10.Text);
            double X2 = Convert.ToDouble(textBox11.Text);
            double Y2 = Convert.ToDouble(textBox12.Text);
            double X3 = Convert.ToDouble(textBox13.Text);
            double Y3 = Convert.ToDouble(textBox14.Text);
            double X4 = Convert.ToDouble(textBox15.Text);
            double Y4 = Convert.ToDouble(textBox16.Text);

            double[,] GirisMatrisi = new double[8, 8];
            // { x1, y1, 1, 0, 0, 0, -x1 * X1, -y1 * X1 }
            GirisMatrisi[0, 0] = x1;
            GirisMatrisi[0, 1] = y1;
            GirisMatrisi[0, 2] = 1;
            GirisMatrisi[0, 3] = 0;
            GirisMatrisi[0, 4] = 0;
            GirisMatrisi[0, 5] = 0;
            GirisMatrisi[0, 6] = -x1 * X1;
            GirisMatrisi[0, 7] = -y1 * X1;
            //{ 0, 0, 0, x1, y1, 1, -x1 * Y1, -y1 * Y1 }
            GirisMatrisi[1, 0] = 0;
            GirisMatrisi[1, 1] = 0;
            GirisMatrisi[1, 2] = 0;
            GirisMatrisi[1, 3] = x1;
            GirisMatrisi[1, 4] = y1;
            GirisMatrisi[1, 5] = 1;
            GirisMatrisi[1, 6] = -x1 * Y1;
            GirisMatrisi[1, 7] = -y1 * Y1;
            //{ x2, y2, 1, 0, 0, 0, -x2 * X2, -y2 * X2 } 
            GirisMatrisi[2, 0] = x2;
            GirisMatrisi[2, 1] = y2;
            GirisMatrisi[2, 2] = 1;
            GirisMatrisi[2, 3] = 0;
            GirisMatrisi[2, 4] = 0;
            GirisMatrisi[2, 5] = 0;
            GirisMatrisi[2, 6] = -x2 * X2;
            GirisMatrisi[2, 7] = -y2 * X2;
            //{ 0, 0, 0, x2, y2, 1, -x2 * Y2, -y2 * Y2 }
            GirisMatrisi[3, 0] = 0;
            GirisMatrisi[3, 1] = 0;
            GirisMatrisi[3, 2] = 0;
            GirisMatrisi[3, 3] = x2;
            GirisMatrisi[3, 4] = y2;
            GirisMatrisi[3, 5] = 1;
            GirisMatrisi[3, 6] = -x2 * Y2;
            GirisMatrisi[3, 7] = -y2 * Y2;
            //{ x3, y3, 1, 0, 0, 0, -x3 * X3, -y3 * X3 }
            GirisMatrisi[4, 0] = x3;
            GirisMatrisi[4, 1] = y3;
            GirisMatrisi[4, 2] = 1;
            GirisMatrisi[4, 3] = 0;
            GirisMatrisi[4, 4] = 0;
            GirisMatrisi[4, 5] = 0;
            GirisMatrisi[4, 6] = -x3 * X3;
            GirisMatrisi[4, 7] = -y3 * X3;
            //{ 0, 0, 0, x3, y3, 1, -x3 * Y3, -y3 * Y3 }
            GirisMatrisi[5, 0] = 0;
            GirisMatrisi[5, 1] = 0;
            GirisMatrisi[5, 2] = 0;
            GirisMatrisi[5, 3] = x3;
            GirisMatrisi[5, 4] = y3;
            GirisMatrisi[5, 5] = 1;
            GirisMatrisi[5, 6] = -x3 * Y3;
            GirisMatrisi[5, 7] = -y3 * Y3;
            //{ x4, y4, 1, 0, 0, 0, -x4 * X4, -y4 * X4 }
            GirisMatrisi[6, 0] = x4;
            GirisMatrisi[6, 1] = y4;
            GirisMatrisi[6, 2] = 1;
            GirisMatrisi[6, 3] = 0;
            GirisMatrisi[6, 4] = 0;
            GirisMatrisi[6, 5] = 0;
            GirisMatrisi[6, 6] = -x4 * X4;
            GirisMatrisi[6, 7] = -y4 * X4;
            //{ 0, 0, 0, x4, y4, 1, -x4 * Y4, -y4 * Y4 } 
            GirisMatrisi[7, 0] = 0;
            GirisMatrisi[7, 1] = 0;
            GirisMatrisi[7, 2] = 0;
            GirisMatrisi[7, 3] = x4;
            GirisMatrisi[7, 4] = y4;
            GirisMatrisi[7, 5] = 1;
            GirisMatrisi[7, 6] = -x4 * Y4;
            GirisMatrisi[7, 7] = -y4 * Y4;
            //---------------------------------------------------------------------------
            double[,] matrisBTersi = MatrisTersiniAl(GirisMatrisi);
            //----------------------------------- A Dönüşüm Matrisi (3x3) ---------------

            double a00 = 0, a01 = 0, a02 = 0, a10 = 0, a11 = 0, a12 = 0, a20 = 0, a21 = 0, a22 = 0;
            a00 = matrisBTersi[0, 0] * X1 + matrisBTersi[0, 1] * Y1 + matrisBTersi[0, 2] *
            X2 + matrisBTersi[0, 3] * Y2 + matrisBTersi[0, 4] * X3 + matrisBTersi[0, 5] * Y3 +
            matrisBTersi[0, 6] * X4 + matrisBTersi[0, 7] * Y4;
            a01 = matrisBTersi[1, 0] * X1 + matrisBTersi[1, 1] * Y1 + matrisBTersi[1, 2] *
            X2 + matrisBTersi[1, 3] * Y2 + matrisBTersi[1, 4] * X3 + matrisBTersi[1, 5] * Y3 +
            matrisBTersi[1, 6] * X4 + matrisBTersi[1, 7] * Y4;
            a02 = matrisBTersi[2, 0] * X1 + matrisBTersi[2, 1] * Y1 + matrisBTersi[2, 2] *
            X2 + matrisBTersi[2, 3] * Y2 + matrisBTersi[2, 4] * X3 + matrisBTersi[2, 5] * Y3 +
            matrisBTersi[2, 6] * X4 + matrisBTersi[2, 7] * Y4;
            a10 = matrisBTersi[3, 0] * X1 + matrisBTersi[3, 1] * Y1 + matrisBTersi[3, 2] *
            X2 + matrisBTersi[3, 3] * Y2 + matrisBTersi[3, 4] * X3 + matrisBTersi[3, 5] * Y3 +
            matrisBTersi[3, 6] * X4 + matrisBTersi[3, 7] * Y4;
            a11 = matrisBTersi[4, 0] * X1 + matrisBTersi[4, 1] * Y1 + matrisBTersi[4, 2] *
            X2 + matrisBTersi[4, 3] * Y2 + matrisBTersi[4, 4] * X3 + matrisBTersi[4, 5] * Y3 +
            matrisBTersi[4, 6] * X4 + matrisBTersi[4, 7] * Y4;
            a12 = matrisBTersi[5, 0] * X1 + matrisBTersi[5, 1] * Y1 + matrisBTersi[5, 2] *
            X2 + matrisBTersi[5, 3] * Y2 + matrisBTersi[5, 4] * X3 + matrisBTersi[5, 5] * Y3 +
            matrisBTersi[5, 6] * X4 + matrisBTersi[5, 7] * Y4;
            a20 = matrisBTersi[6, 0] * X1 + matrisBTersi[6, 1] * Y1 + matrisBTersi[6, 2] *
            X2 + matrisBTersi[6, 3] * Y2 + matrisBTersi[6, 4] * X3 + matrisBTersi[6, 5] * Y3 +
            matrisBTersi[6, 6] * X4 + matrisBTersi[6, 7] * Y4;
            a21 = matrisBTersi[7, 0] * X1 + matrisBTersi[7, 1] * Y1 + matrisBTersi[7, 2] *
            X2 + matrisBTersi[7, 3] * Y2 + matrisBTersi[7, 4] * X3 + matrisBTersi[7, 5] * Y3 +
            matrisBTersi[7, 6] * X4 + matrisBTersi[7, 7] * Y4;
            a22 = 1;
            PerspektifDuzelt(a00, a01, a02, a10, a11, a12, a20, a21, a22);
        }

        private void PerspektifDuzelt(double a00, double a01, double a02, double a10, double a11, double a12, double a20, double a21, double a22)
        {
            Bitmap GirisResmi, CikisResmi;
            Color OkunanRenk;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            double X, Y, z;
            for (int x = 0; x < (ResimGenisligi); x++)
            {
                for (int y = 0; y < (ResimYuksekligi); y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    z = a20 * x + a21 * y + 1;
                    X = (a00 * x + a01 * y + a02) / z;
                    Y = (a10 * x + a11 * y + a12) / z;
                    if (X > 0 && X < ResimGenisligi && Y > 0 && Y < ResimYuksekligi)
                        //Picturebox ın dışına çıkan kısımlar oluşturulmayacak.
                        CikisResmi.SetPixel((int)X, (int)Y, OkunanRenk);
                }
            }
            pictureBox2.Image = CikisResmi;

        }
        public double[,] MatrisTersiniAl(double[,] GirisMatrisi)
        {
            int MatrisBoyutu = Convert.ToInt16(Math.Sqrt(GirisMatrisi.Length));

            double[,] CikisMatrisi = new double[MatrisBoyutu, MatrisBoyutu];
            for (int i = 0; i < MatrisBoyutu; i++)
            {
                for (int j = 0; j < MatrisBoyutu; j++)
                {
                    if (i == j)
                        CikisMatrisi[i, j] = 1;
                    else
                        CikisMatrisi[i, j] = 0;
                }
            }
            double d, k;
            for (int i = 0; i < MatrisBoyutu; i++)
            {
                d = GirisMatrisi[i, i];
                for (int j = 0; j < MatrisBoyutu; j++)
                {
                    if (d == 0)
                    {
                        d = 0.0001;
                    }
                    GirisMatrisi[i, j] = GirisMatrisi[i, j] / d;
                    CikisMatrisi[i, j] = CikisMatrisi[i, j] / d;
                }
                for (int x = 0; x < MatrisBoyutu; x++)
                {
                    if (x != i)
                    {
                        k = GirisMatrisi[x, i];
                        for (int j = 0; j < MatrisBoyutu; j++)
                        {
                            GirisMatrisi[x, j] = GirisMatrisi[x, j] - GirisMatrisi[i, j] * k;
                            CikisMatrisi[x, j] = CikisMatrisi[x, j] - CikisMatrisi[i, j] * k;
                        }
                    }
                }
            }
            return CikisMatrisi;

        }

        private void tersiniAlmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            double Aci = 180;
            double RadyanAci = Aci * 2 * Math.PI / 360;
            double x2 = 0, y2 = 0;
            //Resim merkezini buluyor. Resim merkezi etrafında döndürecek.
            int x0 = ResimGenisligi / 2;
            int y0 = ResimYuksekligi / 2;
            for (int x1 = 0; x1 < (ResimGenisligi); x1++)
            {
                for (int y1 = 0; y1 < (ResimYuksekligi); y1++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x1, y1);

                    double Delta = (x1 - x0) * Math.Sin(RadyanAci) - (y1 - y0) * Math.Cos(RadyanAci);
                    x2 = Convert.ToInt16(x1 + 2 * Delta * (-Math.Sin(RadyanAci)));
                    y2 = Convert.ToInt16(y1 + 2 * Delta * (Math.Cos(RadyanAci)));
                    if (x2 > 0 && x2 < ResimGenisligi && y2 > 0 && y2 < ResimYuksekligi)
                        CikisResmi.SetPixel((int)x2, (int)y2, OkunanRenk);
                }
            }

            pictureBox2.Image = CikisResmi;
        }

        private void aşındırmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            //  eşiklenmiş Siyah Beyaz resim olmalı.
            Bitmap SiyahBeyazResim = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = SiyahBeyazResim.Width;
            int ResimYuksekligi = SiyahBeyazResim.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            GirisResmi = SiyahBeyazResim;
            int x, y, i, j;
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int Esikleme = 0;
            try
            {
                Esikleme = Convert.ToInt16(textBox1.Text);
            }
            catch
            {
                Esikleme = 128;
            }
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    OkunanRenk = GirisResmi.GetPixel(x, y);
                    if (OkunanRenk.R > Esikleme) //Rengin Beyaz, Komşularında da siyaha varsa kendi rengini siyah yapacak.
                    {
                        bool KomsulardaSiyahVarMi = false;
                        //Komşuların içerisinde siyah var mı yok mu, o.
                        for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                                if (OkunanRenk.R < Esikleme) //Komşularda Siyah Var ise
                                    KomsulardaSiyahVarMi = true;
                            }
                        }
                        //Komşularda Beyaz var ise, Kendi rengimizi Beyaz yapalım.
                        if (KomsulardaSiyahVarMi == true) //Komşularda Beyaz varsa
                        {
                            Color KendiRengi = GirisResmi.GetPixel(x, y);
                            CikisResmi.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                        else //komşularda siyah yok ise kendi rengin yine beyaz kalmalı.
                            CikisResmi.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            pictureBox2.Image = CikisResmi;
        }

        private void yaymaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color OkunanRenk;

            Bitmap GirisResmi, CikisResmi;

            Bitmap OrjinalResim, GenislemisResim;

            GirisResmi = new Bitmap(pictureBox1.Image);



            int ResimGenisligi = GirisResmi.Width;

            int ResimYuksekligi = GirisResmi.Height;

            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);



            int x, y, i, j;

            int SablonBoyutu = 3;

            int ElemanSayisi = SablonBoyutu * SablonBoyutu;

            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) //Re taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.

            {

                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)

                {

                    bool RenkSiyah = false;

                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)

                    {

                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)

                        {

                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);

                            if (OkunanRenk.R < 128) //Siyah ise

                                RenkSiyah = true;

                        }

                    }

                    if (RenkSiyah == true) //Komşularda siyah varsa

                    {

                        Color KendiRengi = GirisResmi.GetPixel(x, y);

                        if (KendiRengi.R > 128) //kendi rengin beyaz ise onu da siyah yap.

                            CikisResmi.SetPixel(x, y, Color.FromArgb(255, 0, 0)); //siyah yerine kırmı kullandık.Genişleyen bölgeyi görmek için

                    }

                    else //komşularda siyah yok ise kendi rengi yine aynı beyaz kalmalı.

                        CikisResmi.SetPixel(x, y, Color.FromArgb(255, 255, 255));

                }

            }

            pictureBox2.Image = CikisResmi;
        }
    } 
}
