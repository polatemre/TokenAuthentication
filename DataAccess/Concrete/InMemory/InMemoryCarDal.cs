//using DataAccess.Abstract;
//using Entities.Concrete;
//using Entities.DTOs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;

//namespace DataAccess.Concrete.InMemory
//{
//    public class InMemoryCarDal : ICarDal
//    {
//        readonly List<Assign> _cars;

//        public InMemoryCarDal()
//        {
//            _cars = new List<Assign>{
//                new Assign{Id=1,BrandId=1,ColorId=1,DailyPrice=1000,Description="asds1",ModelYear="2021" },
//                new Assign{Id=2,BrandId=2,ColorId=2,DailyPrice=1200,Description="asds2",ModelYear="2020" },
//                new Assign{Id=3,BrandId=3,ColorId=2,DailyPrice=3000,Description="asds3",ModelYear="2019" },
//                new Assign{Id=4,BrandId=3,ColorId=3,DailyPrice=1400,Description="asds4",ModelYear="2018" },
//                new Assign{Id=5,BrandId=4,ColorId=3,DailyPrice=2400,Description="asds5",ModelYear="2017" }
//            };
//        }

//        public void Add(Assign car)
//        {
//            _cars.Add(car);
//        }

//        public void Delete(Assign car)
//        {
//            Assign carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
//            _cars.Remove(carToDelete);
//        }

//        public Assign Get(Expression<Func<Assign, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public List<Assign> GetAll()
//        {
//            return _cars;
//        }

//        public List<Assign> GetAll(Expression<Func<Assign, bool>> filter = null)
//        {
//            throw new NotImplementedException();
//        }

//        public Assign GetById(int id)
//        {
//            return _cars.Where(c => c.Id == id).SingleOrDefault();
//        }

//        public List<CarDetailDto> GetCarDetails()
//        {
//            throw new NotImplementedException();
//        }

//        public List<CarDetailDto> GetCarDetails(Expression<Func<Assign, bool>> filter = null)
//        {
//            throw new NotImplementedException();
//        }

//        public void Update(Assign car)
//        {
//            Assign carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
//            carToUpdate.Id = car.Id;
//            carToUpdate.BrandId = car.BrandId;
//            carToUpdate.ColorId = car.ColorId;
//            carToUpdate.Description = car.Description;
//            carToUpdate.DailyPrice = car.DailyPrice;
//        }
//    }
//}
