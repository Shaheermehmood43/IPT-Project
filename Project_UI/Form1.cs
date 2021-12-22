using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;

namespace Project_UI
{
    public partial class Form1 : Form
    {
        int flag = 0;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<AdminCredentials> Admincred; 
        public Form1()
        {
            InitializeComponent();
            client = new MongoClient("mongodb+srv://Ammar:ProAmmar10@iptcluster.uy0ro.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            database = client.GetDatabase("HBL");
            Admincred = database.GetCollection<AdminCredentials>("AdminCred");
            aboutUs1.SendToBack();
            contactUsControl1.SendToBack();
            pictureBox5.BringToFront();
            panel3.BringToFront();
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            contactUsControl1.SendToBack();
            pictureBox5.SendToBack();
            aboutUs1.BringToFront();
        }
        public void hidePanels()
        {
            panel4.Width = 0;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox5;
            label1.BackColor = Color.Transparent;
            hidePanels();
            panel3.Parent = pictureBox5;
            panel3.BackColor = Color.FromArgb(120, 255,255,255);
            panel4.Parent = panel3;
            panel4.BackColor = Color.FromArgb(120, 255, 255, 255);

        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            aboutUs1.SendToBack();
            contactUsControl1.SendToBack();
            panel3.SendToBack();
            panel4.SendToBack();
            pictureBox5.BringToFront();
        }

        private void buttonContact_Click(object sender, EventArgs e)
        {
            aboutUs1.SendToBack();
            pictureBox5.SendToBack();
            panel3.SendToBack();
            panel4.SendToBack();
            contactUsControl1.Height = pictureBox5.Height;
            contactUsControl1.BringToFront();
        }

        private void buttonFPass_Click(object sender, EventArgs e)
        {
            aboutUs1.SendToBack();
            contactUsControl1.SendToBack();
            pictureBox5.BringToFront();
            panel4.Width = 344;
            panel4.BringToFront();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Proceed Button
            MessageBox.Show("Pass Changed");
            panel4.Width = 0;
            panel4.SendToBack();
            panel3.BringToFront();
            string id = textBox4.Text;
            string oldpass = textBox5.Text;
            string newpass = textBox3.Text;
            string newpass2 = textBox6.Text;

            var filter = Builders<AdminCredentials>.Filter.Eq(a => a.HBL_ID, id) & Builders<AdminCredentials>.Filter.Eq(b => b.Password, oldpass);
            if (newpass == newpass2)
            {
                var update = Builders<AdminCredentials>.Update.Set(a => a.Password, newpass);
                Admincred.UpdateOne(filter, update);
            }
            else
            {
                MessageBox.Show("Error Updating Password");
            }
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string pass = textBox2.Text;
            string name = null;

            var builder = Builders<AdminCredentials>.Filter;
            var query = builder.Eq("HBL_ID",id) & builder.Eq("Password", pass);
            var result = await Admincred.Find(query).ToListAsync();
            foreach (var item in result)
            {
                if(item.HBL_ID == id && item.Password == pass)
                {
                    flag = 1;
                    name = item.name;
                    break;
                }
                else
                {
                    flag = 0;
                }
            }
            if (flag == 1)
            {
                this.Hide();
                AfterLoginForm afterLoginForm = new AfterLoginForm(name);
                afterLoginForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid User Login");
            }
        }


    }
}
