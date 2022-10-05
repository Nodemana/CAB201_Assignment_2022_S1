using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProgram
{
    public class HeliFlight : Flight
    {
        private double no_passenger_time { get; } = 0.16;
        public HeliFlight(string MyDeparture_place, string MyArrival_place, string MyDeparture_time, int MyDistance) : base(MyDeparture_place, MyArrival_place, MyDeparture_time, MyDistance)
        {
            Type = "Helicopter";
            Cost_Per_Distance = 5; //cost per distance (km)
            Cost_Per_Hour = Cost_Per_Distance * 120;
            Speed = 120;
            Capacity = 2;
            Rem_capacity = Capacity;
            Cost = Cost_Per_Distance * Distance;
            FlightNum = NextFlightNum++;
            Calculate_Flight_Time();
        }
        /// <summary>
		/// Calculates flight time, and formats it.
		/// </summary>
		/// <returns>formatted time.</returns>
        public override string Calculate_Flight_Time()
        {
            double extra_time = 0.083 * (Capacity - Rem_capacity);
            double hours = (Distance / Speed) + no_passenger_time + extra_time;
            double minutes = hours % 10;
            minutes = Math.Round((minutes % 1) * 60);
            hours = Math.Floor(hours);
            Time = hours + ":" + minutes;
            return Time;
        }
        /// <summary>
		/// Displays specific general aircraft information of aircraft type.
		/// </summary>
		/// <returns>void.</returns>
        public override void Detail_Display()
        {
            Console.WriteLine("");
            Console.WriteLine("Aircraft Details:");
            Console.WriteLine("|| {0, -14} || {1, -14} || {2,-9} || {3,-11} ||", "Aircraft", "Cost Per hour", "Speed", "Max Capacity");
            Console.WriteLine("|| {0, -14} || {1, -14} || {2,-9} || {3,-11} ||", Type, "$" + Cost_Per_Hour, Speed + "km/hr", Capacity);
            Console.WriteLine("");
            Console.WriteLine("ATTENTION:");
            Console.WriteLine("The flying time of this aircraft requires an additional 10 minutes for no passengers, 15 for one passenger and 20 for two passengers");
        }
    }
}
