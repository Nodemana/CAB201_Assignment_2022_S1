using System;

namespace AirlineProgram
{
    public class Customer
    {
        private string name, email, address;
        private int mobile_number;

        public string Name { get { return name; } set { name = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Address { get { return address; } set { address = value; } }
        public int Mobile_Number { get { return mobile_number; } set { mobile_number = value; } }

        public Customer(string MyName, string MyEmail, string MyAddress, int MyMobile)
        {
            Name = MyName;
            Email = MyEmail;
            Address = MyAddress;
            Mobile_Number = MyMobile;
        }
    }
}
