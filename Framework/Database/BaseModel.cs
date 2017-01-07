using System;
using Shaolinq;
using Shaolinq.Sqlite;

namespace Framework.Database
{
    public class BaseModel<T> where T : DataAccessModel
    {
        protected T model;

        public BaseModel()
        {
            var configuration = SqliteConfiguration.Create("database.sqlite", null);
            //var configuration = MySqlConfiguration.Create("wow", "127.0.0.1", "homestead", "secret");

            try
            {
                model = DataAccessModel.BuildDataAccessModel<T>(configuration);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
