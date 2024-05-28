using System;
using System.Collections.Generic;

namespace AdGem.Runtime
{
	public class PlayerMetadata
	{
		public enum Gender
		{
			UNKNOWN,
			MALE,
			FEMALE
		}

		/// <summary>
		/// Create a new Player Metadata object.
		/// </summary>
		/// <param name="id">Unique player ID.</param>
		public PlayerMetadata(string id)
		{
			this.id = id;
		}

		/// <summary>
		/// Required: unique player ID.
		/// </summary>
		public string id;

		/// <summary>
		/// Optional gender.
		/// </summary>
		public Gender gender = Gender.UNKNOWN;

		/// <summary>
		/// Optional age. 0-99
		/// </summary>
		public int age = NOT_SET;

		/// <summary>
		/// Optional level. 0-1000000000
		/// </summary>
		public int level = NOT_SET;

		/// <summary>
		/// Optional placement/rank. 0-1000000000
		/// </summary>
		public long placement = NOT_SET;

		/// <summary>
		/// Optional flag indicating if this player is a payer.
		/// </summary>
		public bool isPayer = false;

		/// <summary>
		/// Optional player creation time.
		/// </summary>
		public DateTime createdAt = DateTime.MinValue;

		/// <summary>
		/// Optional custom fields (up to 5).
		/// </summary>
		public List<string> customFields = new List<string>();

		private const int NOT_SET = -1;
	}
}