using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace FirestoreTest.Domain
{
    [FirestoreData]
    public class Duiker
    {
        public string adres { get; set; }
        public IEnumerable<string> nummerplaten { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public DateTime geboorteDatum { get; set; }
        public string gemeente { get; set; }
        public bool heeftBeperking { get; set; }
        public bool isBegeleider { get; set; }
        public string naam { get; set; }
        public int postcode { get; set; }
        public string gsm { get; set; }
    }
}
