using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ворлкскилс
{
    public partial class Menu_ispol : Form
    {
        SqlConnection sqlConn;

        public Menu_ispol()
        {
            InitializeComponent();
        }
        private async void Menu_ispol_Load(object sender, EventArgs e)
        {
            string ConnStr = @"Data Source=БАБУШКА-ПК\CENTAUR;Initial Catalog=rdrevDB;Integrated Security=True";

            sqlConn = new SqlConnection(ConnStr);

            await sqlConn.OpenAsync();

            obnov();
        }
        //private async void Menu_ispo_Load(object sender, EventArgs e)
        //{
        //    string ConnStr = @"Data Source=БАБУШКА-ПК\CENTAUR;Initial Catalog=rdrevDB;Integrated Security=True";

        //    sqlConn = new SqlConnection(ConnStr);

        //    await sqlConn.OpenAsync();

        //    obnov();
        //}//загрузка даных при открытие формы 
        private async void obnov()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();


            SqlDataReader sqlRead = null;

            SqlCommand comend = new SqlCommand("SELECT * FROM [list2]", sqlConn);

            try
            {
                listBox1.Items.Add("id" + "\t" +
                       "ФИО мен" + "\t\t\t" +
                       "ФИО испо" + "\t\t\t" +
                       "Грейд" + " \t" +
                      "Junior" + "\t" +
                       "Middle" + "\t" +
                       "Senior" + "\t" +
                       "Kap" + "\t" +//Коэф для Анализ и проектирование
                       "Ky" + "\t" +//Коэф для Установка оборудования
                       "Kto" + "\t" +// Коэф для Техническое обслуживание и сопровождение
                       "dct" + "\t\t\t" +//Коэф времени
                       "dcs" + "\t\t\t" +//Коэф сложности
                       "Tb"); //Коэф для перевода в денежный эквивалент

                sqlRead = await comend.ExecuteReaderAsync();
                while (await sqlRead.ReadAsync())
                {
                    if (Form1.Glab.login == Convert.ToString(sqlRead[@"Логин исполнителя"]))
                        listBox1.Items.Add(Convert.ToString(sqlRead[@"id"]) + "\t" +
                       Convert.ToString(sqlRead[@"ФИО менеджера"]) + "\t" +
                       Convert.ToString(sqlRead[@"ФИО исполнителя"]) + "\t" +
                       Convert.ToString(sqlRead[@"Грейд исполнителя"]) + " \t" +
                       Convert.ToString(sqlRead[@"Junior мин ЗП"]) + "\t" +
                       Convert.ToString(sqlRead[@"Middle мин ЗП"]) + "\t" +
                       Convert.ToString(sqlRead[@"Senior мин ЗП"]) + "\t" +
                       Convert.ToString(sqlRead[@"Коэффициент для Анализ и проектирование"]) + "\t" +
                       Convert.ToString(sqlRead[@"Коэффициент для Установка оборудования"]) + "\t" +
                       Convert.ToString(sqlRead[@"Коэффициент для Техническое обслуживание и сопровождение"]) + "\t" +
                       Convert.ToString(sqlRead[@"Коэффициент времени"]) + "\t\t" +
                       Convert.ToString(sqlRead[@"Коэффициент сложности"]) + "\t\t" +
                       Convert.ToString(sqlRead[@"Коэффициент для перевода в денежный эквивалент"]));                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (sqlRead != null)
                sqlRead.Close();

            comend = new SqlCommand("SELECT * FROM [list1]", sqlConn);

            try
            {
                listBox2.Items.Add(
                        "Логин исполнителя" + "\t\t\t" +
                        "Заголовок" + "\t\t\t\t\t" +
                        "Сложность" + "\t\t\t" +
                        "Статус" + "\t\t\t" +
                        "Характер работы");

                sqlRead = await comend.ExecuteReaderAsync();
                while (await sqlRead.ReadAsync())
                {
                    if (Form1.Glab.login == Convert.ToString(sqlRead[@"Логин исполнителя"]))
                    {
                        int loginLength = Convert.ToString(sqlRead[@"Логин исполнителя"]).Length,
                        ZagolovokLength = Convert.ToString(sqlRead[@"Заголовок"]).Length,
                        SloznostiLength = Convert.ToString(sqlRead[@"Сложность"]).Length,
                        StatistiLength = Convert.ToString(sqlRead[@"Статус"]).Length,
                        KharakterLength = Convert.ToString(sqlRead[@"Характер работы"]).Length;

                        int numberOfSpaces1 = 100 - loginLength - ZagolovokLength,
                            numberOfSpaces2 = 20 - ZagolovokLength - SloznostiLength + 70,
                            numberOfSpaces3 = 17 - SloznostiLength - StatistiLength + 40,
                            numberOfSpaces4 = 53 - StatistiLength - KharakterLength;

                        string spaces1 = string.Empty,
                               spaces2 = string.Empty,
                               spaces3 = string.Empty,
                               spaces4 = string.Empty;


                        for (int i = 0; i < numberOfSpaces1; i++)
                        {
                            spaces1 = spaces1 + " ";
                        }


                        for (int i = 0; i < numberOfSpaces2; i++)
                        {
                            spaces2 = spaces2 + " ";
                        }


                        for (int i = 0; i < numberOfSpaces3; i++)
                        {
                            spaces3 = spaces3 + " ";
                        }

                        for (int i = 0; i < numberOfSpaces4; i++)
                        {
                            spaces4 = spaces4 + " ";
                        }



                        listBox2.Items.Add(
                       Convert.ToString(sqlRead[@"Логин исполнителя"]) + spaces1 +
                       Convert.ToString(sqlRead[@"Заголовок"]) + spaces2 +
                       Convert.ToString(sqlRead[@"Сложность"]) + spaces3 +
                       Convert.ToString(sqlRead[@"Статус"]) + spaces4 +
                       Convert.ToString(sqlRead[@"Характер работы"]));

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlRead != null)
                    sqlRead.Close();
            }
        }// обновление просмотра 

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConn != null && sqlConn.State != ConnectionState.Closed)
                sqlConn.Close();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obnov();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ipdatePasswordBox.Text) && !string.IsNullOrWhiteSpace(ipdatePasswordBox.Text))
                {
                    DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите изменить пароль",
                       "Согласие", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlCommand comend = new SqlCommand("UPDATE [list2] SET [Password] = @password WHERE [Login] = @login", sqlConn);

                        comend.Parameters.AddWithValue("password", ipdatePasswordBox.Text);
                        comend.Parameters.AddWithValue("login", Form1.Glab.login);
                        await comend.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
