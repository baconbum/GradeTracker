using System;
using Mono.Data.Sqlite;

namespace GradeTracker.Data
{
	/// <summary>
	/// A helper class to retrieve a connection to the Grade Tracker database.
	/// </summary>
	public static class DatabaseConnection
	{
		/// <summary>
		/// Gets the database connection.
		/// </summary>
		/// <returns>The database connection.</returns>
		public static SqliteConnection GetConnection()
		{
			const string connectionString = "URI=file:../../GradeTracker.db";

			return new SqliteConnection(connectionString);
		}
	}
}
