using System;
using System.Data;
using System.Collections.Generic;
using Commons.FileReader;
using Commons.FileReader.Model;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Commons.DbAccesorContext;
using System.Linq;

namespace CommonsNUnitTest
{
    public class DbContextTest
    {
        [Test]
        public void GetDbContextTest()
        {
            PlayDbContext pdb = new PlayDbContext();
            pdb.SetConnectiton();

            var a = pdb.Students.FirstOrDefault();

        }

    }
}