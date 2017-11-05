using System;
using Mono.Data.Sqlite;

namespace GradeTracker.Data
{
	public static class DatabaseConnection
	{
		public static SqliteConnection GetConnection()
		{
			const string connectionString = "URI=file:../../GradeTracker.db";

			return new SqliteConnection(connectionString);
		}
	}
}

