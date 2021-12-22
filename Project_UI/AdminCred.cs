using MongoDB.Bson;
using System;



namespace Project_UI
{
    internal class AdminCredentials
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string HBL_ID { get; set; }
        public string Password { get; set; }
    }
}