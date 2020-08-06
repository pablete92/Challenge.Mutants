using Challenge.Mutants.Infrastructure.Data.Contracts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Challenge.Mutants.Infrastructure.Data
{
    public class QueryManager : IQueryManager
    {
        private static IDictionary<SqlCommandTypeInvocation, string> invocationTypePrefix = new Dictionary<SqlCommandTypeInvocation, string>
        {
            { SqlCommandTypeInvocation.StoredProcedure, "exec " },
            { SqlCommandTypeInvocation.Function, "select dbo." }
        };

        private IList<SqlParameter> internalParameters;

        public string Query { get; private set; }

        public SqlParameter[] Parameters
                => internalParameters == null ? Enumerable.Empty<SqlParameter>().ToArray() : this.internalParameters.ToArray();

        public void GenerateQuery(SqlCommandTypeInvocation typeInvocation, string name, IDictionary<string, object> parameters)
        {
            var newQuery = new StringBuilder();

            newQuery.Append(string.Concat(this.GetPrefix(typeInvocation), name, " "));

            if (parameters != null && parameters.Count > 0)
            {
                if (this.IsBetweenBrackets(typeInvocation))
                {
                    newQuery.Append("(");
                }

                this.internalParameters = new List<SqlParameter>();

                foreach (var parameter in parameters)
                {
                    this.internalParameters.Add(new SqlParameter(parameter.Key, parameter.Value));

                    newQuery.Append(string.Concat("@", parameter.Key, ","));
                }

                this.Query = this.Query.Substring(0, this.Query.Length - 1);

                if (this.IsBetweenBrackets(typeInvocation))
                {
                    newQuery.Append(")");
                }
            }

            this.Query = newQuery.ToString();
        }

        private string GetPrefix(SqlCommandTypeInvocation invocacionType)
        {
            if (!invocationTypePrefix.ContainsKey(invocacionType))
            {
                throw new InvalidEnumArgumentException("tipoInvocacion", (int)invocacionType, typeof(SqlCommandTypeInvocation));
            }

            return invocationTypePrefix[invocacionType];
        }

        private bool IsBetweenBrackets(SqlCommandTypeInvocation invocacionType)
        {
            return invocacionType == SqlCommandTypeInvocation.Function;
        }
    }
}
