using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace English
{
    public partial class Form1 : Form
    {
        private string _theWord;
        private int _unit;
        private int _rowcount;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _unit = Convert.ToInt32(textBox1.Text);
            ChkCon();
            var commandText = $"select count(*) from englishwords where unit={_unit}";
            var command = new SqlCommand(commandText, _connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                _rowcount = (int) reader[0];
            reader.Close();
            NewWord();
        }

        private void NewWord()
        {
            button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = label2.Visible =
                button2.Visible = button3.Visible = button4.Visible = button5.Visible = button6.Visible = true;
            var tick = DateTime.Now.Ticks;
            var random = new Random((int) (tick & 0xffffffffL) | (int) (tick >> 32));
            var a = new int[5];
            a[1] = random.Next(1, _rowcount);
            a[2] = random.Next(1, _rowcount);
            while (a[1] == a[2])
                a[2] = random.Next(1, _rowcount);
            a[3] = random.Next(1, _rowcount);
            while (a[1] == a[3])
                a[3] = random.Next(1, _rowcount);
            while (a[2] == a[3])
                a[3] = random.Next(1, _rowcount);
            a[4] = random.Next(1, _rowcount);
            while (a[1] == a[4])
                a[4] = random.Next(1, _rowcount);
            while (a[2] == a[4])
                a[4] = random.Next(1, _rowcount);
            while (a[3] == a[4])
                a[4] = random.Next(1, _rowcount);
            ChkCon();

            var string1 = $"select word from englishwords where unit={_unit} and id={a[random.Next(1, 4)]}";
            var string2 = $"select mean from englishwords where unit={_unit} and id=";
            var commandWord = new SqlCommand(string1, _connection);
            var reader = commandWord.ExecuteReader();
            while (reader.Read())
            {
                _theWord = (string) reader[0];
                label2.Text = $@"{(string) reader[0]} means:";
            }
            reader.Close();
            for (var i = 1; i <= 4; i++)
            {
                var command = new SqlCommand(string2 + a[i], _connection);
                var reader1 = command.ExecuteReader();
                while (reader1.Read())
                    switch (i)
                    {
                        case 1:
                            button2.Text = (string) reader1[0];
                            break;
                        case 2:
                            button3.Text = (string) reader1[0];
                            break;
                        case 3:
                            button4.Text = (string) reader1[0];
                            break;
                        default:
                            button5.Text = (string) reader1[0];
                            break;
                    }
                reader1.Close();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            NewWord();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ChkCon();
            var commandText =
                $"select word from englishwords where word='{_theWord}' and mean='{button2.Text}'";
            var command = new SqlCommand(commandText, _connection);
            var reader = command.ExecuteReader();
            var word = new object();
            while (reader.Read())
                word = reader[0];
            reader.Close();
            try
            {
                var unused = (string) word;
            }
            catch
            {
                button2.Enabled = false;
                button2.Text = @"Wrong";
                return;
            }
            button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = false;
            label2.Text += button2.Text;
            button3.Text = button4.Text = button5.Text = @"Wrong";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ChkCon();
            var commandText =
                $"select word from englishwords where word='{_theWord}' and mean='{button3.Text}'";
            var command = new SqlCommand(commandText, _connection);
            var reader = command.ExecuteReader();
            var word = new object();
            while (reader.Read())
                word = reader[0];
            reader.Close();
            try
            {
                var unused = (string) word;
            }
            catch
            {
                button3.Enabled = false;
                button3.Text = @"Wrong";
                return;
            }
            button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = false;
            label2.Text += button3.Text;
            button2.Text = button4.Text = button5.Text = @"Wrong";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ChkCon();
            var commandText =
                $"select word from englishwords where word='{_theWord}' and mean='{button4.Text}'";
            var command = new SqlCommand(commandText, _connection);
            var reader = command.ExecuteReader();
            var word = new object();
            while (reader.Read())
                word = reader[0];
            reader.Close();
            try
            {
                var unused = (string) word;
            }
            catch
            {
                button4.Enabled = false;
                button4.Text = @"Wrong";
                return;
            }
            button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = false;
            label2.Text += button4.Text;
            button2.Text = button3.Text = button5.Text = @"Wrong";
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ChkCon();
            var commandText =
                $"select word from englishwords where word='{_theWord}' and mean='{button5.Text}'";
            var command = new SqlCommand(commandText, _connection);
            var reader = command.ExecuteReader();
            var word = new object();
            while (reader.Read())
                word = reader[0];
            reader.Close();
            try
            {
                var unused = (string) word;
            }
            catch
            {
                button5.Enabled = false;
                button5.Text = @"Wrong";
                return;
            }
            button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = false;
            label2.Text += button5.Text;
            button2.Text = button3.Text = button4.Text = @"Wrong";
        }

        #region SQL Server Connection

        private readonly SqlConnection _connection = new SqlConnection(Assets.Co_N__sT___r);

        private void ChkCon()
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        #endregion
    }
}