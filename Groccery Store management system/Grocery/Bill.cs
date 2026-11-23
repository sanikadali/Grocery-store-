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
namespace Grocery
{
    public partial class Bill : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ADMIN\OneDrive\Documents\Visual Studio 2012\Projects\Grocery\GRCDB.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        String str = "";
        public Bill()
        {
          
            InitializeComponent();
            Fillname();
        }
        private void Fillname()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ADMIN\OneDrive\Documents\Visual Studio 2012\Projects\Grocery\GRCDB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlCommand cmd = new SqlCommand("select Name from getcostock", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Load(Rdr);
            comboBox1.ValueMember = "Name";
            comboBox1.DataSource = dt;
            con.Close();
        }
        /*SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Admin\Documents\Visual Studio 2012\Projects\Grocery\GRCDB.mdf;Integrated Security=True;Connect Timeout=30");
        int buy, sale;
        double tot;
        private void getStock()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(Quantity) from Product where Name='" + comboBox1.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            buy = Convert.ToInt32(dt.Rows[0][0].ToString());
            SqlDataAdapter sda1 = new SqlDataAdapter("Select Sum(Qty) from Income where Name='" + comboBox1.Text + "'", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            sale = Convert.ToInt32(dt1.Rows[0][0].ToString());
            tot = buy - sale;
            Stocklbl.Text = "" + tot;
            Con.Close();
        }*/
             
        int Amount=0;
        private void Addtobill()
        {
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(GridViewBill);
            newRow.Cells[0].Value=comboBox1.SelectedValue.ToString();
            newRow.Cells[1].Value = Convert.ToInt32(textQuantity.Text);
            newRow.Cells[2].Value = Convert.ToInt32(textCost.Text);
            newRow.Cells[3].Value = Convert.ToInt32(textTot.Text);
            GridViewBill.Rows.Add(newRow);
            Amount = Amount + Convert.ToInt32(newRow.Cells[3].Value);
            clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ADMIN\OneDrive\Documents\Visual Studio 2012\Projects\Grocery\GRCDB.mdf;Integrated Security=True;Connect Timeout=30");
            if (comboBox1.SelectedIndex == -1 && textQuantity.Text != "" && textCost.Text != "" && textTot.Text != "")
            {
                MessageBox.Show("Please , Insert all Information ... ", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Income values('" + comboBox1.SelectedValue.ToString() + "'," + textQuantity.Text + "," + textCost.Text + "," + textTot.Text + ",'" + dateTimePicker1.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Inserted Successfully", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Addtobill();
                    con.Close();
                    clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }         
        }
        
        private void clear()
        {
            comboBox1.SelectedIndex = -1;
            Stocklbl.Text = "";
            textQuantity.Text = "";
            textCost.Text = "";
            textTot.Text = "";

        }
        private void Bill_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }
        private void getCost()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ADMIN\OneDrive\Documents\Visual Studio 2012\Projects\Grocery\GRCDB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            str = "select Cost from getcost where Name='" + comboBox1.Text + "'";
            cmd = new SqlCommand(str, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textCost.Text = dr["Cost"].ToString();
            }
            con.Close();
        }
        
        private void getStock()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ADMIN\OneDrive\Documents\Visual Studio 2012\Projects\Grocery\GRCDB.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(Quantity) from Product where Name='" + comboBox1.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SqlDataAdapter sda1 = new SqlDataAdapter("Select Sum(Qty) from Income where Name='" + comboBox1.Text + "'", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            int buy, sale;
            int tot;
            buy = Convert.ToInt32(dt.Rows[0][0].ToString());
            sale = Convert.ToInt32(dt1.Rows[0][0].ToString());
            tot = buy - sale;
            Stocklbl.Text = "" + tot;
            con.Close();
        }
        private void textCost_Leave(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStock();
            getCost();
        }

        private void textQuantity_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textQuantity_Leave(object sender, EventArgs e)
        {
            
        }

        private void panel2_Click(object sender, EventArgs e)
        {
           
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }
        
        private void GridViewBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        
        
        private void printBtn_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
        string prodname;
        int  prodqty, prodprice, tottal, pos = 340;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Me. Nathuram Shankarsheth Dali.", new Font("Bell MT", 37, FontStyle.Bold), Brushes.MediumVioletRed, new Point(23, 15));
            e.Graphics.DrawString("Wholesaler and Retailer of Groceries", new Font("Bell MT", 25, FontStyle.Bold), Brushes.MediumVioletRed, new Point(130, 79));
            e.Graphics.DrawString("Bajarpeth, Chiplun, pin:- 415605", new Font("Bell MT", 23, FontStyle.Bold), Brushes.MediumVioletRed, new Point(190, 120));
            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Bell MT", 20, FontStyle.Bold), Brushes.Black, new Point(27, 200));
            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  ", new Font("Cooper Black", 10, FontStyle.Bold), Brushes.Black, new Point(19, 250));
            e.Graphics.DrawString("Item Name              Quantity      Unit Price      Total Amount", new Font("Bell MT", 20, FontStyle.Bold), Brushes.Black, new Point(30, 270));
            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  ", new Font("Cooper Black", 10, FontStyle.Bold), Brushes.Black, new Point(19, 310));
            foreach (DataGridViewRow row in GridViewBill.Rows)
            {
                prodname = "" + row.Cells["Column1"].Value;
                prodqty = Convert.ToInt32(row.Cells["Column2"].Value);
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                tottal = Convert.ToInt32(row.Cells["Column4"].Value);
                e.Graphics.DrawString("" + prodname, new Font("Bell MT", 20, FontStyle.Bold), Brushes.Black, new Point(50, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Bell MT", 20, FontStyle.Bold), Brushes.Black, new Point(320, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Bell MT", 20, FontStyle.Bold), Brushes.Black, new Point(485, pos));
                e.Graphics.DrawString("" + tottal, new Font("Bell MT", 20, FontStyle.Bold), Brushes.Black, new Point(680, pos));
                pos = pos + 35;
            }
            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  ", new Font("Cooper Black", 10, FontStyle.Bold), Brushes.Black, new Point(19, pos));
            e.Graphics.DrawString("Grand Total:   RS " + Amount, new Font("Bell MT", 20, FontStyle.Bold), Brushes.DarkSlateGray, new Point(500, pos + 50));
            //e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - GroceryShop - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Bell MT", 20, FontStyle.Bold), Brushes.DarkSlateGray, new Point(11, pos + 85));
            GridViewBill.Rows.Clear();
            GridViewBill.Refresh();
            pos = 100;
            //Amount = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void textTot_Click(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(textQuantity.Text) * Convert.ToInt32(textCost.Text);
            textTot.Text = "" + total;
        }
    }
}
