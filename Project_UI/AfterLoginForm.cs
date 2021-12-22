using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_UI
{
    public partial class AfterLoginForm : Form
    {
        string n = null;
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<Token> Token;
        public AfterLoginForm(string name)
        {
            n = name;
            InitializeComponent();
            client = new MongoClient("mongodb+srv://Ammar:ProAmmar10@iptcluster.uy0ro.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            database = client.GetDatabase("HBL");
            Token = database.GetCollection<Token>("Token");
            ReadDocs();
        }
        public void ReadDocs()
        {
            var documents = Token.Find(Builders<Token>.Filter.Empty).Project<TokenNoID>
          (Builders<Token>.Projection.Exclude(f => f._id)).ToListAsync();
            this.dataGridView1.DataSource = documents.Result;
        }

        private void AfterLoginForm_Load_1(object sender, EventArgs e)
        {
            textBox1.Text = "Welcome, " + n;
            textBox1.ReadOnly = true;
            
        }
    }
}
