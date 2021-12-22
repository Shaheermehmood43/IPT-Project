using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Timers;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Project_UI
{

    partial class Service1 : ServiceBase
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<Token> Token;
        private Timer timer;
        public Service1()
        {
            InitializeComponent();

        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            this.timer.Interval = 1000 * 60;
            this.timer.Elapsed += new ElapsedEventHandler(Tick);
            //timer.AutoReset = true;
            this.timer.Enabled = true;
            ProjectServiceClass.WriteErrorLog("Service started");
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer = null;
            ProjectServiceClass.WriteErrorLog("Service stopped");
        }
        private void Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            ProjectServiceClass.WriteErrorLog("Into Tick");
            if (DateTime.Now.Hour == 6 && DateTime.Now.Minute == 35)
            {
                client = new MongoClient("mongodb+srv://Ammar:ProAmmar10@iptcluster.uy0ro.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
                database = client.GetDatabase("HBL");
                Token = database.GetCollection<Token>("Token");
                ProjectServiceClass.WriteErrorLog("Fired!");
                var filter = Builders<Token>.Filter.Empty;//.Token.DeleteManyAsync(Builders<Token>.Filter.Empty);
                Token.DeleteManyAsync(filter);
            }
        }
    }
}
