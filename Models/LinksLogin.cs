using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace WebAppProj3.Models
{
    public class LinksLogin
    {
 private MySqlConnection dbConnection;
		private string connectionString;
		private MySqlCommand dbCommand;
		private MySqlDataReader dbReader;
		private HttpContext context;

		private string _username;
		private string _password;
		private bool _access;

		public LinksLogin(string myConnectionString, HttpContext myHttpContext) {
			// initialization
			_username = "";
			_password = "";
			_access = false;

			connectionString = myConnectionString;
			context = myHttpContext;
			// clear out the session (all session variables are deleted from server)
			context.Session.Clear();
		}

		// ------------------------------------------------------- gets/sets
		public string username {
			set { _username = (value == null ? "" : value); }
		}

		public string password {
			set { _password = (value == null ? "" : value); }
		}

		public bool access {
			get { return _access; }
		}

		// ------------------------------------------------------- public methods
		public void Signout(){
			_username = "";
			_password = "";
			_access = false;
			// clear out the session (all session variables are deleted from server)
			context.Session.Clear();
		}

		public bool unlock() {
			// assume no access is granted
			_access = false;

			// trim username / password to 10 characters
			_username = truncate(_username, 10);
			_password = truncate(_password, 10);

			// check if username and password exists in tblLogin of database
			try {
				dbConnection = new MySqlConnection(connectionString);
				dbConnection.Open();
				dbCommand = new MySqlCommand("SELECT password,salt FROM tblLinksLogin WHERE username=?username", dbConnection);
				dbCommand.Parameters.AddWithValue("?username", _username);
				dbReader = dbCommand.ExecuteReader();

				// did the query return any records? If not then username is wrong
				if (!dbReader.HasRows) {
					dbConnection.Close();
					return _access;
				}

				// move to the first (and only) record
				dbReader.Read();

				// hash and salt the password provided by the user
				string hashedPassword = getHashed(_password, dbReader["salt"].ToString());

				// compare them!
				if (hashedPassword == dbReader["password"].ToString()) {
					// access granted!
					_access = true;
					// store data in session
					context.Session.SetString("auth", "true");
					context.Session.SetString("user", _username);
				}
			} finally {
				dbConnection.Close();
			}

			return _access;
		}

		// ------------------------------------------------------- private methods
		private string truncate(string value, int maxLength) {
			return (value.Length <= maxLength ? value : value.Substring(0, maxLength));
		}

		private string getSalt() {
			// generate a 128-bit salt using a secure PRNG (pseudo-random number generator)
			byte[] salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create()) {
				rng.GetBytes(salt);
			}
			//Console.WriteLine(">>> Salt: " + Convert.ToBase64String(salt));

			return Convert.ToBase64String(salt);
		}

		private string getHashed(string myPassword, string mySalt) {
			// convert string to Byte[] for hashing
			byte[] salt = Convert.FromBase64String(mySalt);
	
			// hashing done using PBKDF2 algorithm
			// derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: myPassword,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));
			//Console.WriteLine(">>> Hashed: " + hashed);

			return hashed;
		}

	}
}