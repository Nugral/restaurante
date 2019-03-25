using System;
using MySql.Data.MySqlClient;

namespace API.DAO
{
    interface IDAO<in Object> : IDisposable
    {
        int Insert(Object model, MySqlTransaction transaction);

        int Update(Object model, MySqlTransaction transaction);

        int Remove(Object model, MySqlTransaction transaction);
    }
}
