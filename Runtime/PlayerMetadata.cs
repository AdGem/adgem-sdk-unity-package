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
		public string id { get; }

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
		/// Optional amount of money spent on In-App-Purchases. 0-99999.99
		/// </summary>
		public float iapTotalUsd = NOT_SET;

		/// <summary>
		/// Optional player creation time.
		/// </summary>
		public DateTime createdAt = DateTime.MinValue;

		/// <summary>
		/// Optional custom fields (up to 5). Other items will be ignored.
		/// Each field's length must not exceed 99 characters.
		/// </summary>
		public List<string> customFields { get; } = new List<string>();

		private const int NOT_SET = -1;
	}
}