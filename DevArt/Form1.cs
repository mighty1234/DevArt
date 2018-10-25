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
using System.Data.SQLite;
using System.Configuration;
using System.Data.SqlClient;


namespace DevArt
{
    public partial class Form1 : Form
    { 
       

        public Form1()
        {
            InitializeComponent();
        }
        FileModel file = new FileModel();

        private void button2_Click(object sender, EventArgs e)
        {    
            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show("Введите имя файла");
            }
            else
            {
                try
                {
                    SQLiteConnection m_dbConnection;
                    m_dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Documents1"].ConnectionString);
               
                    file.Name = textBox1.Text;                    
                    m_dbConnection.Open();
         
                  string sql= "INSERT INTO Docs (Name, Body) VALUES (@name, @body)";
                     SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.Parameters.Add("@name", DbType.String).Value = file.Name;
                    command.Parameters.Add("@body", DbType.Binary).Value = file.Body;
                    command.ExecuteNonQuery();
                    m_dbConnection.Close();                   
                }
                catch (SQLiteException k)
                {
                    MessageBox.Show(k.ToString());
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
              
                var x = openFileDialog1.FileName;          
                using (var stream = new FileStream(openFileDialog1.FileName, FileMode.Open))
                {
                    byte[] body= new byte[stream.Length];
                    stream.Read(body, 0, (int)stream.Length);
                    stream.Close();
                    FilePathBox.Text = openFileDialog1.FileName;                    
                    file.Body = body;
                }

            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == String.Empty)
            {
                MessageBox.Show("Введите имя файла");
            }
            else

                try
                {
                    SQLiteConnection m_dbConnection;
                    m_dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Documents1"].ConnectionString);
                    file.Name = textBox1.Text;
                    m_dbConnection.Open();
                    string sql = "SELECT  Body,Name From Docs where Docs.Name= @name";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.Parameters.Add("@name", DbType.String).Value = textBox2.Text;

                    var x = command.ExecuteScalar();
                    m_dbConnection.Close();
                    file.Name = textBox2.Text;
                    file.Body = (byte[])x; 
                    richTextBox2.Text = System.Text.Encoding.UTF8.GetString((byte[])x);
                }
                catch (SQLiteException k)
                {
                    MessageBox.Show(k.ToString());
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                using (Stream s = File.Open(sfd.FileName, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(System.Text.Encoding.UTF8.GetString((byte[])file.Body));
                }

            }
        }
    }

       
    }


       
    


