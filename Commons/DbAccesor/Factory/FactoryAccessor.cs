//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Common.DbAccessor;

//namespace Commons.DbAccessor.Factory
//{
//    public class FactoryAccessor
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        public FactoryAccessor() 
//        {
            
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="DbType"></param>
//        /// <returns></returns>
//        public IAccessor GetInstansByName(string DbType) 
//        {
//            switch (DbType) 
//            {
//                case "ORACLE" :
//                    return new AccessorOracle();
//                case "MYSQL" :
//                    return new AccessorMySql();
//                case "MSSQL":
//                    return new AccessorMssql();
//                case "NPGSSL":
//                    return new AccessorNpgsql();
//         }
//            return null;
//        }
//    }
//}
