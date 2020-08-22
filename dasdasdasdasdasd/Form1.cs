using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dasdasdasdasdasd
{
    public partial class Form1 : Form
    {
        
        string ORADB = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                            "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User ID=hyoin;Password=1234;";
        OracleConnection conn;
        OracleCommand cmd;
        OracleDataReader dr;
        string beforeName;
        public Form1()
        {
            InitializeComponent();
            conn = new OracleConnection(ORADB);
            cmd = new OracleCommand();
            //dbConnection(conn);
            //update(conn, cmd);
            //delete(conn, cmd);
            //select(conn, cmd);

            //if(conn!=null)
            //    dbClose(conn);

        }
        void dbConnection(OracleConnection conn)
        {
            try
            {
                conn.Open();
                Console.WriteLine("db연결");
            }
            catch(OracleException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        void dbClose(OracleConnection conn)
        {
            try
            {
                conn.Close();
                Console.WriteLine("db닫음");

            }
            catch(OracleException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        void insert(OracleConnection conn, OracleCommand cmd)
        {
            try
            {
                dbConnection(conn);
                string name = textBox1.Text;
                int age = int.Parse(textBox2.Text);
                
                cmd.Connection = conn;
                cmd.CommandText = string.Format("insert into aaaaa values('{0}','{1}')", name, age);
                //cmd.Parameters.Add("@name", name);
                //cmd.Parameters.Add("@age", age);
                cmd.ExecuteNonQuery();
            }
            catch(OracleException e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
                return;
            }
            finally
            {
                if (conn != null)
                    dbClose(conn);
            }
            
        }
        void select(OracleConnection conn, OracleCommand cmd)
        {
            dbConnection(conn);
            dataGridView1.Rows.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "select * from aaaaa";
            cmd.CommandType = System.Data.CommandType.Text;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["NAME"], dr["AGE"]);
                }
            }
            dr.Close();
            if (conn != null)
                dbClose(conn);
        }
        void update(OracleConnection conn, OracleCommand cmd)
        {
            dbConnection(conn);
            
            string name = textBox1.Text;
            int age = int.Parse(textBox2.Text);
            cmd.Connection = conn;
            Console.WriteLine(beforeName);
            cmd.CommandText = string.Format("update aaaaa set NAME='{0}', age='{1}' where name = '{2}'", name, age,beforeName);
            cmd.ExecuteNonQuery();
            if (conn != null)
                dbClose(conn);

        }
        void delete(OracleConnection conn, OracleCommand cmd)
        {
            dbConnection(conn);
            
            cmd.Connection = conn;
            cmd.CommandText = $"delete aaaaa where name = '{beforeName}'";
            cmd.ExecuteNonQuery();
            dbClose(conn);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            insert(conn, cmd);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            select(conn, cmd);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dbConnection(conn);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dbClose(conn);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //textBox1.Text = dataGridView1[e.ColumnIndex,e.RowIndex].Value.ToString();
            //textBox2.Text = dataGridView1[e.ColumnIndex,e.RowIndex].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1[0, e.RowIndex].Value.ToString();
            beforeName = textBox1.Text;
            textBox2.Text = dataGridView1[1, e.RowIndex].Value.ToString();
        }

        private void name(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            update(conn, cmd);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            delete(conn, cmd);
        }
    }
}
