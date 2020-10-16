using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    

    public partial class Form1 : Form{
        TextBox[] tb; //масив текст боксів длч полінома
        Label[] Lm;   //масив лейблів для полінома
        Label[] Lg;
        Label[] LL;
        private double[] cor;//polinom
        private double[] cr;// masiv corniv
        private double[] crl;
        public int cnt_text = 0; //кількість ячейок
        public Form1() //конструктор форм
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //створюємо ячейки
        {
            if (cnt_text != 0)
                groupBox1.Controls.Clear(); //чистить все поле для ячейок
            tb = new TextBox[(int)numericUpDown1.Value+1]; //ініціалізуємо масив текст боксів
            Lm = new Label[(int)numericUpDown1.Value+1]; // /-/ лейблів для полінома
            cnt_text = (int)numericUpDown1.Value+1; 
            for (int i = 0; i < cnt_text; i++) // функція яка малює ячейки та поля
            {
                Lm[i] = new Label();
                groupBox1.Controls.Add(Lm[i]);
                Lm[i].Location = new Point(56 + i * 81, 20);
                Lm[i].Size = new Size(35, 20);
                Lm[i].Text = "X^" + (cnt_text - i-1)+"+";
                tb[i] = new TextBox();
                groupBox1.Controls.Add(tb[i]);
                tb[i].Location = new Point(15 + i * 81, 20 );
                tb[i].Size = new Size(35, 20);
            }
            Lm[cnt_text - 1].Text="= 0" ;
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
                button1_Click(sender, e);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//gorner
        {
            Gorner();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && !have_char(textBox1.Text)) // перевірка якщо пусте місце то нуль
                Lobachevskiy(1.0/ Convert.ToDouble(textBox1.Text));
            else Lobachevskiy(0.00000001);    
        }
        private void Enter(object sender, KeyPressEventArgs e)
        {

        }
        public bool have_char( string A)
        {
            for (int i = 0; i < A.Length; i++)
                if (A[i] < 48 || A[i] > 57)
                    if(A[i]!=43&&A[i]!=45)
                        return true;
            return false;
        }
        public void Gorner()
        {
            groupBox2.Controls.Clear();
            cor = new double[cnt_text]; //масив полінома
            cr = new double[cnt_text]; //масив корнів
            for (int i = 0; i < cnt_text; i++) //зчитує поля з текст боксів полінома
                if (tb[i].Text != ""&& !have_char(tb[i].Text)) // перевірка якщо пусте місце то нуль
                    cor[i] = Convert.ToDouble(tb[i].Text); //////пошукати методи щоб перевіряло чи це цифра чи ні??????
                else cor[i] = 0;
            int cnt_cor =0; //кількісь корнів
            double[] corn = new double[cor.Length]; //записуєм наш поліном
            Array.Copy(cor, 0, corn, 0, cor.Length);
            int cnt = cor.Length - 1; //схема горнера дальше
            if (corn[cnt] == 0) //перевірка чи корень 0
            {
                cr[cnt_cor++] = 0;
                cnt--;
                for (int i = cnt;cnt!=-1 ; i--)
                {
                    if (corn[cnt] == 0) { 
                        cnt--;
                  
                        } 
                        else
                    {
                        break;
                    }
                }
                
            }
            if (cnt != -1)
            {
                //cnt_cor = 0;
                for (int j = 1; j <= Math.Abs(corn[cnt]); j++)
                {

                    if (corn[cnt] == ((int)corn[cnt] / j) * j)
                    {
                        if (coren(j) == 0)
                        {
                            cr[cnt_cor++] = j;
                            for (int i = 1; i < cor.Length; i++)
                            {
                                corn[i] += corn[i - 1] * j;

                            }
                            cnt--;


                        }

                        if (coren(-j) == 0)
                        {
                            cr[cnt_cor++] = -j;
                            for (int i = 1; i < cor.Length; i++)
                            {
                                corn[i] += corn[i - 1] * (-j);

                            }
                            cnt--;

                        }
                    }

                }
            }
          if ((cr.Length) != cnt_cor)
                Array.Resize(ref cr, cnt_cor);
            Lg = new Label[cnt_cor];
            int o = 0;
            for (int i = 0; i < cnt_text; i++)
                if (cor[i] != 0)
                {
                    o = 1;
                    break;
                }
            if(o==1)
            for (int i = 0; i < cr.Length; i++)
            {
                Lg[i] = new Label();
                groupBox2.Controls.Add(Lg[i]);
                Lg[i].Location = new Point(10, 10 + i * 30 + 5);
                Lg[i].Size = new Size(50, 20);
                Lg[i].Text = "X" + (i+1) + "= "+cr[i]; // записуємо наші корні в текст бокс

            }
            if (o == 0||cnt_cor==0)
            {
                Lg = new Label[1];
                Lg[0] = new Label();
                groupBox2.Controls.Add(Lg[0]);
                Lg[0].Location = new Point(10, 10 + 1 * 30 + 5);
                Lg[0].Size = new Size(400, 20);
                Lg[0].Text = "Net korney"; // записуємо наші корні в текст бокс
            }
            return;
        }

        public double coren(double x) //функція перевірки на корінь
        {
            double k = 0;
            for (int i = (int)cor.Length ; i >= 1; i--)
            {
                k += (cor[i-1] * Math.Pow(x, cor.Length - i));
            }
            if (Double.IsNaN(x))
                return 1000;
            return k;
        }
        void Lobachevskiy(double eps)
        {
            groupBox3.Controls.Clear();
            int z = 0;
            cor = new double[cnt_text];
            for (int i = 0; i < cnt_text; i++)
                if (tb[i].Text != ""&&!have_char(tb[i].Text))
                    cor[i] = Convert.ToDouble(tb[i].Text);
                else cor[i] = 0;
            int cnt_cor = 0;
            double [] crl = new double[cnt_text-1]; //корні
            double[] m1 = new double[cnt_text]; //масив которий зберігає змінений поліном
            Array.Copy(cor, 0, m1, 0, cor.Length);
            double[] m2 = new double[cor.Length];
            double[] cr2 = new double[cor.Length-1];
            for (int i = 0; i < (int)crl.Length; i++) //знаходить початкові корні
                crl[i] = m1[i + 1] / m1[i];
            int cnt_for = 0;
            for (int cnt = 2;cnt_for<1000 ; cnt *= 2,cnt_for+=5) //процес квадрування і знаходження корнів
            {
                for (int i = 0; i < (int)cor.Length; i++)
                {
                    m2[i] = m1[i] * m1[i];

                    for (int j = 1, l = 1; i - j >= 0 && ((i + j) < (int)cor.Length); j++, l++)
                    {
                        m2[i] += Math.Pow(-1, l) * 2 * m1[i - j] * m1[i + j];
                    }
                }
                for (int i = 0; i < (int)crl.Length; i++)
                {
                    cr2[i] = Math.Pow((m2[i + 1] / m2[i]), (double)1 / cnt);
                    if(coren(cr2[i])<1||coren(-cr2[i])<1)
                    if (Math.Abs(cr2[i] - crl[i]) < eps && cnt != 2)
                    {
                        for (int k = 0; k < (int)crl.Length; k++)
                        {

                            if (Math.Abs(coren(cr2[k])) > 1)
                            {
                                if (Math.Abs(coren(-cr2[k])) < 1 && !Double.IsNaN(coren(cr2[k])))
                                {
                                    crl[z++] = -cr2[k];
                                }
                                continue;

                            }
                            if(!Double.IsNaN(coren(cr2[k])))
                             crl[z++] = cr2[k];
                        }
                            //Array.Resize(ref crl, crl.Length-z-1);
                        LL = new Label[z];
                        for (int k = 0; k < z; k++)
                        {
                            LL[k] = new Label();
                            groupBox3.Controls.Add(LL[k]);
                            LL[k].Location = new Point(10, 10 + k * 30+5);
                            LL[k].Size = new Size(400, 20);
                            LL[k].Text = "X" + (k+1) + "= " + crl[k]+"   "+coren(crl[k]);
                        }
                        
                        return;
                    }
                }
                Array.Copy(m2, 0, m1, 0, m1.Length);
                Array.Copy(cr2, 0, crl, 0, crl.Length);
            }

            LL = new Label[1];
            LL[0] = new Label();
            groupBox3.Controls.Add(LL[0]);
            LL[0].Location = new Point(10, 10 + 1 * 30+5);
            LL[0].Size = new Size(400, 20);
            LL[0].Text = "net korney, bolshaya tochnost ili nuli"+cnt_for;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    };
}

