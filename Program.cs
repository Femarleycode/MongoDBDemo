using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using static MongoDBDemo.AddressModel;

namespace MongoDBDemo
{
	class Program
	{
		[Obsolete]
		static void Main(string[] args)
		{
			MongoCRUD db = new MongoCRUD("AddressBook");

			//PersonModel person = new PersonModel
			//{
			//	FirstName = "Joe",
			//	LastName = "Smith",
			//	PrimaryAddress = new AddressModel
			//	{
			//		StreetAddress = "101 Oak Street",
			//		City = "Scranton",
			//		State = "PA",
			//		ZipCode = "18512"
			//	}
			//};

			//db.InsertRecord("Users", person);

			//var recs = db.LoadRecord<PersonModel>("Users");

			//foreach (var rec in recs)
			//{
			//	Console.WriteLine($"{ rec.Id }: { rec.FirstName } { rec.LastName }");
			//	if (rec.PrimaryAddress != null)
			//	{
			//		Console.WriteLine(rec.PrimaryAddress.City);
			//	}
			//	Console.WriteLine();
			//}

			var oneRec = db.LoadRecordById<PersonModel>("Users", new Guid("bdc73c6f-5530-42c2-bd4e-e2b324762431"));
			oneRec.DateOfBirth = new DateTime(1982, 10, 31, 0, 0, 0, 0, DateTimeKind.Utc);
			db.UpsertRecord("Users", oneRec.Id, oneRec);

			Console.ReadLine();
		}
	}

	public class PersonModel {
		[BsonId] // id
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public AddressModel PrimaryAddress { get; set; }
		[BsonElement("dob")]
		public DateTime DateOfBirth { get; set; }
	}

	public class AddressModel
	{
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
	}

	public class MongoCRUD
	{
		private IMongoDatabase db;

		public MongoCRUD(string database)
		{
			var client = new MongoClient();
			db = client.GetDatabase(database);
		}

		public void InsertRecord<T>(string table, T record) {
			var collection = db.GetCollection<T>(table);
			collection.InsertOne(record);
		}

		public List<T> LoadRecords<T>(string table)
		{
			var collection = db.GetCollection<T>(table);

			return collection.Find(new BsonDocument()).ToList();
		}

		public T LoadRecordById<T>(string table, Guid id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = Builders<T>.Filter.Eq("Id", id);

			return collection.Find(filter).First();
		}

		[Obsolete]
		public void UpsertRecord<T>(string table, Guid id, T record)
		{
			var collection = db.GetCollection<T>(table);

			var result = collection.ReplaceOne(
				new BsonDocument("_id", id),
				record,
				new UpdateOptions { IsUpsert = true });

		}

		public void DeleteRecord<T>(string table, Guid id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = Builders<T>.Filter.Eq("Id", id);
			collection.DeleteOne(filter);
		}

	}
}
