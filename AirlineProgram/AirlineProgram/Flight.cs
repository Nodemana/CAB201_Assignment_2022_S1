using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
namespace AirlineProgram
{
	public abstract class Flight
	{
		protected string departure_place, arrival_place, departure_time, type, time;
		protected int flightnum, rem_capacity, capacity;
		protected double distance, cost, cost_per_distance, cost_per_hour, speed;

		public List<Customer> PassengerList { get; protected set; }
		public static int NextFlightNum = 1; // Stores next available flight number
		public string Type { get { return type; } set { type = value; } }
		public double Cost_Per_Distance { get { return cost_per_distance; } set { cost_per_distance = value; } }
		public double Cost_Per_Hour { get { return cost_per_hour; } set { cost_per_hour = value; } }
		public double Speed { get { return speed; } set { speed = value; } }
		public string Time { get { return time; } set { time = value; } }
		public double Cost { get { return cost; } set { cost = value; } }
		public int Rem_capacity { get { return rem_capacity; } set { rem_capacity = value; } }
		public int Capacity { get { return capacity; } set { capacity = value; } }
		public int FlightNum { get { return flightnum; } set { flightnum = value; } }
		public string Departure_place { get { return departure_place; } set { departure_place = value; } }
		public string Arrival_place { get { return arrival_place; } set { arrival_place = value; } }
		public string Departure_time { get { return departure_time; } set { departure_time = value; } }
		public double Distance { get { return distance; } set { distance = value; } }



		public Flight(string MyDeparture_place, string MyArrival_place, string MyDeparture_time, double MyDistance)
		{
			//FlightNum = Interlocked.Increment(ref nextFlightNum); //Auto Incrementing Flight Numbers
			//FlightNum = nextFlightNum++;
			Departure_place = MyDeparture_place;
			Arrival_place = MyArrival_place;
			Departure_time = MyDeparture_time;
			Distance = MyDistance;
			PassengerList = new List<Customer>(); //Find out how to protect this
		}

		/// <summary>
		/// Calculates flight time, and formats it.
		/// </summary>
		/// <returns>formatted time.</returns>
		public virtual string Calculate_Flight_Time()
		{
			double hours = Distance / Speed;
			double minutes = hours % 10;
			minutes = Math.Round((minutes % 1) * 60);
			hours = Math.Floor(hours);
			Time = hours + ":" + minutes;
			return Time;
		}
		/// <summary>
		/// Displays flight information of an instance.
		/// </summary>
		/// <returns>void.</returns>
		public void Display()
		{
			Console.WriteLine("|| {0, -13} || {1, -14} || {2,-9} || {3,-11} || {4,-14} || {5,-14} || {6,-11} || {7,-8:N2} || {8,-18} ||", FlightNum, Type, Departure_place, Arrival_place, Departure_time, Distance, Time, "$" + Cost, Rem_capacity);
		}
		//Abstracted method.
		public abstract void Detail_Display();

		/// <summary>
		/// Displays passenger list of a flight instance.
		/// </summary>
		/// <returns>void.</returns>
		public void Passenger_Display()
		{
			Console.WriteLine("");
			Console.WriteLine("Passenger List:");
			Console.WriteLine("|| {0, -13} || {1, -14} || {2,-13} || {3,-14} ||", "Name", "Email", "Mobile Number", "Address");
			foreach (Customer passenger in PassengerList)
			{
				Console.WriteLine("|| {0, -13} || {1, -14} || {2,-13} || {3,-14} ||", passenger.Name, passenger.Email, passenger.Mobile_Number, passenger.Address);
			}
			Console.WriteLine("");
		}
	}
}
