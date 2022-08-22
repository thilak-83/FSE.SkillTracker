using Microsoft.Azure.Cosmos;
using System.Text;

namespace FSE.SkillTracker.Application.Specifications
{
    public abstract class SpecificationBase
    {
        protected List<string> _conditions = new List<string>();

        protected Dictionary<string, object> _parameters = new Dictionary<string, object>();

        public QueryDefinition GetQueryDefinition()
        {
            if (string.IsNullOrEmpty(SelectClause)) throw new ApplicationException("Attempting to build QueryDefinition but no select clause supplied");

            var sql = new StringBuilder();
            sql.AppendLine(SelectClause);
            sql.AppendLine(FromClause);

            if (_conditions.Any())
            {
                sql.Append("where \r\n(");
                sql.Append(string.Join(")\r\n and (", _conditions));
                sql.AppendLine(")");
            }
            if (!string.IsNullOrWhiteSpace(OrderByClause))
            {
                sql.AppendLine(OrderByClause);
            }

            var sqlQuery = sql.ToString();
            var query = new QueryDefinition(sqlQuery);

            foreach (var param in _parameters.Keys)
            {
                if (!sqlQuery.Contains(param)) throw new ApplicationException($"Parameter '{param}' not found in cosmosdb query '{sqlQuery}'.");
                query.WithParameter(param, _parameters[param]);
            }

            return query;
        }

        /// <summary>
        /// Fields for select excluding SELECT keyword
        /// </summary>
        protected string SelectClause { get; set; } = "select *";

        protected void AddCondition(string whereCondition)
        {
            _conditions.Add(whereCondition);
        }

        protected void AddCondition(string whereCondition, string parameterName, object parameterValue)
        {
            _conditions.Add(whereCondition);
            _parameters.Add(parameterName, parameterValue);
        }

        protected void AddConditionIfNotNullOrWhitespace(string whereCondition, string parameterName, object parameterValue)
        {
            if ((parameterValue is string &&! string.IsNullOrWhiteSpace((string)parameterValue)) || parameterValue != null)
            {
                AddCondition(whereCondition, parameterName, parameterValue);
            }
        }

        public string FromClause { get; set; } = "from c";

        public string OrderByClause { get; set; }
    }
}
