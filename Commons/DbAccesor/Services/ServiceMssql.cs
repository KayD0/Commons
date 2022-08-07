using Commons.DbAccessor;
using Commons.DbAccessor.Parameters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Commons.DbAccesor.Services
{
    public class ServiceMssql
    {
        private AccessorMssql mssql = new AccessorMssql();

        public JObject GetStudents()
        {
            DataTable dt = new DataTable();
            List<DbParamerter> prams = new List<DbParamerter>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine("   * ");
            sql.AppendLine(" FROM ");
            sql.AppendLine("   PlayGround.dbo.Student ");
            mssql.ExecuteQuery(ref dt, sql.ToString(), prams);

            string tblJson = JsonConvert.SerializeObject(dt);
            return JObject.Parse(tblJson);
        }

        public JObject InsertStudents()
        {
            DataTable dt = new DataTable();
            List<DbParamerter> prams = new List<DbParamerter>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine("   * ");
            sql.AppendLine(" FROM ");
            sql.AppendLine("   PlayGround.dbo.Student ");
            mssql.ExecuteQuery(ref dt, sql.ToString(), prams);

            string tblJson = JsonConvert.SerializeObject(dt);
            return JObject.Parse(tblJson);
        }

        public JObject UpdateStudents()
        {
            DataTable dt = new DataTable();
            List<DbParamerter> prams = new List<DbParamerter>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine("   * ");
            sql.AppendLine(" FROM ");
            sql.AppendLine("   PlayGround.dbo.Student ");
            mssql.ExecuteQuery(ref dt, sql.ToString(), prams);

            string tblJson = JsonConvert.SerializeObject(dt);
            return JObject.Parse(tblJson);
        }

        public void InsertImage(int iid , byte[] imageByteArray, string name)
        {
            List<DbParamerter> prams = new List<DbParamerter>();
            
            DbParamerter pIid = new DbParamerter();
            pIid.Name = "@Iid";
            pIid.DbTypeMssql = SqlDbType.BigInt;
            pIid.Val = iid; //ここにイメージデータが入る予定
            pIid.Direction = ParameterDirection.Input;

            DbParamerter pImage = new DbParamerter();
            pImage.Name = "@Image";
            pImage.DbTypeMssql = SqlDbType.Image;
            pImage.Val = imageByteArray; //ここにイメージデータが入る予定
            pImage.Direction = ParameterDirection.Input;

            DbParamerter pName = new DbParamerter();
            pName.Name = "@Name";
            pName.DbTypeMssql = SqlDbType.NVarChar;
            pName.Val = name; //ここにイメージデータが入る予定
            pName.Direction = ParameterDirection.Input;

            prams.Add(pIid);
            prams.Add(pImage);
            prams.Add(pName);

            //INSERT INTO syain(id, name, romaji) VALUES(1, '鈴木', 'suzuki');
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" INSERT INTO PlayGround.dbo.DATAIMAGE ");
            sql.AppendLine(" (    ");
            sql.AppendLine("   Iid  ");
            sql.AppendLine("   ,Image  ");
            sql.AppendLine("   ,Name  ");
            sql.AppendLine(" )  ");
            sql.AppendLine(" VALUES  ");
            sql.AppendLine(" (    ");
            sql.AppendLine("   @Iid  ");
            sql.AppendLine("   ,@Image  ");
            sql.AppendLine("   ,@Name  ");
            sql.AppendLine(" )  ");
            mssql.ExecuteQuery(sql.ToString(), prams);
        }

        public byte[] ReadImage(int iid)
        {
            List<DbParamerter> dbParams = new List<DbParamerter>();

            DbParamerter pIid = new DbParamerter();
            pIid.Name = "@Iid";
            pIid.DbTypeMssql = SqlDbType.BigInt;
            pIid.Val = iid; //ここにイメージデータが入る予定
            pIid.Direction = ParameterDirection.Input;
            dbParams.Add(pIid);

            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT ");
            sql.AppendLine("   Image ");
            sql.AppendLine(" FROM ");
            sql.AppendLine("   PlayGround.dbo.DATAIMAGE ");
            mssql.ExecuteQuery(ref dt, sql.ToString(), dbParams);

            var image = (Byte[])dt.Rows[0]["Image"];
            return image;
        }
    }
}


