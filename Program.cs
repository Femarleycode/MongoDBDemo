using System;
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

			// READ
			var recs = db.LoadRecords<NameModel>("Users");

			foreach (var rec in recs)
			{
				Console.WriteLine($"{ rec.FirstName } { rec.LastName }");
				Console.WriteLine();
			}

			// First READ
			//foreach (var rec in recs)
			//{
			//	Console.WriteLine($"{ rec.Id }: { rec.FirstName } { rec.LastName }");

			//	if (rec.PrimaryAddress != null)
			//	{
			//		Console.WriteLine(rec.PrimaryAddress.City);
			//	}
			//	Console.WriteLine();
			//}

			//-------

			// Update or Delete
			//var oneRec = db.LoadRecordById<PersonModel>("Users", new Guid("bdc73c6f-5530-42c2-bd4e-e2b324762431"));
			// UPDATE
			//oneRec.DateOfBirth = new DateTime(1982, 10, 31, 0, 0, 0, 0, DateTimeKind.Utc);
			//db.UpsertRecord("Users", oneRec.Id, oneRec);
			// DELETE
			//db.DeleteRecord<PersonModel>("Users", oneRec.Id);

			Console.ReadLine();
		}
	}
}
