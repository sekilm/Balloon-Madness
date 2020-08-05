using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

	private string connectionString;

	private List<Highscore> highScores = new List<Highscore>();

	public GameObject scorePrefab;

	public Transform scoreParent;

	public int topRanks;

	void Start () 
	{
		connectionString = "URI=file:" + Application.dataPath + "/HighscoreDB.sqlite3";
		ShowScores (); 
	}

	void Update ()
	{
		
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

	private void ShowScores()
	{
		GetScores ();
		for (int i = 0; i < topRanks; i++)
		{
			if (i <= highScores.Count - 1)
			{
				GameObject tmpObject = Instantiate (scorePrefab);
				Highscore tmpScore = highScores [i];
				tmpObject.GetComponent<HighscoreScript> ().SetScore (tmpScore.Name, tmpScore.Score.ToString (), "#" + (i + 1).ToString ());
				tmpObject.transform.SetParent (scoreParent);
			}
		}
	}
}
