using Shaolinq;
using Shaolinq.Sqlite;
using System.IO;

namespace Framework.Database
{
    public class BaseModel<T> where T : DataAccessModel
    {
        protected T model;

        public BaseModel()
        {
            //if (File.Exists(@"database.sqlite"))
                //File.Delete(@"database.sqlite");

            var configuration = SqliteConfiguration.Create("database.sqlite", null);
            this.model = DataAccessModel.BuildDataAccessModel<T>(configuration);

            // Recria a base inteira
            //this.model.Create(DatabaseCreationOptions.DeleteExistingDatabase);
        }
    }
}
