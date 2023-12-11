using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaffeeShop
{
    public abstract class Person
    {
        private string id;
        private string name;
        private string phoneNumber;
        private string account;
        private string passWord;

        public string Name { get { return name; } set {  name = value.Trim(); } }
        public string Account { get {  return account; } set { account = value.Trim(); } }
        public string PhoneNumber { get {  return phoneNumber; } set {  phoneNumber = value.Trim(); } }
        public string Id { get { return id;  } set { id = value.Trim(); }  }

        public string PassWord { get => passWord; set => passWord = value; }

        public virtual string PrintDetail()
        {
            return "\nID: " + Id + "\nName: " + Name + "\nPhone number: "  + PhoneNumber ;  
        }

        public Person() {}
        public Person (string id, string name, string phoneNumber, string account ,string passWord)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Id = id;
            PassWord = passWord;
            Account = account;
        }

        protected Person(string name, string account, string phoneNumber, string passWord)
        {
            Name = name;
            Account = account;
            PhoneNumber = phoneNumber;
            PassWord = passWord;
        }

        public abstract void ChangePassword(string NewPassword);
    }
}