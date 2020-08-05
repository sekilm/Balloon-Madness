using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour {

	private string connectionString;

	private List<Highscore> highScores = new List<Highscore>();

	public int saveScores;

	public InputField enterName;

	void Start () 
	{
		connectionString = "URI=file:" + Application.dataPath + "/HighscoreDB.sqlite3";
		CreateTable ();
	}

	void Update ()
	{

	}

	public void InsertScore(string name, int newScore)
	{
		GetScores ();
		int hsCount = highScores.Count;
		if (highScores.Count > 0) 
		{
			Highscore lowestScore = highScores [highScores.Count - 1];
			if (lowestScore != null && newScore > lowestScore.Score && saveScores > 0 && highScores.Count >= saveScores) 
			{
				DeleteScore (lowestScore.ID);
				hsCount--;
			}
		}
		if (hsCount < saveScores) 
		{
			using (IDbConnection dbConnection = new SqliteConnection (connectionString))
			{
				dbConnection.Open ();
				using (IDbCommand dbCmd = dbConnection.CreateCommand ())
				{
					string sqlQuery = string.Format ("INSERT INTO HighScores (Name,Score) VALUES (\"{0}\", {1})", name, newScore);
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
					dbConnection.Close ();
				}
			}
		}
	}

	private void DeleteScore(int id)
	{
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = string.Format("DELETE FROM HighScores WHERE PlayerID = {0}", id);
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}

	public void GetScores()
	{
		highScores.Clear ();
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = "SELECT * FROM HighScores";
				dbCmd.CommandText = sqlQuery;
				using (IDataReader reader = dbCmd.ExecuteReader ()) 
				{
					while (reader.Read ()) 
					{
						highScores.Add (new Highscore (reader.GetInt32 (0), reader.GetString (1), reader.GetInt32 (2)));
					}
					dbConnection.Close ();
					reader.Close ();
				}
			}
		}
		highScores.Sort ();
	}

	public void EnterName()
	{
		if (enterName.text != string.Empty) 
		{
			InsertScore (enterName.text, Score.score);
			enterName.text = string.Empty;
		}
	}

	private void CreateTable()
	{
		using (IDbConnection dbConnection = new SqliteConnection (connectionString))
		{
			dbConnection.Open ();
			using (IDbCommand dbCmd = dbConnection.CreateCommand ())
			{
				string sqlQuery = string.Format ("CREATE TABLE if not exists [HighScores] (PlayerID integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,Name text NOT NULL,Score integer NOT NULL)");
				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}
}
