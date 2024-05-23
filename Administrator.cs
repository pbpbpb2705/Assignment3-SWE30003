using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Administrator
    {
        private Database _db;
        private Analytics _analytics;
        public Administrator() 
        { 
        }
        public void AddDatabase(Database db)
        {
            _db.AddDatabase(db);
        }
        public void DeleteDatabase(Database db)
        {
            _db.DeleteDatabase(db);
        }
        public void UpdateDatabase(Database db)
        {
            _db.UpdateDatabase(db);
        }
        public void HandleAnalyic(Analytics analytics)
        {
            _analytics.HandleAnalytics();
        }


    }
}
