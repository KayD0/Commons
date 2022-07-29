using Commons.DbAccesorContext.Model;
using Commons.FileReader;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Text;

namespace Commons.DbAccesorContext
{
    public class PlayDbContext : DbContext
    {
        public PlayDbContext() 
        {
            
        }

        public void SetConnectiton() 
        {
            SerializerJson sj = new SerializerJson("appsettings.json");
            JObject jo = sj.DeserializeToObjext();
            this.Database.Connection.ConnectionString = jo["Data"]["PlayConnectionString"].ToString();
        }

        public DbSet<Student> Students { get; set; }
    }
}
