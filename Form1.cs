using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //--------------MACED HALLAK-------------------
        //---------------190508659-----------------

        //----ikili ağaç arama classı oluşturma----- 
        public class BSTree
        {
            public int deger;
            public BSTree sag;
            public BSTree sol;
        }
        public BSTree kok;
        public int dugumsayi;

        private void button1_Click(object sender, EventArgs e)
        {
            BSTree yeni = new BSTree();
            yeni.deger = Convert.ToInt32(textBox1.Text);

            if (kok == null)
            {
                kok = yeni;
            }
            else
            {
                Ekleme(kok,yeni.deger);
            }
            dugumsayi++;
            label1.Text ="["+ Convert.ToString(yeni.deger) + "] Düğüm Eklendi";

        }
        //-------dugum eklemek icin------------
        public void Ekleme(BSTree dugum, int sayi)
        {
            BSTree yeni = new BSTree();
            yeni.deger = sayi;

            if (sayi > dugum.deger)
            {
                if (dugum.sag == null)
                {
                    dugum.sag = yeni;
                }
                else
                {
                    Ekleme(dugum.sag, sayi);
                }

            }
            else
            {
                if (dugum.sol == null)
                {
                    dugum.sol = yeni;
                }
                else
                {
                    Ekleme(dugum.sol, sayi);
                }

            }

        }
        //----------duğum hangi düzeyde olduğunu gösteriliyor-----
        public int duzey;
        private void button2_Click(object sender, EventArgs e)
        {
            BSTree yeni = new BSTree();
            int sayi = Convert.ToInt32(textBox2.Text);
            int x = bul(sayi);
            if (x == 0)
            {
                label2.Text = "Ağaç Boş";
            }
            else if (x == -1)
            {
                label2.Text = "["+sayi + "] Bu Sayı \n Ağaçtaki Bulunmadı";
            }
            else
            {
                label2.Text = "Düğüm " + Convert.ToString(x) + ". Düzeyinde";
            }



        }
        //----------bu metod hangi düzeyde olduğunu döndürüyor-------
        public int bul(int sayi)
        {

            return buluyor(kok, sayi, duzey);

        }

        public int buluyor(BSTree dugum, int sayi, int duzey)
        {
            
            if (dugum == null || dugum == null)//aranan sayi ağaçtaki yok ise 
            {
                return -1;
            }
            else
            {
                if (kok == null)
                {
                    return 0;
                }
                else if (sayi > dugum.deger)
                {
                    return buluyor(dugum.sag, sayi, ++duzey);

                }
                else if (sayi < dugum.deger)
                {
                    return buluyor(dugum.sol, sayi, ++duzey);
                }
                else
                {

                    return ++duzey;
                }
            }
        }
//-------------------ağaç bilgileri------------
        private void button5_Click(object sender, EventArgs e)
        {
            if (kok == null)
            {
                MessageBox.Show("Henüz bir düğüm eklenmedi!");
            }
            else
            {


                textBox4.Text = null;
                textBox5.Text = null;
                textBox6.Text = null;
                textBox7.Text = null;
                textBox8.Text = null;
                textBox9.Text = null;

                PreOrder(kok);
                InOrder(kok);
                PostOrder(kok);
                yaprak(kok);
                int yuks = yuksek(kok);
                textBox7.Text = Convert.ToString(dugumsayi);
                textBox9.Text = Convert.ToString(yuks - 1);
               
            }
        }

        //-----------pre-order---------------
        public void PreOrder(BSTree kokPreOr)
        {


            if (kokPreOr != null)
            {
                textBox4.Text += kokPreOr.deger + "→";
                PreOrder(kokPreOr.sol);
                PreOrder(kokPreOr.sag);
            }
        }
        //-----------in-order---------------
        public void InOrder(BSTree kokInOr)
        {

            if (kokInOr != null)
            {
                InOrder(kokInOr.sol);
                textBox5.Text += kokInOr.deger + "→";
                InOrder(kokInOr.sag);
            }
        }
        //-----------post-order---------------
        public void PostOrder(BSTree kokPostOr)
        {


            if (kokPostOr != null)
            {
                PostOrder(kokPostOr.sol);
                PostOrder(kokPostOr.sag);
                textBox6.Text += kokPostOr.deger + "→";
            }
        }


        //--------------------yukseklik------------------
        public int yuksek(BSTree kok)
        {
            if (kok == null)
            {
                return 0;
            }
            else
            {
                return max(yuksek(kok.sag), yuksek(kok.sol)) + 1;

            }

        }
        public int max(int x, int y)
        {

            if (x > y)
            {
                return x;
            }
            else
                return y;
        }

        //------------------------yaprak dugumler------------------------
        public BSTree yaprak(BSTree dugum)
        {
            if (dugum == null)
            {
                return null;
            }
            if (dugum.sol != null || dugum.sag != null)
            {
                dugum.sol = yaprak(dugum.sol);
                dugum.sag = yaprak(dugum.sag);
            }
            else
            {
                textBox8.Text += Convert.ToString(dugum.deger)+" ";
            }

            return dugum;
        }

        //-----------silme işlemleri------------------
        int sayibulundumu = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            sayibulundumu = 0;
            int SilenecekDugum = Convert.ToInt32(textBox3.Text);
                Delete(kok, SilenecekDugum);

                if (sayibulundumu == 0)
                {
                    label3.Text = " Sayi Ağaçtaki\n Bulunmadı";
                }
                else
                {
                    label3.Text = "["+SilenecekDugum + "] Düğüm Silindi";
                    --dugumsayi;
                }
            
        }
        public BSTree Delete(int sayi)
        {
            return Delete(kok, sayi);
        }

        //duğum silmek için ----------
       
        public BSTree Delete(BSTree kok1, int sayi)
        {

            if (kok1 == null)//kök boş ise
            {
               return null;
            }
            else if (sayi < kok1.deger)//kök silenecek sayidan büyük ise
            {
              
                kok1.sol = Delete(kok1.sol, sayi);
            }
            else if (sayi > kok1.deger)//kök silenecek sayidan küçük ise
            {
              
                kok1.sag = Delete(kok1.sag, sayi);
            }
            else
            {
                //******************silinecek sayi bulunduysa*******************************
                // cocugu yok ise
                if (kok1.sag == null && kok1.sol == null)
                {
                    sayibulundumu++;
                    return kok1 = null;
                }
                // bir cocuk ise
                else if (kok1.sol == null)//sol çocuk yok ise
                {
                    sayibulundumu++;
                    return kok1.sag;

                }
                else if (kok1.sag == null)//sağ çocuk yok ise
                {
                    sayibulundumu++;
                    return kok1.sol;

                }
                // iki cocuk ise--
                else
                {
                    sayibulundumu++;
                    kok1.deger = Minbul(kok1.sag);//minmum deger döndürüyor
                    kok1.sag = Delete(kok1.sag, kok1.deger);

                }
              
                
                
            }
            return kok1;
        }

        public int Minbul(BSTree dugum)
        {
            int minv = dugum.deger;
            while (dugum.sol != null)
            {
                minv = dugum.sol.deger;
                dugum = dugum.sol;
            }
            return minv;
           
        }


        //--------------ağaçtaki duğumleri yazdırma kısmı---------------------
        private void button4_Click(object sender, EventArgs e)
        {
            if (kok == null)
            {
                MessageBox.Show("Ağaç boş\nGösterilecek bir düğüm yok");
            }
            else
            {
                listBox1.Items.Clear();
                birDefaCalis = 0;
                print();
            }
        }

        public void print()
        {

            int h = yuksek(kok);
            int i;
            for (i = 1; i <= h; i++)
            {
                print(kok, i);
            }
        }

        public void print(BSTree x, int leve)
        {
            if (x == null)
            {
                return;

            }
            if (leve == 1)
            {
               
                listBox1.Items.Add(x.deger + konum(x.deger));
            }
            else if (leve > 1)
            {
                
                print(x.sol, leve - 1);
                print(x.sag, leve - 1);
            }     

        }

       //---------------bu metod duğumun ağaçtaki yeri belirtiyor--------    
        public string konum(int x)
        {
            return konum(kok, x,"");
        }
        int birDefaCalis = 0;
        public string konum(BSTree dugum, int c,string st)
        {

            if (dugum == null)
            {
                return null;
            }
            if (birDefaCalis == 0)
            {
                birDefaCalis++;
                return " .KÖK";
            }           
            else if (c == dugum.deger)
            {
                return " "+st;
            }
            else if (c < dugum.deger)
            {
               
                return konum(dugum.sol, c,st+=".SOL");
            }
            else

            {
                return konum(dugum.sag, c,st+=".SAĞ");
                   
            }
            
        }

//*******************************************************************************
//********************************************************************************
        //-------class kuyruk olsrurmak-------------
        //public class queue
        //{//if (x == null)
            //{
            //    return;
            //}
            //add(x);
            //while (on != null)
            //{

            //    //int p = pop().sayi;
            //    BTree q;
            //   //= ((BTree)pop());
            //    listBox1.Text += q.deger + " ";
            //    if (q.sol != null)
            //    {
            //        add(q.sol);
            //    }
            //    else
            //    {
            //        add(q.sag);
            //    }
        //    //}
        //    public int sayi;
        //    public queue balanti;
        //}
        //public queue on;
        //public queue arka;
        ////-----------------------add-------------------
        //public void add(BSTree node)
        //{
        //    queue yeni = new queue();
        //    yeni.sayi = node.deger;
        //    if (on == null)
        //    {
        //        on = arka = yeni;
        //    }
        //    else
        //    {
        //        arka.balanti = yeni;
        //        arka = yeni;
        //    }


        //}
        ////------------------------silme--------------
        //public queue pop()
        //{
        //    queue temp = new queue();

        //    temp = on;
        //    temp = temp.balanti;
        //    on = temp;
        //    return on;

        //}
        //public BSTree Parent(BSTree dugum, int sayi)
        //{
        //    return ParentBul(kok, sayi);
        //}


        //public BSTree ParentBul(BSTree dugum, int sayi)
        //{

        //    if (dugum == null)
        //    {
        //        return dugum;
        //    }
        //    else if (sayi < dugum.deger)
        //    {
        //        if (dugum.sol.deger == sayi || dugum.sag.deger == sayi)
        //        {
        //            return dugum;

        //        }
        //        else
        //            return ParentBul(dugum.sol, sayi);
        //    }
        //    else if (sayi > dugum.deger)
        //    {
        //        if (dugum.sag.deger == sayi || dugum.sol.deger == sayi)
        //        {

        //            return dugum;
        //        }
        //        else
        //            return ParentBul(dugum.sag, sayi);
        //    }
        //    else
        //    {
        //        return dugum;
        //    }
        //}

    }
}
