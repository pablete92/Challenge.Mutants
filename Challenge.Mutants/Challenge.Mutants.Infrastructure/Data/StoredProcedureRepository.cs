using Challenge.Mutants.Infrastructure.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Snickler.EFCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Mutants.Infrastructure.Data
{
    public class StoredProcedureRepository<TDbContext> : IStoredProcedureRepository<TDbContext>
           where TDbContext : DbContext
    {
        private readonly DbContext context;

        public StoredProcedureRepository(TDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<TDataModel>> GetStoreProcedureAsync<TDataModel>(string name, IList<ParameterModel> nameValueParams) where TDataModel : DataModelBase
        {
            var dbCommand = context.LoadStoredProc(name, commandTimeout: 300);

            foreach (var param in nameValueParams)
            {
                dbCommand.WithSqlParam(param.FieldName, param.Value);
            }

            IList<TDataModel> response = null;
            await dbCommand.ExecuteStoredProcAsync((handler) =>
            {
                response = (IList<TDataModel>)handler.ReadToList<DataModelBase>();
            });

            return response;
        }

        public async Task ExecuteStoredNonQueryAsync(string name, IList<ParameterModel> nameValueParams)
        {
            var dbCommand = context.LoadStoredProc(name, commandTimeout: 300);

            foreach (ParameterModel param in nameValueParams)
            {
                dbCommand.WithSqlParam(param.FieldName, param.Value);
            }

            await dbCommand.ExecuteStoredNonQueryAsync();
        }
    }

    public class ParameterModel
    {
        public ParameterModel(string fieldName, object value)
        {
            this.FieldName = fieldName;
            this.Value = value;
        }

        public string FieldName { get; }
        public object Value { get; }
    }
}
