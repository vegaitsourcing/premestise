using DataAccessLayer;
using DataAccessLayer.Contracts.Domain;
using DataAccessLayer.Implementation;
using System;
using System.Collections.Generic;

namespace ConsoleTestAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            RequestRepository r = new RequestRepository();/*
            IEnumerable<Request> re = r.GetAll();
            foreach (var item in re)
            {
                Console.WriteLine(item.ParentName, item.ChildName);

            }
           */
            KindergardenRepository k = new KindergardenRepository();
            //Console.WriteLine(k.GetById(1).LocationType);

            IEnumerable<Kindergarden> kin = k.GetAll();
            foreach (var item in kin)
            {
                Console.WriteLine(item.City);
            }
            Console.ReadLine();

        }
    }
}
