using Labo13.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
    public class BDTestFixture
    {
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Labo13;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False";

        public Labo13Context CreateContext()
        {
            return new Labo13Context(new DbContextOptionsBuilder<Labo13Context>().UseSqlServer(ConnectionString).Options);
        }
    }
}
