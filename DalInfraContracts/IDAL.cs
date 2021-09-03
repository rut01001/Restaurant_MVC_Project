using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DalInfraContracts
{
    public interface IDAL
    {
        public void ExecuteNonQuery(string spName, params IParameter[] parameters);
        public DataSet ExecuteQuery(string spName, params IParameter[] parameters);

        public IParameter CreateParameter(string paramName, object value);

    }
}
