using MongoDB.Bson;
using System;



namespace Project_UI
{
    internal class Token
    {
        public ObjectId _id { get; set; }
        public string num { get; set; }
        public string custType { get; set; }
        public string cnic { get; set; }

    }
}