using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Database.Domain.Entities;
using Database.Domain.Repositories;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepositoryContext repositoryContext;

        public UserController(IRepositoryContext repoContext)
        {
            repositoryContext = repoContext;
        }

        static UserController()
        {
            Mapper.CreateMap<UserModel, IUser>();
            Mapper.CreateMap<IUser, UserModel>();
        }

        //
        // GET: /User/
        public ActionResult Index()
        {
            var userList = repositoryContext.Users.GetUserList();

            var list = Mapper.Map<IEnumerable<IUser>, IEnumerable<UserModel>>(userList);
            return View(list);
        }

        //
        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            return GetDetailView(id);
        }

        //
        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositoryContext.Users.AddUser(user.Name, user.Surname, user.Address, user.TelephoneNumber);
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            catch
            {
                return View(user);
            }
        }

        //
        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            return GetDetailView(id);
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repositoryContext.Users.UpdateUser(user.Id, user.Name, user.Surname, user.Address, user.TelephoneNumber);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(user);
                }
            }

            return View(user);
        }

        //
        // GET: /User/Delete/5
        public ActionResult Delete(int id)
        {
            return GetDetailView(id);
        }

        //
        // POST: /User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repositoryContext.Users.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult GetDetailView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = repositoryContext.Users.Find(a => a.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<IUser, UserModel>(user));
        }
    }
}