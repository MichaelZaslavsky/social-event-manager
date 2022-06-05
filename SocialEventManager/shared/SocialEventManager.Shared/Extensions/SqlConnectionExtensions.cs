using System.Data.SqlClient;

namespace SocialEventManager.Shared.Extensions;

public static class SqlConnectionExtensions
{
    public static async Task<int> ExecuteNonQueryAsync(this SqlConnection connection, string command)
    {
        using SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = command;

        return await cmd.ExecuteNonQueryAsync();
    }

    public static async Task<T> ExecuteScalarAsync<T>(this SqlConnection connection, string command)
    {
        using SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = command;

        return (T)await cmd.ExecuteScalarAsync();
    }
}
