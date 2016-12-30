using Shaolinq;
using Shaolinq.MySql;
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
            this.model = DataAccessModel.BuildDataAccessModel<T>(configuration);
        }
    }
}
