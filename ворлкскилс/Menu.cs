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
    public partial class Menu : Form
    {
        SqlConnection sqlConn;

        public Menu()
        {
            InitializeComponent();
        }

        public static class Glab
        {
            public static int avtoInkrement = 1;
        }

        private async void Menu_Load(object sender, EventArgs e)
        {
            string ConnStr = @"Data Source=БАБУШКА-ПК\CENTAUR;Initial Catalog=rdrevDB;Integrated Security=True";

            sqlConn = new SqlConnection(ConnStr);

            await sqlConn.OpenAsync();
            comboBox1.SelectedItem = "Все запеси";
            comboBox2.SelectedItem = "Все запеси";
            comboBox3.SelectedItem = "Менеджер";

            this.comboBox1.SelectedIndexChanged +=
            new System.EventHandler(ComboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged +=
            new System.EventHandler(ComboBox2_SelectedIndexChanged);
            obnov();
        }//загрузка даных при открытие формы 
      
        private void ComboBox1_SelectedIndexChanged(object sender,System.EventArgs e)
        {
            if (comboBox1.Text != "Записи с выбраном испольнителем")
                textBox8.Visible = false;
            obnov();
        }
        private void ComboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (comboBox2.Text != "Записи с выбраном испольнителем")
                textBox8.Visible = false;
            obnov();
        }
     
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConn != null && sqlConn.State != ConnectionState.Closed)
                sqlConn.Close();
        }//закрыть подключение 

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obnov();
        }//обновить просмотр

        private async void buttonCi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                         !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    SqlCommand comend = new SqlCommand("UPDATE [list1] SET [Характер работы] = @Коэффициент WHERE [Заголовок] = @id", sqlConn);

                    comend.Parameters.AddWithValue("@Коэффициент", textBox3.Text);
                    comend.Parameters.AddWithValue("@id", textBox4.Text);

                    await comend.ExecuteNonQueryAsync();
                    obnov();
                    tabControl1.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//изменить характер работы
        
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
                    if (comboBox1.Text == "Все запеси")
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

                    else if (comboBox1.Text == "Мои записи" && Form1.Glab.login == Convert.ToString(sqlRead[@"Логин менеджера"]))
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

                    else if (comboBox1.Text == "Записи с выбраном испольнителем")
                    {
                        textBox8.Visible = true;
                        if(textBox8.Text == Convert.ToString(sqlRead[@"Логин исполнителя"]))
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
                    Glab.avtoInkrement++;
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
                    if (comboBox2.Text == "Все запеси")
                        listBox2.Items.Add(
                        Convert.ToString(sqlRead[@"Логин исполнителя"]) + spaces1 +
                        Convert.ToString(sqlRead[@"Заголовок"]) + spaces2 +
                        Convert.ToString(sqlRead[@"Сложность"]) + spaces3 +
                        Convert.ToString(sqlRead[@"Статус"]) + spaces4 +
                        Convert.ToString(sqlRead[@"Характер работы"]));
                    else if (comboBox1.Text == "Записи с выбраном испольнителем")
                    {
                        textBox10.Visible = true;
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

        private void Menu_FormClosing(object sender, EventArgs e)
        {
            if (sqlConn != null && sqlConn.State != ConnectionState.Closed)
                sqlConn.Close();
            this.Close();
        }

        private async void apdeitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (GmCheckBox.Checked)
                {
                    if (!string.IsNullOrEmpty(ipNewBox.Text) && !string.IsNullOrWhiteSpace(ipNewBox.Text) &&
                          !string.IsNullOrEmpty(GmBox.Text) && !string.IsNullOrWhiteSpace(GmBox.Text))
                    {
                        SqlCommand comend = new SqlCommand("UPDATE [list2] SET [Junior мин ЗП] = @Junior WHERE [id] = @id", sqlConn);

                        comend.Parameters.AddWithValue("Junior", GmBox.Text);
                        comend.Parameters.AddWithValue("id", ipNewBox.Text);

                        await comend.ExecuteNonQueryAsync();
                    }
                }
                if (DcCheckBox.Checked)
                {
                    if (!string.IsNullOrEmpty(ipNewBox.Text) && !string.IsNullOrWhiteSpace(ipNewBox.Text))
                    {
                        SqlCommand comend = new SqlCommand("UPDATE [list2] SET [Коэффициент времени] = @Коэффициент WHERE [id] = @id", sqlConn);

                        comend.Parameters.AddWithValue("Коэффициент", DcBox.Value);
                        comend.Parameters.AddWithValue("id", ipNewBox.Text);

                        await comend.ExecuteNonQueryAsync();
                    }
                }
                if (TbCheckBox.Checked)
                {
                    if (!string.IsNullOrEmpty(TbBox.Text) && !string.IsNullOrWhiteSpace(TbBox.Text) &&
                      !string.IsNullOrEmpty(ipNewBox.Text) && !string.IsNullOrWhiteSpace(ipNewBox.Text))
                    {
                        SqlCommand comend = new SqlCommand("UPDATE [list2] SET [Коэффициент для перевода в денежный эквивалент] = @Коэффициент WHERE [id] = @id", sqlConn);

                        comend.Parameters.AddWithValue("Коэффициент", TbBox.Text);
                        comend.Parameters.AddWithValue("id", ipNewBox.Text);

                        await comend.ExecuteNonQueryAsync();
                    }
                }
                if (TcCheckBox.Checked)
                {
                    if (!string.IsNullOrEmpty(ipNewBox.Text) && !string.IsNullOrWhiteSpace(ipNewBox.Text))
                    {
                        SqlCommand comend = new SqlCommand("UPDATE [list2] SET [Коэффициент сложности] = @Коэффициент WHERE [id] = @id", sqlConn);

                        comend.Parameters.AddWithValue("Коэффициент", TcBox.Value);
                        comend.Parameters.AddWithValue("id", ipNewBox.Text);

                        await comend.ExecuteNonQueryAsync();
                    }
                }

                obnov();
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void UsersInsertButton_Click(object sender, EventArgs e)
        {
            if (    !string.IsNullOrEmpty(U1textBox.Text) && !string.IsNullOrWhiteSpace(U1textBox.Text) &&
                    !string.IsNullOrEmpty(U2textBox.Text) && !string.IsNullOrWhiteSpace(U2textBox.Text) &&
                    !string.IsNullOrEmpty(U3textBox.Text) && !string.IsNullOrWhiteSpace(U3textBox.Text) &&
                    !string.IsNullOrEmpty(U4textBox.Text) && !string.IsNullOrWhiteSpace(U4textBox.Text) &&
                    !string.IsNullOrEmpty(U5textBox.Text) && !string.IsNullOrWhiteSpace(U5textBox.Text) )
            {
                SqlCommand comend = new SqlCommand(
                    "INSERT INTO [list1] " +
                    
                    "([Логин исполнителя]," +                    
                    "[Заголовок]," +
                    "[Сложность]," +
                    "[Статус]," +
                    "[Характер работы])" +
                   
                    " VALUES" +
                    " (@Логин," +
                    "@Заголовок," +
                    "@Сложность," +
                    "@Статус," +
                    "@Характер)"
                    , sqlConn);


                comend.Parameters.AddWithValue("@Логин", U1textBox.Text);
                comend.Parameters.AddWithValue("@Заголовок", U2textBox.Text);
                comend.Parameters.AddWithValue("@Сложность", U3textBox.Text);
                comend.Parameters.AddWithValue("@Статус", U4textBox.Text);
                comend.Parameters.AddWithValue("@Характер", U5textBox.Text);


                await comend.ExecuteNonQueryAsync();
                obnov();
                tabControl1.SelectedIndex = 1;
            }
        }

        private async void tasksInsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TastextBox1.Text) && !string.IsNullOrWhiteSpace(TastextBox1.Text) &&
                        !string.IsNullOrEmpty(TastextBox2.Text) && !string.IsNullOrWhiteSpace(TastextBox2.Text) &&
                        !string.IsNullOrEmpty(TastextBox3.Text) && !string.IsNullOrWhiteSpace(TastextBox3.Text) &&
                        !string.IsNullOrEmpty(TastextBox4.Text) && !string.IsNullOrWhiteSpace(TastextBox4.Text) &&
                        !string.IsNullOrEmpty(TastextBox5.Text) && !string.IsNullOrWhiteSpace(TastextBox5.Text) &&
                        !string.IsNullOrEmpty(TastextBox6.Text) && !string.IsNullOrWhiteSpace(TastextBox6.Text) &&
                        !string.IsNullOrEmpty(TastextBox7.Text) && !string.IsNullOrWhiteSpace(TastextBox7.Text) &&
                        !string.IsNullOrEmpty(TastextBox8.Text) && !string.IsNullOrWhiteSpace(TastextBox8.Text) &&
                        !string.IsNullOrEmpty(TastextBox9.Text) && !string.IsNullOrWhiteSpace(TastextBox9.Text) &&
                        !string.IsNullOrEmpty(TastextBox10.Text) && !string.IsNullOrWhiteSpace(TastextBox10.Text) &&
                        !string.IsNullOrEmpty(TastextBox11.Text) && !string.IsNullOrWhiteSpace(TastextBox11.Text) &&
                        !string.IsNullOrEmpty(TastextBox12.Text) && !string.IsNullOrWhiteSpace(TastextBox12.Text))
                {
                    SqlCommand comend = new SqlCommand(
                        "INSERT INTO [list2] " +

                        "([id]," +
                        "[Логин менеджера]," +
                        "[Грейд исполнителя]," +
                        "[Логин исполнителя]," +
                        "[ФИО исполнителя]," +
                        "[ФИО менеджера]," +
                        "[Junior мин ЗП]," +
                        "[Middle мин ЗП]," +
                        "[Senior мин ЗП]," +
                        "[Коэффициент для Анализ и проектирование]," +
                        "[Коэффициент для Установка оборудования]," +
                        "[Коэффициент для Техническое обслуживание и сопровождение]," +
                        "[Коэффициент времени]," +
                        "[Коэффициент сложности]," +
                        "[Коэффициент для перевода в денежный эквивалент])" +

                        " VALUES" +
                        " (@id," +
                        "@Логин_мендж," +
                        "@Грейд_испол," +
                        "@Логин_испол," +
                        "@ФИО_испол," +
                        "@ФИО_менедж," +
                        "@Junior," +
                        "@Middle," +
                        "@Senior," +
                        "@Анализ," +
                        "@оборудования," +
                        "@сопровождение," +
                        "@времени," +
                        "@сложности," +
                        "@денги)"
                        , sqlConn);


                    comend.Parameters.AddWithValue("@id", Glab.avtoInkrement);
                    comend.Parameters.AddWithValue("@Логин_мендж", TastextBox1.Text);
                    comend.Parameters.AddWithValue("@Грейд_испол", TastextBox2.Text);
                    comend.Parameters.AddWithValue("@Логин_испол", TastextBox3.Text);
                    comend.Parameters.AddWithValue("@ФИО_испол", TastextBox4.Text);
                    comend.Parameters.AddWithValue("@ФИО_менедж", TastextBox5.Text);
                    comend.Parameters.AddWithValue("@Junior", TastextBox6.Text);
                    comend.Parameters.AddWithValue("@Middle", TastextBox7.Text);
                    comend.Parameters.AddWithValue("@Senior", TastextBox8.Text);
                    comend.Parameters.AddWithValue("@Анализ", TastextBox9.Text);
                    comend.Parameters.AddWithValue("@оборудования", TastextBox10.Text);
                    comend.Parameters.AddWithValue("@сопровождение", TastextBox11.Text);
                    comend.Parameters.AddWithValue("@времени", tasksDateTimePicker1.Value);
                    comend.Parameters.AddWithValue("@сложности", tasksDateTimePicker2.Value);
                    comend.Parameters.AddWithValue("@денги", TastextBox12.Text);

                    await comend.ExecuteNonQueryAsync();
                    obnov();
                    tabControl1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tasksDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tasksDeleteBox.Text) && !string.IsNullOrWhiteSpace(tasksDeleteBox.Text))
                {
                    DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите удалить запись",
                        "Согласие", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlCommand comend = new SqlCommand("DELETE FROM [list2] WHERE [id] = @id", sqlConn);

                        comend.Parameters.AddWithValue("@id", tasksDeleteBox.Text);

                        await comend.ExecuteNonQueryAsync();
                        obnov();
                        tabControl1.SelectedIndex = 0;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void usersDeleteButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(usersDeleteBox.Text) && !string.IsNullOrWhiteSpace(usersDeleteBox.Text))
            {
                try
                {
                    DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите удалить запись",
                        "Согласие", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlCommand comend = new SqlCommand("DELETE FROM [list1] WHERE [Заголовок] = @id", sqlConn);

                        comend.Parameters.AddWithValue("@id", usersDeleteBox.Text);

                        await comend.ExecuteNonQueryAsync();
                        obnov();
                        tabControl1.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void NewUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginNewBox.Text) && !string.IsNullOrWhiteSpace(loginNewBox.Text) &&
                   !string.IsNullOrEmpty(passwordNewBox.Text) && !string.IsNullOrWhiteSpace(passwordNewBox.Text) &&
                   !string.IsNullOrEmpty(fioNewBox.Text) && !string.IsNullOrWhiteSpace(fioNewBox.Text))
                {
                    SqlCommand comend = new SqlCommand("INSERT INTO [Avtoris] (login,password,FIO,dostup)VALUES(@login,@password,@FIO,@dostup)", sqlConn);

                    comend.Parameters.AddWithValue("login", loginNewBox.Text);
                    comend.Parameters.AddWithValue("password", passwordNewBox.Text);
                    comend.Parameters.AddWithValue("FIO", fioNewBox.Text);
                    if (comboBox3.Text == "Менеджер")
                        comend.Parameters.AddWithValue("dostup", 1);
                    else
                        comend.Parameters.AddWithValue("dostup", 0);

                    await comend.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginDeleteBox.Text) && !string.IsNullOrWhiteSpace(loginDeleteBox.Text))
                {
                    DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите удалить пользователя",
                        "Согласие", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlCommand command = new SqlCommand("DELETE FROM [Avtoris] WHERE [login] = @log", sqlConn);

                        command.Parameters.AddWithValue("log", loginDeleteBox.Text);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ipdeitPasswordButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ipdateLoginBox.Text) && !string.IsNullOrWhiteSpace(ipdateLoginBox.Text) &&
                    !string.IsNullOrEmpty(ipdatePasswordBox.Text) && !string.IsNullOrWhiteSpace(ipdatePasswordBox.Text))
                {
                    DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите изменить пароль",
                       "Согласие", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SqlCommand comend = new SqlCommand("UPDATE [list2] SET [Password] = @password WHERE [Login] = @login", sqlConn);

                        comend.Parameters.AddWithValue("password", ipdatePasswordBox.Text);
                        comend.Parameters.AddWithValue("login", ipdateLoginBox.Text);

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
